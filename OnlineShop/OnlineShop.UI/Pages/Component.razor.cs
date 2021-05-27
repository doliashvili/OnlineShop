using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OnlineShop.UI.Models.Product;

namespace OnlineShop.UI.Pages
{
    public partial class Component
    {
        private ProductsViewModel products;

        protected override async Task OnInitializedAsync() => await GetProductsViewModelAsync();

        public async Task GetProductsViewModelAsync()
        {
            try
            {
                products = await ProductService.GetProductsAsync(new GetProductsRequest() { Page = 1, PageSize = 10 }, CancellationToken.None);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}