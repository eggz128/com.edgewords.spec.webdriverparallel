using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using System.Threading;
using NUnit.Framework;


namespace com.edgewords.spec.scenariocontext.Steps
{
    [Binding]
    public class FirstSteps
    {
        private readonly ScenarioContext _scenarioContext; //Context is injected by specflow in the constructor.
        private readonly IWebDriver _driver; //Read only for thread safety?
        public FirstSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _driver = (IWebDriver)_scenarioContext["driver"]; //With a cast...this works...
        }
        [Given(@"I am on the Bing Home page")]
        public void GivenIAmOnTheBingHomePage()
        {
            _driver.Url = "http://www.bing.com";
            Console.WriteLine(_scenarioContext["SomeDataToPassRound"]);
            Thread.Sleep(5000);
        }

        [Given(@"I am on the Edgewords Login Page")]
        public void GivenIAmOnTheEdgewordsLoginPage()
        {
            _driver.Url = "https://www.edgewordstraining.co.uk/webdriver2/";
            _driver.FindElement(By.LinkText("Login To Restricted Area")).Click();
        }

        [When(@"I use the credentials")]
        public void WhenIUseTheCredentials(Table table)
        {
            TableRow row = table.Rows[0];
            string username = row["Username"];
            string password = row["Password"];

            _driver.FindElement(By.Id("username")).SendKeys(username);
            _driver.FindElement(By.Id("password")).SendKeys(password);

        }

        [Then(@"I can login")]
        public void ThenICanLogin()
        {
            _driver.FindElement(By.LinkText("Submit")).Click();
            Thread.Sleep(5000);
        }

        [When(@"I put '(.*)' in the SomeDataToPassAround key")]
        public void WhenIPutInTheSomeDataToPassAroundKey(string newData)
        {
            Console.WriteLine("SomeDataToPassRound is :" + _scenarioContext["SomeDataToPassRound"]);
            _scenarioContext["SomeDataToPassRound"] = newData;
            Console.WriteLine("SomeDataToPassRound now is :" + _scenarioContext["SomeDataToPassRound"]);
        }



        [Then(@"the SomeDataToPassAround value is still Hello World")]
        public void ThenTheSomeDataToPassAroundValueIsStillHelloWorld()
        {
            Console.WriteLine(_scenarioContext["SomeDataToPassRound"]);
            Assert.That(_scenarioContext["SomeDataToPassRound"], Is.EqualTo("Hello World"), "Context changed");
        }

    }
}