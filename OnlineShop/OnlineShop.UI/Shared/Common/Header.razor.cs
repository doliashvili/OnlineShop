using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OnlineShop.UI.Models.Category;

namespace OnlineShop.UI.Shared.Common
{
    public partial class Header : ComponentBase
    {
        private List<CategoryViewModel> categories = new(10);

        protected override async Task OnInitializedAsync()
        {
            categories = await _categoryService.GetCategoriesAsync(CancellationToken.None);
        }
    }
}