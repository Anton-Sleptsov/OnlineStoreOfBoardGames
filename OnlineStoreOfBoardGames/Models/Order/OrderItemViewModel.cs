namespace OnlineStoreOfBoardGames.Models.Order
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }
        public int BoardGameId { get; set; }
        public string BoardGameTitle { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
