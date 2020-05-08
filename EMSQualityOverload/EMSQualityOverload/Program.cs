/*
* FILE : Program.cs
* PROJECT : INFO2180 - Software Quality I - Term Project - EMS 
* PROGRAMMERS : Ronnie Skowron, David Pitters, Carl Wilson, and Leveson Cocarell
* FIRST VERSION : 2016-11-17
* DESCRIPTION :
* This file contains the main entry point to the EMS program. From here 
* the user interface console menu will be instantiated and the user can run the EMS. 
* This file invokes the ReadAll() method of the FileIO class, which will insert all 
* existing files in the database into the employee class
*/

using System;
using Presentation;
using TheCompany;
using Supporting;
using AllEmployees;
using System.Collections;
using System.Collections.Generic;

namespace EMSQualityOverload
{
    /// <summary>
    /// This is the main processing component of the system.
    /// This class acts as the Business Layer. It is dependant on TheCompany class
    /// for functionality. 
    /// <BR>
    /// <b>Exception Strategy:</b> The validation in this program class is built on validating and confirming //
    /// user key inputs.
    /// </summary>
    class Program
    {

        ///
        ///<summary> Main entry-point for this application. </summary>
        ///
        ///<param name="args">   An array of command-line argument strings. </param>
        ///
        static void Main(string[] args)
        {
            

            UIMenu userInterface = new UIMenu();

            ConsoleKeyInfo mainMenuInput;
            ConsoleKeyInfo input;
            ConsoleKeyInfo menuFourInput;

            string localEmployee = null; //This is to hold the information of a basic employee that has not had its details specified
                                         //Rather than making Employee class non-abstract and instantiating one, just keep the basic details in a string

            Console.ForegroundColor = ConsoleColor.White;

            //Load the flatfile database on startup
            Employees.ReadDatabase();

            while (true)
            {


                mainMenuInput = userInterface.MainMenu(); // Display and get the key pressed when on the main menu

                // Check what option was picked
                // NOTE: The user will pretty much loop IN the switch statement
                switch (mainMenuInput.KeyChar)
                {
                    case '1':   // if one was picked then loop the user in menu 2 until they press 9

                        do
                        {

                            input = userInterface.FileManagementMenu(); // Display menu 2 and get the input.
                            if (input.KeyChar == '1')
                            {
                                userInterface.CustomPrint("Are you sure you want to overwrite the current Employee Set? Y/N", false);
                                ConsoleKeyInfo choice = Console.ReadKey(true);
                                if (choice.KeyChar == 'Y' || choice.KeyChar == 'y')
                                {
                                    Employees.ReadDatabase();
                                    userInterface.CustomPrint("Employees read from database.", true);
                                }
                            }
                            else if (input.KeyChar == '2')
                            {
                                userInterface.CustomPrint("Are you sure you want to overwrite the database? Y/N", false);
                                ConsoleKeyInfo choice = Console.ReadKey(true);
                                if (choice.KeyChar == 'Y' || choice.KeyChar == 'y')
                                {
                                    Employees.WriteEmployeesToDatabase();
                                    userInterface.CustomPrint("Employees saved to database.", true);
                                }
                            }
                        } while (input.KeyChar != '9');

                        break;

                    case '2': // if one was picked then loop the user in menu 3 until they press 9

                        do
                        {

                            input = userInterface.EmpManagementMenu(); // Display menu 3 and return the option they chose
                            if (input.KeyChar == '1') //Display employee set
                            {
                                int currentPage = 1;
                                do
                                {
                                    ConsoleKeyInfo navigator;
                                    //Get every group of employees by type
                                    List<ContractEmployee> cEmps = Employees.GetContractEmployees();
                                    List<FulltimeEmployee> fEmps = Employees.GetFulltimeEmployees();
                                    List<ParttimeEmployee> pEmps = Employees.GetParttimeEmployees();
                                    List<SeasonalEmployee> sEmps = Employees.GetSeasonalEmployees();

                                    //Paginate the displaying of employee set to 1 page per employee type.
                                    string detailsToPrint = "";
                                    if (currentPage == 1)
                                    {
                                        Console.Clear();
                                        userInterface.CustomPrint("CONTRACT EMPLOYEES\n======================", false);
                                        foreach (ContractEmployee ce in cEmps)
                                        {
                                            detailsToPrint = ce.Details();
                                            userInterface.CustomPrint(detailsToPrint, false);
                                            userInterface.CustomPrint("", false);
                                        }
                                        userInterface.CustomPrint("", false);
                                        userInterface.CustomPrint("Navigate -> (Right Arrow)\nPress q to return to Employee Management Menu", false);
                                        Console.SetCursorPosition(0, 0);
                                    }
                                    else if (currentPage == 2)
                                    {
                                        Console.Clear();
                                        userInterface.CustomPrint("FULLTIME EMPLOYEES\n======================", false);
                                        foreach (FulltimeEmployee fe in fEmps)
                                        {
                                            detailsToPrint = fe.Details();
                                            userInterface.CustomPrint(detailsToPrint, false);
                                            userInterface.CustomPrint("", false);
                                        }
                                        userInterface.CustomPrint("", false);
                                        userInterface.CustomPrint("(Left Arrow) <- Navigate -> (Right Arrow)\nPress q to return to Employee Management Menu", false);
                                        Console.SetCursorPosition(0, 0);
                                    }
                                    else if (currentPage == 3)
                                    {
                                        Console.Clear();
                                        userInterface.CustomPrint("PARTTIME EMPLOYEES\n======================", false);
                                        foreach (ParttimeEmployee pe in pEmps)
                                        {
                                            detailsToPrint = pe.Details();
                                            userInterface.CustomPrint(detailsToPrint, false);
                                            userInterface.CustomPrint("", false);
                                        }
                                        userInterface.CustomPrint("", false);
                                        userInterface.CustomPrint("(Left Arrow) <- Navigate -> (Right Arrow)\nPress q to return to Employee Management Menu", false);
                                        Console.SetCursorPosition(0, 0);
                                    }
                                    else if (currentPage == 4)
                                    {
                                        Console.Clear();
                                        userInterface.CustomPrint("SEASONAL EMPLOYEES\n======================", false);
                                        foreach (SeasonalEmployee se in sEmps)
                                        {
                                            detailsToPrint = se.Details();
                                            userInterface.CustomPrint(detailsToPrint, false);
                                            userInterface.CustomPrint("", false);
                                        }
                                        userInterface.CustomPrint("", false);
                                        userInterface.CustomPrint("(Left Arrow) <- Navigate\nPress q to return to Employee Management Menu", false);
                                        Console.SetCursorPosition(0, 0);
                                    }
                                    navigator = Console.ReadKey();
                                    //If the user wants to go to the next page, increase current page by 1
                                    if (navigator.Key.Equals(ConsoleKey.RightArrow))
                                    {
                                        currentPage++;
                                    }
                                    //If the user wants to go back, decrease the current page by 1.
                                    else if (navigator.Key.Equals(ConsoleKey.LeftArrow))
                                    {
                                        currentPage--;
                                    }
                                    else if (navigator.KeyChar == 'q')
                                    {
                                        break;
                                    }
                                } while(currentPage != 0 && currentPage != 5); //If the user goes 1 page past either way, exit
                            }
                            else if (input.KeyChar == '2' || input.KeyChar == '3') // Create/Modify Employee
                            {
                                do 
                                {
                                    if (input.KeyChar == '2') //They chose CREATE NEW EMPLOYEE
                                    {
                                        menuFourInput = userInterface.EmpDetailsMenu(false); // Display menu 4 and return the option they chose
                                        if (menuFourInput.KeyChar == '1') // Specify base details
                                        {
                                            string employeeDetails = "";
                                            bool cont = false; //did the user add type-specific details as well?
                                            employeeDetails = userInterface.SpecifyBaseDetails(out cont, null); //Get the BASE Details from the user
                                            if (cont)
                                            {
                                                //Copy the details and find the employee type by parsing out the pipes
                                                string detailsCopy = employeeDetails;
                                                bool employeeWasAdded = false;
                                                employeeWasAdded = AddAnEmployee(detailsCopy);
                                                if (!employeeWasAdded)
                                                {
                                                    userInterface.CustomPrint("Employee was invalid and was not added to list of employees.", true);
                                                    localEmployee = employeeDetails;
                                                    Employees.AddInvalidEmployee(employeeDetails); //the employee was not added because it is invalid. add it to the list
                                                }
                                                else
                                                {
                                                    userInterface.CustomPrint("Employee was added to list of employees.", true);
                                                    //erase the local employee since it had details specified and was added
                                                    localEmployee = "";
                                                }
                                            }
                                            else
                                            {
                                                localEmployee = employeeDetails;
                                                userInterface.CustomPrint("Base details for Invalidated Employee stored for later.\nTo continue specifying details for this employee, go back to Menu 3-> Select Option 3-> Select Option 2.", true);
                                            }
                                            
                                            Console.Clear();
                                        }
                                    }
                                    else//They chose MODIFY EXISTING EMPLOYEE
                                    {
                                        menuFourInput = userInterface.EmpDetailsMenu(true); // Display menu 4 and return the option they chose
                                        if (menuFourInput.KeyChar == '1')
                                        {
                                            if (localEmployee != null)
                                                //update the local employee base details
                                            {
                                                bool continued = false;
                                                string localEmployeeCopy = userInterface.SpecifyBaseDetails(out continued, localEmployee);
                                                if (localEmployeeCopy != null && (localEmployeeCopy != localEmployee))
                                                {
                                                    localEmployee = localEmployeeCopy;
                                                }
                                            }
                                            else
                                            {
                                                userInterface.CustomPrint("No local employee to specify base details for.", true);
                                            }
                                        }
                                        else if (menuFourInput.KeyChar == '2') //Specify Unique Details for Invalidated Employee
                                        {
                                            if (localEmployee != null)
                                            //update the local employee details with the type-specific details as well
                                            {
                                                string localEmployeeCopy = userInterface.SpecifyUniqueDetails(localEmployee);
                                                //Add the employee to the database
                                                bool employeeWasAdded = false;
                                                employeeWasAdded = AddAnEmployee(localEmployeeCopy);
                                                if (!employeeWasAdded)
                                                {
                                                    userInterface.CustomPrint("\nEmployee was invalid and was not added to list of employees.", true);
                                                    localEmployee = localEmployeeCopy;
                                                    Employees.AddInvalidEmployee(localEmployeeCopy); //the employee was not added because it is invalid. add it to the list
                                                }
                                                else
                                                {
                                                    userInterface.CustomPrint("\nEmployee was added to list of employees.", true);
                                                    //erase the local employee since it had details specified and was added
                                                    localEmployee = "";
                                                }
                                            }
                                            else
                                            {
                                                userInterface.CustomPrint("No local employee to specify unique details for.", true);
                                            }
                                        }
                                        else if (menuFourInput.KeyChar == '3')
                                        {
                                            bool successfulEdit = userInterface.EditValidatedEmployee();
                                            if (successfulEdit)
                                            {
                                                userInterface.CustomPrint("\nEmployee successfully edited.", false);
                                                userInterface.CustomPrint("\nAny changes made will not be permanent until saved.", true);
                                            }
                                            else
                                            {
                                                userInterface.CustomPrint("\nEmployee was not edited.", true);
                                            }
                                        }
                                    }

                                } while (menuFourInput.KeyChar != '9');
                            }
                            else if (input.KeyChar == '4')//Remove Employee
                            {
                                userInterface.CustomPrint("Are you sure you want to remove an Employee? Y/N", false);
                                ConsoleKeyInfo choice = Console.ReadKey(true);
                                if (choice.KeyChar == 'Y' || choice.KeyChar == 'y')
                                {
                                    bool employeeWasRemoved = false;
                                    string SINNum = userInterface.GetEmployeeSIN();
                                    employeeWasRemoved = RemoveAnEmployee(SINNum);
                                    if (employeeWasRemoved)
                                    {
                                        userInterface.CustomPrint("\n Employee was removed successfully.", false);
                                        userInterface.CustomPrint("\nAny changes made will not be permanent until saved.", true);
                                    }
                                    else
                                    {
                                        userInterface.CustomPrint("\n Employee was not removed.", true);
                                    }
                                }
                            }
                        } while (input.KeyChar != '9');

                        break;
                    case '9':
                        Console.Clear();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            }
        }
        /// <summary>
        /// This method takes a pipe delimited string with all details of an employee, parses it, and adds the Employee to the container
        /// </summary>
        /// <param name="fullDetails">The entire details string of the Employee.</param>
        /// <returns>Whether the employee was added to the container or not.</returns>
        public static bool AddAnEmployee(string fullDetails)
        {
            bool added = false;
            try
            {
                //Parse the details
                string[] parsedDetails = fullDetails.Split('|');
                string empType = parsedDetails[0];
                string empFname = parsedDetails[1];
                string empLname = parsedDetails[2];
                string empSIN = parsedDetails[3];
                string empBday = parsedDetails[4];
                switch (empType)
                {
                    case "CT": //Add a Contract Employee
                        string empStartDate = parsedDetails[5];
                        string empStopDate = parsedDetails[6];
                        string empFixedAmount = parsedDetails[7];
                        added = Employees.AddContractEmployee(empFname, empLname, empSIN, Convert.ToDateTime(empBday), Convert.ToDateTime(empStartDate), Convert.ToDateTime(empStartDate), Convert.ToDouble(empFixedAmount));
                        break;
                    case "PT": //Add a Part Time Employee
                        string pempHireDate = parsedDetails[5];
                        string pempTermDate = parsedDetails[6];
                        string empHourlyRate = parsedDetails[7];
                        added = Employees.AddParttimeEmployee(empFname, empLname, empSIN, Convert.ToDateTime(empBday), Convert.ToDateTime(pempHireDate), Convert.ToDateTime(pempTermDate), Convert.ToDouble(empHourlyRate));
                        break;
                    case "FT": //Add a Full Time Employee
                        string fempHireDate = parsedDetails[5];
                        string fempTermDate = parsedDetails[6];
                        string empSalary = parsedDetails[7];
                        added = Employees.AddFulltimeEmployee(empFname, empLname, empSIN, Convert.ToDateTime(empBday), Convert.ToDateTime(fempHireDate), Convert.ToDateTime(fempTermDate), Convert.ToDouble(empSalary));
                        break;
                    case "SN": //Add a Seasonal Employee
                        string empPiecePay = parsedDetails[5];
                        string empSeason = parsedDetails[6];
                        added = Employees.AddSeasonalEmployee(empFname, empLname, empSIN, Convert.ToDateTime(empBday), empSeason, Convert.ToDouble(empPiecePay));
                        break;
                }
            }
            catch (Exception)
            {
                added = false;
            }
            return added;
        }

        /// <summary>
        /// This method takes the SIN number of an employee and does a lookup. If the employee is found, they are removed from the employee set.
        /// </summary>
        /// <param name="theSIN">The SIN number of the Employee to Remove</param>
        /// <returns>Whether the employee was removed or not</returns>
        public static bool RemoveAnEmployee(string theSIN)
        {
            bool employeeRemoved = false;
            string theEmployee = Employees.FindAnEmployee(theSIN);

            if (theEmployee != "")
            {
                try
                {
                    //Parse the details
                    string[] parsedDetails = theEmployee.Split('|');
                    string empType = parsedDetails[0];
                    string empFname = parsedDetails[1];
                    string empLname = parsedDetails[2];
                    string empSIN = parsedDetails[3];
                    string empBday = parsedDetails[4];
                    switch (empType)
                    {
                        case "CT": //Remove a Contract Employee
                            string empStartDate = parsedDetails[5];
                            string empStopDate = parsedDetails[6];
                            string empFixedAmount = parsedDetails[7];
                            employeeRemoved = Employees.RemoveContractEmployee(empSIN);
                            break;
                        case "PT": //Remove a Part Time Employee
                            string pempHireDate = parsedDetails[5];
                            string pempTermDate = parsedDetails[6];
                            string empHourlyRate = parsedDetails[7];
                            employeeRemoved = Employees.RemoveParttimeEmployee(empSIN);
                            break;
                        case "FT": //Remove a Full Time Employee
                            string fempHireDate = parsedDetails[5];
                            string fempTermDate = parsedDetails[6];
                            string empSalary = parsedDetails[7];
                            employeeRemoved = Employees.RemoveFulltimeEmployee(empSIN);
                            break;
                        case "SN": //Remove a Seasonal Employee
                            string empPiecePay = parsedDetails[5];
                            string empSeason = parsedDetails[6];
                            employeeRemoved = Employees.RemoveSeasonalEmployee(empSIN);
                            break;
                    }
                }
                catch (Exception)
                {
                    employeeRemoved = false;
                }
            }

            return employeeRemoved;
        }
    }
}
