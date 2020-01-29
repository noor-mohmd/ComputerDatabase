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
    class AddComputer
    {
        [FindsBy(How = How.Id, Using = "add")]
        IWebElement AddButton;

        [FindsBy(How = How.Id, Using = "name")]
        IWebElement ComputerName;

        [FindsBy(How = How.Id, Using = "introduced")]
        IWebElement IntroducedDate;

        [FindsBy(How = How.Id, Using = "discontinued")]
        IWebElement DiscontinuedDate;

        [FindsBy(How = How.Id, Using = "company")]
        IWebElement Company;

        [FindsBy(How = How.CssSelector, Using = "input[type='submit'][value='Create this computer']")]
        IWebElement CreateButton;

        [FindsBy(How = How.CssSelector, Using = "input[type='submit'][value='Save this computer']")]
        IWebElement UpdateButton;

        [FindsBy(How = How.XPath, Using = "//div[@class='alert-message warning']")]
        IWebElement SuccessMessageLabel;

        string SuccessMessageIdentifier = "//div[@class='alert-message warning']/strong";

        public IWebDriver driver;
        public AddComputer(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void AddNewComputer(ComputerData computerData)
        {

            AddButton.Click();

            Utilities.ScreenCapture(driver, "AddScreen");

            ComputerName.SendKeys(computerData.ComputerName);

            if (!string.IsNullOrEmpty(computerData.IntroducedDate))
            {
                IntroducedDate.SendKeys(computerData.IntroducedDate);

            }
            if (!string.IsNullOrEmpty(computerData.DiscontinuedDate))
            {
                DiscontinuedDate.SendKeys(computerData.DiscontinuedDate);

            }
            if (!string.IsNullOrEmpty(computerData.Company))
            {
                SelectElement cboCompany = new SelectElement(Company);
                cboCompany.SelectByText(computerData.Company);
            }

            Utilities.ScreenCapture(driver, "AddedData");
            CreateButton.Click();

            Assert.IsTrue(Utilities.CheckWebElementExistsAndVisible(driver, SuccessMessageIdentifier));
            Assert.AreEqual(SuccessMessageLabel.Text, "Done! Computer " + computerData.ComputerName + " has been created");
            Utilities.ScreenCapture(driver, "ComputerAdded");
        }

        public void UpdateComputer(ComputerData computerData)
        {
            Utilities.ScreenCapture(driver, "UpdateScreen");

            ComputerName.Clear();
            ComputerName.SendKeys(computerData.NewComputerName);

            if (!string.IsNullOrEmpty(computerData.NewIntroducedDate))
            {
                IntroducedDate.Clear();
                IntroducedDate.SendKeys(computerData.NewIntroducedDate);
            }

            if (!string.IsNullOrEmpty(computerData.NewDiscontinuedDate))
            {
                DiscontinuedDate.Clear();
                DiscontinuedDate.SendKeys(computerData.NewDiscontinuedDate);
            }

            if (!string.IsNullOrEmpty(computerData.NewCompany))
            {
                SelectElement cboCompany = new SelectElement(Company);
                cboCompany.SelectByText(computerData.NewCompany);
            }
            Utilities.ScreenCapture(driver, "DataUpdated");

            UpdateButton.Click();

            Assert.IsTrue(Utilities.CheckWebElementExistsAndVisible(driver, SuccessMessageIdentifier));
            Assert.AreEqual(SuccessMessageLabel.Text, "Done! Computer " + computerData.NewComputerName + " has been updated");
            Utilities.ScreenCapture(driver, "ComputerUpdated");
        }

        public void TryAddNewComputer(ComputerData computerData)
        {

            AddButton.Click();

            Utilities.ScreenCapture(driver, "AddScreen");

            ComputerName.SendKeys(computerData.ComputerName);

            if (!string.IsNullOrEmpty(computerData.IntroducedDate))
            {
                IntroducedDate.SendKeys(computerData.IntroducedDate);

            }
            if (!string.IsNullOrEmpty(computerData.DiscontinuedDate))
            {
                DiscontinuedDate.SendKeys(computerData.DiscontinuedDate);

            }
            if (!string.IsNullOrEmpty(computerData.Company))
            {
                SelectElement cboCompany = new SelectElement(Company);
                cboCompany.SelectByText(computerData.Company);
            }

            Utilities.ScreenCapture(driver, "AddedData");
            CreateButton.Click();

            Utilities.ScreenCapture(driver, "ComputerAdded");
        }

        public void VerifyErrorDisplayed(ComputerData computerData)
        {
            IWebElement ErrorField = driver.FindElement(By.XPath("//fieldset/div[" + computerData.ErrorOnField + "]"));

            Assert.AreEqual(ErrorField.GetAttribute("class"), "clearfix error");
        }

    }
}
