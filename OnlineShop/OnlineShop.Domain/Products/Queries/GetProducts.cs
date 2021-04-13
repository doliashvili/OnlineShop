using System.Collections.Generic;
using System.Text.Json.Serialization;
using CoreModels.Messaging;
using OnlineShop.Domain.Products.ReadModels;

namespace OnlineShop.Domain.Products.Queries
{
    public class GetProducts : IQuery<PagingProductModel>
    {
        [JsonConstructor]
        public GetProducts(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
        public GetProducts()
        {
            
        }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
