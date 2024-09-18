using OnlineStoreOfBoardGames.Data.Enums;

namespace OnlineStoreOfBoardGames.Models.User
{
    public class IndexViewModel
    {
        public List<UserPermissionViewModel> Users { get; set; }
        
        public List<Permission> AvailablePermissions { get; set; }
        public bool CanEditPermissions { get; set; }
    }
}
