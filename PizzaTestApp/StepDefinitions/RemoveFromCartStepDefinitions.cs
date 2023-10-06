using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace PizzaTestApp.StepDefinitions
{
    [Binding]
    public class RemoveFromCartStepDefinitions
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public RemoveFromCartStepDefinitions()
        {
            // Configure the WebDriver (you can use other browsers as well)
            driver = new ChromeDriver();

            // Set up a WebDriverWait with a timeout
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Given("the user is on the cart page for removing items")]
        public void GivenTheUserIsOnTheCartPageForRemovingItems()
        {
            driver.Navigate().GoToUrl("https://localhost:7295/Home/Cart"); // Replace with the actual URL
        }

        [When("the user clicks on the \"Remove\" button for the item")]
        public void WhenTheUserClicksOnRemoveButton()
        {
            driver.FindElement(By.CssSelector(".btn-danger")).Click();

            // Check if the item is removed and the cart is updated
            if (!IsItemRemovedFromCart() || !IsCartUpdated())
            {
                throw new ApplicationException("Item was not removed from the cart.");
            }
        }

        [Then("the item should be removed from the cart")]
        public void ThenTheItemShouldBeRemovedFromTheCart()
        {
            // Implement code to verify that the item is removed from the cart
            Assert.IsFalse(IsItemInCart());
        }

        [Then("the cart should be updated with the new total price")]
        public void ThenTheCartShouldBeUpdatedWithNewTotalPrice()
        {
            // Implement code to verify that the cart is updated with the new total price
            Assert.IsTrue(IsCartUpdated());
        }

        [AfterScenario]
        public void AfterScenario()
        {
            // Dispose of the WebDriver after each scenario
            driver.Quit();
        }

        private bool IsItemRemovedFromCart()
        {
            return !driver.PageSource.Contains("Pepperoni");
        }

        private bool IsItemInCart()
        {
            // Implement code to check if the item is still in the cart
            // You can locate elements that uniquely identify the item in the cart
            return driver.PageSource.Contains("Pepperoni");
        }
        private bool IsCartUpdated()
        {
            return driver.PageSource.Contains("Rs.334.00");
        }
    }
}
