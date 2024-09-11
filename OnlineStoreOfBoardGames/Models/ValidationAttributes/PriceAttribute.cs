using OnlineStoreOfBoardGames.LocalizationResources.BoardGame;
using System.ComponentModel.DataAnnotations;

namespace OnlineStoreOfBoardGames.Models.ValidationAttributes
{
    public class PriceAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                return ErrorMessage;
            }

            var defaultErrorMessage = BoardGame_CreateAndUpdateGame.Price_ValidationErrorMessage;

            if (ErrorMessageResourceType is not null
                && ErrorMessageResourceName is not null)
            {
                var property = ErrorMessageResourceType.GetProperty(ErrorMessageResourceName);
                var value = property!.GetValue(null);
                defaultErrorMessage = (string)value!;
            }

            return defaultErrorMessage;
        }

        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return false;
            }

            if (value is not double)
            {
                throw new ArgumentException($"Wrong type for validation. {nameof(PriceAttribute)} can work only with double");
            }

            double valueInDouble = (double)value;
            if (valueInDouble <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
