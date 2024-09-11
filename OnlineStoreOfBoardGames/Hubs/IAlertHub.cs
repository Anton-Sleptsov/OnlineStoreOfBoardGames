namespace OnlineStoreOfBoardGames.Hubs
{
    public interface IAlertHub
    {
        Task AlertWasCreatedAsync(int alertId, string text);
        Task NewBoardGameAlertWasCreatedAsync(int alertId, string boardGameTitle);
    }
}
