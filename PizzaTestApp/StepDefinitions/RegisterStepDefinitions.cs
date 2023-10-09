using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace PizzaTestApp.StepDefinitions
{
    [Binding]
    public class RegisterStepDefinitions
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public RegisterStepDefinitions()
        {
            // Configure the WebDriver (you can use other browsers as well)
            driver = new ChromeDriver();

            // Set up a WebDriverWait with a timeout
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Given("the user is on the registration page")]
        public void GivenTheUserIsOnTheRegistrationPage()
        {
            driver.Navigate().GoToUrl("https://localhost:7295/Identity/Account/Register");
        }

        [When("the user enters their registration details")]
        public void WhenTheUserEntersTheirRegistrationDetails()
        {

            driver.FindElement(By.Id("Input_Email")).SendKeys("knayak12091@gmail.com");
            driver.FindElement(By.Id("Input_Password")).SendKeys("1234Trip*");
            driver.FindElement(By.Id("Input_ConfirmPassword")).SendKeys("1234Trip*");
        }

        [When("the user clicks on the \"Register\" button")]
        public void WhenTheUserClicksOnTheRegisterButton()
        {
            driver.FindElement(By.Id("registerSubmit")).Click();
        }

        [Then("the user should be registered and logged in")]
        public void ThenTheUserShouldBeRegisteredAndLoggedIn()
        {
            Assert.IsTrue(driver.Url.Contains("RegisterConfirmation"));
        }

        [AfterScenario]
        public void AfterScenario()
        {
            // Dispose of the WebDriver after each scenario
            driver.Quit();
        }
    }
}
