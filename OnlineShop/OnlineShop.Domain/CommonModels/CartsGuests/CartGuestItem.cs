namespace OnlineShop.Domain.CommonModels.CartsGuests
{
    public record CartGuestItem
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public byte Quantity { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}