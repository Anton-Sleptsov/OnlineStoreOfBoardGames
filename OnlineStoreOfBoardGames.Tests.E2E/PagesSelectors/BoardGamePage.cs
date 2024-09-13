using OpenQA.Selenium;

namespace OnlineStoreOfBoardGames.Tests.E2E.PagesSelectors
{
    public class BoardGamePage
    {
        public static By CreateReviewButton = By.CssSelector(".create-review-button");
        public static By ReviewsContainer = By.CssSelector(".reviews-container");
        public static By Review = By.CssSelector(".review");
        public static By LastUpdateButton = By.XPath("(//div[@class='reviews-container']//a[contains(@class, 'update-button')])[last()]\r\n");
        public static By LastDeleteButton = By.XPath("(//div[@class='reviews-container']//a[contains(@class, 'delete-button')])[last()]\r\n");
        public static By LastReviewText = By.XPath("(//div[@class='reviews-container']//div[@class='review'])[last()]//p[@class='text']\r\n");
    }
}
