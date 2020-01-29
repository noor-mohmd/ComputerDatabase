using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Computer.Database.UI.PageObjects;
using Computer.Database.Utils;
using Computer.Database.Data;

namespace Computer.Database.Binding
{
    [Binding]
    public class CommInsureSteps
    {
        public IWebDriver driver { get; set; }

        public static string _currentDateTime;

        public static string strProjectDir = Path.GetDirectoryName(Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory));

        public static string strTestResultsPath = Path.Combine(strProjectDir, "Results");

        public ComputerData ComputerDataTable;

        public static void CustomSpecflowScenarioStart(string strScenarioTitle)
        {
            _currentDateTime = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            Directory.CreateDirectory(Path.Combine(strTestResultsPath, strScenarioTitle.Replace(" ", "") + "_" + _currentDateTime));
            Utilities.CurrentTestCaseResultPath = Path.Combine(strTestResultsPath, strScenarioTitle.Replace(" ", "") + "_" + _currentDateTime);
        }

        public void InitializeDriver(string Browser)
        {
            driver = _InitializeDriver(driver, Browser);

        }

        [Given(@"the user is on home page")]
        public void GivenTheUserIsOnHomePage()
        {
            CustomSpecflowScenarioStart(ScenarioContext.Current.ScenarioInfo.Title);
            InitializeDriver("chrome");
            driver.Navigate().GoToUrl("http://computer-database.gatling.io/computers");

        }

        [When(@"the user adds a new computer with following data")]
        public void WhenTheUserAddsANewComputerWithFollowingData(Table table)
        {
            var computerData = table.CreateInstance<ComputerData>();

            this.ComputerDataTable = computerData;
            AddComputer addComputer = new AddComputer(driver);
            addComputer.AddNewComputer(computerData);

        }

        [Then(@"the computer should be (added|updated) with name '(.*)'")]
        public void ThenTheComputerShouldBeAdded(string strAction, string strComputerName)
        {
            SearchComputer searchComputer = new SearchComputer(driver);
            if (strAction.Equals("added"))
            {
                searchComputer.SearchForNewComputer(strComputerName, ComputerDataTable);
            }
            else
            {
                searchComputer.SearchForUpdatedComputer(strComputerName, ComputerDataTable);
            }
        }

        [Then(@"the user updates existing computer with following data")]
        public void ThenTheUserUpdatesExistingComputerWithFollowingData(Table table)
        {
            var computerData = table.CreateInstance<ComputerData>();

            this.ComputerDataTable = computerData;
            AddComputer addComputer = new AddComputer(driver);
            addComputer.UpdateComputer(computerData);
        }

        [Then(@"the user deletes the computer")]
        public void ThenTheUserDeletesTheComputer()
        {
            DeleteComputer deleteComputer = new DeleteComputer(driver);

            deleteComputer.DeleteExistingComputer();
        }

        [Then(@"the computer should be deleted with name '(.*)'")]
        public void ThenTheComputerShouldBeDeletedWithName(string strComputerName)
        {
            SearchComputer searchComputer = new SearchComputer(driver);

            searchComputer.SearchForNonExistingComputer(strComputerName, ComputerDataTable);
        }

        [When(@"the user tries to add a new computer with following data")]
        public void WhenTheUserTriesToAddANewComputerWithFollowingData(Table table)
        {
            var computerData = table.CreateInstance<ComputerData>();

            this.ComputerDataTable = computerData;
            AddComputer addComputer = new AddComputer(driver);
            addComputer.TryAddNewComputer(computerData);
        }

        [Then(@"the computer should not be added")]
        public void ThenTheComputerShouldNotBeAddedWithName()
        {
            AddComputer addComputer = new AddComputer(driver);
            addComputer.VerifyErrorDisplayed(ComputerDataTable);        
        }



        [AfterScenario]
        public void AfterScenario()
        {
            driver.Quit();
            driver.Dispose();
        }



        public IWebDriver _InitializeDriver(IWebDriver driver, string Browser)
        {
            string strDriverPath = Path.Combine(strProjectDir, "Drivers");

            switch (Browser.ToLower())
            {
                case "chrome":
                    ChromeOptions options = new ChromeOptions();
                    options.Proxy = null;
                    options.AddArguments("disable-infobars");
                    options.AddArgument("--disable-extensions");
                    options.AddArgument("--start-maximized");
                    options.AddAdditionalCapability("useAutomationExtension", false);
                    options.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);
                    options.AddArgument("--ignore-certificate-errors");
                    driver = new ChromeDriver(strDriverPath, options);
                    break;

                case "internet explorer":
                case "ie":
                    var ieOptions = new InternetExplorerOptions();
                    ieOptions.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    ieOptions.RequireWindowFocus = true;
                    ieOptions.EnsureCleanSession = true;
                    ieOptions.AddAdditionalCapability(CapabilityType.AcceptSslCertificates, true);
                    ieOptions.EnablePersistentHover = false;
                    ieOptions.IgnoreZoomLevel = true;
                    driver = new InternetExplorerDriver(strDriverPath, ieOptions);
                    break;

                default:
                    driver = new ChromeDriver(new ChromeOptions { Proxy = null });
                    break;
            }

            driver.Manage().Window.Maximize();

            return driver;
        }



    }

}
