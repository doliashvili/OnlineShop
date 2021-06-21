using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
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

        public void ChangeMainImage(Image smallImage)
        {
            image = smallImage;
        }
    }
}