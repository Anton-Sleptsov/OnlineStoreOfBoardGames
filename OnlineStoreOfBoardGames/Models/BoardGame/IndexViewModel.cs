namespace OnlineStoreOfBoardGames.Models.BoardGame
{
    public class IndexViewModel
    {
        public List<BoardGameIndexViewModel> BoardGames { get; set; }
        public bool CanCreateAndUpdateBoardGames { get; set; }
        public bool CanDeleteBoardGames { get; set; }
        public List<FavoriteBoardGameIndexViewModel> Top3FavoriteBoardGames { get; set; }
    }
}
