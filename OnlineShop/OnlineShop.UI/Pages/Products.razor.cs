using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using OnlineShop.UI.Helpers;
using OnlineShop.UI.Models.Cart;
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

        private async Task<bool> DeleteProductAsync(long productId)
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

        private async Task<bool> AddCartAsync(long productId)
        {
            var userId = await _localStorageService.GetItemAsync<string>("userId");

            var request = new AddCartRequest()
            {
                ProductId = productId,
                Quantity = 1,
                UserId = userId
            };

            var isAdded = await _cartService.AddCartAsync(request, CancellationToken.None);

            if (isAdded)
            {
                _toastService.ShowSuccess("პროდუქტი დაემატა კალათაში");
                StateHasChanged();
                return true;
            }

            _toastService.ShowError("სამწუხაროთ პროდუქტი კალათაში ვერ დაემატა");
            return false;
        }

        private void NavigateToProductDetailsComponent(long productId)
        {
            _navigationManager.NavigateTo($"productDetails/{productId}");
        }
    }
}