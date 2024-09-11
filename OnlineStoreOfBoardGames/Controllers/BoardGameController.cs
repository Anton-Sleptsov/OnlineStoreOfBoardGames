using Microsoft.AspNetCore.Mvc;
using OnlineStoreOfBoardGames.Models.BoardGame;
using OnlineStoreOfBoardGames.Data.Model;
using Microsoft.AspNetCore.Authorization;
using OnlineStoreOfBoardGames.Controllers.ActionFilterAttributes;
using OnlineStoreOfBoardGames.Data.Enums;
using OnlineStoreOfBoardGames.Mappers;
using System.Reflection;
using OnlineStoreOfBoardGames.Services.Apis;
using OnlineStoreOfBoardGames.Data.Model.Alerts;
using Microsoft.AspNetCore.SignalR;
using OnlineStoreOfBoardGames.Hubs;
using OnlineStoreOfBoardGames.Services.Interfaces;
using OnlineStoreOfBoardGames.Services.AuthStuff.Interfaces;
using OnlineStoreOfBoardGames.Data.Repositories.Interfaces;
using OnlineStoreOfBoardGames.Data.CacheServices;

namespace OnlineStoreOfBoardGames.Controllers
{
    [Authorize]
    public class BoardGameController : Controller
    {
        private readonly IBoardGameRepositories _gameRepositories;
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authServise;
        private readonly IPathHelper _pathHelper;
        private readonly BoardGameMapper _mapper;
        private readonly HttpBoardGameOfDayServise _boardGameOfDayServise;
        private readonly HttpBestBoardGameServise _bestBoardGameServise;
        private readonly IAlertRepository _alertRepository;
        private readonly BoardGameCache _cache;
        private readonly IHubContext<AlertHub, IAlertHub> _alertHub;

        public BoardGameController(IBoardGameRepositories gameRepositories,
            IUserRepository userRepository,
            IAuthService authService,
            IPathHelper pathHelper,
            BoardGameMapper mapper,
            HttpBoardGameOfDayServise boardGameOfDayServise,
            HttpBestBoardGameServise bestBoardGameServise,
            IAlertRepository alertRepository,
            IHubContext<AlertHub, IAlertHub> alertHub,
            BoardGameCache cache)
        {
            _gameRepositories = gameRepositories;
            _userRepository = userRepository;
            _authServise = authService;
            _pathHelper = pathHelper;
            _mapper = mapper;
            _boardGameOfDayServise = boardGameOfDayServise;
            _bestBoardGameServise = bestBoardGameServise;
            _alertRepository = alertRepository;
            _alertHub = alertHub;
            _cache = cache;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var top3BoardGames = _gameRepositories
                .GetTop3()
                .Select(_mapper.BuildFavoriteBoardGameIndexViewModel)
                .ToList();

            var gamesViewModel = _cache
                .GetBoardGames()
                .Select(_mapper.BuildBoardGameIndexViewModel)
                .ToList();

            var canCreateAndUpdate = false;
            var canDelete = false;
            if (_authServise.IsAuthenticated())
            {
                canCreateAndUpdate = _authServise.HasPermission(Permission.CanCreateAndUpdateBoardGames);
                canDelete = _authServise.HasPermission(Permission.CanDeleteBoardGames);
            }

            var indexViewModel = new IndexViewModel()
            {
                BoardGames = gamesViewModel,
                CanCreateAndUpdateBoardGames = canCreateAndUpdate,
                CanDeleteBoardGames = canDelete,
                Top3FavoriteBoardGames = top3BoardGames,
            };

            return View(indexViewModel);
        }

        [HttpGet]
        [HasPermission(Permission.CanCreateAndUpdateBoardGames)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [HasPermission(Permission.CanCreateAndUpdateBoardGames)]
        public async Task<IActionResult> Create(BoardGameCreateViewModel boardGameViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(boardGameViewModel);
            }

            BoardGame game = _mapper.BuildBoardGameDataModelFromCreate(boardGameViewModel);

            _gameRepositories.Create(game);

            var pathToMainImage = _pathHelper.GetPathToBoardGameMainImage(game.Id);
            using (var fs = new FileStream(pathToMainImage, FileMode.Create))
            {
                boardGameViewModel.MainImage.CopyTo(fs);
            }

            if (boardGameViewModel.SideImage is not null)
            {
                var pathToSideImage = _pathHelper.GetPathToBoardGameSideImage(game.Id);
                using (var fs = new FileStream(pathToSideImage, FileMode.Create))
                {
                    boardGameViewModel.SideImage.CopyTo(fs);
                }
            }

            var alert = new Alert()
            {
                Text = game.Title,
                EndDate = DateTime.UtcNow.AddDays(3),
                IsNewBoardGameAlert = true
            };
            _alertRepository.Create(alert);
            _cache.ResetCache();

            await _alertHub.Clients.All.NewBoardGameAlertWasCreatedAsync(alert.Id, alert.Text);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [HasPermission(Permission.CanCreateAndUpdateBoardGames)]
        public IActionResult Update(int id)
        {
            BoardGame boardGameForUpdate = _gameRepositories.Get(id);
            BoardGameUpdateViewModel viewModel = _mapper.BuildBoardGameUpdateDataModel(boardGameForUpdate);

            return View(viewModel);
        }

        [HttpPost]
        [HasPermission(Permission.CanCreateAndUpdateBoardGames)]
        public IActionResult Update(BoardGameUpdateViewModel boardGameViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(boardGameViewModel);
            }

            BoardGame updatedReview = _mapper.BuildBoardGameDataModelFromUpdate(boardGameViewModel);
            _gameRepositories.Update(updatedReview);

            var pathToMainImage = _pathHelper.GetPathToBoardGameMainImage(boardGameViewModel.Id);
            System.IO.File.Delete(pathToMainImage);
            using (var fs = new FileStream(pathToMainImage, FileMode.Create))
            {
                boardGameViewModel.MainImage.CopyTo(fs);
            }

            if (_pathHelper.IsBoardGameSideImageExist(boardGameViewModel.Id))
            {
                var pathToSideImage = _pathHelper.GetPathToBoardGameSideImage(boardGameViewModel.Id);
                System.IO.File.Delete(pathToSideImage);
            }

            if (boardGameViewModel.SideImage is not null)
            {
                var pathToSideImage = _pathHelper.GetPathToBoardGameSideImage(boardGameViewModel.Id);
                using (var fs = new FileStream(pathToSideImage, FileMode.Create))
                {
                    boardGameViewModel.SideImage.CopyTo(fs);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> BoardGame(int id)
        {
            var boardGame = _gameRepositories.Get(id)!;
            var viewModel = _mapper.BuildBoardGameViewModel(boardGame);

            if (_authServise.IsAuthenticated())
            {
                int userId = _authServise.GetUserId();

                User user = _userRepository.GetWithFavoriteBoardGames(userId);
                if (user.FavoriteBoardsGames.Any(boardGame => boardGame.Id == id))
                {
                    viewModel.IsFavoriteForUser = true;
                }
            }

            var boardGameOfDayTask = _boardGameOfDayServise.GetBoardGameOfDayAsync();
            var bestBoardGameTask = _bestBoardGameServise.GetBestBoardGameAsync();

            await Task.WhenAll(boardGameOfDayTask, bestBoardGameTask);

            if (boardGameOfDayTask.Result.IsSuccess)
            {
                var boardGameOfDayViewModel = _mapper.BuildBoardGameOfDayViewModel(boardGameOfDayTask.Result.Data);
                viewModel.BoardGameOfDay = boardGameOfDayViewModel;
            }

            if (bestBoardGameTask.Result.IsSuccess)
            {
                var bestBoardGameViewModel = _mapper.BuildBestBoardGameViewModel(bestBoardGameTask.Result.Data);
                viewModel.BestBoardGame = bestBoardGameViewModel;
            }

            return View(viewModel);
        }

        public IActionResult UserFavoriteBoardGames()
        {
            string userName = _authServise.GetUserName();
            int userId = _authServise.GetUserId();

            List<BoardGame> favoriteBoardGames = _gameRepositories.GetFavoriteBoardGamesForUser(userId);

            UserFavoriteBoardGamesViewModel viewModel = new()
            {
                Name = userName,
                FavoriteBoardGames = favoriteBoardGames.Select(_mapper.BuildBoardGameViewModel).ToList()
            };

            return View(viewModel);
        }

        public IActionResult ApiMethods()
        {
            List<ApiMethodViewModel> viewModel = new();

            var apiType = typeof(ApiControllers.BoardGameController);
            var apiMethods = apiType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var method in apiMethods)
            {
                List<ParameterViewModel> parametersViewModel = new();
                var parameters = method.GetParameters();
                foreach (var parameter in parameters)
                {
                    var parameterViewModel = new ParameterViewModel { ParameterTitle = parameter.Name, ParameterTypeName = GetTypeName(parameter.ParameterType) };
                    parametersViewModel.Add(parameterViewModel);
                }

                var methodViewModel = new ApiMethodViewModel { Name = method.Name, ReturnTypeName = GetTypeName(method.ReturnType), Parameters = parametersViewModel };

                viewModel.Add(methodViewModel);
            }

            return View(viewModel);
        }

        private string GetTypeName(Type type)
        {
            if (!type.IsGenericType)
            {
                return type.Name;
            }

            var genericType = type.GetGenericTypeDefinition();
            var genericArguments = type.GetGenericArguments();
            var genericTypeName = genericType.Name.Split('`')[0];
            var argumentTypeNames = string.Join(", ", genericArguments.Select(GetTypeName));
            return $"{genericTypeName}<{argumentTypeNames}>";
        }
    }
}
