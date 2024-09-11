using OnlineStoreOfBoardGames.Data.Enums;
using OnlineStoreOfBoardGames.Data.Model.Alerts;

namespace OnlineStoreOfBoardGames.Data.Model
{
    public class User : BaseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; } // TODO Store only hash

        public Language Language { get; set; } = Language.Ru;

        public Permission Permission { get; set; }
        public UserRole Role {  get; set; }

        public virtual List<BoardGame> FavoriteBoardsGames { get; set; }
        public virtual List<AlertUser> AlertsWhichISaw { get; set; }
    }
}
