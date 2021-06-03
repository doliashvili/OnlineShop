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

            products = await ProductService.GetProductsAsync(productsRequest, CancellationToken.None);
        }
    }
}