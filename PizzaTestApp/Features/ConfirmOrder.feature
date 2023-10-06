@confirmOrder
Feature: Confirm Order

Scenario: User confirms the order
  Given the user is on the cart page for confirming the order
  When the user clicks on "Confirm Order"
  Then the user should be on the order confirmation page
  And the user should see the order details
  And the user should see the order total
