namespace BoardGamesReviewsApi.Data
{
    internal class DatabaseSettings
    {
        public static readonly string DbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "192.168.213.129:9001";
        public static readonly string DbUsername = Environment.GetEnvironmentVariable("DB_USER") ?? "testuser";
        public static readonly string DbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "testpassword";
        public static readonly string DbDbName = Environment.GetEnvironmentVariable("DB_DBNAME") ?? "reviewsdb";
    }
}
