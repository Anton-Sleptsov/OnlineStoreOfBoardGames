using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreOfBoardGames.Data.Model
{
    public class OrderItem : BaseModel
    {
        public int BoardGameId { get; set; }
        public BoardGame BoardGame { get; set; }
        public  Order Order { get; set; }
        public  int OrderId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

    }
}
