using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome; // Replace with your preferred WebDriver library
using NUnit.Framework;

namespace PizzaTestApp.Steps
{
    [Binding]
    public class LoginSteps
    {
        private IWebDriver driver;

        public LoginSteps()
        {
            // Initialize the WebDriver (make sure you have the WebDriver installed and configured)
            driver = new ChromeDriver(); // Replace with your preferred WebDriver
        }

        [Given("the user is on the login page")]
        public void GivenTheUserIsOnTheLoginPage()
        {
            // Navigate to the login page
            driver.Navigate().GoToUrl("https://localhost:7295/Identity/Account/Login"); // Replace with your login page URL
        }

        [When("the user enters valid login credentials")]
        public void WhenTheUserEntersValidLoginCredentials()
        {
            // Find and fill in the login form fields with valid credentials
            IWebElement usernameField = driver.FindElement(By.Id("Input_Email")); // Replace with the actual element locator
            IWebElement passwordField = driver.FindElement(By.Id("Input_Password")); // Replace with the actual element locator

            usernameField.SendKeys("triptinayak23@gmail.com"); // Replace with a valid username
            passwordField.SendKeys("2345Trip*"); // Replace with a valid password
        }

        [When("the user clicks on \"Login\"")]
        public void WhenTheUserClicksOnLogin()
        {
            // Find and click the login button
            IWebElement loginButton = driver.FindElement(By.Id("login-submit")); // Replace with the actual element locator
            loginButton.Click();
        }

        [Then("the user should be logged in")]
        public void ThenTheUserShouldBeLoggedIn()
        {
            string expectedHomePageUrlPattern = "https://localhost:7295/";
            string currentUrl = driver.Url;

            Assert.IsTrue(currentUrl.Contains(expectedHomePageUrlPattern), "User should be redirected to the homepage.");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            // Clean up: Close the WebDriver after the scenario
            driver.Quit();
        }
    }
}
