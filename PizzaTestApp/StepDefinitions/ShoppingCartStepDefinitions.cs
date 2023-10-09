using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace PizzaTestApp.StepDefinitions
{
    [Binding]
    public class ShoppingCartStepDefinitions
    {
        private IWebDriver driver;

        [BeforeScenario]
        public void BeforeScenario()
        {
            // Initialize your WebDriver (e.g., ChromeDriver)
            driver = new ChromeDriver();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            // Dispose of the WebDriver after the scenario
            driver.Quit();
        }

        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage()
        {
            driver.Navigate().GoToUrl("https://localhost:7295/Identity/Account/Login");
        }

        [When(@"I enter my credentials and click the login button")]
        public void WhenIEnterMyCredentialsAndClickTheLoginButton()
        {
            IWebElement usernameField = driver.FindElement(By.Id("Input_Email")); // Replace with the actual element locator
            IWebElement passwordField = driver.FindElement(By.Id("Input_Password")); // Replace with the actual element locator

            usernameField.SendKeys("triptinayak23@gmail.com"); // Replace with a valid username
            passwordField.SendKeys("2345Trip*"); // Replace with a valid password
            IWebElement loginButton = driver.FindElement(By.Id("login-submit")); // Replace with the actual element locator
            loginButton.Click();
        }

        [Then(@"I should be redirected to the homepage")]
        public void ThenIShouldBeRedirectedToTheHomepage()
        {
            Assert.AreEqual("https://localhost:7295/", driver.Url);
        }

        [Then(@"I specify the quantity as ""([^""]*)"" in ""([^""]*)"" pizza card")]
        public void ThenISpecifyTheQuantityAsInPizzaCard(string p0, string pizzaName)
        {
            // Find the input field associated with the pizza name and set its value
            string inputField = $"//input[@id='{pizzaName}']"; // Replace with the actual ID pattern
            IWebElement quantityInput = driver.FindElement(By.XPath(inputField));
            quantityInput.Clear();
            quantityInput.SendKeys(p0);
        }

        [When(@"I click on the add to cart button for ""([^""]*)"" pizza")]
        public void WhenIClickOnTheAddToCartButtonForPizza(string pizzaName)
        {
            string xpath = $"//button[@id='{pizzaName}']";

            var addToCartButton = driver.FindElement(By.XPath(xpath));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", addToCartButton);
        }

        [Then(@"I should be redirected to the cart page")]
        public void ThenIShouldBeRedirectedToTheCartPage()
        {
            Assert.AreEqual("https://localhost:7295/Home/Cart", driver.Url);
        }

        [Then(@"the cart should contain the pizza name ""([^""]*)"" and quantity ""([^""]*)""")]
        public void ThenTheCartShouldContainThePizzaNameAndQuantity(string pizzaName, string p1)
        {
            string xpath = $"//tr[@id='{pizzaName}']";
            IWebElement cartItem = driver.FindElement(By.XPath(xpath));
            string cartItemText = cartItem.Text;

            // Verify that the cart item contains the expected pizza name and quantity
            Assert.IsTrue(cartItemText.Contains(pizzaName));
            Assert.IsTrue(cartItemText.Contains(p1));
        }

        [Given(@"I am on the Home page")]
        public void GivenIAmOnTheHomePage()
        {
            driver.Navigate().GoToUrl("https://localhost:7295");
        }


        [When(@"I click on the remove button for ""([^""]*)"" pizza")]
        public void WhenIClickOnTheRemoveButtonForPizza(string pizzaName)
        {
            string xpath = $"//button[@id='{pizzaName}']";

            var addToCartButton = driver.FindElement(By.XPath(xpath));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", addToCartButton);
        }

        [Then(@"the ""([^""]*)"" pizza is removed from the cart")]
        public void ThenThePizzaIsRemovedFromTheCart(string pizzaName)
        {
            try
            {
                string xpath1 = $"//tr[@id='{pizzaName}']";
                IWebElement pizzaElement = driver.FindElement(By.XPath(xpath1));
                Assert.Fail($"Pizza with name '{xpath1}' should not be present in the cart.");
            }
            catch (NoSuchElementException)
            {

            }
        }


    }
}
