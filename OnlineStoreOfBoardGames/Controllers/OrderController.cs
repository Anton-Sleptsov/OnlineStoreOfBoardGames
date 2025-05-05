using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreOfBoardGames.Controllers.ActionFilterAttributes;
using OnlineStoreOfBoardGames.Data.Enums;
using OnlineStoreOfBoardGames.Data.Model;
using OnlineStoreOfBoardGames.Data.Repositories;
using OnlineStoreOfBoardGames.Mappers;
using OnlineStoreOfBoardGames.Models.Order;
using OnlineStoreOfBoardGames.Services.AuthStuff.Interfaces;

namespace OnlineStoreOfBoardGames.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly OrderRepository _orderRepository;
        private readonly CartRepository _cartRepository;
        private readonly IAuthService _authServise;
        private readonly CartMapper _cartMapper;

        public OrderController(OrderRepository orderRepositories,
            CartRepository cartRepository,
            IAuthService authService,
            CartMapper cartMapper)
        {
            _orderRepository = orderRepositories;
            _cartRepository = cartRepository;
            _authServise = authService;
            _cartMapper = cartMapper;
        }

        public IActionResult Index(bool isAll = false)
        {
            var userId = _authServise.GetUserId();
            List<Order> orders = [];
            if (isAll)
            {
                if (_orderRepository.UserHasOrders(userId))
                {
                    orders = _orderRepository.GetByUser(userId);
                }
            }
            else
            {
                if (_orderRepository.UserHasActiveOrders(userId))
                {
                    orders = _orderRepository.GetActiveByUser(userId);
                }
            }
            var listOfViewModels = orders.Select(_cartMapper.BuildOrdersAllViewModel).ToList();
            var viewModel = new OrderIndexViewModel
            {
                IsAll = isAll,
                ViewModels = listOfViewModels
            };
            return View(viewModel);
        }

        [HasPermission(Permission.CanChangeOrderStatus)]
        public IActionResult All(bool isAll = false)
        {
            List<Order> orders = [];
            if (isAll)
            {
                if (_orderRepository.Any())
                {
                    orders = _orderRepository.GetAllWithItems();
                }
            }
            else
            {
                if (_orderRepository.AnyActiveOrders())
                {
                    orders = _orderRepository.GetAllActive();
                }
            }
            var listOfViewModels = orders.Select(_cartMapper.BuildOrdersAllViewModel).ToList();
            var viewModel = new OrderIndexViewModel
            {
                IsAll = isAll,
                ViewModels = listOfViewModels
            };
            return View(viewModel);
        }

        public IActionResult CurrentOrder(int orderId)
        {
            var orderData = _orderRepository.GetWithItems(orderId);
            var orderViewModel = _cartMapper.BuildOrderViewModel(orderData);
            if (_authServise.HasPermission(Permission.CanChangeOrderStatus))
            {
                orderViewModel.IsAdmin = true;
            }
            else
            {
                orderViewModel.IsAdmin = false;
            }
               
            return View(orderViewModel);
        }

        [HasPermission(Permission.CanChangeOrderStatus)]
        public IActionResult SetIsDelivered(int orderId)
        {
            _orderRepository.SetIsDelivered(orderId);

            return RedirectToAction(nameof(CurrentOrder), new { orderId });
        }

        public IActionResult CreateOrder(int cartId)
        {
            var cart = _cartRepository.GetWithIncludes(cartId);
            var orderDataModel = _cartMapper.BuildOrderDataModel(cart);
            var order = _orderRepository.Create(orderDataModel);
            _cartRepository.Delete(cartId);

            return RedirectToAction(nameof(CurrentOrder), new { orderId = order.Id });
        }
    }
}
