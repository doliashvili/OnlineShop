using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnlineShop.UI.Extensions;
using OnlineShop.UI.Models.Common;
using OnlineShop.UI.Models.Product.AdminProduct;
using OnlineShop.UI.Services.Abstract;

namespace OnlineShop.UI.Services.Implements
{
    public sealed class AdminService : IAdminService
    {
        private readonly HttpClient _httpClient;

        public AdminService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("OnlineShop");
        }

        public async Task<Result<string>> AddProductAsync(AddProductRequest request, CancellationToken cancellationToken)
        {
            var result = await _httpClient.SendAsync<Result<string>>(HttpMethod.Post, "v1/Product/CreateProduct", request.AsJson(),
                cancellationToken);

            return result;
        }

        public Task<Result<string>> AddProductImageAsync(AddImageRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result<string>> DeleteProductAsync(DeleteProductRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result<string>> DeleteProductImageAsync(DeleteProductImageRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<string>> UploadImageAsync(UploadedFile file)
        {
            var result = await _httpClient.SendAsync<Result<string>>(HttpMethod.Post, "api/v1/file/upload-image", file.FileContent.AsJson(),
                  CancellationToken.None);

            return result;
        }
    }
}