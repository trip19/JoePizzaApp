@registration
Feature: Register
  As a new user
  I want to register for an account
  So that I can access the website's features

  Scenario: User registers for an account
    Given the user is on the registration page
    When the user enters their registration details
    And the user clicks on the "Register" button
    Then the user should be registered and logged in