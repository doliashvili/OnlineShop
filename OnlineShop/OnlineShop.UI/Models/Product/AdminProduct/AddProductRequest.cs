using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineShop.UI.Enums;

namespace OnlineShop.UI.Models.Product.AdminProduct
{
    public class AddProductRequest
    {
        [Required(ErrorMessage = "გთხოთ მიუთითოთ თანხა")]
        public decimal Price { get; set; }

        public string? Color { get; set; }
        public string? Brand { get; set; }

        [Required(ErrorMessage = "გთხოვთ მიუთითოთ პროდუქტის ტიპი")]
        public string ProductType { get; set; }

        public Weight? Weight { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Gender? Gender { get; set; }
        public bool? ForBaby { get; set; }
        public string? Size { get; set; }

        [Required(ErrorMessage = "გთხოვთ მიუთითოთ პროდუქტის რაოდენობა")]
        public short Quantity { get; set; }

        public float? Discount { get; set; }
        public DateTime? CreateTime { get; set; }

        [Required(ErrorMessage = "გთხოვთ ატვირთოთ სურათი")]
        public List<Image> Images { get; set; }

        public DateTime? Expiration { get; set; }
    }
}