using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreOfBoardGames.Data.Repositories;
using OnlineStoreOfBoardGames.Mappers;
using OnlineStoreOfBoardGames.Models.Cart;
using OnlineStoreOfBoardGames.Services.AuthStuff.Interfaces;

namespace OnlineStoreOfBoardGames.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly CartRepository _cartRepository;
        private readonly IAuthService _authServise;
        private readonly CartMapper _cartMapper;

        public CartController(CartRepository cartRepositories,
            IAuthService authService,
            CartMapper cartMapper)
        {
            _cartRepository = cartRepositories;
            _authServise = authService;
            _cartMapper = cartMapper;
        }

        public IActionResult Index()
        {
            var userId = _authServise.GetUserId();

            if (_cartRepository.CartForUserIsExist(userId))
            {
                var cartData = _cartRepository.GetByUser(userId);
                var cartViewModel = _cartMapper.BuildCartViewModel(cartData);

                return View(cartViewModel);
            }
            else
            {
                return View(new CartViewModel());
            }          
        }

        public IActionResult AddGame(int boardGameId)
        {
            var userId = _authServise.GetUserId();
            if (_cartRepository.CartForUserIsExist(userId))
            {
                var cartId = _cartRepository.GetByUser(userId).Id;
                if(_cartRepository.ThisGameInCartForUserIsExist(userId, boardGameId))
                {
                    _cartRepository.IncrementGame(cartId, boardGameId);
                }
                else
                {
                    _cartRepository.AddGameInCart(cartId, boardGameId);
                }
            }
            else
            {
                var cart = _cartRepository.Create(userId);
                _cartRepository.AddGameInCart(cart.Id, boardGameId);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult IncrementGame(int boardGameId)
        {
            var userId = _authServise.GetUserId();
            var cart = _cartRepository.GetByUser(userId);

            _cartRepository.IncrementGame(cart.Id, boardGameId);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult DecrementGame(int boardGameId)
        {
            var userId = _authServise.GetUserId();
            var cart = _cartRepository.GetByUser(userId);

            _cartRepository.DecrementGame(cart.Id, boardGameId);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteGame(int boardGameId)
        {
            var userId = _authServise.GetUserId();
            var cart = _cartRepository.GetByUser(userId);

            _cartRepository.DeleteGameFromCart(cart.Id, boardGameId);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteCart()
        {
            var userId = _authServise.GetUserId();
            var cart = _cartRepository.GetByUser(userId);

            _cartRepository.Delete(cart.Id);

            return RedirectToAction(nameof(Index));
        }
    }
}
