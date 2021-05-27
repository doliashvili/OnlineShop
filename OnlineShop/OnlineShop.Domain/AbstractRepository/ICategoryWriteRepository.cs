using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Domain.Categories.DomainObjects;
using OnlineShop.Domain.Categories.ReadModels;

namespace OnlineShop.Domain.AbstractRepository
{
    public interface ICategoryWriteRepository
    {
        Task AddCategoryAsync(Category category);

        Task DeleteCategoryAsync(long id);
    }
}