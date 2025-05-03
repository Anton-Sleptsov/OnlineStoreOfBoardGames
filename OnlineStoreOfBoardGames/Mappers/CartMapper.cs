using OnlineStoreOfBoardGames.Data.Model;
using OnlineStoreOfBoardGames.Models.Cart;

namespace OnlineStoreOfBoardGames.Mappers
{
    public class CartMapper
    {
        public CartViewModel BuildCartViewModel(Cart cart)
            => new CartViewModel
            {
                Id = cart.Id,
                UserId = cart.UserId,
                Games = cart.Games.Select(BuildCartItemViewModel).ToList(),
                Sum = cart.Sum
            };

        public CartItemViewModel BuildCartItemViewModel(CartItem cartItem)
            => new CartItemViewModel
            {
                Id = cartItem.Id,
                BoardGameId = cartItem.BoardGameId,
                BoardGameTitle = cartItem.BoardGame.Title,
                CartId = cartItem.CartId,
                Price = cartItem.Price,
                Quantity = cartItem.Quantity
            };
    }
}
