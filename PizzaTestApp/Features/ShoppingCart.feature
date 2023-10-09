@addToCart
Feature: ShoppingCart
  As a user
  I want to add pizzas to my cart
  So that I can place an order

  Scenario: User adds a pizza to the cart
    Given I am on the login page
    When I enter my credentials and click the login button
    Then I should be redirected to the homepage
    Then I specify the quantity as "2" in "Vegetarian" pizza card
    When I click on the add to cart button for "Vegetarian" pizza
    Then I should be redirected to the cart page
    And the cart should contain the pizza name "Vegetarian" and quantity "2"

  Scenario: User adds multiple pizzas with quantities to the cart and removes one pizza
    Given I am on the login page
    When I enter my credentials and click the login button
    Then I should be redirected to the homepage
    Then I specify the quantity as "2" in "Margherita" pizza card
    When I click on the add to cart button for "Margherita" pizza
    Then I should be redirected to the cart page
    And the cart should contain the pizza name "Margherita" and quantity "2"

    Given I am on the Home page
    Then I specify the quantity as "1" in "Pepperoni" pizza card
    When I click on the add to cart button for "Pepperoni" pizza
    Then I should be redirected to the cart page
    And the cart should contain the pizza name "Pepperoni" and quantity "1"

    When I click on the remove button for "Pepperoni" pizza
	Then the "Pepperoni" pizza is removed from the cart