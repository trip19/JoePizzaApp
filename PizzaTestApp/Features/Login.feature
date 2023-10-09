@authentication
Feature: Login

User logs in
Scenario: User logs in first
    Given the user is on the login page
    When the user enters valid login credentials
    And the user clicks on "Login"
    Then the user should be logged in

