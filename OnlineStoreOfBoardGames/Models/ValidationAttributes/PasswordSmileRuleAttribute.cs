using System.ComponentModel.DataAnnotations;

namespace OnlineStoreOfBoardGames.Models.ValidationAttributes
{
    public class PasswordSmileRuleAttribute : ValidationAttribute
    {
        private string _defaultErrorMessage;
        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? _defaultErrorMessage
                : ErrorMessage;
        }

        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                _defaultErrorMessage = "Ты бы хоть что-то написал в пароль";
                return false;
            }

            var password = (string)value;
            if (password.Length > 3)
            {
                _defaultErrorMessage = "Слишком сложный пароль. Признайся, ты его запишешь, вместо того что бы запомнить";
                return false;
            }

            if (!int.TryParse(password, out int numbers))
            {
                _defaultErrorMessage = "Ещё проще. Давай только цифры";
                return false;
            }

            if (numbers < 0)
            {
                _defaultErrorMessage = "И зачем тебе отрицательные цифры?";
                return false;
            }

            return true;
        }
    }
}
