namespace OnlineStoreOfBoardGames.Data.Repositories.DataModel
{
    public class BestBoardGameDataModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int CountOfUserWhoLikeIt { get; set; }
    }
}
