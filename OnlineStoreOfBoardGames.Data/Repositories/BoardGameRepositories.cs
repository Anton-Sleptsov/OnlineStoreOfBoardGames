using Microsoft.EntityFrameworkCore;
using OnlineStoreOfBoardGames.Data.Model;
using OnlineStoreOfBoardGames.Data.Repositories.DataModel;
using OnlineStoreOfBoardGames.Data.Repositories.Interfaces;
using OnlineStoreOfBoardGames.Data.Repositories.RawSql;

namespace OnlineStoreOfBoardGames.Data.Repositories
{
    public class BoardGameRepositories : BaseRepository<BoardGame>, IBoardGameRepositories
    {
        public BoardGameRepositories(PortalDbContext dbContext) : base(dbContext) { }

        public string GetName(int id)
            => _dbSet
            .First(boardGame => boardGame.Id == id)
            .Title;

        public BoardGame GetWithUsersWhoFavoriteThisBoardGame(int id)
            => _dbSet
            .Include(boardGame => boardGame.UsersWhoFavoriteThisBoardGame)
            .Single(boardGame => boardGame.Id == id);

        public List<BoardGame> GetFavoriteBoardGamesForUser(int userId)
            => _dbSet
            .Where(boardGame => boardGame.UsersWhoFavoriteThisBoardGame.Any(user => user.Id == userId))
            .ToList();

        public void Update(BoardGame boardGame)
        {
            BoardGame updatedboardGame = Get(boardGame.Id);
            updatedboardGame.Title = boardGame.Title;
            updatedboardGame.MiniTitle = boardGame.MiniTitle;
            updatedboardGame.Description = boardGame.Description;
            updatedboardGame.Essence = boardGame.Essence;
            updatedboardGame.Tags = boardGame.Tags;
            updatedboardGame.Price = boardGame.Price;
            updatedboardGame.ProductCode = boardGame.ProductCode;

            _dbContext.SaveChanges();
        }

        public bool Delete(int gameId)
        {
            var game = Get(gameId);

            if (game is null)
            {
                throw new KeyNotFoundException();
            }

            _dbSet.Remove(game);
            _dbContext.SaveChanges();

            if (Get(gameId) is null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddUserWhoFavoriteThisBoardGame(User user, int gameId)
        {
            BoardGame boardGame = GetWithUsersWhoFavoriteThisBoardGame(gameId);
            boardGame.UsersWhoFavoriteThisBoardGame.Add(user);

            _dbContext.SaveChanges();
        }

        public void RemoveUserWhoFavoriteThisBoardGame(User user, int gameId)
        {
            BoardGame boardGame = GetWithUsersWhoFavoriteThisBoardGame(gameId);
            boardGame.UsersWhoFavoriteThisBoardGame.Remove(user);

            _dbContext.SaveChanges();
        }

        public IEnumerable<Top3BoardGameDataModel> GetTop3()
        {
            return CustomSqlQuery<Top3BoardGameDataModel>(SqlQueryManager.GetTop3BoardGames)
                .ToList();
        }

        public BoardGameOfDay GetBoardGameOfDay()
        {
            return CustomSqlQuery<BoardGameOfDay>(SqlQueryManager.GetBoardGameOfDay)
                .First();
        }

        public BestBoardGameDataModel GetBest()
        {
            return CustomSqlQuery<BestBoardGameDataModel>(SqlQueryManager.GetBestBoardGame)
                .First();
        }
    }
}
