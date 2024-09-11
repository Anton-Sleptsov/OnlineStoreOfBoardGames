using Microsoft.EntityFrameworkCore;

namespace OnlineStoreOfBoardGames.Data.Model.Alerts
{
    public class AlertUser : BaseModel
    {
        public virtual User User { get; set; }
        public virtual Alert Alert { get; set; }

        public DateTime WhenUserSawAlert { get; set; }
    }
}
