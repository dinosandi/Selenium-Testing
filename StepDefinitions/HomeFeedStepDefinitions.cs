using TechTalk.SpecFlow;
using OpenQA.Selenium;
using InstagramAutomationTest.Pages;

namespace InstagramAutomationTest.StepDefinitions
{
    [Binding]
    public class HomeFeedStepDefinitions
    {
        private readonly HomeFeedPage _homeFeedPage;

        public HomeFeedStepDefinitions(IWebDriver driver)
        {
            _homeFeedPage = new HomeFeedPage(driver);
        }

        [Given(@"I have navigated to Instagram")]
        [Given(@"I navigate to Instagram")]
        public void GivenINavigateToInstagram()
        {
            _homeFeedPage.NavigateToInstagram();
        }

        [Given(@"I have injected valid session cookies")]
        [Given(@"I login with valid session")]
        public void GivenIHaveInjectedValidSessionCookies()
        {
            _homeFeedPage.InjectSessionCookies();
        }

        [When(@"I refresh the page")]
        public void WhenIRefreshThePage()
        {
            _homeFeedPage.RefreshPage();
        }

        [When(@"I view the first story on home")]
        public void WhenIViewTheFirstStoryOnHome()
        {
            _homeFeedPage.ClickFirstStory();
        }

        [Then(@"I should be on the home feed page")]
        public void ThenIShouldBeOnTheHomeFeedPage()
        {
            _homeFeedPage.VerifyHomePage();
        }

        [Then(@"I should see the story displayed")]
        public void ThenIShouldSeeTheStoryDisplayed()
        {
            _homeFeedPage.VerifyStoryDisplayed();
        }
    }
}