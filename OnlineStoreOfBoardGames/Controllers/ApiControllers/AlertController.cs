using Microsoft.AspNetCore.Mvc;
using OnlineStoreOfBoardGames.Data.Model.Alerts;
using OnlineStoreOfBoardGames.Data.Repositories;
using OnlineStoreOfBoardGames.Data.Repositories.Interfaces;
using OnlineStoreOfBoardGames.Mappers;
using OnlineStoreOfBoardGames.Models.Alert;
using OnlineStoreOfBoardGames.Services.AuthStuff;

namespace OnlineStoreOfBoardGames.Controllers.ApiControllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class AlertController : Controller
    {
        private AuthService _authService;
        private IAlertRepository _alertRepository;
        private AlertMapper _alertMapper;

        public AlertController(AuthService authService, IAlertRepository alertRepository, AlertMapper alertMapper)
        {
            _authService = authService;
            _alertRepository = alertRepository;
            _alertMapper = alertMapper;
        }

        public void UserWasNottified(int alertId)
        {
            if (!_authService.IsAuthenticated())
            {
                return;
            }

            var userId = _authService.GetUserId();
            _alertRepository.UserWasNottified(userId, alertId);
        }

        public List<AlertShortInfoViewModel> GetAlertWhatIMiss()
        {
            if (!_authService.IsAuthenticated())
            {
                return new List<AlertShortInfoViewModel>();
            }

            var userId = _authService.GetUserId();
            var alerts = _alertRepository
                .GetAlertsForUser(userId)
                .Select(_alertMapper.MapToShortInfo)
                .ToList();

            return alerts;
        }
    }
}
