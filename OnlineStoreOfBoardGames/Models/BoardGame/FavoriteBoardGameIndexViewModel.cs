namespace OnlineStoreOfBoardGames.Models.BoardGame
{
    public class FavoriteBoardGameIndexViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CountOfUserWhoLikeIt { get; set; }
        public int Rank { get; set; }
    }
}
