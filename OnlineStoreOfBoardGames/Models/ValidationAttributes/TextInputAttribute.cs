using System.ComponentModel.DataAnnotations;
using OnlineStoreOfBoardGames.LocalizationResources.BoardGame;

namespace OnlineStoreOfBoardGames.Models.ValidationAttributes
{
    public class TextInputAttribute : ValidationAttribute
    {
        public string ErrorMessageResourceNameFew { get; set; }
        public string ErrorMessageResourceNameMany { get; set; }
        public string ResourceNameSymbolFirstForm { get; set; }
        public string ResourceNameSymbolSecondForm { get; set; }

        private readonly int _minLenght;
        private readonly int _maxLenght;
        private string _errorMessageTemplate;
        private bool _isFew;

        public TextInputAttribute(int minLenght, int maxLenght)
        {
            _minLenght = minLenght;
            _maxLenght = maxLenght;
        }

        public TextInputAttribute(int minLenght)
        {
            _minLenght = minLenght;
            _maxLenght = int.MaxValue;
        }

        public override string FormatErrorMessage(string name)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                return ErrorMessage;
            }

            var errorMessageWithFormat = string.Empty;

            if (_errorMessageTemplate is not null)
            {
                var countOfSymbols = _isFew ? _minLenght : _maxLenght;
                string symbolWithCorrectEnding = GetlWithCorrectEnding(countOfSymbols);

                errorMessageWithFormat = string.Format(_errorMessageTemplate, name, countOfSymbols, symbolWithCorrectEnding);
            }

            return errorMessageWithFormat;
        }

        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return false;
            }

            string valueInString = (string)value;

            if (valueInString.Length < _minLenght)
            {
                _isFew = true;
                if (ErrorMessageResourceType is not null
                    && ErrorMessageResourceNameFew is not null)
                {                   
                    var property = ErrorMessageResourceType.GetProperty(ErrorMessageResourceNameFew);
                    var valueOfProperty = property!.GetValue(null);
                    _errorMessageTemplate = (string)valueOfProperty!;
                }
                else
                {
                    _errorMessageTemplate = BoardGame_UniversalAttributes.TextInput_ValidationErrorMessageFew;
                }
                return false;
            }

            if (valueInString.Length > _maxLenght)
            {
                _isFew = false;
                if (ErrorMessageResourceType is not null
                   && ErrorMessageResourceNameMany is not null)
                {
                    var property = ErrorMessageResourceType.GetProperty(ErrorMessageResourceNameMany);
                    var valueOfProperty = property!.GetValue(null);
                    _errorMessageTemplate = (string)valueOfProperty!;
                }
                else
                {
                    _errorMessageTemplate = BoardGame_UniversalAttributes.TextInput_ValidationErrorMessageMany;
                }
                return false;
            }

            return true;
        }

        private string GetlWithCorrectEnding(int number)
        {
            string symbolEndingFirstForm;
            string symbolEndingSecondForm;

            if (ErrorMessageResourceType is not null 
                && ResourceNameSymbolFirstForm is not null
                && ResourceNameSymbolSecondForm is not null)
            {
                symbolEndingFirstForm = (string)ErrorMessageResourceType.GetProperty(ResourceNameSymbolFirstForm)!.GetValue(null)!;
                symbolEndingSecondForm = (string)ErrorMessageResourceType.GetProperty(ResourceNameSymbolSecondForm)!.GetValue(null)!;
            }
            else
            {
                symbolEndingFirstForm = BoardGame_UniversalAttributes.TextInput_SymbolEndingFirstForm;
                symbolEndingSecondForm = BoardGame_UniversalAttributes.TextInput_SymbolEndingSecondForm;
            }

            int lastDigit = number % 10;
            int lastTwoDigit = number % 100;

            if (lastDigit == 1 && lastTwoDigit != 11)
            {
                return symbolEndingFirstForm;
            }
            else
            {
                return symbolEndingSecondForm;
            }
        }
    }
}
