namespace BoardGamesReviewsApi.Dtos
{
    public class DtoBoardGameReview
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Text { get; set; }
        public int BoardGameId { get; set; }
    }
}
