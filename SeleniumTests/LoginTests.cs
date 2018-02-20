using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests
{
    [TestClass]
    public class LoginTests
    {
        const string Login_Url = "http://iemosoft.com/selenium-test-login/";
        IWebDriver _driver;
      
        [TestInitialize]
        public void NaviageToLogin()
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl(Login_Url);
        }

        [TestCleanup]
        public void CloseBrowser()
        {
            _driver.Close();
            _driver.Dispose();
        }

        [TestMethod]
        public void AnInvalidUserName_WithAValidPassword()
        {
            //** HINT **.   Manually navigate to Login_Url in a chrome browser
            //              Right click the 'Username' input element and select 'Inspect'
            //              Note the id attribute (eg. <input id="THE ID IS HERE">) 
            //              Replace the string 'USER_NAME_ELEMENT_ID_HERE' with the id value you found in the html
            _driver.FindElement(By.Id("USER_NAME_ELEMENT_ID_HERE")).SendKeys("aBadBadUserName");


            _driver.FindElement(By.Id("password")).SendKeys("P@assword");

            //Next click the submit button here.
            //** HINT **.   Manually navigate to Login_Url in a chrome browser
            //              Right click the 'Submit' button and select 'Inspect'
            //              In the Elementents tab, <input type='button'... should be highlighted, 
            //              Right click on <input type="button"... and select Copy -> Copy XPath

            var submitButtonXPath = ""; //**** PUT THE XPATH VALUE HERE!!!
            _driver.FindElement(By.XPath(submitButtonXPath)).Click();

            //Sleep for 200 milliseconds to give the message a chance to display
            System.Threading.Thread.Sleep(200);

            //Now see if the error message is displayed
            var errorMessageIsDisplayed = _driver.FindElement(By.Id("errorMessage")).Displayed;
            Assert.IsTrue(errorMessageIsDisplayed, "No 'Invalid Username or Password' message displayed");

            //The error message should disappear
            Thread.Sleep(5000);
            errorMessageIsDisplayed = _driver.FindElement(By.Id("errorMessage")).Displayed;
            Assert.IsFalse(errorMessageIsDisplayed, "Error message is displayed but should have faded out");
        }

        [TestMethod]
        public void AValidUserName_WithAnInvalidPassword()
        {
            throw new NotImplementedException("Test not implemented");

            /*Assert:
             * Error message displays and then fades out
             */
        }

        [TestMethod]
        public void A_Physician_Logs_In_Successfully()
        {
            throw new NotImplementedException("Test not implemented");
           
            /*Assert:
             * The physician is taken to the physician page
             * HINT: use _driver.Url
             */
        }

        [TestMethod]
        public void A_Nurse_Logs_In_Successfully()
        {
            throw new NotImplementedException("Test not implemented");

            /*Assert:
             * The nurse is taken to the nurse page
             */
        }

        [TestMethod]
        public void An_Administrator_Logs_In_Successfully()
        {
            throw new NotImplementedException("Test not implemented");

            /*Assert:
             * The admin is taken to the admin page
             * and verify the random number is between 0 and 100
             */
        }

    }
}
