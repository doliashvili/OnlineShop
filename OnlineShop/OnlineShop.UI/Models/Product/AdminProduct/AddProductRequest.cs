using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineShop.UI.Enums;

namespace OnlineShop.UI.Models.Product.AdminProduct
{
    public class AddProductRequest
    {
        [Required(ErrorMessage = "გთხოთ მიუთითოთ თანხა")]
        [Range(0.1, 922_337_203_685_477, ErrorMessage = "თანხა არ შეიძლება იყოს 0.1 ზე ნაკლები")]
        public decimal Price { get; set; }

        public string? Color { get; set; }
        public string? Brand { get; set; }

        [Required(ErrorMessage = "გთხოვთ მიუთითოთ პროდუქტის ტიპი")]
        public string ProductType { get; set; }

        public Weight? Weight { get; set; }

        [Required(ErrorMessage = "გთხოთ მიუთითოთ სახელი")]
        public string? Name { get; set; }

        public string? Description { get; set; }
        public Gender? Gender { get; set; }
        public bool? ForBaby { get; set; }
        public string? Size { get; set; }

        [Required(ErrorMessage = "გთხოვთ მიუთითოთ პროდუქტის რაოდენობა")]
        public short Quantity { get; set; }

        public float? Discount { get; set; }

        [Required(ErrorMessage = "გთხოვთ ატვირთოთ სურათი")]
        public List<Image> Images { get; set; }

        public DateTime? Expiration { get; set; }
    }
}