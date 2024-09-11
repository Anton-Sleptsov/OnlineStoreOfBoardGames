using OnlineStoreOfBoardGames.Data.Enums;
using OnlineStoreOfBoardGames.Data.Model;

namespace OnlineStoreOfBoardGames.Data.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        bool Exist(string login);
        User? GetByLoginAndPasswrod(string login, string password);
        Language GetLanguage(int userId);
        User? GetWithFavoriteBoardGames(int id);
        void UpdatePermission(int userId, Permission userPermission);
    }
}