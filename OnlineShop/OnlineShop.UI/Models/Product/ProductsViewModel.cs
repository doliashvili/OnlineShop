using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.UI.Models.Product
{
    public class ProductsViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
    }
}