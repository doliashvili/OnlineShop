using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OnlineShop.UI.Models.Category;

namespace OnlineShop.UI.Services.Abstract
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetCategoriesAsync(CancellationToken cancellationToken);
    }
}