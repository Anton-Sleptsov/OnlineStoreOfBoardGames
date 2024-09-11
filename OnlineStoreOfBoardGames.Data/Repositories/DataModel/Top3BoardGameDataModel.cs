namespace OnlineStoreOfBoardGames.Data.Repositories.DataModel
{
    public class Top3BoardGameDataModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CountOfUserWhoLikeIt { get; set; }
        public int Rank { get; set; }
    }
}
