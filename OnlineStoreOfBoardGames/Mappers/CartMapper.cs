using OnlineStoreOfBoardGames.Data.Model;
using OnlineStoreOfBoardGames.Models.Cart;
using OnlineStoreOfBoardGames.Models.Order;

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

        public Order BuildOrderDataModel(Cart cart)
            => new Order
            {
                UserId = cart.UserId,
                Games = cart.Games.Select(BuildOrderItemDataModel).ToList(),
                Sum = cart.Sum
            };

        public OrderItem BuildOrderItemDataModel(CartItem cartItem)
            => new OrderItem
            {
                BoardGameId = cartItem.BoardGameId,
                Price = cartItem.Price,
                Quantity = cartItem.Quantity
            };

        public OrdersAllViewModel BuildOrdersAllViewModel(Order order)
            => new OrdersAllViewModel
            {
                UserId = order.UserId,
                OrderId = order.Id,
                Sum = order.Sum,
                DateOfCreate = order.DateOfCreate,
                IsDelivered = order.IsDelivered,
                Status = order.IsDelivered ? "Доставлен" : "Не доставлен"
            };
        public OrderViewModel BuildOrderViewModel(Order order)
            => new OrderViewModel
            {
                Id = order.Id,
                UserId = order.UserId,
                Games = order.Games.Select(BuildOrderItemViewModel).ToList(),
                Sum = order.Sum,
                DateOfCreate = order.DateOfCreate,
                IsDelivered = order.IsDelivered,
                Status = order.IsDelivered ? "Доставлен" : "Не доставлен"
            };

        public OrderItemViewModel BuildOrderItemViewModel(OrderItem orderItem)
            => new OrderItemViewModel
            {
                Id = orderItem.Id,
                BoardGameId = orderItem.BoardGameId,
                BoardGameTitle = orderItem.BoardGame.Title,
                OrderId = orderItem.Id,
                Price = orderItem.Price,
                Quantity = orderItem.Quantity,
            };
    }
}
