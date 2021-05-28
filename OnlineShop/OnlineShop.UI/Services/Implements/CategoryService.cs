using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnlineShop.UI.Extensions;
using OnlineShop.UI.Helpers;
using OnlineShop.UI.Models.Category;
using OnlineShop.UI.Services.Abstract;

namespace OnlineShop.UI.Services.Implements
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("OnlineShop");
        }

        public async Task<List<CategoryViewModel>> GetCategoriesAsync(CancellationToken cancellationToken)
        {
            var response = await _httpClient.SendAsync<List<CategoryViewModel>>(HttpMethod.Get, "v1/Category/GetCategories?", null, cancellationToken);
            return response;
        }
    }
}