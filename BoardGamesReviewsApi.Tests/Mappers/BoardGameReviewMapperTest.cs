using BoardGamesReviewsApi.Mappers;
using BoardGamesReviewsApi.Data.Models;
using NUnit.Framework;

namespace BoardGamesReviewsApi.Tests.Mappers
{
    public class BoardGameReviewMapperTest
    {
        [Test]
        public void BuildBoardGameReviewDto()
        {
            // Prepare
            var mapper = new BoardGameReviewMapper();

            var model = new BoardGameReview()
            {
                Id = 10,
                UserName = "test",
                UserId = 15,
                DateOfCreation = new DateTime(2024, 07, 01, 15, 43, 50),
                Text = "Good game",
                BoardGameId = 20
            };

            // Act
            var result = mapper.BuildBoardGameReviewDto(model);

            // Assert
            Assert.That(result.Id, Is.EqualTo(10));
            Assert.That(result.UserName, Is.EqualTo("test"));
            Assert.That(result.UserId, Is.EqualTo(15));
            Assert.That(result.DateOfCreation.Day, Is.EqualTo(1));
            Assert.That(result.Text, Is.EqualTo("Good game"));
            Assert.That(result.BoardGameId, Is.EqualTo(20));
        }
    }
}
