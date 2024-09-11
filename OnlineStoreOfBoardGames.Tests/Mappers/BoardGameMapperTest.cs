using BoardGamesReviewsApi.Dtos;
using Moq;
using NUnit.Framework;
using OnlineStoreOfBoardGames.Data.Repositories.DataModel;
using OnlineStoreOfBoardGames.Data.Repositories.Interfaces;
using OnlineStoreOfBoardGames.Mappers;
using OnlineStoreOfBoardGames.Services.AuthStuff.Interfaces;
using OnlineStoreOfBoardGames.Services.Interfaces;

namespace OnlineStoreOfBoardGames.Tests.Mappers
{
    public class BoardGameMapperTest
    {
        private Mock<IAuthService> _authServiseMock;
        private Mock<IPathHelper> _pathHelperMock;
        private Mock<IBoardGameRepositories> _gameRepositoriesMock;
        private BoardGameMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _authServiseMock = new Mock<IAuthService>();
            _pathHelperMock = new Mock<IPathHelper>();
            _gameRepositoriesMock = new Mock<IBoardGameRepositories>();
            _mapper = new BoardGameMapper(_authServiseMock.Object, _pathHelperMock.Object, _gameRepositoriesMock.Object);
        }

        [Test]
        public void BuildFavoriteBoardGameIndexViewModel()
        {
            // Prepare
            var model = new Top3BoardGameDataModel() 
            { 
                Id = 10,
                Title = "Test board game",
                CountOfUserWhoLikeIt = 1
            };

            // Act
            var result = _mapper.BuildFavoriteBoardGameIndexViewModel(model);

            // Assert
            Assert.That(result.Id, Is.EqualTo(10));
            Assert.That(result.Title, Is.EqualTo("Test board game"));
            Assert.That(result.CountOfUserWhoLikeIt, Is.EqualTo(1));
        }

        [Test]
        public void BuildBoardGameUpdateRewievViewModel()
        {
            // Prepare
            var model = new DtoBoardGameReview()
            {
                BoardGameId = 10,
                Text = "Good board game"
            };

            _gameRepositoriesMock
                .Setup(x => x.GetName(model.BoardGameId))
                .Returns("Test board game");

            // Act
            var result = _mapper.BuildBoardGameUpdateRewievViewModel(model);

            // Assert
            Assert.That(result.Text, Is.EqualTo("Good board game"));
            Assert.That(result.BoardGameName, Is.EqualTo("Test board game"));
        }
    }
}
