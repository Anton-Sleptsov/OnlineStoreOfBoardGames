namespace OnlineStoreOfBoardGames.Models.Order
{
    public class OrdersAllViewModel
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public double Sum { get; set; }
        public bool IsDelivered { get; set; }
        public string Status { get; set; }
        public DateTime DateOfCreate { get; set; }
    }
}
