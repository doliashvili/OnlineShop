using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.Products.ReadModels
{
    public sealed class PagingProductModel
    {
        public PagingProductModel(List<ProductReadModel> products, int pageCount, int currentPage)
        {
            Products = products;
            PageCount = pageCount;
            CurrentPage = currentPage;
        }

        public List<ProductReadModel> Products { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
    }
}
