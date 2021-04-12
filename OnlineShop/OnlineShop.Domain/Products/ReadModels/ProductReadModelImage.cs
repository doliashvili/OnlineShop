using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.Products.ReadModels
{
    public class ProductReadModelImage
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public bool MainImage { get; set; }
    }
}
