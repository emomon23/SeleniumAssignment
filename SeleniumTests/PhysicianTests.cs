using SeleniumTests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SeleniumTests
{
    //**************************************************************
    //** HINT: UNCOMMENT [TestClass], and rebuild the project
    //**************************************************************
    //[TestClass]
    public class PhysicianTests
    {
        TestDatabaseRepository _databaseRepo = new TestDatabaseRepository();

        //** HINT:  Don't create a new ChromeDriver and login again for every test (too inefficient).  
        //          Insteaad, create one instance of a ChromeDriver, Login as dr smith ONE time ([ClassInitialize])
        //          BEFORE each test executes  ([TestInitialize]), navigate back to the physician landing page
        //          When ALL tests have completed ([ClassCleanup]), close the browser and dispose of the driver
        private static string _physicianURL = "http://iemosoft.com/selenium-test-physician/";

        [ClassInitialize]
        public static void BeforeAnyTestRuns()
        {

        }

        [ClassCleanup]
        public static void AfterALLTestsRun()
        {

        }

        [TestInitialize]
        public void BeforeEachTestExecutes()
        {

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

            //** HINT 2: Fill out the form COMPLETELY with test data and click submit

            //** Assert that the 'Data has been saved' message displays and then fades
            //** Assert that the data has been saved in the database by calling the CheckIfPatientWasCreatedInDatabase method on the
            //   _databaseRepo object

            //** NOTE: Notice the call to RollbackTestData in the AfterEachTestExecutes() method above
            //** NOTE: This 'new patient' isn't actually getting saved anywhere, but in a real application expect that it would

            //** HINT 3: Run some custom javascript.  There is a javascript function in the 'Selenium Test - New Patient' page, called
            //          'aCustomJavaScript()', call this function from selenium Google 'c# selenium run javascript'

            //** Assert a browser 'Alert' window pops up 

            //** HINT 4: Cancel the Alert window

            //** Assert the Alert went away

        }

        [TestMethod]
        public void Physician_Can_Only_See_Their_Own_Patient()
        {
            //** NOTE: I'm using the _databaseRepo to get all of the patients for dr. smith FROM THE DATABASE, so that I can compare it against the UI
            //         This is one example of 'Data Driven Test Automation'.  Rather than hard code the patients that should be displayed, get it 
            //         from the database One advantage of this appraoch, is that as new patients are added to the database, via manual tests, etc,
            //         this test will still work and be a valid test
            var expectedPhysicianPatientList = _databaseRepo.GetPatientListForPhysician("dr smith");

            //** HINT: Get List of patients displayed on the UI using Selenium
            List<string> patientListDisplayed = GetAllOfThePatientsDisplayedOnTheScreen();

            //Assert that all of the patients found in the database (using the _databaseRepo) for dr smith are displayed on the UI
            //Assert no patients, OTHER THAN the ones assigned to dr. smith are displayed on the UI
        }

        private List<string> GetAllOfThePatientsDisplayedOnTheScreen()
        {
            //** HINT Option 1: execute java script ' return document.getElementsByTagName("table")[0].getElementsByTagName("td")[0].innerHTML'
            //                  better yet: document.getElementsByTagName("table")[0].getElementsByTagName("td").length;

            //** HINT Option 2: _driver.FindElements(By.XPath(...))  Google 'C# Selenium find all the columns in a table'

            return null;
        }


    }
}
