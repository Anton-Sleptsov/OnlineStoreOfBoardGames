using Microsoft.EntityFrameworkCore;
using OnlineStoreOfBoardGames.Data.Model;

namespace OnlineStoreOfBoardGames.Data.Repositories
{
    public class CartRepository : BaseRepository<Cart>
    {
        public CartRepository(PortalDbContext dbContext) : base(dbContext) { }

        public Cart GetByUser(int userId)
        {
            var cart = _dbSet.Include(c => c.Games).ThenInclude(ci => ci.BoardGame).First(c => c.UserId == userId);

            foreach (var item in cart.Games)
            {
                item.Price = item.BoardGame.Price * item.Quantity;
            }

            cart.Sum = cart.Games.Sum(g => g.Price);
            _dbContext.SaveChanges();
            return cart;
        }

        public Cart GetWithIncludes(int cartId)
            => _dbSet.Include(c => c.Games).ThenInclude(ci => ci.BoardGame).First(x => x.Id == cartId);

        public bool CartForUserIsExist(int userId) =>
            _dbSet
            .Any(cart => cart.UserId == userId);

        public bool ThisGameInCartForUserIsExist(int userId, int boardGameId) =>
            _dbContext.CartItems
            .Any(item => item.BoardGameId == boardGameId && item.Cart.UserId == userId);

        public Cart Create(int userId)
        {
            var cart = new Cart
            {
                Games = [],
                UserId = userId,
                User = _dbContext.Users.First(x => x.Id == userId),
                Sum = 0
            };

            return base.Create(cart);
        }

        public void AddGameInCart(int cartId, int boardGameId)
        {
            var game = _dbContext.BoardGames.First(x => x.Id == boardGameId);
            var cart = _dbSet.Include(c => c.Games).First(x => x.Id == cartId);

            var item = new CartItem
            {
                CartId = cartId,
                Cart = cart,
                Quantity = 1,
                BoardGameId = boardGameId,
                BoardGame = game,
                Price = game.Price
            };
            _dbContext.CartItems.Add(item);

            cart.Games.Add(item);

            cart.Sum = cart.Games.Sum(g => g.Price);

            _dbContext.SaveChanges();
        }

        public void IncrementGame(int cartId, int boardGameId)
        {
            var cart = _dbSet.Include(c => c.Games).First(x => x.Id == cartId);
            var game = _dbContext.BoardGames.First(x => x.Id == boardGameId);

            var thisItem = cart.Games.First(x => x.BoardGameId == boardGameId);
            thisItem.Quantity++;
            thisItem.Price = game.Price * thisItem.Quantity;

            cart.Sum = cart.Games.Sum(g => g.Price);
            _dbContext.SaveChanges();
        }

        public void DecrementGame(int cartId, int boardGameId)
        {
            var cart = _dbSet.Include(c => c.Games).First(x => x.Id == cartId);
            var game = _dbContext.BoardGames.First(x => x.Id == boardGameId);

            var thisItem = cart.Games.First(x => x.BoardGameId == boardGameId);
            if (thisItem.Quantity > 1)
            {
                thisItem.Quantity--;
                thisItem.Price = game.Price * thisItem.Quantity;
                cart.Sum = cart.Games.Sum(g => g.Price);
                _dbContext.SaveChanges();
            }
            else
            {
                DeleteGameFromCart(cartId, boardGameId);
            }
        }

        public void DeleteGameFromCart(int cartId, int boardGameId)
        {
            var cart = _dbSet.Include(c => c.Games).First(x => x.Id == cartId);

            var thisItem = cart.Games.First(x => x.BoardGameId == boardGameId);
            cart.Games.Remove(thisItem);
            _dbContext.CartItems.Remove(thisItem);

            cart.Sum = cart.Games.Sum(g => g.Price);

            _dbContext.SaveChanges();
        }
    }
}
