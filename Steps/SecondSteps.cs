using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace com.edgewords.spec.scenariocontext.Steps
{
    [Binding]
    public class SecondSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IWebDriver _driver;

        public SecondSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _driver = (IWebDriver)_scenarioContext["driver"];
        }
        [Given(@"I am on the Google Home page")]
        public void GivenIAmOnTheGoogleHomePage()
        {
            _driver.Url = "http://www.google.com";
            //Fresh browser instance. Will need to accept cookies!
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement CookieFrame = wait.Until(drv => drv.FindElement(By.CssSelector("iframe[src*='consent.google.com']")));

            
            _driver.SwitchTo().Frame(CookieFrame);
            _driver.FindElement(By.CssSelector("#introAgreeButton > span > span")).Click();
            _driver.SwitchTo().DefaultContent(); //Doesn't seem to be needed
            Thread.Sleep(1000);
        }

        [When(@"I search for '(.*)'")]
        public void WhenISearchFor(string searchterm)
        {
            _driver.FindElement(By.Name("q")).SendKeys(searchterm + Keys.Return);
        }

        [Then(@"'(.*)' should be in the top result")]
        public void ThenShouldBeInTheTopResult(string searchterm)
        {
            string ResultText = _driver.FindElement(By.CssSelector("div[class=g] > div:first-of-type div.yuRUbf h3")).Text;
            Assert.That(ResultText, Does.Contain(searchterm).IgnoreCase,"Word not in top result");
        }

        [Then(@"the new value is here")]
        public void ThenTheNewValueIsHere()
        {

            Console.WriteLine("SHould not be Hello World: " + _scenarioContext["SomeDataToPassRound"]);
            Assert.That(_scenarioContext["SomeDataToPassRound"], Is.EqualTo("FooBar"), "Context was not changed");
        }
    }
}
