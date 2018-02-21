using System;

namespace SeleniumTests.TestData
{
    //*************************************************************************************
    //** NOTE: This class isn't really going to hit any real database, but imagine it is.  
    //         Use this class as though it were being used for data driven tests
    //*************************************************************************************
    public class TestDatabaseRepository
    {
        public string [] GetPatientListForPhysician(string userName)
        {
            return new string[] { "John Travolta", "Angelena Jolie" };
        }

        public bool CheckIfPatientWasCreatedInDatabase(string patientFirstName, string patientLastName, DateTime DOB)
        {
            //** NOTE: If this were a real database repo, this code would query the database and check if the record was created in the db
            return true;
        }      

        public void RollbackTestData()
        {
            //** NOTE: If this class were really going to hit a database(s), it would keep track of what the origianl state of the data
            //         was before the test ran and would then roll the data back to what it was
        }
    }
}
