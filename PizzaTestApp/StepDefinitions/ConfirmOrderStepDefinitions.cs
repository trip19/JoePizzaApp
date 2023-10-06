using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace PizzaTestApp.StepDefinitions
{
    [Binding]
    public class ConfirmOrderStepDefinitions
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public ConfirmOrderStepDefinitions()
        {
            // Configure the WebDriver (you can use other browsers as well)
            driver = new ChromeDriver();

            // Set up a WebDriverWait with a timeout
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Given("the user is on the cart page for confirming the order")]
        public void GivenTheUserIsOnTheCartPageForConfirmingTheOrder()
        {
            driver.Navigate().GoToUrl("https://localhost:7295/Home/Cart");
        }

        [When("the user clicks on \"Confirm Order\"")]
        public void WhenTheUserClicksOnConfirmOrder()
        {
            // Implement code to click on the "Confirm Order" button
            driver.FindElement(By.Id("confirmOrderButton")).Click();
        }

        [Then("the user should be on the order confirmation page")]
        public void ThenTheUserShouldBeOnTheOrderConfirmationPage()
        {
            // Implement code to verify that the user is on the order confirmation page
            Assert.IsTrue(driver.Url.Contains("OrderConfirmed"));
        }

        [Then("the user should see the order details")]
        public void ThenTheUserShouldSeeTheOrderDetails()
        {
            // Implement code to verify that order details are displayed
            Assert.IsTrue(driver.FindElement(By.Id("order-details")).Displayed);
        }

        [Then("the user should see the order total")]
        public void ThenTheUserShouldSeeTheOrderTotal()
        {
            // Implement code to verify that the order total is displayed
            Assert.IsTrue(driver.FindElement(By.Id("order-total")).Displayed);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            // Dispose of the WebDriver after each scenario
            driver.Quit();
        }
    }
}
