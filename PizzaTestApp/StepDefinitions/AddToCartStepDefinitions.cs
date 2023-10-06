using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using JoePizzaApp.Models;
using JoePizzaApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using JoePizzaApp;

namespace YourSpecFlowProjectNamespace
{
    [Binding]
    public class AddToCartSteps
    {
        private readonly IWebDriver driver;
        private readonly ApplicationDbContext dbContext; // Your database context
        private int pizzaId; // Store the selected pizza ID for later validation

        // Constructor to initialize the WebDriver and database context
        public AddToCartSteps()
        {
            // Initialize your WebDriver (e.g., ChromeDriver) here
            driver = new ChromeDriver();

            // Initialize your database context here
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PizzaDB")
                .Options;
            dbContext = new ApplicationDbContext(options);
        }

        [Given(@"the user selects ""(.*)"" with a quantity of ""(.*)""")]
        public void GivenTheUserSelectsWithAQuantityOf(string pizzaName, int quantity)
        {
            // Find the pizza by name in the database (assuming you have a Pizza entity)
            var pizza = dbContext.Pizzas.SingleOrDefault(p => p.Name == pizzaName);

            // Ensure the pizza was found
            if (pizza == null)
            {
                throw new Exception($"Pizza '{pizzaName}' not found in the database.");
            }

            // Store the pizza ID for later validation
            pizzaId = pizza.Id;

            // Navigate to the home page
            driver.Navigate().GoToUrl("https://localhost:7295"); // Replace with your actual URL

            // Find the pizza card with the given name
            var pizzaCard = driver.FindElement(By.XPath($"//h5[contains(text(), '{pizzaName}')]"));

            // Find the quantity input field within the pizza card
            var quantityInput = pizzaCard.FindElement(By.Name("quantity"));

            // Clear any existing value and set the quantity
            quantityInput.Clear();
            quantityInput.SendKeys(quantity.ToString());
        }

        [When(@"the user clicks on ""Add to Cart""")]
        public void WhenTheUserClicksOnAddToCart()
        {
            // Find the "Add to Cart" button and click it
            var addToCartButton = driver.FindElement(By.XPath("//button[contains(text(), 'Add to Cart')]"));
            addToCartButton.Click();
        }

        [Then(@"the cart should contain the following pizzas:")]
        public void ThenTheCartShouldContainTheFollowingPizzas(Table table)
        {
            // Ensure that the pizza was added to the cart in the database
            var cartItem = dbContext.CartItems
                .Include(ci => ci.Pizza)
                .SingleOrDefault(ci => ci.PizzaId == pizzaId);


            // Ensure the cart item exists
            if (cartItem == null)
            {
                throw new Exception($"Pizza with ID {pizzaId} was not added to the cart.");
            }

            // Validate the cart item's quantity
            var expectedQuantity = int.Parse(table.Rows[0]["Quantity"]);
            if (cartItem.Quantity != expectedQuantity)
            {
                throw new Exception($"Expected quantity: {expectedQuantity}, Actual quantity: {cartItem.Quantity}");
            }

            // You can also validate other cart details as needed
        }

        [AfterScenario]
        public void AfterScenario()
        {
            // Dispose of or close the WebDriver after each scenario
            driver.Quit();
            // Dispose of or close the database context
            dbContext.Dispose();
        }
    }
}
