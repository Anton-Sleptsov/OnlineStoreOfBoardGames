using OpenQA.Selenium;

namespace OnlineStoreOfBoardGames.Tests.E2E.PagesSelectors
{
    public class BoardGameCreatePage
    {
        public static By TitleInput = By.CssSelector("#Title");
        public static By MiniTitleInput = By.CssSelector("#MiniTitle");
        public static By MainImageFileUpload = By.CssSelector("[name=MainImage]");
        public static By DescriptionInput = By.CssSelector("#Description");
        public static By EssenceInput = By.CssSelector("#Essence");
        public static By TagsInput = By.CssSelector("#Tags");
        public static By PriceInput = By.CssSelector("#Price");
        public static By ProductCodeInput = By.CssSelector("#ProductCode");
        public static By SubmitButton = By.CssSelector("[type=submit]");
    }
}
