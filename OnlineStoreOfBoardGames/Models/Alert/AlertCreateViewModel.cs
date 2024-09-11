namespace OnlineStoreOfBoardGames.Models.Alert
{
    public class AlertCreateViewModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsActive { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
