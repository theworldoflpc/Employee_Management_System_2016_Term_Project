/*
* FILE : EMSUnitTest.cs
* PROJECT : INFO2180 - Software Quality I - Term Project - EMS 
* PROGRAMMERS : RSkowron, DPitters, CWilson, and lpcSetProg
* FIRST VERSION : 2016-27-17
* DESCRIPTION :
* This file contains the source code for the Unit Test Project that accompanies the unit 
* test phase of the EMS software development life cycle. 
*/

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AllEmployees;
using Presentation;
using Supporting;
using TheCompany;

namespace Presentation.Tests
{

    ///
    /// <summary>   (Unit Test Class) the ems unit test. </summary>
    ///
    /// <remarks>   Lcoca, 12/2/2016. </remarks>
    ///


    [TestClass()]
    public class EMSUnitTest
    {

        //MAIN MENU unit tests

        ///
        ///<summary>
        ///Test Name: MainMenuTest_Success()
        ///
        ///Test Description: (Unit Test Method) tests main menu.
        ///
        ///Type of Test: MANUAL
        ///
        ///
        ///Sample Data for Test:
        ///
        ///
        ///Expected Results:
        ///
        ///
        ///Actual Results: TEST OUTCOME:
        ///</summary>
        ///
        [TestMethod()]
        public void MainMenuTest_Success()
        {
           
        }


        
        // Specify Full Time Details unit tests




        ///
        /// <summary>
        /// Test Name: SpecifyFullTimeDetailsTest_AssertFail()
        /// 
        /// Test Description:  (Unit Test Method) tests specify full time details.
        /// 
        /// Type of Test: Automatic
        /// 
        /// Sample Data for Test:
        /// 
        /// 
        /// Expected Results:
        /// 
        /// 
        /// Actual Results: TEST OUTCOME: 
        /// </summary>
        ///
        [TestMethod()]
        public void SpecifyFullTimeDetailsTest_AssertFail()
        {
            Assert.Fail();
        }




        //  Specify Seasonal Details unit tests








//  Date Check unit tests


///
/// <summary>
/// Test Name: DateCheckTest_Success()
/// 
/// Test Description: (Unit Test Method) tests if DateCheck validates different types of dates correctly.
/// 
/// Type of Test: MANUAL 
/// 
/// Sample Data for Test: string date="02/18/1996"
/// 
/// Expected Results: Success
/// 
/// 
/// Actual Results: TEST OUTCOME: Success
/// </summary>
///
/* OLD DATE CHECKER
[TestMethod()]
public void DateCheckTest_Success()
{
    //happy path
    string date = "02/18/1996";
    UIMenu tstMenu = new UIMenu();
    bool success = false;
    tstMenu.DateCheck(date, "Birth", out success);
    Assert.IsTrue(success);
}*/


///
/// <summary>
/// Test Name: DateCheckTest_Exception()
/// 
/// Test Description: (Unit Test Method) tests that DateCheck throws an Exception if invalid Date Format is given. 
/// 
/// Type of Test: Automatic
/// 
/// Sample Data for Test: "123456", "Birth", out success
/// 
/// Expected Results: InvalidOperactionException thrown with message "String was not recognized as a valid DateTime."
/// 
/// 
/// Actual Results: TEST OUTCOME: InvalidOperactionException thrown with message "String was not recognized as a valid DateTime."
/// </summary>
///
/* OLD DATE CHECKER
[TestMethod()]
[ExpectedException(typeof(InvalidOperationException), //
    "String was not recognized as a valid DateTime.")]
public void DateCheckTest_Exception()
{
    UIMenu tstMenu = new UIMenu();
    bool success = false;
    tstMenu.DateCheck("123456", "Birth", out success);
}*/


///
/// <summary>
/// Test Name: DateCheckTest_AssertFail()
/// 
/// Test Description: (Unit Test Method) tests specify seasonal details.
/// 
/// Type of Test: Automatic
/// 
/// Sample Data for Test:
/// 
/// Expected Results:
/// 
/// 
/// Actual Results: TEST OUTCOME: 
/// </summary>
///
[TestMethod()]
public void DateCheckTest_AssertFail()
{
    Assert.Fail();
}

}
}





namespace AllEmployees.Tests
{
[TestClass()]
public class EMSUnitTest
{

//Contract employee unit tests  +

///
/// <summary>
/// Test Name: ContractEmployeeTest_Success()
/// 
/// Test Description: (Unit Test Method) tests contract employee validate.
/// 
/// Type of Test: MANUAL
/// 
/// Sample Data for Test: "Ronnie", "Skowron","123456789",DateTime.Parse("2004-08-17"), DateTime.Parse("2006-08-14"), DateTime.Parse("2010-11-12"),1600.00
/// 
/// Expected Results: Success
/// 
/// 
/// Actual Results: Success TEST OUTCOME: Success
/// </summary>
///
[TestMethod()]
public void ContractEmployeeTest_Success()
{
    ContractEmployee contractEmp = new ContractEmployee("Ronnie", "Skowron","123456789",DateTime.Parse("2004-08-17"), DateTime.Parse("2006-08-14"), DateTime.Parse("2010-11-12"),1600.00);
    bool success = contractEmp.Validate();
    Assert.IsTrue(success);

}


///
/// <summary>
/// Test Name: ContractEmployeeTest_Exception() +
/// 
/// Test Description: (Unit Test Method) tests that contract employee has proper value for FixedContractAmount.
/// 
/// Type of Test: Automatic
/// 
/// Sample Data for Test: "David", "Pitters", "123456789", DateTime.Parse("1996-02-18"), DateTime.Parse("2001-05-20"), DateTime.Parse("2012-12-21"), -1
/// 
/// Expected Results: ArgumentException thrown with message "Fixed Contract Amount cannot be negative"
/// 
/// 
/// Actual Results: TEST OUTCOME: ArgumentException thrown with message "Fixed Contract Amount cannot be negative"
/// </summary>
///
[TestMethod()]
[ExpectedException(typeof(ArgumentException), 
    "Fixed Contract Amount cannot be negative")]
public void ContractEmployeeTest_Exception()
{
    ContractEmployee cEmp = new ContractEmployee("David", "Pitters", "123456789", DateTime.Parse("1996-02-18"), DateTime.Parse("2001-05-20"), DateTime.Parse("2012-12-21"), -1);
}






// Full time employee validation unit tests


///
/// <summary>
/// Test Name: FulltimeEmployeeTest_Success()
/// 
/// Test Description: (Unit Test Method) tests contract employee validate.
/// 
/// Type of Test: MANUAL
/// 
/// Sample Data for Test: "Ronnie", "Skowron", "123456789", DateTime.Parse("2004-08-17"), DateTime.Parse("2006-08-14"), DateTime.Parse("2010-11-12"), 16
/// 
/// Expected Results: Success
/// 
/// 
/// Actual Results: Success TEST OUTCOME: Success
/// </summary>
/// 
[TestMethod()]
public void FulltimeEmployeeTest_Success()
{
    FulltimeEmployee fullTimeEmp = new FulltimeEmployee("Ronnie", "Skowron", "123456789", DateTime.Parse("2004-08-17"), DateTime.Parse("2006-08-14"), DateTime.Parse("2010-11-12"), 16);

    bool success =fullTimeEmp.Validate();
    Assert.IsTrue(success);

}



///
/// <summary>
/// Test Name: FulltimeEmployeeTest_Exception() +
/// 
/// Test Description: (Unit Test Method) tests that fulltime employee has proper value for Salary
/// 
/// Type of Test: Automatic
/// 
/// Sample Data for Test: "David", "Pitters", "123456789", DateTime.Parse("1996-02-18"), DateTime.Parse("2001-05-20"), DateTime.Parse("2012-12-21"), -1
/// 
/// Expected Results: ArgumentException thrown with message "Salary cannot be negative"
/// 
/// 
/// Actual Results: Success TEST OUTCOME: ArgumentException thrown with message "Salary cannot be negative"
/// </summary>
/// 
[TestMethod()]
[ExpectedException(typeof(ArgumentException),
    "Salary cannot be negative")]
public void FulltimeEmployeeTest_Exception()
{
    FulltimeEmployee fEmp = new FulltimeEmployee("David", "Pitters", "123456789", DateTime.Parse("1996-02-18"), DateTime.Parse("2001-05-20"), DateTime.Parse("2012-12-21"), -1);
}




// Part time employee validation unit tests


///
/// <summary>
/// Test Name: ParttimeEmployeeValidateTest_Success() +
/// 
/// Test Description: (Unit Test Method) tests contract employee validate.
/// 
/// Type of Test: MANUAL 
/// 
/// Sample Data for Test: "Ronnie", "Skowron", "123456789", DateTime.Parse("2004-08-17"), DateTime.Parse("2006-08-14"), DateTime.Parse("2010-11-12"), 17.95
/// 
/// Expected Results: Success
/// 
/// 
/// Actual Results: Success TEST OUTCOME: Success
/// </summary>
/// 
[TestMethod()]
public void ParttimeEmployeeTest_Success()
{
    ParttimeEmployee partTimeEmp = new ParttimeEmployee("Ronnie", "Skowron", "123456789", DateTime.Parse("2004-08-17"), DateTime.Parse("2006-08-14"), DateTime.Parse("2010-11-12"),17.95);
    bool success = partTimeEmp.Validate();
    Assert.IsTrue(success);
}


///
/// <summary>
/// Test Name: ParttimeEmployeeValidateTest_Exception() +
/// 
/// Test Description: (Unit Test Method) tests that fulltime employee has proper value for HourlyRate.
/// 
/// Type of Test: Automatic
/// 
/// Sample Data for Test: "David", "Pitters", "123456789", DateTime.Parse("1996-02-18"), DateTime.Parse("2001-05-20"), DateTime.Parse("2012-12-21"), -1.00
/// 
/// Expected Results: ArgumentException thrown with message "Hourly Rate cannot be negative"
/// 
/// 
/// Actual Results: TEST OUTCOME: ArgumentException thrown with message "Hourly Rate cannot be negative"
/// </summary>
[TestMethod()]
[ExpectedException(typeof(ArgumentException),
    "Hourly Rate cannot be negative")]
public void ParttimeEmployeeTest_Exception()
{
    ParttimeEmployee pEmp = new ParttimeEmployee("David", "Pitters", "123456789", DateTime.Parse("1996-02-18"), DateTime.Parse("2001-05-20"), DateTime.Parse("2012-12-21"), -1.00);
}




///
/// <summary>
/// Test Name: ParttimeEmployeeValidateTest_AssertFail() +
/// 
/// Test Description: (Unit Test Method) tests contract employee validate.
/// 
/// Type of Test: Automatic
/// 
/// Sample Data for Test: N/A
/// 
/// Expected Results: Tests failed
/// 
/// 
/// Actual Results: TEST OUTCOME: Test failed
/// </summary>
///
[TestMethod()]
public void ParttimeEmployeeValidateTest_AssertFail()
{
    Assert.Fail();
}




// Seasonal employee validation unit tests


///
/// <summary>
/// Test Name: SeasonalEmployeeValidateTest_Success()
/// 
/// Test Description: (Unit Test Method) tests seasonal employee validate.
/// 
/// Type of Test: MANUAL 
/// 
/// Sample Data for Test: "Ronnie", "Skowron", "123456789", DateTime.Parse("2004-08-17"), 20.00, "Winter"
/// 
/// Expected Results: Success
/// 
/// 
/// Actual Results: Success TEST OUTCOME: Success
/// </summary>
///
[TestMethod()]
public void SeasonalEmployeeTest_Success()
{
    SeasonalEmployee seasonalEmp = new SeasonalEmployee("Ronnie", "Skowron", "123456789", DateTime.Parse("2004-08-17"), 20.00, "Winter");
    bool success = seasonalEmp.Validate();
    Assert.IsTrue(success);
}


///
/// <summary>
/// Test Name: SeasonalEmployeeValidateTest_Exception()
/// 
/// Test Description: (Unit Test Method) tests that fulltime employee has proper value for PiecePay.
/// 
/// Type of Test: Automatic 
/// 
/// Sample Data for Test: "David", "Pitters", "123456789", DateTime.Parse("1996-02-18"), -1.00, "Summer" 
/// 
/// Expected Results: ArgumentException thrown with message "PiecePay cannot be negative"
/// 
/// 
/// Actual Results: TEST OUTCOME: ArgumentException thrown with message "PiecePay cannot be negative"
/// </summary>
[TestMethod()]
[ExpectedException(typeof(ArgumentException),
    "PiecePay cannot be negative")]
public void SeasonalEmployeeTest_Exception()
{
    SeasonalEmployee sEmp = new SeasonalEmployee("David", "Pitters", "123456789", DateTime.Parse("1996-02-18"), -1.00, "Summer");
}




}
}






namespace Supporting.Tests
{
[TestClass()]
public class EMSUnitTest
{


// logging class unit tests


///
/// <summary>
/// Test Name: LoggingTest_Success()
/// 
/// Test Description: Unit Test Method) tests logging.
/// 
/// Type of Test: Manual
/// 
/// Sample Data for Test: "TestMessage", "TestClass", "TestMethod"
/// 
/// Expected Results: success
/// 
/// 
/// Actual Results: TEST OUTCOME: success
/// </summary>
///
[TestMethod()]
public void LoggingTest_Success()
{
    try
    {
        Logging log = new Logging();
        log.Log("TestMessage", "TestClass", "TestMethod");
    }
    catch(Exception)
    {
        Assert.Fail();
    }
}



///
/// <summary>
/// Test Name: LoggingTest_Exception()
/// 
/// Test Description: Unit Test Method) tests logging.
/// 
/// Type of Test: Automatic
/// 
/// Sample Data for Test: N/A
/// 
/// Expected Results: Fail
/// 
/// 
/// Actual Results: TEST OUTCOME: Fail
/// </summary>
///
[TestMethod()]
public void LoggingTest_Exception()
{
    throw new NotImplementedException();
}



// Write database unit tests


///
/// <summary>
/// Test Name: WriteDatabaseTest_Success()
/// 
/// Test Description: (Unit Test Method) tests write database.
/// 
/// Type of Test: MANUAL 
/// 
/// Sample Data for Test: "123456789"
/// 
/// Expected Results: Success
/// 
/// 
/// Actual Results: TEST OUTCOME: 
/// </summary>
///
[TestMethod()]
public void WriteDatabaseTest_Success()
{



}









}
}






namespace TheCompany.Tests
{
[TestClass()]
public class EMSUnitTest
{

// Add full time employee unit tests


///
/// <summary>
/// Test Name: AddFulltimeEmployeeTest_Success()
/// 
/// Test Description:  (Unit Test Method) adds fulltime employee test. 
/// 
/// Type of Test: Manual
/// 
/// Sample Data for Test: "Carl", "Wilson", "123456789", DateTime.Parse("2004-08-17"), DateTime.Parse("2006-08-14"), DateTime.Parse("2010-11-12"), 16
/// 
/// Expected Results: success
/// 
/// 
/// Actual Results: TEST OUTCOME: success
/// </summary>
///
[TestMethod()]
public void AddFulltimeEmployeeTest_Success()
{
    bool success = false;
    success = Employees.AddFulltimeEmployee("Carl", "Wilson", "123456789", DateTime.Parse("2004-08-17"), DateTime.Parse("2006-08-14"), DateTime.Parse("2010-11-12"), 16);
    Assert.IsTrue(success);
}







// Add part time employee unit tests


///
/// <summary>
/// Test Name: AddParttimeEmployeeTest_Success()
/// 
/// Test Description: (Unit Test Method) adds parttime employee test.
/// 
/// Type of Test: MANUAL
/// 
/// Sample Data for Test: "Carl", "Wilson", "123456789", DateTime.Parse("2004-08-17"), DateTime.Parse("2006-08-14"), DateTime.Parse("2010-11-12"), 16
/// 
/// Expected Results: success
/// 
/// 
/// Actual Results: TEST OUTCOME: success
/// </summary>
///
[TestMethod()]
public void AddParttimeEmployeeTest_Success()
{
    bool success = false;
    success = Employees.AddParttimeEmployee("Carl", "Wilson", "123456789", DateTime.Parse("2004-08-17"), DateTime.Parse("2006-08-14"), DateTime.Parse("2010-11-12"), 16);
    Assert.IsTrue(success);
}





// Add contract employee unit tests

///
/// <summary>
/// Test Name: AddContractEmployeeTest_Success()
/// 
/// Test Description:  (Unit Test Method) adds contract employee test.
/// 
/// Type of Test: Manual
/// 
/// Sample Data for Test: "Carl", "Wilson", "123456789", DateTime.Parse("2004-08-17"), DateTime.Parse("2006-08-14"), DateTime.Parse("2010-11-12"), 16
/// 
/// Expected Results: success
/// 
/// 
/// Actual Results: TEST OUTCOME: success
/// </summary>
///
[TestMethod()]
public void AddContractEmployeeTest_Success()
{
    bool success = false;
    success = Employees.AddContractEmployee("Carl", "Wilson", "123456789", DateTime.Parse("2004-08-17"), DateTime.Parse("2006-08-14"), DateTime.Parse("2010-11-12"), 16);
    Assert.IsTrue(success);
}







// Add Seasonal Employee Unit Tests

///
/// <summary>
/// Test Name: AddSeasonalEmployeeTest_Success()
/// 
/// Test Description:  (Unit Test Method) adds contract employee test.
/// 
/// Type of Test: Manual
/// 
/// Sample Data for Test: "Carl", "Wilson", "123456789", DateTime.Parse("2004-08-17"), "Winter", 16
/// 
/// Expected Results: success
/// 
/// 
/// Actual Results: TEST OUTCOME: success
/// </summary>
///
[TestMethod()]
public void AddSeasonalEmployeeTest_Success()
{
    bool success = false;
    success = Employees.AddSeasonalEmployee("Carl", "Wilson", "123456789", DateTime.Parse("2004-08-17"), "Winter", 16);
    Assert.IsTrue(success);
}








// Remove Full Time Employees Unit Tests


///
/// <summary>   
/// Test Name: RemoveFulltimeEmployeeTest_Success()
/// 
/// Test Description: (Unit Test Method) removes the fulltime employee test.
/// 
/// Type of Test: MANUAL
/// 
/// Sample Data for Test: "123456789"
/// 
/// Expected Results: success
/// 
/// 
/// Actual Results: TEST OUTCOME: success
/// </summary>
///
[TestMethod()]
public void RemoveFulltimeEmployeeTest_Success()
{
    bool success = false;
    Employees.AddFulltimeEmployee("Carl", "Wilson", "123456789", DateTime.Parse("2004-08-17"), DateTime.Parse("2006-08-14"), DateTime.Parse("2010-11-12"), 16);
    success = Employees.RemoveFulltimeEmployee("123456789");
    Assert.IsTrue(success);
}




// Remove contract employee unit tests
///
/// <summary>   
/// Test Name: RemoveContractEmployeeTest_Success()
/// 
/// Test Description:  (Unit Test Method) removes the contract employee test.
/// 
/// Type of Test: Manual
/// 
/// Sample Data for Test:
/// 
/// Expected Results:
/// 
/// 
/// Actual Results: TEST OUTCOME:
/// </summary>
///
[TestMethod()]
public void RemoveContractEmployeeTest_Success()
{

}


// Remove seasonal employee unit tests

///
/// <summary>   
/// Test Name: RemoveSeasonalEmployeeTest_Success()
/// 
/// Test Description:  (Unit Test Method) removes the seasonal employee test.
/// 
/// Type of Test: Manual
/// 
/// Sample Data for Test: "12345678"
/// 
/// Expected Results: Success
/// 
/// 
/// Actual Results: TEST OUTCOME:
/// </summary>
///
[TestMethod()]
public void RemoveSeasonalEmployeeTest_Success()
{

}





}
}




///
// namespace: EMSUnitTestProject
//
// summary:	/.
///

namespace EMSUnitTestProject
{
///
/// <summary>   (Unit Test Class) the ems unit test. </summary>
///
///
///
[TestClass]
public class EMSUnitTest
{



}
}
