using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading;

namespace InstagramAutomationTest.Pages
{
    public class HomeFeedPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public HomeFeedPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
        }

        public void NavigateToInstagram()
        {
            _driver.Navigate().GoToUrl("https://www.instagram.com/");
            Thread.Sleep(3000);
        }

        public void InjectSessionCookies()
        {
            _driver.Manage().Cookies.DeleteAllCookies();
            _driver.Manage().Cookies.AddCookie(new Cookie("sessionid", "", ".instagram.com", "/", DateTime.Now.AddDays(7)));
            _driver.Manage().Cookies.AddCookie(new Cookie("ds_user_id", "", ".instagram.com", "/", DateTime.Now.AddDays(7)));
            _driver.Manage().Cookies.AddCookie(new Cookie("csrftoken", "", ".instagram.com", "/", DateTime.Now.AddDays(7)));
            _driver.Manage().Cookies.AddCookie(new Cookie("mid", "", ".instagram.com", "/", DateTime.Now.AddDays(7)));
        }

        public void RefreshPage()
        {
            _driver.Navigate().Refresh();
            Thread.Sleep(5000);
        }

        // Changed method name to match step definition
        public void VerifyHomePage()
        {
            var currentUrl = _driver.Url;
            Assert.That(currentUrl, Does.Not.Contain("accounts/login"), "Redirected to login, cookie might be invalid.");

            Thread.Sleep(5000);

            var homeFeedElement = _wait.Until(driver =>
            {
                try
                {
                    return driver.FindElement(By.CssSelector("main[role='main'], section[role='main'], [aria-label='Home'], [aria-label='Beranda']"));
                }
                catch
                {
                    return null;
                }
            });

            Assert.That(homeFeedElement, Is.Not.Null, "Home feed element not found");
            Assert.That(homeFeedElement.Displayed, Is.True, "Home feed not displayed");

            TakeScreenshot("BerandaWithComplete.png");
        }

        public void VerifyStoryDisplayed()
        {
            try
            {
                var storyModal = _wait.Until(ExpectedConditions.ElementIsVisible(
                    By.CssSelector("div[role='dialog'], div[style*='transform: scale']")));
                Assert.That(storyModal.Displayed, Is.True, "Story is not displayed");
                TakeScreenshot("StoryDisplayed.png");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Failed to verify story display: {ex.Message}");
                throw;
            }
        }

        public void ClickFirstStory()
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementExists(By.CssSelector("ul[role='presentation']")));

                var stories = _driver.FindElements(By.CssSelector("ul[role='presentation'] li"));
                if (stories.Count > 0)
                {
                    stories[0].Click();
                    Thread.Sleep(5000);
                }
                else
                {
                    throw new NoSuchElementException("No stories available to view");
                }
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Failed to click first story: {ex.Message}");
                throw;
            }
        }

        private void TakeScreenshot(string fileName)
        {
            try
            {
                var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
                var projectDir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!
                                            .Parent!.Parent!.Parent!.FullName;
                var path = Path.Combine(projectDir, "Screenshots", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                screenshot.SaveAsFile(path);
                TestContext.WriteLine($"Screenshot saved: {path}");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Failed to take screenshot: {ex.Message}");
            }
        }
    }
}