using SeleniumTests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SeleniumTests
{
    //***************************************************
    //UNCOMMENT [TestClass], and rebuild the project
    //***************************************************
    //[TestClass]
    public class PhysicianTests
    {
        TestDatabaseRepository _databaseRepo = new TestDatabaseRepository();

        [TestCleanup]
        public void RollBackTestData()
        {
            _databaseRepo.RollbackTestData();
        }

        [TestMethod]
        public void Physician_Can_Only_See_Their_Own_Patient()
        {            
            var expectedPhysicianPatientList = _databaseRepo.GetPatientListForPhysician("dr smith");
            
            //***Get List of patients displayed on the UI using Selenium
            string[] patientListDisplayed = null;

            //Assert that all of the patients found in the database (using the _databaseRepo) for dr smith are displayed on the UI
            //Assert no patients, OTHER THAN the ones assigned to dr. smith are displayed on the UI
        }

        [TestMethod]
        public void Phsician_Can_Update_Comments_From_Landing_Page_For_A_Given_Patient()
        {
            
        }
    }
}
