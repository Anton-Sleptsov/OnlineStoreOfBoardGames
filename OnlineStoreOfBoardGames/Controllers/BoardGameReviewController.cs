using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreOfBoardGames.Mappers;
using OnlineStoreOfBoardGames.Models.BoardGameReview;
using OnlineStoreOfBoardGames.Services.Apis;
using OnlineStoreOfBoardGames.Data.Repositories.Interfaces;
using OnlineStoreOfBoardGames.Services.AuthStuff.Interfaces;

namespace OnlineStoreOfBoardGames.Controllers
{
    [Authorize]
    public class BoardGameReviewController : Controller
    {
        private readonly IBoardGameRepositories _gameRepositories;
        private readonly BoardGameMapper _mapper;
        private readonly HttpBoardGamesReviewsApiService _httpService;
        private readonly IAuthService _authService;

        public BoardGameReviewController(IBoardGameRepositories gameRepositories,
            BoardGameMapper mapper,
            HttpBoardGamesReviewsApiService httpService,
            IAuthService authService)
        {
            _gameRepositories = gameRepositories;
            _mapper = mapper;
            _httpService = httpService;
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Create(int gameId)
        {
            BoardGameCreateReviewViewModel createReviewViewModel =
            new BoardGameCreateReviewViewModel
                {
                    BoardGameId = gameId,
                    BoardGameName = _gameRepositories.Get(gameId)!.Title,
                };

            return View(createReviewViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BoardGameCreateReviewViewModel boardGameReviewViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(boardGameReviewViewModel);
            }

            var review = _mapper.BuildBoardGameRewievDataModelFromCreate(boardGameReviewViewModel);
            review.BoardGameId = boardGameReviewViewModel.BoardGameId;
            await _httpService.CreateReviewAsync(review);

            return RedirectToAction(nameof(BoardGameController.BoardGame), nameof(BoardGameController)[..^"Controller".Length], new { id = review.BoardGameId });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id, int gameId)
        {          
            var reviewForUpdate = await _httpService.GetReviewAsync(id);

            if (_authService.GetUserId() != reviewForUpdate.UserId)
            {
                return RedirectToAction(nameof(AuthController.AccessDenied), nameof(AuthController)[..^"Controller".Length]);
            } 

            var viewModel = _mapper.BuildBoardGameUpdateRewievViewModel(reviewForUpdate);
            viewModel.BoardGameId = gameId;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(BoardGameUpdateReviewViewModel boardGameReviewViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(boardGameReviewViewModel);
            }

            var review = await _httpService.GetReviewAsync(boardGameReviewViewModel.Id);

            if (_authService.GetUserId() != review.UserId)
            {
                return RedirectToAction(nameof(AuthController.AccessDenied), nameof(AuthController)[..^"Controller".Length]);
            }

            var updatedReview = _mapper.BuildBoardGameRewievDataModelFromUpdate(boardGameReviewViewModel);
            await _httpService.UpdateReviewAsync(updatedReview);

            return RedirectToAction(nameof(BoardGameController.BoardGame), nameof(BoardGameController)[..^"Controller".Length], new { id = boardGameReviewViewModel.BoardGameId });
        }     
    }
}
