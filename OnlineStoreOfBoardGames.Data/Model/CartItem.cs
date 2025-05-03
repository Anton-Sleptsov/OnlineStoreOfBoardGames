using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreOfBoardGames.Data.Model
{
    public class CartItem : BaseModel
    {
        public int BoardGameId { get; set; }
        public BoardGame BoardGame { get; set; }
        public Cart Cart { get; set; }
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
