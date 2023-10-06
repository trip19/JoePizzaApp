@removingFromCart
Feature: RemoveFromCart
    As a user
    I want to remove items from the cart
    So that I can modify my order

Scenario: User removes an item from the cart
   Given the user is on the cart page for removing items
   When the user clicks on the "Remove" button for the item
   Then the item should be removed from the cart
   And the cart should be updated with the new total price
