using OnlineStoreOfBoardGames.Data.Model;
using OnlineStoreOfBoardGames.Models.BoardGame;
using OnlineStoreOfBoardGames.Services;
using OnlineStoreOfBoardGames.Models.BoardGameReview;
using OnlineStoreOfBoardGames.Data.Repositories.DataModel;
using BoardGamesReviewsApi.Dtos;
using OnlineStoreOfBoardGames.Data.Repositories.Interfaces;
using OnlineStoreOfBoardGames.Services.AuthStuff.Interfaces;
using OnlineStoreOfBoardGames.Services.Interfaces;
using BoardGameOfDayApi.Dtos;
using BestBoardGameApi.Dtos;
using OnlineStoreOfBoardGames.Data.Enums;

namespace OnlineStoreOfBoardGames.Mappers
{
    public class BoardGameMapper
    {
        private readonly IAuthService _authServise;
        private readonly IPathHelper _pathHelper;
        private readonly IBoardGameRepositories _gameRepositories;

        public BoardGameMapper(IAuthService authService, IPathHelper pathHelper, IBoardGameRepositories gameRepositories)
        {
            _authServise = authService;
            _pathHelper = pathHelper;
            _gameRepositories = gameRepositories;
        }

        #region BoardGameBuilders
        public BoardGameViewModel BuildBoardGameViewModel(BoardGame game)
        => new BoardGameViewModel
        {
            Id = game.Id,
            Title = game.Title,
            MiniTitle = game.MiniTitle,
            HasMainImage = _pathHelper.IsBoardGameMainImageExist(game.Id),
            HasSideImage = _pathHelper.IsBoardGameSideImageExist(game.Id),
            Description = game.Description,
            Essence = game.Essence,
            Tags = game.Tags,
            Price = game.Price,
            ProductCode = game.ProductCode
        };

        public BoardGame BuildBoardGameDataModelFromCreate(BoardGameCreateViewModel gameViewModel)
            => new BoardGame
            {
                Title = gameViewModel.Title,
                MiniTitle = gameViewModel.MiniTitle,
                Description = gameViewModel.Description,
                Essence = gameViewModel.Essence,
                Tags = gameViewModel.Tags,
                Price = gameViewModel.Price.Value,
                ProductCode = gameViewModel.ProductCode.Value,
            };

        public BoardGame BuildBoardGameDataModelFromCreateReact(BoardGameCreateViewModelReact gameViewModel)
            => new BoardGame
            {
                Title = gameViewModel.Title,
                MiniTitle = gameViewModel.MiniTitle,
                Description = gameViewModel.Description,
                Essence = gameViewModel.Essence,
                Tags = gameViewModel.Tags,
                Price = gameViewModel.Price.Value,
                ProductCode = gameViewModel.ProductCode.Value,
            };

        public BoardGameUpdateViewModel BuildBoardGameUpdateDataModel(BoardGame game)
            => new BoardGameUpdateViewModel
            {
                OriginalTitle = game.Title,
                Title = game.Title,
                MiniTitle = game.MiniTitle,
                Description = game.Description,
                Essence = game.Essence,
                Tags = game.Tags,
                Price = game.Price,
                ProductCode = game.ProductCode,
            };

        public BoardGame BuildBoardGameDataModelFromUpdate(BoardGameUpdateViewModel gameViewModel)
             => new BoardGame
             {
                 Id = gameViewModel.Id,
                 Title = gameViewModel.Title,
                 MiniTitle = gameViewModel.MiniTitle,
                 Description = gameViewModel.Description,
                 Essence = gameViewModel.Essence,
                 Tags = gameViewModel.Tags,
                 Price = gameViewModel.Price.Value,
                 ProductCode = gameViewModel.ProductCode.Value,
             };

        public BoardGameIndexViewModel BuildBoardGameIndexViewModel(BoardGame game)
            => new BoardGameIndexViewModel
            {
                Id = game.Id,
                Title = game.Title,
            };

        public FavoriteBoardGameIndexViewModel BuildFavoriteBoardGameIndexViewModel(Top3BoardGameDataModel game)
            => new FavoriteBoardGameIndexViewModel
            {
                Id = game.Id,
                Title = game.Title,
                CountOfUserWhoLikeIt = game.CountOfUserWhoLikeIt,
                Rank = game.Rank,
            };

        public BoardGameOfDayViewModel BuildBoardGameOfDayViewModel(DtoBoardGameOfDay game)
            => new BoardGameOfDayViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Price = game.Price,
                HasMainImage = _pathHelper.IsBoardGameMainImageExist(game.Id),
            };

        public BestBoardGameViewModel BuildBestBoardGameViewModel(DtoBestBoardGame game)
            => new BestBoardGameViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Price = game.Price,
                CountOfUserWhoLikeIt = game.CountOfUserWhoLikeIt,
                HasMainImage = _pathHelper.IsBoardGameMainImageExist(game.Id),
            };
        #endregion

        #region ReviewBuilders
        public BoardGameReviewViewModel BuildBoardGameReviewViewModel(DtoBoardGameReview review, User currentUser, bool isModerator)
            => new BoardGameReviewViewModel
            {
                Id = review.Id,
                UserId = review.UserId,
                UserName = review.UserName,
                DateOfCreation = review.DateOfCreation,
                Text = review.Text,
                CanEdit = review.UserId == currentUser?.Id,
                CanDelete = review.UserId == currentUser?.Id || isModerator,
            };

        public DtoBoardGameReviewCreate BuildBoardGameRewievDataModelFromCreate(BoardGameCreateReviewViewModel reviewViewModel)
            => new DtoBoardGameReviewCreate
            {
                UserName = _authServise.GetUserName(),
                UserId = _authServise.GetUserId(),
                DateOfCreation = DateTime.UtcNow,
                Text = reviewViewModel.Text,
            };

        public DtoBoardGameReviewUpdate BuildBoardGameRewievDataModelFromUpdate(BoardGameUpdateReviewViewModel reviewViewModel)
            => new DtoBoardGameReviewUpdate
            {
                Id = reviewViewModel.Id,
                Text = reviewViewModel.Text,
            };

        public BoardGameUpdateReviewViewModel BuildBoardGameUpdateRewievViewModel(DtoBoardGameReview review)
            => new BoardGameUpdateReviewViewModel
            {
                BoardGameName = _gameRepositories.GetName(review.BoardGameId),
                Text = review.Text,
            };
        #endregion
    }
}
