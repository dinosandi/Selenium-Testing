using OpenQA.Selenium;
using TechTalk.SpecFlow;
using InstagramAutomationTest.Pages;

namespace InstagramAutomationTest.StepDefinitions
{
    [Binding]
    public class ProfileStepDefinitions
    {
        private readonly IWebDriver _driver;
        private readonly ProfilePage _profilePage;

        public ProfileStepDefinitions(IWebDriver driver)
        {
            _driver = driver;
            _profilePage = new ProfilePage(_driver);
        }

        [When(@"I click on the profile button")]
        public void WhenIClickOnTheProfileButton()
        {
            _profilePage.ClickProfileButton("dnosndii");
        }

        [Then(@"I should see my profile page with username ""(.*)""")]
        public void ThenIShouldSeeMyProfilePage(string username)
        {
            _profilePage.VerifyProfilePage(username);
        }
    }
}