namespace PackMarket.Data.Models
{
    public record CartProduct:IComparable<CartProduct>
    {
        public int ProductId { get; set; }
        public int Count { get; set; }

        public int CompareTo(CartProduct other)
        {
            return ProductId.CompareTo(other.ProductId);
        }
    }
}
