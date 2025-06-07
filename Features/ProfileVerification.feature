Feature: Profile Verification
  Scenario: Verify logged-in user profile is displayed
    Given I have navigated to Instagram
    And I have injected valid session cookies
    When I refresh the page
    And I click on the profile button
    Then I should see my profile page with username "dnosndii"
