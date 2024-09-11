using OnlineStoreOfBoardGames.Data.Model.Alerts;

namespace OnlineStoreOfBoardGames.Data.Repositories.Interfaces
{
    public interface IAlertRepository : IBaseRepository<Alert>
    {
        void UserWasNottified(int userId, int alertId);

        List<Alert> GetAlertsForUser(int userId);
    }
}
