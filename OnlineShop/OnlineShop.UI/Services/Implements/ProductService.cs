using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using OnlineShop.UI.Helpers;
using OnlineShop.UI.Models.Product;
using OnlineShop.UI.Services.Abstract;
using OnlineShop.UI.Extensions;

namespace OnlineShop.UI.Services.Implements
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductsViewModel> GetProductsAsync(GetProductsRequest getProductsRequest, CancellationToken cancellationToken)
        {
            var qpc = new QueryParamCollection
            {
                { "pageSize", getProductsRequest.PageSize },
                { "page", getProductsRequest.Page }
            }.ToQueryString();

            var response = await _httpClient.SendAsync<ProductsViewModel>(HttpMethod.Get, $"v1/Product/GetProducts?{qpc}", null, cancellationToken);
            return response;
        }

        public Task<ProductsViewModel> GetFilteredProductsAsync(GetFilteredProductsRequest getFilteredProductsRequest, CancellationToken cancellationToken)
        {
            var qpc = new QueryParamCollection
            {
                {"pageSize", getFilteredProductsRequest.PageSize},
                {"page", getFilteredProductsRequest.Page},
                {"Brand", getFilteredProductsRequest.Brand},
                {"Color", getFilteredProductsRequest.Color},
                {"ForBaby", getFilteredProductsRequest.ForBaby},
                {"Gender", getFilteredProductsRequest.Gender},
                {"Name", getFilteredProductsRequest.Name},
                {"PriceFrom", getFilteredProductsRequest.PriceFrom},
                {"PriceTo", getFilteredProductsRequest.PriceTo},
                {"ProductType", getFilteredProductsRequest.ProductType},
                {"size", getFilteredProductsRequest.Size},
            };
            throw new NotImplementedException();
        }

        public Task<ProductViewModel> GetProductByIdAsync(GetProductByIdRequest getProductByIdRequest, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}