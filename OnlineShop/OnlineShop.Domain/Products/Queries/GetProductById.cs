using System;
using System.Text.Json.Serialization;
using CoreModels.Messaging;
using OnlineShop.Domain.Products.ReadModels;

namespace OnlineShop.Domain.Products.Queries
{
    public class GetProductById : IQuery<ProductReadModel>
    {
        public long Id { get; set; }

        [JsonConstructor]
        public GetProductById(long id)
        {
            Id = id;
        }

        public GetProductById()
        {
            
        }
    }
}
