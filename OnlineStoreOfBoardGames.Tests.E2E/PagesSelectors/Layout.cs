using OpenQA.Selenium;

namespace OnlineStoreOfBoardGames.Tests.E2E.PagesSelectors
{
    public static class Layout
    {
        public static By LoginLink = By.CssSelector(".login-link");
        public static By LogoutLink = By.CssSelector(".logout-link");
        public static By BoardGameLink = By.CssSelector(".board-game-link");
    }
}
