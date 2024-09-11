using Microsoft.Extensions.DependencyInjection;
using OnlineStoreOfBoardGames.Data.Model;
using OnlineStoreOfBoardGames.Data.Repositories;

namespace OnlineStoreOfBoardGames.Data.CacheServices
{
    public class BoardGameCache
    {
        private List<BoardGame> _boardGames;
        private DateTime _lastUpdate = DateTime.MinValue;

        private readonly IServiceScopeFactory _scopeFactory;

        public BoardGameCache(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        
        public List<BoardGame> GetBoardGames()
        {
            if (_lastUpdate.AddMinutes(30) < DateTime.Now)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<BoardGameRepositories>();
                    _boardGames = repository.GetAll();
                }

                _lastUpdate = DateTime.Now;
            }

            return _boardGames;
        }

        public void ResetCache()
        {
            _lastUpdate = DateTime.MinValue;
        }
    }
}
