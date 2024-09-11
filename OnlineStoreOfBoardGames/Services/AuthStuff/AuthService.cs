using OnlineStoreOfBoardGames.Data.Enums;
using OnlineStoreOfBoardGames.Data.Model;
using OnlineStoreOfBoardGames.Data.Repositories.Interfaces;
using OnlineStoreOfBoardGames.Services.AuthStuff.Interfaces;

namespace OnlineStoreOfBoardGames.Services.AuthStuff
{
    public class AuthService : IAuthService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IUserRepository _userRepository;

        public AuthService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public bool IsAuthenticated()
            => _httpContextAccessor.HttpContext!.User.Identity?.IsAuthenticated ?? false;

        public User GetUser()
        {
            var userId = GetUserId();
            return _userRepository.Get(userId)!;
        }

        public string GetUserName()
            => GetClaimValue(AuthClaimsConstants.NAME);

        public UserRole GetUserRole()
        {
            var userRole = GetClaimValue(AuthClaimsConstants.ROLE);
            return Enum.Parse<UserRole>(userRole);
        }

        public Permission GetUserPermission()
        {
            var userRole = GetClaimValue(AuthClaimsConstants.PERMISSION);
            return Enum.Parse<Permission>(userRole);
        }

        public bool HasRoleOrHigher(UserRole role)
        {
            return GetUserRole() >= role;
        }

        public bool HasPermission(Permission permission)
        {
            return GetUserPermission().HasFlag(permission);
        }

        public bool HasRole(UserRole role)
        {
            return GetUserRole() == role;
        }

        public int GetUserId()
        {
            var userIdText = GetClaimValue(AuthClaimsConstants.ID);
            var userId = int.Parse(userIdText);
            return userId;
        }

        public Language GetUserLanguage()
        {
            var userLanguageText = GetClaimValue(AuthClaimsConstants.LANGUAGE);
            var userLanguage = Enum.Parse<Language>(userLanguageText);
            return userLanguage;
        }

        public bool IsAdmin()
        {
            return IsAuthenticated() && GetUserRole() == UserRole.Admin;
        }

        private string GetClaimValue(string claimType)
            => _httpContextAccessor
                .HttpContext!
                .User
                .Claims
                .First(x => x.Type == claimType)
                .Value;
    }
}
