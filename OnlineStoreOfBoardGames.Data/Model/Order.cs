namespace OnlineStoreOfBoardGames.Data.Model
{
    public class Order : BaseModel
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public virtual List<OrderItem> Games { get; set; }
        public double Sum { get; set; }
        public bool IsDelivered { get; set; }
    }
}
