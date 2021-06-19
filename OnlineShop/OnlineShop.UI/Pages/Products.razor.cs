using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using OnlineShop.UI.Models.Product;
using OnlineShop.UI.Models.Product.AdminProduct;

namespace OnlineShop.UI.Pages
{
    public partial class Products : ComponentBase
    {
        private ProductsViewModel products;

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        private ClaimsPrincipal User = new ClaimsPrincipal();

        protected override async Task OnInitializedAsync()
        {
            User = (await authenticationStateTask).User;
            var productsRequest = new GetProductsRequest() { Page = 1, PageSize = 10 };
            products = await _productService.GetProductsAsync(productsRequest, CancellationToken.None);
        }

        public async Task<bool> DeleteProductAsync(long productId)
        {
            var result = await _adminService.DeleteProductAsync(new DeleteProductRequest() { Id = productId }, CancellationToken.None);//todo dasamatebelia cancelltokenebi
            if (result)
            {
                _toastService.ShowSuccess("პროდუქტი წარმატებით წაიშალა");
                return true;
            }

            _toastService.ShowError("პროდუქტი ვერ წაიშალა");
            return false;
        }
    }
}