using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.UI.Models.Product
{
    public class GetProductsRequest
    {
        [Required]
        public int Page { get; set; }

        [Required]
        public int PageSize { get; set; }
    }
}