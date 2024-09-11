using Microsoft.AspNetCore.Mvc;
using OnlineStoreOfBoardGames.Data.Enums;
using OnlineStoreOfBoardGames.Data.Model;
using OnlineStoreOfBoardGames.Data.Repositories;
using OnlineStoreOfBoardGames.Models.User;

namespace OnlineStoreOfBoardGames.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

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
                    .ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UpdatePermissions(int userId, List<int> permissions)
        {
            var userPermission = Permission.None;
            var availablePermissions = Enum.GetValues<Permission>().ToList();
            foreach (var permission in availablePermissions)// 1 2 4 8
            {
                var formData = Request.Form[$"permissions[{(int)permission}]"];//permissions[2]
                if (formData.Any())
                {
                    userPermission = userPermission | permission;
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
