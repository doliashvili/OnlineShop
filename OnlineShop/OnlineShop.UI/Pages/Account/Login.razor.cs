using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineShop.UI.Models.User;

namespace OnlineShop.UI.Pages.Account
{
    public partial class Login
    {
        private readonly LoginRequest model = new LoginRequest();

        private bool ShowErrors { get; set; }

        private IEnumerable<string> Errors { get; set; }

        private async Task SubmitAsync()
        {
            try
            {
                var result = await _userService.LoginAsync(model);
                if (result.Result.Succeeded)
                {
                    ShowErrors = false;
                    var name = await _localStorageService.GetItemAsync<string>("firstName");
                    _toastService.ShowSuccess($"მოგესალმებით ძვირფასო {name}");
                    _navigationManager.NavigateTo("/");
                }
            }
            catch (Exception e)
            {
                Errors = new List<string>() { "invalid error" }; //todo
                this.ShowErrors = true;
            }
        }
    }
}