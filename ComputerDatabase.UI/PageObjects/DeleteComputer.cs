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
    class DeleteComputer
    {
        [FindsBy(How = How.CssSelector, Using = "input[type='submit'][value='Delete this computer']")]
        IWebElement DeleteButton;

        [FindsBy(How = How.XPath, Using = "//div[@class='alert-message warning']")]
        IWebElement SuccessMessageLabel;

        string SuccessMessageIdentifier = "//div[@class='alert-message warning']/strong";

        public IWebDriver driver;
        public DeleteComputer(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void DeleteExistingComputer()
        {
            DeleteButton.Click();

            Assert.IsTrue(Utilities.CheckWebElementExistsAndVisible(driver, SuccessMessageIdentifier));
            Assert.AreEqual(SuccessMessageLabel.Text, "Done! Computer has been deleted");
            Utilities.ScreenCapture(driver, "ComputerDeleted");
        }

    }
}
