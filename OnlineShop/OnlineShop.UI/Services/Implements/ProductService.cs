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

        public async Task<ProductsViewModel> GetFilteredProductsAsync(GetFilteredProductsRequest getFilteredProductsRequest, CancellationToken cancellationToken)
        {
            var qpc = new QueryParamCollection
            {
                {"pageSize", getFilteredProductsRequest.PageSize},
                {"page", getFilteredProductsRequest.Page},
                {"brand", getFilteredProductsRequest.Brand},
                {"color", getFilteredProductsRequest.Color},
                {"forBaby", getFilteredProductsRequest.ForBaby},
                {"gender", getFilteredProductsRequest.Gender},
                {"name", getFilteredProductsRequest.Name},
                {"priceFrom", getFilteredProductsRequest.PriceFrom},
                {"priceTo", getFilteredProductsRequest.PriceTo},
                {"productType", getFilteredProductsRequest.ProductType},
                {"size", getFilteredProductsRequest.Size},
            };

            var response = await _httpClient.SendAsync<ProductsViewModel>(HttpMethod.Get, $"v1/Product/GetFilteredProducts?{qpc}", null, cancellationToken);
            return response;
        }

        public async Task<ProductViewModel> GetProductByIdAsync(GetProductByIdRequest getProductByIdRequest, CancellationToken cancellationToken)
        {
            var qpc = new QueryParamCollection
            {
                {"id", getProductByIdRequest.Id},
            };

            var response = await _httpClient.SendAsync<ProductViewModel>(HttpMethod.Get, $"v1/Product/GetProductById?{qpc}", null, cancellationToken);
            return response;
        }
    }
}