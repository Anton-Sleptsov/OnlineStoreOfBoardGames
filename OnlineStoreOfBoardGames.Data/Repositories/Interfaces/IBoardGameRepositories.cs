using OnlineStoreOfBoardGames.Data.Model;
using OnlineStoreOfBoardGames.Data.Repositories.DataModel;

namespace OnlineStoreOfBoardGames.Data.Repositories.Interfaces
{
    public interface IBoardGameRepositories : IBaseRepository<BoardGame>
    {
        void AddUserWhoFavoriteThisBoardGame(User user, int gameId);
        bool Delete(int gameId);
        List<BoardGame> GetFavoriteBoardGamesForUser(int userId);
        string GetName(int id);
        IEnumerable<Top3BoardGameDataModel> GetTop3();
        BoardGame GetWithUsersWhoFavoriteThisBoardGame(int id);
        void RemoveUserWhoFavoriteThisBoardGame(User user, int gameId);
        void Update(BoardGame boardGame);
    }
}