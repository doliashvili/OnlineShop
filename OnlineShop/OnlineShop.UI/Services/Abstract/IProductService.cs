using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OnlineShop.UI.Models.Product;

namespace OnlineShop.UI.Services.Abstract
{
    public interface IProductService
    {
        Task<ProductsViewModel> GetProductsAsync(GetProductsRequest getProductsRequest, CancellationToken cancellationToken);

        Task<ProductsViewModel> GetFilteredProductsAsync(GetFilteredProductsRequest getFilteredProductsRequest, CancellationToken cancellationToken);

        Task<ProductViewModel> GetProductByIdAsync(GetProductByIdRequest getProductByIdRequest, CancellationToken cancellationToken);
    }
}