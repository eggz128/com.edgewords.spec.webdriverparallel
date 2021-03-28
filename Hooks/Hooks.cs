﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

//Make Features run in parallel. This annotation can only be set once in a project
//This seems as good a place as any.
//From the docs:
//Note: SpecFlow does not support scenario level parallelization with NUnit (when scenarios from the same feature execute in parallel). If you configure a higher level NUnit parallelization than “Fixtures” your tests will fail with runtime errors.

using NUnit.Framework;
[assembly: Parallelizable(ParallelScope.Fixtures)] //Can only parallelise Features
[assembly: LevelOfParallelism(8)] //Worker thread i.e. max amount of Features to run in Parallel

namespace com.edgewords.spec.scenariocontext.Hooks
{
    [Binding]
    class Hooks
    {
        
        private readonly ScenarioContext _scenarioContext;

        //Just a field for use in this class. Not using it in the constructor.
        private IWebDriver _driver;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            
        }
        [BeforeScenario]
        public void Setup()
        {

            _driver = new ChromeDriver();
            _scenarioContext["SomeDataToPassRound"] = "Hello World";
            _scenarioContext["driver"] = _driver; //this...works...


            
        }
        [AfterScenario]
        public void TearDown()
        {
            _driver.Quit(); //Is this parallel safe? Seems to work.
        }
        

    }
}
