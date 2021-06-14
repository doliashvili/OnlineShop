namespace OnlineShop.Domain.Carts.ReadModels
{
    public sealed class CartReadModel
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public long ProductId { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public decimal? DiscountPrice { get; set; }
        public short Quantity { get; set; }
        public string ImageUrl { get; set; }
    }
}