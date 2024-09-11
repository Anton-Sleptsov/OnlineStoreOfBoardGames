using OnlineStoreOfBoardGames.Data.Repositories;
using OnlineStoreOfBoardGames.Data.Repositories.DataModel;

namespace BoardGameOfDayApi.Services
{
    public class CacheService
    {
        private BoardGameOfDay _boardGameOfDay;
        private DateTime _dayOfChange = DateTime.MinValue;

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly object _lock = new();

        public CacheService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public BoardGameOfDay GetBoardGameOfDay()
        {
            if (_dayOfChange.Date != DateTime.Now.Date)
            {
                lock (_lock)
                {
                    if (_dayOfChange.Date != DateTime.Now.Date)
                    {
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var repository = scope.ServiceProvider.GetRequiredService<BoardGameRepositories>();
                            _boardGameOfDay = repository.GetBoardGameOfDay();
                        }
                        _dayOfChange = DateTime.Now.Date;
                    }
                }
            }

            return _boardGameOfDay;
        }
    }
}
