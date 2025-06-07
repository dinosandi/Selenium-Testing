using BoDi;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using InstagramAutomationTest.Drivers;

namespace InstagramAutomationTest.Hooks
{
    [Binding]
    public class WebDriverHooks
    {
        private readonly IObjectContainer _container;
        
        public WebDriverHooks(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            IWebDriver driver = new WebDriverFactory().CreateDriver();
            _container.RegisterInstanceAs<IWebDriver>(driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var driver = _container.Resolve<IWebDriver>();
            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}