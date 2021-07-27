using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OnlineShop.UI.Models.Cart;
using OnlineShop.UI.Models.Product;

namespace OnlineShop.UI.Pages
{
    public partial class ProductDetails
    {
        [Parameter]
        public long ProductId { get; set; }

        private ProductViewModel product;

        private Image image;

        protected override async Task OnInitializedAsync()
        {
            product = await _productService.GetProductByIdAsync(new GetProductByIdRequest() { Id = ProductId },
                CancellationToken.None);
            image = product?.MainImageUrl;
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

        public void ChangeMainImage(Image smallImage)
        {
            image = smallImage;
        }
    }
}