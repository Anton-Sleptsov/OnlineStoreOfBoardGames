using Microsoft.AspNetCore.SignalR;
namespace OnlineStoreOfBoardGames.Hubs
{
    public class BoardGameHub : Hub<IBoardGameHub>
    {
        public void DeleteBoardGame(int gameId)
        {
            Clients.All.NotifyAboutDeleteBoardGame(gameId);
        }

        public void ChangeFavorites()
        {
            Clients.All.NotifyAboutChangeFavorites();
        }
    }
}
