using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;
using Computer.Database.Data;
using Computer.Database.Utils;
using NUnit.Framework;

namespace Computer.Database.UI.PageObjects
{
    class SearchComputer
    {
        [FindsBy(How = How.Id, Using = "searchbox")]
        IWebElement ComputerName;

        [FindsBy(How = How.Id, Using = "searchsubmit")]
        IWebElement FilterButton;

        [FindsBy(How = How.XPath, Using = "//*[@class='computers zebra-striped']")]
        IWebElement ResultsTable;

        string NothingFoundMessage = "//*[@class='well']/em";

        public IWebDriver driver;
        public SearchComputer(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void SearchForNewComputer(string strComputerName, ComputerData computerData)
        {
            ComputerName.SendKeys(strComputerName);
            Utilities.ScreenCapture(driver, "SearchComputer");
            FilterButton.Click();
            Utilities.ScreenCapture(driver, "Results");

            List<IWebElement> rows = ResultsTable.FindElements(By.TagName("tr")).ToList();
            bool bComputerFound = false;

            string strIntroduced = (!string.IsNullOrEmpty(computerData.IntroducedDate)) ?
                                    DateTime.Parse(computerData.IntroducedDate).ToString("dd MMM yyyy") : "-";
            string strDiscontinued = (!string.IsNullOrEmpty(computerData.DiscontinuedDate)) ?
                                    DateTime.Parse(computerData.DiscontinuedDate).ToString("dd MMM yyyy") : "-";
            string strCompany = (!string.IsNullOrEmpty(computerData.Company)) ? computerData.Company : "-";

            foreach (IWebElement row in rows)
            {
                if (row.Text.Contains(strComputerName))
                {
                    bComputerFound = true;
                    Assert.AreEqual(strIntroduced, row.FindElement(By.XPath("./td[2]")).Text);
                    Assert.AreEqual(strDiscontinued, row.FindElement(By.XPath("./td[3]")).Text);
                    Assert.AreEqual(strCompany, row.FindElement(By.XPath("./td[4]")).Text);

                    // Open the record
                    row.FindElement(By.XPath("./td[1]/a")).Click();
                }
            }
            Assert.IsTrue(bComputerFound);
            Utilities.ScreenCapture(driver, "NewRecordOpened");
        }

        public void SearchForUpdatedComputer(string strComputerName, ComputerData computerData)
        {
            ComputerName.SendKeys(strComputerName);
            Utilities.ScreenCapture(driver, "SearchUpdatedComputer");
            FilterButton.Click();
            Utilities.ScreenCapture(driver, "UpdatedResults");

            List<IWebElement> rows = ResultsTable.FindElements(By.TagName("tr")).ToList();
            bool bComputerFound = false;

            string strIntroduced = (!string.IsNullOrEmpty(computerData.NewIntroducedDate)) ?
                                    DateTime.Parse(computerData.NewIntroducedDate).ToString("dd MMM yyyy") : (!string.IsNullOrEmpty(computerData.IntroducedDate)) ?
                                    DateTime.Parse(computerData.IntroducedDate).ToString("dd MMM yyyy") : "-";
            string strDiscontinued = (!string.IsNullOrEmpty(computerData.NewDiscontinuedDate)) ?
                                    DateTime.Parse(computerData.NewDiscontinuedDate).ToString("dd MMM yyyy") : (!string.IsNullOrEmpty(computerData.DiscontinuedDate)) ?
                                    DateTime.Parse(computerData.DiscontinuedDate).ToString("dd MMM yyyy") : "-";
            string strCompany = (!string.IsNullOrEmpty(computerData.NewCompany)) ? computerData.NewCompany : (!string.IsNullOrEmpty(computerData.Company)) ?
                                    computerData.Company : "-";

            foreach (IWebElement row in rows)
            {
                if (row.Text.Contains(strComputerName))
                {
                    bComputerFound = true;
                    Assert.AreEqual(strIntroduced, row.FindElement(By.XPath("./td[2]")).Text);
                    Assert.AreEqual(strDiscontinued, row.FindElement(By.XPath("./td[3]")).Text);
                    Assert.AreEqual(strCompany, row.FindElement(By.XPath("./td[4]")).Text);

                    // Open the record
                    row.FindElement(By.XPath("./td[1]/a")).Click();
                }
            }
            Assert.IsTrue(bComputerFound);
            Utilities.ScreenCapture(driver, "UpdatedRecordOpened");
        }

        public void SearchForNonExistingComputer(string strComputerName, ComputerData computerData)
        {
            ComputerName.SendKeys(strComputerName);
            Utilities.ScreenCapture(driver, "SearchUpdatedComputer");
            FilterButton.Click();
            Utilities.ScreenCapture(driver, "UpdatedResults");

            Assert.IsTrue(Utilities.CheckWebElementExistsAndVisible(driver, NothingFoundMessage));
            string NotFoundMessage = driver.FindElement(By.XPath(NothingFoundMessage)).Text;
            Assert.AreEqual(NotFoundMessage, "Nothing to display");

        }

    }
}
