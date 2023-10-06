@addingToCart
Feature: AddToCart

AddToCart
Scenario: User selects pizzas and adds them to the cart
    Given the user selects "Pepperoni " with a quantity of "1"
    And the user selects "Vegetarian" with a quantity of "1"
    When the user clicks on "Add to Cart"
    Then the cart should contain the following pizzas:
      | Name       | Quantity |
      | Pepperoni  | 1        |
      | Vegetarian | 1        |