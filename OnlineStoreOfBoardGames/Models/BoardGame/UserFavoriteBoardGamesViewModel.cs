using OnlineStoreOfBoardGames.Data.Model;

namespace OnlineStoreOfBoardGames.Models.BoardGame
{
    public class UserFavoriteBoardGamesViewModel
    {
        public string Name { get; set; }
        public List<BoardGameViewModel> FavoriteBoardGames { get; set; }
    }
}
