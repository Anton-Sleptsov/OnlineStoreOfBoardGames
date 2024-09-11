using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using OnlineStoreOfBoardGames.LocalizationResources.BoardGame;

namespace OnlineStoreOfBoardGames.Models.ValidationAttributes
{
    public class ProductCodeAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                return ErrorMessage;
            }

            var defaultErrorMessage = BoardGame_CreateAndUpdateGame.ProductCode_ValidationErrorMessage;

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

            if (value is not long)
            {
                throw new ArgumentException($"Wrong type for validation. {nameof(ProductCodeAttribute)} can work only with long");
            }

            long valueInLong = (long)value;
            if (valueInLong < 10000 || valueInLong > 99999)
            {
                return false;
            }

            return true;
        }
    }
}