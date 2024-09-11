namespace OnlineStoreOfBoardGames.Data.Model.Alerts
{
    public class Alert : BaseModel
    {
        public string Text { get; set; }

        public bool IsActive { get; set; }

        public DateTime? EndDate { get; set; }

        public virtual List<AlertUser> UsersWhoAlreadySawIt { get; set; }
        public bool IsNewBoardGameAlert { get; set; }
    }
}
