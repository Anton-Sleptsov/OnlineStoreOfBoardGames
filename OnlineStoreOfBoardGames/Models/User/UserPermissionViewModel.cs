using OnlineStoreOfBoardGames.Data.Enums;

namespace OnlineStoreOfBoardGames.Models.User
{
    public class UserPermissionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Permission Permission { get; set; }
    }
}
