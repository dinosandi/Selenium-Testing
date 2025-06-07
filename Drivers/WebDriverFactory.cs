using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace InstagramAutomationTest.Drivers
{
    public class WebDriverFactory
    {
        public IWebDriver CreateDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            // Add other options as needed
            
            return new ChromeDriver(options);
        }
    }
}
