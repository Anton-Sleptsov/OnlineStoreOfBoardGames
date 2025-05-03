using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreOfBoardGames.Data.Model
{
    public class Cart : BaseModel
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public virtual List<CartItem> Games { get; set; }
        public double Sum { get; set; }
    }
}
