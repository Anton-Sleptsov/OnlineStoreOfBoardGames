using BoardGamesReviewsApi.Dtos;
using BoardGamesReviewsApi.Data.Models;

namespace BoardGamesReviewsApi.Mappers
{
    public class BoardGameReviewMapper
    {
        public DtoBoardGameReview BuildBoardGameReviewDto(BoardGameReview review)
            => new DtoBoardGameReview()
            {
                Id = review.Id,
                UserName = review.UserName,
                UserId = review.UserId,
                DateOfCreation = review.DateOfCreation,
                Text = review.Text,
                BoardGameId = review.BoardGameId
            };

        public BoardGameReview BuildBoardGameReviewFromCreate(DtoBoardGameReviewCreate reviewDto)
            => new BoardGameReview()
            {
                UserName = reviewDto.UserName,
                UserId = reviewDto.UserId,
                DateOfCreation = reviewDto.DateOfCreation,
                Text = reviewDto.Text,
                BoardGameId = reviewDto.BoardGameId
            };

        public BoardGameReview BuildBoardGameReviewFromUpdate(DtoBoardGameReviewUpdate reviewDto)
            => new BoardGameReview()
            {
                Id = reviewDto.Id,
                Text = reviewDto.Text,
            };
    }
}
