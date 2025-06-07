using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;
using System;
using System.IO;

namespace InstagramAutomationTest.Pages
{
    public class ProfilePage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public ProfilePage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(45));
        }

        public void ClickProfileButton(string username)
        {
            try
            {
                _wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

                var profileIcon = _wait.Until(ExpectedConditions.ElementExists(
                    By.XPath($"//a[contains(@href, '/{username}') and descendant::img[contains(@alt, 'profile picture')]]")
                ));

                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", profileIcon);
                System.Threading.Thread.Sleep(1000);
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", profileIcon);

                TakeScreenshot("ButtonProfileWithComplete.png");
            }
            catch (WebDriverTimeoutException)
            {
                TakeScreenshot("profile_not_found.png");
                throw new Exception("Failed to find or click profile button. Screenshot saved.");
            }
        }

        public void VerifyProfilePage(string username)
        {
            try
            {
                var profileHeader = _wait.Until(ExpectedConditions.ElementIsVisible(
                    By.XPath($"//span[text()='{username}']"))
                );

                Assert.That(profileHeader.Displayed, "Username not displayed on profile page.");

                TakeScreenshot("PageProfileWithComplete.png");

                System.Threading.Thread.Sleep(3000);
            }
            catch (WebDriverTimeoutException)
            {
                TakeScreenshot("verify_profile_failed.png");
                throw new Exception("Failed to verify profile page. Screenshot saved.");
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
