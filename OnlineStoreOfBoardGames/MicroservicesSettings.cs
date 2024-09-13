namespace OnlineStoreOfBoardGames
{
    internal class MicroservicesSettings
    {
        public static readonly string ReviewsHost = Environment.GetEnvironmentVariable("REVIEWS_HOST") ?? "localhost:5171";
        public static readonly string BoardGameOfDayHost = Environment.GetEnvironmentVariable("BOARD_DAME_OF_DAY_HOST") ?? "localhost:5273";
        public static readonly string BestBoardGameHost = Environment.GetEnvironmentVariable("BEST_BOARD_GAME_HOST") ?? "localhost:5029";
    }
}
