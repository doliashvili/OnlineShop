using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OnlineShop.UI.Models.Product;

namespace OnlineShop.UI.Pages
{
    public partial class Products : ComponentBase
    {
        private ProductsViewModel products = new();

        protected override async Task OnInitializedAsync()
        {
            var productsRequest = new GetProductsRequest() { Page = 1, PageSize = 10 };
            products = await _productService.GetProductsAsync(productsRequest, CancellationToken.None);
            foreach (var productViewModel in products.Products)
            {
                var images = productViewModel.Images;
                for (int i = 0; i < images.Count; i++)
                {
                    images[i].Url = images[i].Url.Insert(0, "http://127.0.0.1:8887/").Replace(@"images\", "");
                }
            }
        }
    }
}