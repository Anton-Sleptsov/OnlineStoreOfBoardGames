using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OnlineStoreOfBoardGames.Tests.E2E.PagesSelectors;
using System.Reflection;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Xml;

namespace OnlineStoreOfBoardGames.Tests.E2E.Tests
{
    public class BoardGameTests
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
        public void BoardGameCreateAndDelete()
        {
            _driver.Login("boardGameCreator", "1");

            _driver.FindElement(Layout.BoardGameLink).Click();

            var boardGamesBeforeAddOneMore = _driver.FindElements(BoardGameIndexPage.BoardGame).Count;

            _driver.FindElement(BoardGameIndexPage.AddBoardGameButton).Click();

            _driver.FindElement(BoardGameCreatePage.TitleInput).SendKeys("Ticket to Ride: Европа");
            _driver.FindElement(BoardGameCreatePage.MiniTitleInput).SendKeys("Постройте железные дороги по всей Европе!");
            _driver.FindElement(BoardGameCreatePage.MainImageFileUpload).SendKeys(GetPathToDefaultCover());
            _driver.FindElement(BoardGameCreatePage.DescriptionInput).SendKeys("Эта увлекательная игра предлагает захватывающее путешествие из дождливого Эдинбурга в солнечный Константинополь. В настольной игре \"Ticket To Ride: Европа\" Вы посетите величественные европейские города, осталось только взять билет на поезд.");
            _driver.FindElement(BoardGameCreatePage.EssenceInput).SendKeys("\"Билет на поезд: Европа\" (Ticket to Ride: Europe) стала второй в серии настольный игр о путешествиях по железной дороге. Здесь вы можете прокладывать маршруты, соединяя города, пускать новые составы и при случае обгонять соперников по количеству заработанных очков. В настольной игре \"Билет на поезд: Европа\" вы перенесетесь в самые красивые города. В новой игре в распоряжении игрока несколько оригинальных механик, добавились игровые элементы, и более разнообразными стали правила. Невероятные ощущения, динамика и новые открытия ждут вас в этой настольной игре.");
            _driver.FindElement(BoardGameCreatePage.TagsInput).SendKeys("Игра из серии");
            _driver.FindElement(BoardGameCreatePage.PriceInput).SendKeys("3900");
            _driver.FindElement(BoardGameCreatePage.ProductCodeInput).SendKeys("31458");

            var button = _wait.Until(ExpectedConditions.ElementToBeClickable(BoardGameCreatePage.SubmitButton));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", button);

            var boardGamesAfterAddOneMore = _driver.FindElements(BoardGameIndexPage.BoardGame).Count;

            Assert.That(boardGamesBeforeAddOneMore, Is.EqualTo(boardGamesAfterAddOneMore - 1));

            _driver.Logout();
            _driver.Login("boardGameModerator", "1");

            _driver.FindElement(Layout.BoardGameLink).Click();
            _driver.FindElement(BoardGameIndexPage.LastBoardGame).Click();
            _driver.FindElement(BoardGameIndexPage.DeleteButton).Click();

            Thread.Sleep(1000);

            var boardGamesAfterRemoveOneMore = _driver.FindElements(BoardGameIndexPage.BoardGame).Count;

            Assert.That(boardGamesAfterRemoveOneMore, Is.EqualTo(boardGamesBeforeAddOneMore));
        }

        private string GetPathToDefaultCover()
        {
            var pathToCurrentAssembly = Assembly.GetExecutingAssembly().Location;
            var pathToCurrentFolder = Path.GetDirectoryName(pathToCurrentAssembly);
            return Path.Combine(pathToCurrentFolder, "ResourceForTet", "Images", "mainImage-default.jpg");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _driver.Quit();
        }
    }
}
