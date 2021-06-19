using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using OnlineShop.UI.Models.Category;

namespace OnlineShop.UI.Shared.Common
{
    public partial class Header : ComponentBase
    {
        private List<CategoryViewModel> categories;

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        private ClaimsPrincipal User = new ClaimsPrincipal();

        protected override async Task OnInitializedAsync()
        {
            User = (await authenticationStateTask).User;
            categories = await _categoryService.GetCategoriesAsync(CancellationToken.None);
        }
    }
}