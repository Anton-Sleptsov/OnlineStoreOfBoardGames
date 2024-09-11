using OnlineStoreOfBoardGames.Data.Enums;
using OnlineStoreOfBoardGames.Data.Model;

namespace OnlineStoreOfBoardGames.Services.AuthStuff.Interfaces
{
    public interface IAuthService
    {
        User GetUser();
        int GetUserId();
        Language GetUserLanguage();
        string GetUserName();
        Permission GetUserPermission();
        UserRole GetUserRole();
        bool HasPermission(Permission permission);
        bool HasRole(UserRole role);
        bool HasRoleOrHigher(UserRole role);
        bool IsAdmin();
        bool IsAuthenticated();
    }
}