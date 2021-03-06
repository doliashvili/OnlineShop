using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Http;
using OnlineShop.UI.Extensions;
using OnlineShop.UI.Models.Common;
using OnlineShop.UI.Models.Product.AdminProduct;
using OnlineShop.UI.Services.Abstract;

namespace OnlineShop.UI.Services.Implements
{
    public sealed class AdminService : IAdminService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageService _localStorage;
        private HttpClient _httpClient;

        public AdminService(IHttpClientFactory httpClientFactory, ILocalStorageService localStorage)
        {
            _httpClientFactory = httpClientFactory;
            _localStorage = localStorage;
            _httpClient = httpClientFactory.CreateClient("OnlineShop");
        }

        public async Task<bool> AddProductAsync(AddProductRequest request, CancellationToken cancellationToken)
        {
            var savedToken = await _localStorage.GetItemAsync<string>("authToken");

            if (!string.IsNullOrWhiteSpace(savedToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
            }

            var result = await _httpClient.PostAsync(HttpMethod.Post, "v1/Product/CreateProduct", request.AsJson(),
                cancellationToken);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> AddProductImageAsync(AddImageRequest request, CancellationToken cancellationToken)
        {
            var savedToken = await _localStorage.GetItemAsync<string>("authToken");

            if (!string.IsNullOrWhiteSpace(savedToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
            }

            var result = await _httpClient.PostAsync(HttpMethod.Post, "v1/Product/AddProductImage", request.AsJson(),
                cancellationToken);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProductAsync(DeleteProductRequest request, CancellationToken cancellationToken)
        {
            var savedToken = await _localStorage.GetItemAsync<string>("authToken");

            if (!string.IsNullOrWhiteSpace(savedToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
            }

            var result = await _httpClient.PostAsync(HttpMethod.Delete, "v1/Product/DeleteProduct", request.AsJson(),
                cancellationToken);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProductImageAsync(DeleteProductImageRequest request, CancellationToken cancellationToken)
        {
            var savedToken = await _localStorage.GetItemAsync<string>("authToken");

            if (!string.IsNullOrWhiteSpace(savedToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
            }

            var result = await _httpClient.PostAsync(HttpMethod.Delete, "v1/Product/DeleteProductImage", request.AsJson(),
                cancellationToken);

            return result.IsSuccessStatusCode;
        }

        public async Task<HttpResponseMessage> UploadImageAsync(MultipartFormDataContent content)
        {
            //var result = await _httpClient.SendAsync<Result<string>>(HttpMethod.Post, "api/v1/file/upload-image", file.FileContent.AsJson(),
            //      CancellationToken.None);
            _httpClient = _httpClientFactory.CreateClient("OnlineShop");
            var savedToken = await _localStorage.GetItemAsync<string>("authToken");

            if (!string.IsNullOrWhiteSpace(savedToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
            }

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.PostAsync("api/v1/file/upload-files", content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return response;
        }
    }
}