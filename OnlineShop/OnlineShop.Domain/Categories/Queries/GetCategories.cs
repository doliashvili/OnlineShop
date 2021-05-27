using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreModels.Messaging;
using OnlineShop.Domain.Categories.DomainObjects;
using OnlineShop.Domain.Categories.ReadModels;

namespace OnlineShop.Domain.Categories.Queries
{
    public class GetCategories : IQuery<List<CategoryReadModel>>
    {
    }
}