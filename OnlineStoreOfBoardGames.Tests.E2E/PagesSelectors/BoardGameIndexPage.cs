using OpenQA.Selenium;

namespace OnlineStoreOfBoardGames.Tests.E2E.PagesSelectors
{
    public static class BoardGameIndexPage
    {
        public static By FirstBoardGame = By.CssSelector(".board-game-title");
        public static By AddBoardGameButton = By.CssSelector(".add-board-game-link");
        public static By BoardGame = By.CssSelector(".board-game");
        public static By LastBoardGame = By.XPath("(//ul[@class='board-games']//li[contains(@class, 'board-game')])[last()]\r\n");
        public static By DeleteButton = By.XPath("(//ul[@class='board-games']//img[contains(@class, 'delete-icon')])[last()]\r\n");
    }
}
