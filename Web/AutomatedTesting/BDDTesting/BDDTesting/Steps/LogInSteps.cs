using System;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;

namespace BDDTesting.Steps
{
	[Binding]
    public class LogInSteps
    {
        private IWebDriver _driver;

        [Given(@"Launch Firefox")]
        public void GivenLaunchFirefox()
        {
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"D:\Прога\labs\Term7\Курсач\SpeachRecognition\Web\AutomatedTesting\BDDTesting\BDDTesting\Drivers", "geckodriver.exe");
            _driver = new FirefoxDriver(service);

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(1);
        }

        [Given(@"Navigate to Web Frontend")]
        public void GivenNavigateToWebFrontend()
        {
            _driver.Navigate().GoToUrl("http://localhost:4200");
        }

        [When(@"Enter admin for login and password")]
        public void WhenEnterAdminForLoginAndPassword()
        {
            _driver.FindElement(By.Id("email")).SendKeys("admin");
            _driver.FindElement(By.Id("password")).SendKeys("admin");
        }

        [When(@"Click on LogIn button")]
        public void WhenClickOnLogInButton()
        {
            _driver.FindElement(By.Id("login")).Click();
        }

        [When(@"Click on Validatio button")]
        public void WhenClickOnValidatioButton()
        {
            _driver.FindElement(By.Id("validate")).Click();
        }

        [Then(@"Token and valdation result should be visible")]
        public void ThenTokenAndValdationResultShouldBeVisible()
        {
            var token = _driver.FindElement(By.Id("token")).Text;
            var validation = _driver.FindElement(By.Id("validation")).Text;

            _driver.Quit();

            token
                .Should()
                .NotBeNull();

            token
                .Should()
                .NotBeNull();

            validation
                .Should()
                .NotBeNull();

            validation
                .Should()
                .Be("true");
        }
    }
}
