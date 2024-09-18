using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreOfBoardGames.Controllers.ActionFilterAttributes;
using OnlineStoreOfBoardGames.Data.Enums;
using OnlineStoreOfBoardGames.Data.Model;
using OnlineStoreOfBoardGames.Data.Repositories;
using OnlineStoreOfBoardGames.Models.User;
using OnlineStoreOfBoardGames.Services.AuthStuff.Interfaces;

namespace OnlineStoreOfBoardGames.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly IAuthService _authService;

        public UserController(UserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        [HasPermission(Permission.CanViewPremission)]
        public IActionResult Index()
        {
            var userViewModel = _userRepository
                .GetAll()
                .Select(BuildUserViewModel)
                .ToList();

            var viewModel = new IndexViewModel
            {
                Users = userViewModel,
                AvailablePermissions = Enum
                    .GetValues<Permission>()
                    .Where(x => x > 0)
                    .ToList(),
                CanEditPermissions = _authService.HasPermission(Permission.CanEditPremission)
            };

            return View(viewModel);
        }

        [HasPermission(Permission.CanEditPremission)]
        [HttpPost]
        public IActionResult UpdatePermissions(int userId)
        {
            var userPermission = Permission.None;
            var availablePermissions = Enum.GetValues<Permission>().ToList();
            foreach (var permission in availablePermissions)
            {
                var formData = Request.Form[$"permissions[{(int)permission}]"];
                if (formData.Any())
                {
                    userPermission |= permission;
                }
            }

            _userRepository.UpdatePermission(userId, userPermission);

            return RedirectToAction("Index");
        }

        private UserPermissionViewModel BuildUserViewModel(User user)
        {
            return new UserPermissionViewModel
            {
                Id = user.Id,
                Name = user.UserName,
                Permission = user.Permission,
            };
        }
    }
}
