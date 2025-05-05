using Microsoft.EntityFrameworkCore;
using OnlineStoreOfBoardGames.Data.Model;

namespace OnlineStoreOfBoardGames.Data.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(PortalDbContext dbContext) : base(dbContext) { }

        public List<Order> GetByUser(int userId) 
            => _dbSet.Include(c => c.Games)
            .ThenInclude(ci => ci.BoardGame)
            .Where(c => c.UserId == userId)
            .ToList();

        public Order GetWithItems(int orderId)
          => _dbSet.Include(c => c.Games)
          .ThenInclude(ci => ci.BoardGame)
          .First(c => c.Id == orderId);

        public List<Order> GetActiveByUser(int userId)
            => _dbSet.Include(c => c.Games)
            .ThenInclude(ci => ci.BoardGame)
            .Where(c => c.UserId == userId && !c.IsDelivered)
            .ToList();

        public List<Order> GetAllActive()
            => _dbSet.Include(c => c.Games)
            .ThenInclude(ci => ci.BoardGame)
            .Where(c => !c.IsDelivered)
            .ToList();

        public List<Order> GetAllWithItems()
            => _dbSet.Include(c => c.Games)
            .ThenInclude(ci => ci.BoardGame)
            .ToList();

        public bool UserHasActiveOrders(int userId)
            => _dbSet.Where(c => c.UserId == userId).Any(c => !c.IsDelivered);

        public bool UserHasOrders(int userId)
            => _dbSet.Where(c => c.UserId == userId).Any();

        public bool AnyActiveOrders()
            => _dbSet.Any(c => !c.IsDelivered);

        public new Order Create(Order order)
        {
            order.User = _dbContext.Users.First(x => x.Id == order.UserId);
            order.DateOfCreate = DateTime.UtcNow;
            foreach (var item in order.Games)
            {
                item.Order = order;
                item.BoardGame = _dbContext.BoardGames.First(x => x.Id == item.BoardGameId);
            }

            _dbSet.Add(order);
            _dbContext.SaveChanges();
            foreach (var item in order.Games)
            {
                item.OrderId = order.Id;
            }
            _dbContext.SaveChanges();
            return order;
        }

        public bool IsDelivered(int orderId)
            => _dbSet
            .First(c => c.Id == orderId)
            .IsDelivered;

        public void SetIsDelivered(int orderId)
        {
            var order = _dbSet.First(c => c.Id == orderId);
            order.IsDelivered = true;
            order.DateOfDelivery = DateTime.UtcNow;

            _dbContext.SaveChanges();
        }
    }
}
