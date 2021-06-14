using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnlineShop.UI.Models.Common;
using OnlineShop.UI.Models.Product;
using OnlineShop.UI.Models.Product.AdminProduct;

namespace OnlineShop.UI.Services.Abstract
{
    public interface IAdminService
    {
        Task<Result<string>> AddProductAsync(AddProductRequest request, CancellationToken cancellationToken);

        Task<Result<string>> AddProductImageAsync(AddImageRequest request, CancellationToken cancellationToken);

        Task<Result<string>> DeleteProductAsync(DeleteProductRequest request, CancellationToken cancellationToken);

        Task<Result<string>> DeleteProductImageAsync(DeleteProductImageRequest request, CancellationToken cancellationToken);

        Task<Result<string>> UploadImageAsync(UploadedFile file);
    }
}