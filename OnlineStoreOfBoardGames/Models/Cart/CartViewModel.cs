using OnlineStoreOfBoardGames.Data.Model;

namespace OnlineStoreOfBoardGames.Models.Cart
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual List<CartItemViewModel> Games { get; set; }
        public double Sum { get; set; }
    }
}
