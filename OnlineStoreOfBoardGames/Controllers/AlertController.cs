using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OnlineStoreOfBoardGames.Controllers.ActionFilterAttributes;
using OnlineStoreOfBoardGames.Data.Repositories.Interfaces;
using OnlineStoreOfBoardGames.Hubs;
using OnlineStoreOfBoardGames.Mappers;
using OnlineStoreOfBoardGames.Models.Alert;
using OnlineStoreOfBoardGames.Services;
using OnlineStoreOfBoardGames.Data.Enums;
using Microsoft.AspNetCore.Authorization;
using OnlineStoreOfBoardGames.Services.AuthStuff.Interfaces;

namespace OnlineStoreOfBoardGames.Controllers
{
    [Authorize]
    public class AlertController : Controller
    {
        private readonly IAlertRepository _alertRepository;
        private readonly AlertMapper _alertMapper;
        public IHubContext<AlertHub, IAlertHub> _alertHub;
        public LocalizatoinService _localizatoinService;
        private readonly IAuthService _authServise;

        public AlertController(IAlertRepository alertRepository,
            AlertMapper alertMapper,
            IHubContext<AlertHub, IAlertHub> alertHub,
            LocalizatoinService localizatoinService,
            IAuthService authServise)
        {
            _alertRepository = alertRepository;
            _alertMapper = alertMapper;
            _alertHub = alertHub;
            _localizatoinService = localizatoinService;
            _authServise = authServise;
        }

        public IActionResult Index()
        {
            var alerts = _alertRepository.GetAll();
            var alertViewModels = new List<AlertIndexViewModel>();

            foreach (var alert in alerts)
            {
                var alertViewModel = _alertMapper.MapToIndex(alert);
                if (alert.IsNewBoardGameAlert)
                {
                    alertViewModel.Text = _localizatoinService.GetLocalizedNewBoardGameAlert(alert.Text);
                }
                alertViewModels.Add(alertViewModel);
            }

            var alertsViewModel = new AlertsIndexViewModel
            {
                CanCreateAndDelete = _authServise.HasPermission(Permission.CanCreateAndDeleteAlerts),
                Alerts = alertViewModels
            };

            return View(alertsViewModel);
        }

        [HasPermission(Permission.CanCreateAndDeleteAlerts)]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HasPermission(Permission.CanCreateAndDeleteAlerts)]
        [HttpPost]
        public async Task<IActionResult> Create(AlertCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var alert = _alertMapper.MapToDataBaseModel(viewModel);

            if(alert.EndDate is not null)
            {
                alert.EndDate = alert.EndDate.Value.ToUniversalTime();
            }
            _alertRepository.Create(alert);

            await _alertHub.Clients.All.AlertWasCreatedAsync(alert.Id, alert.Text);

            return RedirectToAction(nameof(Index));
        }

        [HasPermission(Permission.CanCreateAndDeleteAlerts)]
        public IActionResult Delete(int id)
        {
            _alertRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
