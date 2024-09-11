using OnlineStoreOfBoardGames.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using OnlineStoreOfBoardGames.LocalizationResources.BoardGame;

namespace OnlineStoreOfBoardGames.Models.BoardGameReview
{
    public class BoardGameCreateReviewViewModel
    {
        public int BoardGameId { get; set; }
        public string BoardGameName { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(BoardGame_CreateAndUpdateReview),
            ErrorMessageResourceName = nameof(BoardGame_CreateAndUpdateReview.RequiredText_ErrorMessage))]
        [TextInput(10, 500,
            ErrorMessageResourceType = typeof(BoardGame_UniversalAttributes),
            ErrorMessageResourceNameFew = nameof(BoardGame_UniversalAttributes.TextInput_ValidationErrorMessageFew),
            ErrorMessageResourceNameMany = nameof(BoardGame_UniversalAttributes.TextInput_ValidationErrorMessageMany),
            ResourceNameSymbolFirstForm = nameof(BoardGame_UniversalAttributes.TextInput_SymbolEndingFirstForm),
            ResourceNameSymbolSecondForm = nameof(BoardGame_UniversalAttributes.TextInput_SymbolEndingSecondForm))]
        [Display(
            ResourceType = typeof(BoardGame_CreateAndUpdateReview),
            Name = nameof(BoardGame_CreateAndUpdateReview.DisplayText_Name))]
        public string Text { get; set; }
    }
}
