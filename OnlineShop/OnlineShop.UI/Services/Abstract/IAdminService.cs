using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        Task<bool> AddProductAsync(AddProductRequest request, CancellationToken cancellationToken);

        Task<bool> AddProductImageAsync(AddImageRequest request, CancellationToken cancellationToken);

        Task<bool> DeleteProductAsync(DeleteProductRequest request, CancellationToken cancellationToken);

        Task<bool> DeleteProductImageAsync(DeleteProductImageRequest request, CancellationToken cancellationToken);

        Task<HttpResponseMessage> UploadImageAsync(MultipartFormDataContent content);
    }
}