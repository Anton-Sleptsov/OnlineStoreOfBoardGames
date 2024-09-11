using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using OnlineStoreOfBoardGames.Data.Enums;
using OnlineStoreOfBoardGames.Data.Model;
using OnlineStoreOfBoardGames.Data.Repositories;
using OnlineStoreOfBoardGames.Data.Repositories.Interfaces;
using OnlineStoreOfBoardGames.Services.AuthStuff;
using System.Security.Claims;

namespace OnlineStoreOfBoardGames.Tests.Services.AuthStuff
{
    public class AuthServiceTest
    {
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private Mock<IUserRepository> _userRepositoryMock;

        private AuthService _authService;

        [SetUp]
        public void Setup()
        {
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _userRepositoryMock = new Mock<IUserRepository>();

            _authService = new AuthService(
                _httpContextAccessorMock.Object,
                _userRepositoryMock.Object);
        }

        [Test]
        public void IsAuthenticated_UserAuthenticated()
        {
            // Prepare
            _httpContextAccessorMock
                .Setup(x => x.HttpContext.User.Identity.IsAuthenticated)
                .Returns(true);

            // Act
            var result = _authService.IsAuthenticated();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsAuthenticated_UserNotAuthenticated()
        {
            // Prepare
            _httpContextAccessorMock
                .Setup(x => x.HttpContext.User.Identity)
                .Returns<System.Security.Principal.IIdentity>(null);

            // Act
            var result = _authService.IsAuthenticated();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void IsAuthenticated_ThorwExeptionIfHttpContexIsNull()
        {
            // Prepare
            _httpContextAccessorMock
                .Setup(x => x.HttpContext)
                .Returns<HttpContext>(null);

            // Act
            // Assert
            Assert.Throws<NullReferenceException>(() =>
                _authService.IsAuthenticated());
        }

        [Test]
        [TestCase("1", 1)]
        [TestCase("77", 77)]
        public void GetUser_UserExist(string idStr, int id)
        {
            // Prepare
            var listClaims = new List<Claim> { new Claim("ID", idStr) };
            _httpContextAccessorMock
                .Setup(x => x.HttpContext.User.Claims)
                .Returns(listClaims);

            var userDbModel = new User();
            _userRepositoryMock.Setup(x => x.Get(id))
                .Returns(userDbModel);

            // Act
            var result = _authService.GetUser();

            // Assert
            Assert.That(result, Is.SameAs(userDbModel));
        }

        [Test]
        public void GetUser_ClaimsHasntId()
        {
            // Prepare
            var listClaims = new List<Claim> { new Claim("Smile", "Fun") };
            _httpContextAccessorMock
                .Setup(x => x.HttpContext.User.Claims)
                .Returns(listClaims);

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => _authService.GetUser());
        }

        [Test]
        [TestCase("q")]
        [TestCase("1.0")]
        public void GetUser_UserIdNotANumber(string idStr)
        {
            // Prepare
            var listClaims = new List<Claim> { new Claim("ID", idStr) };
            _httpContextAccessorMock
                .Setup(x => x.HttpContext.User.Claims)
                .Returns(listClaims);

            // Act
            // Assert
            Assert.Throws<FormatException>(() => _authService.GetUser());
        }

        [Test]
        [TestCase(true, "Admin", true)]
        [TestCase(false, "Admin", false)]
        [TestCase(true, "User", false)]
        [TestCase(false, "User", false)]
        public void IsAdmin(bool isAuthenticated, string roleStr, bool isAdminExpected)
        {
            // Prepare
            _httpContextAccessorMock
                .Setup(x => x.HttpContext.User.Identity.IsAuthenticated)
                .Returns(isAuthenticated);

            var listClaims = new List<Claim> { new Claim("ROLE", roleStr) };
            _httpContextAccessorMock
                .Setup(x => x.HttpContext.User.Claims)
                .Returns(listClaims);

            // Act
            var isAdminActual = _authService.IsAdmin();

            // Assert
            Assert.That(isAdminActual, Is.EqualTo(isAdminExpected));
        }

        [Test]
        [TestCase("User", "Moderator", false)]
        public void HasRoleOrHigher(string currentRole, UserRole role, bool isCurrentRoleEqualOrHigher)
        {
            // Prepare
            var listClaims = new List<Claim> { new Claim("ROLE", currentRole) };
            _httpContextAccessorMock
                .Setup(x => x.HttpContext.User.Claims)
                .Returns(listClaims);

            // Act
            var result = _authService.HasRoleOrHigher(role);

            // Assert
            Assert.That(result, Is.EqualTo(isCurrentRoleEqualOrHigher));
        }

        [Test]
        [TestCase("123hhh", "User")]
        [TestCase("Guest", "User")]
        public void HasRoleOrHigher_ClaimsHasntRole(string currentRole, UserRole role)
        {
            // Prepare
            var listClaims = new List<Claim> { new Claim("ROLE", currentRole) };
            _httpContextAccessorMock
                .Setup(x => x.HttpContext.User.Claims)
                .Returns(listClaims);

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => _authService.HasRoleOrHigher(role));
        }
    }
}
