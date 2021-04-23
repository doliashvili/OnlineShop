namespace OnlineShop.Domain.CommonModels.Identity
{
    public class AuthResult
    {
        public AuthResult()
        {
        }

        public AuthResult(SignInResultCustom result, TokenResponse token)
        {
            Token = token;
            Result = result;
        }

        public SignInResultCustom Result { get; set; }
        public TokenResponse Token { get; set; }
    }
}