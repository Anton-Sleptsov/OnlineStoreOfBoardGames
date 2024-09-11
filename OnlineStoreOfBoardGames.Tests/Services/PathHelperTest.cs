using Microsoft.AspNetCore.Hosting;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using OnlineStoreOfBoardGames.Data.Model;
using OnlineStoreOfBoardGames.Services;

namespace OnlineStoreOfBoardGames.Tests.Services
{
    public class PathHelperTest
    {
        public const string FAKE_PROJECT_PATH = "C:\\project";

        private Mock<IWebHostEnvironment> _webHostEnvironmentMock;
        private PathHelper _pathHelper;

        [SetUp]
        public void SetUp()
        {
            _webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            _pathHelper = new PathHelper(_webHostEnvironmentMock.Object);
        }

        [Test]
        [TestCase(25, "C:\\project\\images\\BoardGame\\mainImage-25.jpg")]
        [TestCase(995, "C:\\project\\images\\BoardGame\\mainImage-995.jpg")]
        public void GetPathToBoardGameMainImage(int boardGameId, string resultPath)
        {
            // Prepare
            _webHostEnvironmentMock
                .Setup(x => x.WebRootPath)
                .Returns(FAKE_PROJECT_PATH);

            // Act
            var result = _pathHelper.GetPathToBoardGameMainImage(boardGameId);

            // Assert
            Assert.That(result, Is.EqualTo(resultPath));
        }

        [Test]
        public void GetPathToBoardGameSideImage()
        {
            // Prepare
            _webHostEnvironmentMock
                .Setup(x => x.WebRootPath)
                .Returns(FAKE_PROJECT_PATH);

            // Act
            var result = _pathHelper.GetPathToBoardGameSideImage(5);

            // Assert
            Assert.That(result, Is.EqualTo("C:\\project\\images\\BoardGame\\sideImage-5.jpg"));
        }
    }
}
