using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineShop.UI.Models.User;

namespace OnlineShop.UI.Pages.Account
{
    public partial class Register
    {
        private readonly RegisterRequest model = new();

        public bool ShowErrors { get; set; }

        public IEnumerable<string> Errors { get; set; }

        private async Task SubmitAsync()
        {
            var result = await _userService.RegistrationAsync(model);

            if (result.Succeeded)
            {
                this.ShowErrors = false;

                _toastService.ShowSuccess(
                    "თქვენ წარმატებით დარეგისტრირდით. \n" +
                    "გთხოვთ დაადასტუროთ მეილიდან თქვენი რეგისტრაცია");

                _navigationManager.NavigateTo("/account/login");
            }
            else
            {
                this.Errors = new List<string>() { "დაფიქსირდა შეცდომა" };
                this.ShowErrors = true;
            }
        }
    }
}