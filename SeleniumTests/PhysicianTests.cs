using SeleniumTests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Chrome;
using System;

namespace SeleniumTests
{
    [TestClass]
    public class PhysicianTests
    {
        TestDatabaseRepository _databaseRepo = new TestDatabaseRepository();

        static IWebDriver _driver;



        //** HINT:  Don't create a new ChromeDriver and login again for every test (too inefficient).  
        //          Insteaad, create one instance of a ChromeDriver, Login as dr smith ONE time ([ClassInitialize])
        //          BEFORE each test executes  ([TestInitialize]), navigate back to the physician landing page
        //          When ALL tests have completed ([ClassCleanup]), close the browser and dispose of the driver
        private static string _physicianURL = "http://iemosoft.com/selenium-test-physician/";
        private static string _loginURL = "http://iemosoft.com/selenium-test-login/";
        private static string _loginString = "Submit";
        private static string _addPatientString = "Create New Patient";
        private static string _savePatientString = "Save";

        [ClassInitialize]
        public static void BeforeAnyTestRuns(TestContext testContext)
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl(_loginURL);
            DoLogin("dr smith", "P@ssword");
        }


        [ClassCleanup]
        public static void AfterALLTestsRun()
        {
            _driver.Close();
            _driver.Dispose();
        }

        [TestInitialize]
        public void BeforeEachTestExecutes()
        {
            _driver.Navigate().GoToUrl(_physicianURL);
        }

        [TestCleanup]
        public void AfterEachTestExecutes()
        {
            //** NOTE: I'm using the 'test database repository', to roll back any data in the 'database' after each test is finished running
            _databaseRepo.RollbackTestData();
        }

        [TestMethod]
        public void Physician_Can_Create_A_New_Patient()
        {
            //** HINT 1: From the physicians patient list page, click the 'Create New Patient' link (Google: 'c# Selenium find link by text')
            string xPath = String.Format("//a[text() = '{0}']", _addPatientString);
            _driver.FindElement(By.XPath(xPath)).Click();


            //** HINT 2: Fill out the form COMPLETELY with test data and click submit
            var testUserFirstName = "Lewis";
            var testUserLastName = "Zimmerman";

            // Lewis Zimmerman is Dr. Smith's patient
            _driver.FindElement(By.Id("firstName")).SendKeys(testUserFirstName);
            _driver.FindElement(By.Id("lastName")).SendKeys(testUserLastName);

            // Lewis Zimmerman lives on Jupter Station
            _driver.FindElement(By.Id("planet")).Click();
            _driver.FindElement(By.Id("planet")).SendKeys("ju");

            //Lewis is male
            _driver.FindElement(By.Name("maleRadio")).Click();

            //Lewis is a noun
            _driver.FindElement(By.Name("isPerson")).Click();

            // click the save button
            xPath = String.Format("//input[@value = '{0}']", _savePatientString);
            _driver.FindElement(By.XPath(xPath)).Click();


            //** Assert that the 'Data has been saved' message displays and then fades

            // assert that the message was flashed succesfully
            FlashMessageWasSucessfull(messageId: "toast", expectedMessage: "You’re data has been saved!");
            // also the message uses the wrong your, you're
            // it literally says "You are data has been saved!"


            //** Assert that the data has been saved in the database by calling the CheckIfPatientWasCreatedInDatabase method on the
            //   _databaseRepo object

            // Testing that the db saved the entry
            var saved = _databaseRepo.CheckIfPatientWasCreatedInDatabase(testUserFirstName, testUserLastName, DateTime.Now);

            /* does this only ever return true?
             * or am I using it wrong?
             * also I dont kow what date to user for the birday because you dont specify one while creating a person
             */

            Console.WriteLine(_databaseRepo.CheckIfPatientWasCreatedInDatabase(testUserFirstName, testUserLastName, DateTime.Now));
            Console.WriteLine(_databaseRepo.CheckIfPatientWasCreatedInDatabase("fake", "user", DateTime.Now));
            Console.WriteLine(_databaseRepo.CheckIfPatientWasCreatedInDatabase("sdfasdfsad", "sdfasdgsg", DateTime.Now));

            //
            Assert.IsTrue(saved, "The new patient was not saved");


            //** NOTE: Notice the call to RollbackTestData in the AfterEachTestExecutes() method above
            //** NOTE: This 'new patient' isn't actually getting saved anywhere, but in a real application expect that it would

            //** HINT 3: Run some custom javascript.  There is a javascript function in the 'Selenium Test - New Patient' page, called
            //          'aCustomJavaScript()', call this function from selenium Google 'c# selenium run javascript'


            // I can cause an alert by doing this:
            _driver.Execute("alert('test')");

            /* But I dont think 'aCustomJavaScript()' is actualy working it crashes when the next line is uncommented
             * it gives the error:
             * 
             * SeleniumTests.PhysicianTests.Physician_Can_Create_A_New_Patient threw exception: 
             * System.InvalidOperationException: unknown error: aCustomJavaScript is not defined
             * 
             */

            //_driver.Execute("aCustomJavaScript()");



            //** Assert a browser 'Alert' window pops up 

            var allertText = _driver.SwitchTo().Alert().Text;
            Assert.IsTrue(allertText.Contains("test"), "The alert is wrong");

            //** HINT 4: Cancel the Alert window

            // closes the allert
            _driver.SwitchTo().Alert().Accept();

            //** Assert the Alert went away
            try
            {
                // if alert is still open sets text
                allertText = _driver.SwitchTo().Alert().Text;

            }
            catch (Exception)
            {
                // try fails when aller is gone, clears 'allertText' so next test will pass
                allertText = "";
            }

            Assert.IsFalse(allertText.Contains("test"), "The alert is  still there");


        }



        [TestMethod]
        public void Physician_Can_Only_See_Their_Own_Patient()
        {
            //** NOTE: I'm using the _databaseRepo to get all of the patients for dr. smith FROM THE DATABASE, so that I can compare it against the UI
            //         This is one example of 'Data Driven Test Automation'.  Rather than hard code the patients that should be displayed, get it 
            //         from the database One advantage of this appraoch, is that as new patients are added to the database, via manual tests, etc,
            //         this test will still work and be a valid test

            var expectedPhysicianPatientList = _databaseRepo.GetPatientListForPhysician("dr smith");

            Console.WriteLine("expectedPhysicianPatientList: ");
            
            //** HINT: Get List of patients displayed on the UI using Selenium
            List<string> patientListDisplayed = GetAllOfThePatientsDisplayedOnTheScreen();

            //Assert that all of the patients found in the database (using the _databaseRepo) for dr smith are displayed on the UI
            //Assert no patients, OTHER THAN the ones assigned to dr. smith are displayed on the UI

            CollectionAssert.AreEquivalent(expectedPhysicianPatientList, patientListDisplayed, "The lists do not match");
        }

        private List<string> GetAllOfThePatientsDisplayedOnTheScreen()
        {
            List<string> displayed = new List<string> { };
            //** HINT Option 1: execute java script ' return document.getElementsByTagName("table")[0].getElementsByTagName("td")[0].innerHTML'
            //                  better yet: document.getElementsByTagName("table")[0].getElementsByTagName("td").length;

            IWebElement tableElement = _driver.FindElement(By.TagName("table"));
            IList<IWebElement> tableRows = tableElement.FindElements(By.TagName("tr"));

            foreach (IWebElement row in tableRows)
            {
                string name = String.Format("{0} {1}", row.Text.Split(' ')[0], row.Text.Split(' ')[1]);
                if (!name.Equals("Patient Name"))
                {
                    displayed.Add(name);
                }

            }

            //** HINT Option 2: _driver.FindElements(By.XPath(...))  Google 'C# Selenium find all the columns in a table'

            return displayed;
        }

        // helper methods
        private static void DoLogin(string userName, string password)
        {
            _driver.FindElement(By.Id("userName")).SendKeys(userName);
            _driver.FindElement(By.Id("password")).SendKeys(password);

            string xPath = String.Format("//input[@value = '{0}']", _loginString);
            _driver.FindElement(By.XPath(xPath)).Click();

        }

        private void FlashMessageWasSucessfull(string messageId, string expectedMessage)
        {
            var flashMessageIsDisplayed = _driver.FindElement(By.Id(messageId)).Displayed;
            string flashMessageText = _driver.FindElement(By.Id(messageId)).Text;

            // assure that the message is shown
            string errorMessage = String.Format("No message with id '{0}' is displayed", messageId);
            Assert.IsTrue(flashMessageIsDisplayed, errorMessage);

            // assure that the message is corect
            errorMessage = String.Format("The flash messsage is not displaying the correct string: \n{0}", errorMessage);
            Assert.IsTrue(flashMessageText.Contains(expectedMessage), errorMessage);

            // wait for message to fade
            Thread.Sleep(5000);

            // assure message has faded
            flashMessageIsDisplayed = _driver.FindElement(By.Id(messageId)).Displayed;
            Assert.IsFalse(flashMessageIsDisplayed, "Error message is displayed but should have faded out");

            Console.WriteLine("FlashMessageWasSucessfull Passed!");
        }

    }

    public static class IWebExt
    {

        public static object Execute(this IWebDriver _driver, string script)
        {
            return ((IJavaScriptExecutor)_driver).ExecuteScript(script);
        }
    }

}
