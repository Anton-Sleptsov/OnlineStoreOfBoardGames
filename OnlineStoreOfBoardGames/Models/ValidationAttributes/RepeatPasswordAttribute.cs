using OnlineStoreOfBoardGames.Models.Auth;
using System.ComponentModel.DataAnnotations;

namespace OnlineStoreOfBoardGames.Models.ValidationAttributes
{
    public class RepeatPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var viewModel = validationContext.ObjectInstance as RegisrationViewModel;
            if (viewModel == null)
            {
                throw new Exception($"{nameof(RepeatPasswordAttribute)} can be part only {nameof(RegisrationViewModel)}");
            }

            if (viewModel.Password == viewModel.RepeatPassword)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(
                "Пароли не совпрадают",
                new List<string> { nameof(RegisrationViewModel.Password), nameof(RegisrationViewModel.RepeatPassword) }
                );
        }
    }
}
