using OnlineStoreOfBoardGames.Data.Model.Alerts;
using OnlineStoreOfBoardGames.Models.Alert;

namespace OnlineStoreOfBoardGames.Mappers
{
    public class AlertMapper
    {
        public AlertCreateViewModel Map(Alert alert)
            => new AlertCreateViewModel
            {
                Id = alert.Id,
                Text = alert.Text,
                EndDate = alert.EndDate,
                IsActive = alert.IsActive,
            };

        public AlertIndexViewModel MapToIndex(Alert alert)
            => new AlertIndexViewModel
            {
                Id = alert.Id,
                Text = alert.Text,
                EndDate = alert.EndDate,
            };

        public Alert MapToDataBaseModel(AlertCreateViewModel viewModel)
            => new Alert
            {
                Id = viewModel.Id,
                Text = viewModel.Text,
                EndDate = viewModel.EndDate,
                IsActive = viewModel.IsActive,
            };

        public AlertShortInfoViewModel MapToShortInfo(Alert alert)
            => new AlertShortInfoViewModel
            {
                Id = alert.Id,
                Text = alert.Text,
                IsNewBoardGameAlert = alert.IsNewBoardGameAlert,
            };
    }
}
