namespace OnlineStoreOfBoardGames.Models.BoardGame
{
    public class BoardGameViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string MiniTitle { get; set; }
        public bool HasMainImage { get; set; }
        public bool HasSideImage { get; set; }
        public string Description { get; set; }
        public string? Essence { get; set; }
        public string? Tags { get; set; }
        public double Price { get; set; }
        public long ProductCode { get; set; }
        public bool IsFavoriteForUser { get; set; }
        public BoardGameOfDayViewModel BoardGameOfDay { get; set; }
        public BestBoardGameViewModel BestBoardGame { get; set; }
    }
}
