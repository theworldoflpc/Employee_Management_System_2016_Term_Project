/*
* FILE : TestHarness.cs
* PROJECT : INFO2180 - Software Quality I - Term Project - EMS 
* PROGRAMMERS : Ronnie Skowron, David Pitters, Carl Wilson, and Leveson Cocarell
* FIRST VERSION : 2016-11-17
* DESCRIPTION :
* The file contains the source code for the test harness for the EMS program.
* It is designed to contain test scripts that will handle different test data and test scenarios.
*
* NOTE: This test harness .cs file will be written after the basic stages of the EMS program have been 
* constructed. 
* 
* NOTE FROM CARL to group: This "class" will be turned into a "main" function and we will disclude it from the 
* project (or include it) depending on if we're testing or not.
*/



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCompany;
using Supporting;
using Presentation;
using System.Threading;

namespace EMSQualityOverload
{
    /// <summary>
    /// The file contains the source code for the test harness for the EMS program.
    /// It is designed to contain test scripts that will handle different test data and test scenarios.
    /// NOTE: This test harness .cs file will be written after the basic stages of the EMS program have been constructed. 
    /// <br>
    /// <b> Exception Strategy: </b> TBA
    /// </summary>
    class TestHarness
    {
        static void Main(string[] args)
        {

            ConsoleKeyInfo testMenuInput;

          

                do
                {

                    Console.Clear();
                    Console.WriteLine("Test Harness");
                    Console.WriteLine("\n 0. Test Container Pass");
                    Console.WriteLine("\n 1. Test Container Fail");
                    Console.WriteLine("\n 2. Test Fulltime Employee Pass");
                    Console.WriteLine("\n 3. Test Fulltime Employee Fail");
                    Console.WriteLine("\n 4. Test Parttime Employee Pass");
                    Console.WriteLine("\n 5. Test Parttime Employee Fail");
                    Console.WriteLine("\n 6. Test Contract Employee Pass");
                    Console.WriteLine("\n 7. Test Contract Employee Fail");
                    Console.WriteLine("\n 8. Test Seasonal Employee Pass");
                    Console.WriteLine("\n 9. Test Seasonal Employee Pass");
                    Console.WriteLine("\n q. Quit");

                     testMenuInput = Console.ReadKey();
                    Console.WriteLine(" ");
                    bool success;
                    switch (testMenuInput.KeyChar)
                    {
                        case '0':
                            success = testContainerPass();
                            if (success)
                            {
                                Console.WriteLine("The Container Pass Test Passed.");
                            }
                            else
                            {
                                Console.WriteLine("The Container Pass Test Failed.");
                            }
                            break;

                        case '1':
                            success = testContainerFail();
                            if (success)
                            {
                                Console.WriteLine("The Container Fail Test Passed.");

                            }
                            else
                            {
                                Console.WriteLine("The Container Fail Test Failed.");

                            }
                            break;

                        case '2':
                            success = testFTEmployeePass();
                            if (success)
                            {
                                Console.WriteLine("The FullTime Employee Pass Test Passed.");

                            }
                            else
                            {
                                Console.WriteLine("The FullTime Employee Pass Test Failed.");

                            }
                            break;

                        case '3':
                            success = testFTEmployeeFail();
                            if (success)
                            {
                                Console.WriteLine("The Full Time Employee Fail Test Passed.");

                            }
                            else
                            {
                                Console.WriteLine("The Full Time Employee Fail Test Failed.");

                            }
                            break;

                        case '4':
                            success = testPTEmployeePass();
                            if (success)
                            {
                                Console.WriteLine("The  Part Time  Employee Pass Test Passed.");

                            }
                            else
                            {
                                Console.WriteLine("The Part Time Employee Pass Test Failed.");

                            }
                            break;

                        case '5':
                            success = testPTEmployeeFail();
                            if (success)
                            {
                                Console.WriteLine("The  Part Time  Employee Fail Test Passed.");

                            }
                            else
                            {
                                Console.WriteLine("The  Part Time  Employee Fail Test Failed.");

                            }
                            break;

                        case '6':
                            success = testCEmployeePass();
                            if (success)
                            {
                                Console.WriteLine("The Contract Employee Pass Test Passed.");

                            }
                            else
                            {
                                Console.WriteLine("The Contract Employee Pass Test Failed.");

                            }
                            break;

                        case '7':
                            success = testCEmployeeFail();
                            if (success)
                            {
                                Console.WriteLine("The Contract Employee Fail Test Passed.");

                            }
                            else
                            {
                                Console.WriteLine("The Contract Employee Fail Test Failed.");

                            }
                            break;

                        case '8':
                            success = testSEmployeePass();
                            if (success)
                            {
                                Console.WriteLine("The Seasonal Employee Pass Test Passed.");

                            }
                            else
                            {
                                Console.WriteLine("The Seasonal Employee Pass Test Failed.");

                            }
                            break;

                        case '9':
                            success = testSEmployeeFail();
                            if (success)
                            {
                                Console.WriteLine("The Seasonal Employee Fail Test Passed.");

                            }
                            else
                            {
                                Console.WriteLine("The Seasonal Employee Fail Test Failed.");

                            }
                            break;
                        default:
                            break;
                    }
                  
                    Console.ReadKey();
                } while (testMenuInput.KeyChar != 'q');

               
            

            ////////////////////////////////////////Test the UIMenu


            ////////////////////////////////////////Test the Container (Employees) class


            ////////////////////////////////////////Test the Employees classes

        }

        /// <summary>
        /// Make sure the Container Methods succesfully passes if valid data is inputed 
        /// </summary>
        /// <returns> success: This bool determines if the test container passed </returns>
        public static bool testContainerPass()
        {
            bool success = true;
            bool addContractEmp = false;
            bool addFullTimeEmp = false;
            bool addPartTimeEmp = false;
            bool addSeasonalEmp = false;
            bool findEmployee = false;
            bool removeContractEmp = false;
            bool removeFullTimeEmp = false;
            bool removePartTimeEmp = false;
            bool removeSeasonalEmp = false;
            bool writeEmp = false;


            //Create the company
            // And create a happy path for all the methods in the Employees class.
            try
            {
                Employees company = new Employees();

                if(Employees.AddContractEmployee("","OmniCorp","603456789",Convert.ToDateTime("1960/12/24"), Convert.ToDateTime("2000/03/06"), Convert.ToDateTime("2002/11/05"),12000.00))
                {
                    addContractEmp = true;
                }
                if(Employees.AddFulltimeEmployee("Sean","Clarke", "123456789", Convert.ToDateTime("1960/12/24"), Convert.ToDateTime("2000/03/06"), Convert.ToDateTime("2002/11/05"), 12.00))
                {
                    addFullTimeEmp = true;
                }
                if (Employees.AddParttimeEmployee("Sean", "Clarke", "345678912", Convert.ToDateTime("1960/12/24"), Convert.ToDateTime("2000/03/06"), Convert.ToDateTime("2002/11/05"), 12.00))
                {
                    addPartTimeEmp = true;
                }
                if (Employees.AddSeasonalEmployee("Sean", "Clarke", "789123456", Convert.ToDateTime("1960/12/24"),"winter",12.00))
                {
                    addSeasonalEmp = true;
                }

                string foundEmp = Employees.FindAnEmployee("123456789");
                if (!string.IsNullOrWhiteSpace(foundEmp))
                {
                    findEmployee = true;
                }
                if(Employees.RemoveContractEmployee("603456789"))
                {
                    removeContractEmp = true;
                }
                if(Employees.RemoveFulltimeEmployee("123456789"))
                {
                    removeFullTimeEmp = true;
                }
                if(Employees.RemoveParttimeEmployee("345678912"))
                {
                    removePartTimeEmp = true;
                }
                if(Employees.RemoveSeasonalEmployee("789123456"))
                {
                    removeSeasonalEmp = true;
                }

                if(Employees.WriteEmployeesToDatabase())
                {
                    writeEmp = true;
                }

                // If any of the tests failed then this Container test failed.
                if(addContractEmp == false ||
                   addFullTimeEmp == false ||
                   addPartTimeEmp == false ||
                   addSeasonalEmp == false ||
                   findEmployee == false ||
                   removeContractEmp == false ||
                   removeFullTimeEmp == false ||
                   removePartTimeEmp == false||
                   removeSeasonalEmp == false||
                   writeEmp == false)
                {
                    throw new Exception();
                }


            }
            catch (Exception)
            {
                success = false;

            }
            return success;

        }

        /// <summary>
        /// Make sure the Container Methods succesfully fail if bad data is inputed 
        /// </summary>
        /// <returns> success: This bool determines if the test container failed </returns>
        public static bool testContainerFail()
        {
            bool success = false;
            bool addContractEmp = false;
            bool addFullTimeEmp = false;
            bool addPartTimeEmp = false;
            bool addSeasonalEmp = false;
            bool findEmployee = false;
            bool removeContractEmp = false;
            bool removeFullTimeEmp = false;
            bool removePartTimeEmp = false;
            bool removeSeasonalEmp = false;
            bool writeEmp = false;


            //Create the company
            // And create a failed path for all the methods in the Employees class.           
            try
            {
                Employees company = new Employees();

                if (!Employees.AddContractEmployee("Name", "OmniCorp", "603456789", Convert.ToDateTime("1959/12/24"), Convert.ToDateTime("2000/03/06"), Convert.ToDateTime("2002/11/05"), 12000.00))
                {
                    addContractEmp = false;
                }
                if (Employees.AddFulltimeEmployee("Sean", "Clar33ke", "123456789", Convert.ToDateTime("1960/24"), Convert.ToDateTime("2000/03/06"), Convert.ToDateTime("2002/11/05"), 12.00))
                {
                    addFullTimeEmp = false;
                }
                if (Employees.AddParttimeEmployee("Sea11n", "Clarke", "3458912", Convert.ToDateTime("1960/12/24"), Convert.ToDateTime("2000/03/06"), Convert.ToDateTime("2002/11/05"), 12.00))
                {
                    addPartTimeEmp = false;
                }
                if (Employees.AddSeasonalEmployee("Sean", "Clarke", "789123456", Convert.ToDateTime("1960/12/24"), "season", 12.00))
                {
                    addSeasonalEmp = false;
                }

                string foundEmp = Employees.FindAnEmployee("1234567");

                if (string.IsNullOrWhiteSpace(foundEmp))
                {
                    findEmployee = false;
                }
                if (Employees.RemoveContractEmployee("333456789"))
                {
                    removeContractEmp = false;
                }
                if (Employees.RemoveFulltimeEmployee("23456664"))
                {
                    removeFullTimeEmp = false;
                }
                if (Employees.RemoveParttimeEmployee("4433222344"))
                {
                    removePartTimeEmp = false;
                }
                if (Employees.RemoveSeasonalEmployee("5454545"))
                {
                    removeSeasonalEmp = false;
                }

                if (Employees.WriteEmployeesToDatabase())
                {
                    writeEmp = false;
                }

                // If any of the tests that are supposed to fail, pass. Then the test fails.
                if (addContractEmp == true ||
                   addFullTimeEmp == true ||
                   addPartTimeEmp == true ||
                   addSeasonalEmp == true ||
                   findEmployee == true ||
                   removeContractEmp == true ||
                   removeFullTimeEmp == true ||
                   removePartTimeEmp == true ||
                   removeSeasonalEmp == true ||
                   writeEmp == true)
                {
                    throw new Exception();
                }


            }
            catch (Exception)
            {
                success = true;

            }
            return success;

        }

        /// <summary>
        /// Make sure the Fulltime employee allows valid data
        /// </summary>
        /// <returns> success: This bool determines if the Full time Employee test passed </returns>
        public static bool testFTEmployeePass()
        {
            bool success = false;
            bool addFullTimeEmp = false;
            bool removeFullTimeEmp = false;

            try
            {

                Employees company = new Employees();
                
                if (Employees.AddFulltimeEmployee("Sean", "Clarke", "123456789", Convert.ToDateTime("1960/12/24"), Convert.ToDateTime("2000/03/06"), Convert.ToDateTime("2002/11/05"), 12.00))
                {
                    addFullTimeEmp = true;
                }
                if (Employees.RemoveFulltimeEmployee("123456789"))
                {
                    removeFullTimeEmp = true;
                }

                if (addFullTimeEmp == true ||
                    removeFullTimeEmp == true )
                {
                    success = true;
                }



            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        /// <summary>
        /// Make sure the Full time employee succesfully fails if bad data is inputed 
        /// </summary>
        /// <returns> success: This bool determines if the Full time Employee test failed </returns>
        public static bool testFTEmployeeFail()
        {
            bool success = false;
            bool addFullTimeEmp = false;
            bool removeFullTimeEmp = false;

            try
            {

                Employees company = new Employees();

                if (!Employees.AddFulltimeEmployee("Se1an", "Clarke", "123456789", Convert.ToDateTime("1960/12/24"), Convert.ToDateTime("2000/03/06"), Convert.ToDateTime("2002/11/05"), 12.00))
                {
                    addFullTimeEmp = true;
                }
                if (!Employees.RemoveFulltimeEmployee("1234536789"))
                {
                    removeFullTimeEmp = true;
                }

                if (addFullTimeEmp == true ||
                    removeFullTimeEmp == true)
                {
                    success = true;
                }



            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        /// <summary>
        /// Make sure the Part Time employee allows valid data
        /// </summary>
        /// <returns> success: This bool determines if the Part Time Employee test passed </returns>
        public static bool testPTEmployeePass()
        {
            bool success = false;
            bool addPartTimeEmp = false;
            bool removePartTimeEmp = false;

            try
            {

                Employees company = new Employees();

                if (Employees.AddParttimeEmployee("Sean", "Clarke", "345678912", Convert.ToDateTime("1960/12/24"), Convert.ToDateTime("2000/03/06"), Convert.ToDateTime("2002/11/05"), 12.00))
                {
                    addPartTimeEmp = true;
                }
                if (Employees.RemoveParttimeEmployee("345678912"))
                {
                    removePartTimeEmp = true;
                }

                if (addPartTimeEmp == true ||
                    removePartTimeEmp == true)
                {
                    success = true;
                }



            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        /// <summary>
        /// Make sure the Part Time employee  succesfully fails if bad data is inputed 
        /// </summary>
        /// <returns> success: This bool determines if the Part Time Employee test failed </returns>
        public static bool testPTEmployeeFail()
        {
            bool success = false;
            bool addPartTimeEmp = false;
            bool removePartTimeEmp = false;

            try
            {

                Employees company = new Employees();

                if (!Employees.AddParttimeEmployee("S4ean", "Clarke", "345678912", Convert.ToDateTime("1960/12/24"), Convert.ToDateTime("2000/03/06"), Convert.ToDateTime("2002/11/05"), 12.00))
                {
                    addPartTimeEmp = true;
                }
                if (!Employees.RemoveParttimeEmployee("34567f8912"))
                {
                    removePartTimeEmp = true;
                }

                if (addPartTimeEmp == true ||
                    removePartTimeEmp == true)
                {
                    success = true;
                }



            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        /// <summary>
        /// Make sure the Contract employee allows valid data
        /// </summary>
        /// <returns> success: This bool determines if the Contract Employee test passed </returns>
        public static bool testCEmployeePass()
        {
            bool success = false;
            bool addContractEmp = false;
            bool removeContractEmp = false;

            try
            {

                Employees company = new Employees();

                if (Employees.AddContractEmployee("", "OmniCorp", "603456789", Convert.ToDateTime("1960/12/24"), Convert.ToDateTime("2000/03/06"), Convert.ToDateTime("2002/11/05"), 12000.00))
                {
                    addContractEmp = true;
                }
                if (Employees.RemoveContractEmployee("603456789"))
                {
                    removeContractEmp = true;
                }

                if (addContractEmp == true ||
                    removeContractEmp == true)
                {
                    success = true;
                }



            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        /// <summary>
        /// Make sure the Contract employee succesfully fails if bad data is inputed 
        /// </summary>
        /// <returns> success: This bool determines if the Contract Employee test failed </returns>
        public static bool testCEmployeeFail()
        {
            bool success = false;
            bool addContractEmp = false;
            bool removeContractEmp = false;

            try
            {

                Employees company = new Employees();

                if (!Employees.AddContractEmployee("billy", "OmniCorp", "603456789", Convert.ToDateTime("1960/12/24"), Convert.ToDateTime("2000/03/06"), Convert.ToDateTime("2002/11/05"), 12000.00))
                {
                    addContractEmp = true;
                }
                if (!Employees.RemoveContractEmployee("503456789"))
                {
                    removeContractEmp = true;
                }

                if (addContractEmp == true ||
                    removeContractEmp == true)
                {
                    success = true;
                }



            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        
    }

        /// <summary>
        /// Make sure the Seasonal employee allows valid data
        /// </summary>
        /// <returns> success: This bool determines if the Seasonal Employee test passed </returns>
        public static bool testSEmployeePass()
        {
            bool success = false;
            bool addSeasonalEmp = false;
            bool removeSeasonalEmp = false;

            try
            {

                Employees company = new Employees();

                if (Employees.AddSeasonalEmployee("Sean", "Clarke", "789123456", Convert.ToDateTime("1960/12/24"), "winter", 12.00))
                {
                    addSeasonalEmp = true;
                }
                if (Employees.RemoveSeasonalEmployee("789123456"))
                {
                    removeSeasonalEmp = true;
                }

                if (addSeasonalEmp == true ||
                    removeSeasonalEmp == true)
                {
                    success = true;
                }



            }
            catch (Exception)
            {
                success = false;
            }
            return success;

        }

        /// <summary>
        /// Make sure the Seasonal employee succesfully fails if bad data is inputed 
        /// </summary>
        /// <returns> success: This bool determines if the Seasonal Employee test failed </returns>
        public static bool testSEmployeeFail()
        {
            bool success = false;
            bool addSeasonalEmp = false;
            bool removeSeasonalEmp = false;

            try
            {

                Employees company = new Employees();

                if (!Employees.AddSeasonalEmployee("Sean", "Clarke", "789123456", Convert.ToDateTime("1960/12/24"), "w3inter", 12.00))
                {
                    addSeasonalEmp = true;
                }
                if (!Employees.RemoveSeasonalEmployee("78912f3456"))
                {
                    removeSeasonalEmp = true;
                }

                if (addSeasonalEmp == true ||
                    removeSeasonalEmp == true)
                {
                    success = true;
                }



            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }
    }
}
