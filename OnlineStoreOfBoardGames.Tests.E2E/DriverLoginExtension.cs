using OpenQA.Selenium;
using OnlineStoreOfBoardGames.Tests.E2E.PagesSelectors;

namespace OnlineStoreOfBoardGames.Tests.E2E
{
    public static class DriverLoginExtension
    {
        public const string ADMIN_NAME = "admin";
        public const string ADMIN_PASSWORD = "admin";
        public const string Traveling_ADMIN = "travelingAdmin";
        public const string Traveling_ADMIN_ADMIN_PASSWORD = "travelingAdmin";

        public static void LoginAsAdmin(this IWebDriver driver)
        {
            driver.Url = CommonConstants.BASE_URL;

            driver
                .FindElement(Layout.LoginLink)
                .Click();
            driver
                .FindElement(LoginPage.LoginInput)
                .SendKeys(ADMIN_NAME);
            driver
                .FindElement(LoginPage.PasswordInput)
                .SendKeys(ADMIN_PASSWORD);
            driver
                .FindElement(LoginPage.SubmitButton)
                .Click();
        }

        public static void Login(this IWebDriver driver, string login, string password)
        {
            driver.Url = CommonConstants.BASE_URL;

            driver
                .FindElement(Layout.LoginLink)
                .Click();
            driver
                .FindElement(LoginPage.LoginInput)
                .SendKeys(login);
            driver
                .FindElement(LoginPage.PasswordInput)
                .SendKeys(password);
            driver
                .FindElement(LoginPage.SubmitButton)
                .Click();
        }

        public static void Logout(this IWebDriver driver)
        {
            driver.Url = CommonConstants.BASE_URL;

            driver
                .FindElement(Layout.LogoutLink)
                .Click();
        }
        public static void LoginTravelinAdmin(this IWebDriver driver)
        {
            driver.Url = CommonConstants.BASE_URL;
            var logoutLink = driver.FindElements(Layout.LogoutLink).Count();
            if (logoutLink == 1)
            {
                driver.FindElement(Layout.LogoutLink).Click();
            }

            driver
                .FindElement(Layout.LoginLink)
                .Click();
            driver
                .FindElement(LoginPage.LoginInput)
                .SendKeys(Traveling_ADMIN);
            driver
                .FindElement(LoginPage.PasswordInput)
                .SendKeys(Traveling_ADMIN_ADMIN_PASSWORD);
            driver
                .FindElement(LoginPage.SubmitButton)
                .Click();
        }
    }
}
