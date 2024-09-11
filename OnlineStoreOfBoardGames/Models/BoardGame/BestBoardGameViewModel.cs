namespace OnlineStoreOfBoardGames.Models.BoardGame
{
    public class BestBoardGameViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int CountOfUserWhoLikeIt { get; set; }
        public bool HasMainImage { get; set; }
    }
}
