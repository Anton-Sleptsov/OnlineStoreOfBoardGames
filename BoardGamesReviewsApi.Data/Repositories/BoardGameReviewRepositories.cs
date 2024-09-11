using BoardGamesReviewsApi.Data.Models;

namespace BoardGamesReviewsApi.Data.Repositories
{
    public class BoardGameReviewRepositories
    {
        private ReviewsDbContext _dbContext;

        public BoardGameReviewRepositories(ReviewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BoardGameReview Get(int id) => _dbContext.BoardGameReviews.First(review => review.Id == id);     

        public void Create(BoardGameReview review)
        {
            _dbContext.Add(review);
            _dbContext.SaveChanges();
        }

        public List<BoardGameReview> GetAllForGame(int gameId)
        {
            return _dbContext
                .BoardGameReviews
                .Where(review => review.BoardGameId == gameId)
                .ToList();
        }

        public void Update(BoardGameReview review)
        {       
            BoardGameReview updatedReview = Get(review.Id);
            updatedReview.Text = review.Text;

            _dbContext.SaveChanges();
        }

        public bool Delete(int id)
        {
            try
            {
                var review = _dbContext
                    .BoardGameReviews
                    .First(review => review.Id == id);

                _dbContext.BoardGameReviews.Remove(review);
                _dbContext.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
