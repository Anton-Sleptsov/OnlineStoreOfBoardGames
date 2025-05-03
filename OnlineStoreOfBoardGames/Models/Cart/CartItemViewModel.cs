namespace OnlineStoreOfBoardGames.Models.Cart
{
    public class CartItemViewModel
    {
        public int Id { get; set; }
        public int BoardGameId { get; set; }
        public string BoardGameTitle { get; set; }
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
