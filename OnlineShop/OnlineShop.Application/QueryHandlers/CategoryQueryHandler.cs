using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cqrs;
using OnlineShop.Domain.AbstractRepository;
using OnlineShop.Domain.Categories.Queries;
using OnlineShop.Domain.Categories.ReadModels;

namespace OnlineShop.Application.QueryHandlers
{
    public class CategoryQueryHandler : IQueryHandler<GetCategories, List<CategoryReadModel>>
    {
        private readonly ICategoryReadRepository _categoryRead;

        public CategoryQueryHandler(ICategoryReadRepository categoryRead)
        {
            _categoryRead = categoryRead;
        }

        public Task<List<CategoryReadModel>> HandleAsync(GetCategories query)
        {
            return _categoryRead.GetCategoriesAsync();
        }
    }
}