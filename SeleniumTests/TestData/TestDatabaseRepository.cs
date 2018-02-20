namespace SeleniumTests.TestData
{
    //This class isn't really going to hit any database, but imagine it is.  
    //Use this class as though it were being used for data driven tests
    public class TestDatabaseRepository
    {
        public string [] GetPatientListForPhysician(string userName)
        {
            return null;
        }

        public string GetLastCommentForPatient(string patientName)
        {
            return null;
        }

        public bool GetExistingCommentInTheDatabaseForPatient(string patientName, string expectedComment)
        {
            return true;
        }

        public void RollbackTestData()
        {
            //If this class were really going to hit a database(s), it would keep track of what the origianl state of the data was before the test ran
            //and would then roll the data back to what it was
        }
    }
}
