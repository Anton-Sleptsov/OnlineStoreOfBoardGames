namespace OnlineStoreOfBoardGames.Models.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual List<OrderItemViewModel> Games { get; set; }
        public double Sum { get; set; }
        public bool IsDelivered { get; set; }
        public string Status { get; set; }
        public DateTime DateOfCreate { get; set; }
        public bool IsAdmin { get; set; }
    }
}
