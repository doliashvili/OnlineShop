namespace OnlineShop.Domain.Carts.ReadModels
{
    public sealed class CartReadModel
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public decimal? DiscountPrice { get; set; }
        public byte Quantity { get; set; }
        public string ImageUrl { get; set; }
    }
}