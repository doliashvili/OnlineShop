using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.UI.Models.Product.AdminProduct
{
    public class AddImageRequest
    {
        public long Id { get; protected set; }
        public string Url { get; set; }
        public bool MainImage { get; set; }
        public long? ProductId { get; set; }
    }
}