using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Exceptions.ThrowHelper;
using Helpers.ReadResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OnlineShop.Application.Exceptions;
using OnlineShop.Application.Services.Abstract;
using OnlineShop.Application.Settings;
using OnlineShop.Domain.AbstractRepository;
using OnlineShop.Domain.CommonModels.Identity;

namespace OnlineShop.Application.Services.Implements
{
    public sealed class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSettings _jwtSettings;
        private readonly AppSettings _appSettings;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<IdentityService> _logger;

        public IdentityService(UserManager<ApplicationUser> userManager,
            IOptions<JWTSettings> jwtSettings,
            SignInManager<ApplicationUser> signInManager,
            AppSettings appSettings, ILogger<IdentityService> logger,
            IUserRepository userRepository)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
            _appSettings = appSettings;
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<AuthResult> GetTokenAsync(TokenRequest request, string ipAddress)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            var apiResponse = new AuthResult();
            Throw.Exception.IfNull(user, () => new ApiException(401, "Invalid credentials."));
            var result =
                await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false,
                    lockoutOnFailure: false);
            Throw.Exception.IfFalse(result.Succeeded, () => new ApiException(401, "Invalid credentials."));
            Throw.Exception.IfFalse(user.EmailConfirmed,
                () => new ApiException(400, $"Email is not confirmed for '{request.Email}'."));
            Throw.Exception.IfFalse(user.IsActive,
                () => new ApiException(400,
                    $"Account for '{request.Email}' is not active. Please contact the Administrator."));

            apiResponse.Result = new SignInResultCustom()
            {
                IsLockedOut = result.IsLockedOut,
                IsNotAllowed = result.IsNotAllowed,
                RequiresTwoFactor = result.RequiresTwoFactor,
                Succeeded = result.Succeeded
            };

            var jwtSecurityToken = await GenerateJwTokenAsync(user, ipAddress);
            var response = new TokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                IssuedOn = jwtSecurityToken.ValidFrom.ToLocalTime(),
                ExpiresOn = jwtSecurityToken.ValidTo.ToLocalTime(),
            };
            var refreshToken = GenerateRefreshToken(ipAddress);
            response.RefreshToken = refreshToken.Token;

            user.RefreshToken = refreshToken;
            await _userRepository.UpdateRefreshTokenAsync(user.Email, user.RefreshToken).ConfigureAwait(false);

            apiResponse.Token = response;
            return apiResponse;
        }

        public async Task<Result<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress)
        {
            var user = await _userRepository.FindUserByEmailAsync(request.Email).ConfigureAwait(false);

            var isValidRequest = user != null && user.IsActive && user.EmailConfirmed;
            Throw.Exception.IfFalse(isValidRequest, "User not found or restricted action");

            // ReSharper disable once PossibleNullReferenceException
            var token = user.RefreshToken;
            if (token != null
                && token.Token == request.RefreshToken
                && token.ExpiresAt >= DateTime.UtcNow
                && token.CreatedByIp == ipAddress)
            {
                var jwtSecurityToken = await GenerateJwTokenAsync(user, ipAddress);
                var response = new TokenResponse
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    IssuedOn = jwtSecurityToken.ValidFrom.ToLocalTime(),
                    ExpiresOn = jwtSecurityToken.ValidTo.ToLocalTime(),
                };

                var refreshToken = GenerateRefreshToken(ipAddress);
                response.RefreshToken = refreshToken.Token;

                user.RefreshToken = refreshToken;
                await _userRepository.UpdateRefreshTokenAsync(user.Email, user.RefreshToken).ConfigureAwait(false);

                return Result<TokenResponse>.Success(response, "Authenticated");
            }

            return Result<TokenResponse>.Fail("Refresh token validation failed");
        }

        public async Task<Result<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                throw new ApiException(400, $"Email '{request.Email}' is already taken.");
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email,
                DateOfBirth = request.DateOfBirth,
                Address = request.Address,
                CreatedAt = DateTime.UtcNow,
                IdentificationNumber = request.IdentificationNumber,
                City = request.City,
                Country = request.Country,
                PersonalNumber = request.PersonalNumber,
                PhoneNumber = request.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                throw new ApiException($"{result.Errors}");

            await _userManager.AddToRoleAsync(user, Roles.User.ToString());
            //var verificationUri = await GenerateVerificationEmailAsync(user, origin);

            //var mailRequest = new MailRequest()
            //{
            //    To = user.Email,
            //    Body = _appSettings.ConfirmEmailHtml.Replace("{verificationUri}", verificationUri),
            //    Subject = _appSettings.ConfirmEmailSubject
            //};

            //await _mailService.SendAsync(mailRequest); //TODO email service
            return Result<string>.Success(user.Id,
                "User Registered. Confirmation Mail has been delivered to your Mailbox.");
        }

        public async Task<IResult> ChangePasswordAsync(ChangePasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            Throw.Exception.IfNotNull(user, "User not found");

            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (result.Succeeded)
            {
                return Result.Success("Password changed successfully");
            }

            var errors = result.Errors.ToDictionary(x => x.Code, e => e.Description);
            return Result.Fail(JsonConvert.SerializeObject(errors));
        }


        public async Task<Result<string>> ConfirmEmailAsync(string userId, string code)
        {
            throw new NotImplementedException();
            //var user = await _identityContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            //code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            //var result = await _userManager.ConfirmEmailAsync(user, code);
            //if (!result.Succeeded)
            //    throw new ApiException($"An error occurred while confirming {user.Email}.");

            //try
            //{
            //    user.ActivatedAt = DateTime.UtcNow;
            //    await _identityContext.SaveChangesAsync();
            //}
            //catch (Exception e)
            //{
            //    _logger.LogError(e.ToString());
            //}

            //return Result<string>.Success(user.Id,
            //    message:
            //    $"Account Confirmed for {user.Email}. You can now use the /api/identity/token endpoint to generate JWT.");
        }

        public async Task ForgotPasswordAsync(ForgotPasswordRequest model, string origin)
        {
            throw new NotImplementedException();
            var account = await _userManager.FindByEmailAsync(model.Email);

            // always return ok response to prevent email enumeration
            if (account == null) return;

            var code = await _userManager.GeneratePasswordResetTokenAsync(account);
            //var route = "api/v1/identity/reset-password/"; //TODO from configuration?
            //var endpointUri = new Uri(string.Concat($"{origin}/", route));
            //var emailRequest = new MailRequest()
            //{
            //    Body = _appSettings.ResetPasswordHtml.Replace("{token}", code),
            //    To = model.Email,
            //    Subject = _appSettings.ResetTokenSubject,
            //};
            //await _mailService.SendAsync(emailRequest);
        }

        public async Task<Result<string>> ResetPasswordAsync(ResetPasswordRequest model)
        {
            var account = await _userManager.FindByEmailAsync(model.Email);
            if (account == null) throw new ApiException($"No Accounts Registered with {model.Email}.");
            var result = await _userManager.ResetPasswordAsync(account, model.Token, model.Password);

            if (!result.Succeeded)
                throw new ApiException($"Error occurred while reset the password.");

            return Result<string>.Success(model.Email, "Password renewed.");
        }

        #region Private Methods
        private async Task<string> GenerateVerificationEmailAsync(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = _appSettings.ConfirmEmailPath;
            var endpointUri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(endpointUri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
            return verificationUri;
        }
        private async Task<JwtSecurityToken> GenerateJwTokenAsync(ApplicationUser user, string ipAddress)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(t => new Claim(ClaimTypes.Role, t)).ToList();
            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    //new Claim("thumbnail", user.Thumbnail),
                    new Claim("first_name", user.FirstName),
                    new Claim("last_name", user.LastName),
                }
                .Union(userClaims)
                .Union(roleClaims);
            return JwtGeneration(claims);
        }

        private JwtSecurityToken JwtGeneration(IEnumerable<Claim> claims)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        private static string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private static RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                ExpiresAt = DateTime.UtcNow.AddDays(7), //TODO from config?
                CreatedAt = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }
        #endregion

    }
}