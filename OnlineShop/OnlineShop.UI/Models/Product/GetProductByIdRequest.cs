using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.UI.Models.Product
{
    public class GetProductByIdRequest
    {
        [Required]
        public long Id { get; set; }
    }
}