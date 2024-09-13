using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OnlineStoreOfBoardGames.Tests.E2E.PagesSelectors;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace OnlineStoreOfBoardGames.Tests.E2E.Tests
{
    internal class BoardGameReviewTests
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _driver = new ChromeDriver();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
        }

        [Test]
        public void BoardGameReviewCRUD()
        {
            _driver.LoginAsAdmin();

            _driver.FindElement(Layout.BoardGameLink).Click();
            _driver.FindElement(BoardGameIndexPage.FirstBoardGame).Click();

            var reviewsBeforeAddOneMore = _driver.FindElements(BoardGamePage.Review).Count();

            var button = _wait.Until(ExpectedConditions.ElementToBeClickable(BoardGamePage.CreateReviewButton));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", button);

            _driver.FindElement(ReviewPage.TextInput).SendKeys("Test review!");
            _driver.FindElement(ReviewPage.SendButton).Click();

            var reviewsAfterAddOneMore = _driver.FindElements(BoardGamePage.Review).Count;

            Assert.That(reviewsBeforeAddOneMore, Is.EqualTo(reviewsAfterAddOneMore - 1));

            var button2 = _wait.Until(ExpectedConditions.ElementToBeClickable(BoardGamePage.LastUpdateButton));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", button2);

            _driver.FindElement(ReviewPage.TextInput).SendKeys("Test review!");
            _driver.FindElement(ReviewPage.SendButton).Click();

            var textOfLastReview = _driver.FindElement(BoardGamePage.LastReviewText).Text;

            Assert.That(textOfLastReview, Is.EqualTo("Test review!" + "Test review!"));

            var button3 = _wait.Until(ExpectedConditions.ElementToBeClickable(BoardGamePage.LastDeleteButton));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", button3);
            Thread.Sleep(1000);
            var reviewsAfterRemoveOneMore = _driver.FindElements(BoardGamePage.Review).Count;

            Assert.That(reviewsAfterRemoveOneMore, Is.EqualTo(reviewsBeforeAddOneMore));
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _driver.Quit();
        }
    }
}
