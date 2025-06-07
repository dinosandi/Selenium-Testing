Feature: Instagram Home Feed Access

  Scenario: Successful login via cookies should display home feed
    Given I have navigated to Instagram
    And I have injected valid session cookies
    When I refresh the page
    Then I should be on the home feed page

  Scenario: View first Instagram story
    Given I navigate to Instagram
    And I login with valid session
    When I refresh the page
    And I view the first story on home
    Then I should see the story displayed
