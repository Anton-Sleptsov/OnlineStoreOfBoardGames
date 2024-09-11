using Microsoft.AspNetCore.Mvc;
using OnlineStoreOfBoardGames.Data.Repositories;
using OnlineStoreOfBoardGames.Models.Auth;
using OnlineStoreOfBoardGames.Models.User;

namespace OnlineStoreOfBoardGames.Controllers.ApiControllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class AuthController : Controller
    {
        private UserRepository _userRepository;

        public AuthController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool IsLoginAvailable(string login)
        {
            // throw new Exception("Bad");
            return !_userRepository.Exist(login);
        }

        public ApiUserViewModel Login(string name, string password)
        {
            var user = _userRepository.GetByLoginAndPasswrod(name, password);
            
            return new ApiUserViewModel
            {
                Name = user.UserName,
                Roles = user.Role.ToString(),
                Permissions = user.Permission.ToString(), // "User,Admin,Moderator"
            };
        }

        public UserPermissionViewModel GetUserInfo(int userId)
        {
            var user = _userRepository.Get(userId);
            var viewModel = new UserPermissionViewModel
            {
                Id = userId,
                Name = user.UserName,
                Permission = user.Permission
            };
            return viewModel;
        }

    }
}
