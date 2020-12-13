using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace AutomatedTests
{
    public class LogInTest
    {
        [Test]
        public void LogIn_With_Correct_Credentials_Succesfully()
        {
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"D:\Прога\labs\Term7\Курсач\SpeachRecognition\Web\AutomatedTesting", "geckodriver.exe");
            IWebDriver driver = new FirefoxDriver(service);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(1); 
            driver.Navigate().GoToUrl("http://localhost:4200");

            driver.FindElement(By.Id("email")).SendKeys("admin");
            driver.FindElement(By.Id("password")).SendKeys("admin");

            driver.FindElement(By.Id("login")).Click();
            driver.FindElement(By.Id("validate")).Click();

            var token = driver.FindElement(By.Id("token")).Text;
            var validation = driver.FindElement(By.Id("validation")).Text;

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
