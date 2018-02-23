using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests
{
    //*********************************************************************************************************************
    //** NOTE:  A Note is just a comment I'd like you to read to understand what is happening 
    //** HINT:  Is something YOU NEED TO DO.
    //** Start by scanning all the ** NOTE: comments, than go through all of the ** HINT: comments and do your work
    //*********************************************************************************************************************

    //** NOTE:  This [TestClass] annotation tells the test runner that it should look into this class and try and run any tests.  
    //          You can comment out this one line and test runner will remove all tests from the Test Explorer window
    [TestClass]
    public class LoginTests
    {
        const string Login_Url = "http://iemosoft.com/selenium-test-login/";

        IWebDriver _driver;
        //** NOTE:  IWebDriver, above, is an interface.  There are many implementations of IWebDriver, such as the ChromeDriver, the IEDriver 
        //          and the FireFoxDriver. The _driver object will be used for interacting with a Chrome browser, telling it what buttons to click, etc.


        //** NOTE:  This [TestInitialize] annotation will cause this function to run BEFORE EACH TEST (tests are identified by the [TestMethod] annotation)
        //          We see that this method will create a new ChromeDriver and pop up a new page for each test (is this the most effiecient way to do this?)
        [TestInitialize]
        public void NaviageToLogin()
        {
            //** NOTE:  The ChromeDriver 'Implements' the IWebDriver interface and that is why we can set the _driver = new ChromeDriver()
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl(Login_Url);
        }

        //** NOTE: This annotation tells test runner to execute AFTER EACH test runs (regardless of if it passed or failed).
        [TestCleanup]
        public void CloseBrowser()
        {
           
            _driver.Close();

            //** NOTE: Always check if an object has a 'Dispose' method, if it does, call it when you're done using it.
            _driver.Dispose();
        }

        //** NOTE: Each test gets it's own [TestMethod].
        [TestMethod]
        public void AnInvalidUserName_WithAValidPassword()
        {
            //** HINT **.   Manually navigate to http://iemosoft.com/selenium-test-login/ in a chrome browser
            //              Right click the 'Username' INPUT control and select 'Inspect'
            //              Note the id attribute (eg. <input id="userName">) 
            //              Replace the string 'USER_NAME_ELEMENT_ID_HERE' with the id value you found in the html
            _driver.FindElement(By.Id("USER_NAME_ELEMENT_ID_HERE")).SendKeys("aBadBadUserName");


            //** NOTE: below, we are using the _driver object to ask the ChromeDriver to find a element on the pages with an Id of 'password'), 
            //then we're going to send the value we want on that input control
            _driver.FindElement(By.Id("password")).SendKeys("P@assword");

            //** HINT **.   Next click the submit button here.
            //              Manually navigate to http://iemosoft.com/selenium-test-login/ in a chrome browser
            //              Right click the 'Submit' button and select 'Inspect'
            //              Notice there is no Id attribute.
            //              In the Elementents tab, <input type='button' onclick='onSubmit()' value='Submit'> should be highlighted, 
            //              Right click on <input type="button"... and select Copy -> Copy XPath
            //              Paste the xpath value in the submitButtonXPath variable below

            var submitButtonXPath = ""; //**** PUT THE XPATH VALUE HERE!!!
            _driver.FindElement(By.XPath(submitButtonXPath)).Click();

            //Sleep for 200 milliseconds to give the message a chance to display (fyi: Having sleep statements sprinkled throughout your test is 
            //frowned upon in general, but is often unavoidable)
            System.Threading.Thread.Sleep(200);

            //** NOTE: Now see if the error message is displayed
            var errorMessageIsDisplayed = _driver.FindElement(By.Id("errorMessage")).Displayed;

            //** NOTE: The Assert class has static methods that will fail a test if the condition does not pass
            Assert.IsTrue(errorMessageIsDisplayed, "No 'Invalid Username or Password' message displayed");

            //The error message should disappear
            Thread.Sleep(5000);
            errorMessageIsDisplayed = _driver.FindElement(By.Id("errorMessage")).Displayed;
            Assert.IsFalse(errorMessageIsDisplayed, "Error message is displayed but should have faded out");

            
        }

        [TestMethod]
        public void AValidUserName_WithAnInvalidPassword()
        {
            //** HINT: View the login page in the brower,  Valid username and password combinations have been provided right from the login page
            //         Examine the test above and write the test where the username is valid, but the password is invalid
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
             * The physician is taken to the physician page (iemosoft.com/selenium-test-physician)
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
             * HINT: Use By.ClassName
             *       Google 'Selenium getting text from div'
             */
        }


        //** NOTE:  After completeing this section of the Selenium Assignment, if you understand the code above and what you've done
        //          you now know more than 70% of the functions you will use in Selenium automation, there are other methods of course, 
        //          but these are the most commonly used.
    }
}
