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
        [Range(1, 120)]
        public int Page { get; set; }

        [Required]
        [Range(1, 10)]
        public int PageSize { get; set; }
    }
}