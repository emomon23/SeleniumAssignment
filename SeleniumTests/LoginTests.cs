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
        const string Physician_Url_Test = "selenium-test-physician";
        const string Nurse_Url_Test = "selenium-test-nurse";

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
            // does the test login
            DoLogin("aBadBadUserName", "P@assword");

            // waits for error message to appear
            System.Threading.Thread.Sleep(200);

            // grabs error message
            var errorMessageIsDisplayed = _driver.FindElement(By.Id("errorMessage")).Displayed;

            // asserts it displayed correctly
            Assert.IsTrue(errorMessageIsDisplayed, "No 'Invalid Username or Password' message displayed");

            // waits for the disappear
            Thread.Sleep(5000);

            // tries to grab the message
            errorMessageIsDisplayed = _driver.FindElement(By.Id("errorMessage")).Displayed;

            // assers it was gone
            Assert.IsFalse(errorMessageIsDisplayed, "Error message is displayed but should have faded out");


        }

        [TestMethod]
        public void AValidUserName_WithAnInvalidPassword()
        {
            DoLogin("admin", "bad_password");

            System.Threading.Thread.Sleep(200);

            var errorMessageIsDisplayed = _driver.FindElement(By.Id("errorMessage")).Displayed;

            Assert.IsTrue(errorMessageIsDisplayed, "No 'Invalid Username or Password' message displayed");

            Thread.Sleep(5000);

            errorMessageIsDisplayed = _driver.FindElement(By.Id("errorMessage")).Displayed;
            Assert.IsFalse(errorMessageIsDisplayed, "Error message is displayed but should have faded out");
        }

        [TestMethod]
        public void A_Physician_Logs_In_Successfully()
        {
            // does doc login
            DoLogin("dr smith", "P@ssword");


            // waits for page to load
            Thread.Sleep(3000);

            // grabs url
            string Doc_Url = _driver.Url;

            // compares expected url to actual and assters they are equil
            Assert.IsTrue(Doc_Url.Contains(Physician_Url_Test));
        }

        [TestMethod]
        public void A_Nurse_Logs_In_Successfully()
        {
            // does nurse login
            DoLogin("nurse jones", "P@ssword");

            // waits for page to load
            Thread.Sleep(3000);

            // grabs url
            String Nurse_Url = _driver.Url;

            // compares expected url to actual and assters they are equil
            Assert.IsTrue(Nurse_Url.Contains(Nurse_Url_Test));
        }

        [TestMethod]
        public void An_Administrator_Logs_In_Successfully()
        {
            //throw new NotImplementedException("Test not implemented");

            DoLogin("admin", "P@ssword");

            int numb = get_number();
            Assert.IsTrue(numb >= 0 && numb <= 100, "Number is not between 0 and 100");

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
    

        // login helper functopn
        private void DoLogin(string user_name, string password)
        {
            _driver.FindElement(By.Id("userName")).SendKeys(user_name);
            _driver.FindElement(By.Id("password")).SendKeys(password);
            var submitButtonXPath = "/html/body/div/div[3]/div[1]/div[1]/div/div[2]/form/input[3]"; //**** PUT THE XPATH VALUE HERE!!!
            _driver.FindElement(By.XPath(submitButtonXPath)).Click();
        }

        // div scraper function
        private int get_number()
        {
            string scraped = _driver.FindElement(By.ClassName("msg")).Text;

            if (int.TryParse(scraped, out int parsed))
            {
                return parsed;
            }
            else
            {
                return -1;
            }
        }
    }
}
