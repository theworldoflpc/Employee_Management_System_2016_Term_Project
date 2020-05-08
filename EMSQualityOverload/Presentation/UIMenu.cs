/*
* FILE : UIMenu.cs
* PROJECT : INFO2180 - Software Quality I - Term Project - EMS 
* PROGRAMMERS : RSkowron, DPitters, CWilson, and lpcSetProg
* FIRST VERSION : 2016-11-17
* DESCRIPTION : 
* This file contains the source code the console based user interface menu for the 
* EMS program. It is designed in a hierarchical style and contains several submenus 
* that the user of the program is able to navigate through. The user is able to
* update, delete, search and add employees through this menu console.   
* 
*/


using System;
using TheCompany;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using AllEmployees;
using System.Text;
using System.IO;
using System.Globalization;
namespace Presentation
{
    /// <summary>
    /// Displays information to the screen to the user.
    /// This class acts as the View Layer. <BR>
    /// <b> Exception Strategy: </b> When the user is prompted to enter any date, there may be 
    /// an exception thrown when it is converted to a DateTime variable.
    /// If this happens, the exception is caught and the user is allowed to either
    /// a) Re-enter the date in the correct format
    /// b) Quit the process
    /// </summary>
    public class UIMenu
    {
        public void Run()
        {
            Console.WriteLine("Menu");
            Console.ReadKey();
        }

        /// <summary>
        /// This method allows other classes to print to screen through the View Layer.
        /// </summary>
        /// <param name="messageToPrint">The message to print to screen.</param>
        /// <param name="pause">Whether or not to include a Press any key to continue message.</param>
        public void CustomPrint(string messageToPrint, bool pause)
        {
            Console.WriteLine(messageToPrint);
            if (pause)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
        }

        /// <summary>
        /// Method to display the menu options for the Main Menu
        /// </summary>
        /// <returns> The input is returned as a ConsoleKeyInfo datatype </returns>
        public ConsoleKeyInfo MainMenu()
        {
            ConsoleKeyInfo input;

            Console.WriteLine("Menu 1: MAIN MENU");
            Console.WriteLine("\n 1. Manage EMS DBASE files");
            Console.WriteLine("\n 2. Manage Employees");
            Console.WriteLine("\n 9. Quit");

            input = Console.ReadKey();

            return input;
        }

        /// <summary>
        /// Method to display the menu options for the File Management Menu
        /// </summary>
        /// <returns> The input is returned as a ConsoleKeyInfo datatype </returns>
        public ConsoleKeyInfo FileManagementMenu()
        {
            ConsoleKeyInfo input;
            Console.Clear();

            Console.WriteLine("Menu 2: FILE MANAGEMENT MENU");
            Console.WriteLine("\n 1. Load EMS DBase from file");
            Console.WriteLine("\n 2. Save Employee Set to EMS DBase file");
            Console.WriteLine("\n 9. Return to Main Menu");

            input = Console.ReadKey();
            Console.Clear();
            return input;

        }

        /// <summary>
        /// Method to display the menu options for the Employee Management Menu
        /// </summary>
        /// <returns> The input is returned as a ConsoleKeyInfo datatype </returns>
        public ConsoleKeyInfo EmpManagementMenu()
        {
            ConsoleKeyInfo input;
            Console.Clear();

            Console.WriteLine("Menu 3: EMPLOYEE MANAGEMENT MENU");
            Console.WriteLine("\n 1. Display Employee Set");
            Console.WriteLine("\n 2. Create a NEW Employee");
            Console.WriteLine("\n 3. Modify an EXISTING Employee");
            Console.WriteLine("\n 4. Remove an EXISTING Employee");
            Console.WriteLine("\n 9. Return to Main Menu");
            Console.WriteLine("\n\nAny changes made will not be permanent until saved.");
            input = Console.ReadKey();
            Console.Clear();
            return input;
        }

        /// <summary>
        /// Method to display the menu options for the Employee Details Menu
        /// </summary>
        /// ///<param name="modifying">Whether the user if modifying an employee or creating an employee</param>
        /// <returns> The input is returned as a ConsoleKeyInfo datatype </returns>
        public ConsoleKeyInfo EmpDetailsMenu(bool modifying)
        {
            ConsoleKeyInfo input;
            Console.Clear();

            Console.WriteLine("Menu 4: EMPLOYEE DETAILS MENU");
            if (modifying)
            //If they selected "Modify existing employee"
            //determine if it is an employee that had just base details specified
            //or an employee that was added already
            //switch on employee type and display a different option based on it
            {
                Console.WriteLine("\n 1. Specify Base Details for Invalidated Employee.");
                Console.WriteLine("\n 2. Specify Unique Details for Invalidated Employee.");
                Console.WriteLine("\n 3. Edit Unique Details for Validated Employee.");
            }
            else
            {
                Console.WriteLine("\n 1. Specify Base Employee Details");
                //If they chose to create an employee, no need to show 'Specify Unique Details' until after base details are entered
            }
            Console.WriteLine("\n 9. Return to Employee Management Menu");
            input = Console.ReadKey();
            Console.Clear();
            return input;
        }

        /// <summary>
        /// This method is used to change the unique details for an employee.
        /// </summary>
        /// <returns>Whether the employee was edited successfully or not.</returns>
        public bool EditValidatedEmployee()
        {
            bool success = false;
            string input;
            Console.WriteLine("\nPlease enter the SIN or Business number of the Employee you would like to edit.");
            input = Console.ReadLine(); //Get the SIN from the user and do a lookup.
            Regex sinNum = new Regex(@"^\d{3}([- ]?)\d{3}\1\d{3}$|^0$|^\d{5}([- ]?)\d{4}$");
            Match sinMatch = sinNum.Match(input);
            while (!sinMatch.Success && input != "q")
            {
                Console.WriteLine("\n Please enter the Employee's SIN or Business Number in the provided format.");
                Console.WriteLine("\n SIN Format: NNN NNN NNN");
                Console.WriteLine("\n Business Number Format: NNNNN NNNN");
                input = Console.ReadLine();
                sinMatch = sinNum.Match(input);
            }
            if (input != "q")
            {
                //Take spaces out of the SIN
                string[] tokenizedSIN = input.Split(' ');
                string fullSIN = "";
                switch (tokenizedSIN.Length)
                {
                    case 1:
                        fullSIN = tokenizedSIN[0];
                        break;
                    case 2:
                        fullSIN = tokenizedSIN[0] + tokenizedSIN[1];
                        break;
                    case 3:
                        fullSIN = tokenizedSIN[0] + tokenizedSIN[1] + tokenizedSIN[2];
                        break;
                }

                string employeeID = fullSIN;
                string employeeToMod = Employees.FindAnEmployee(employeeID);

                if (employeeToMod != "")
                {
                    //If the employee exists, find the type of employee and bring the user to the menu to edit its details
                    string[] pieces = employeeToMod.Split('|');
                    string type = pieces[0];
                    string changedEmployee = "";
                    bool quitEarly = false;
                    switch (type)
                    {
                        case "CT":
                            changedEmployee = SpecifyContractDetails(employeeToMod, true, out quitEarly);
                            if (changedEmployee == "success")
                            //If the user quit or the change was rejected, the employee will not be changed
                            {
                                success = true;
                            }
                            break;
                        case "FT":
                            changedEmployee = SpecifyFullTimeDetails(employeeToMod, true, out quitEarly);
                            if (changedEmployee == "success")
                            //If the user quit or the change was rejected, the employee will not be changed
                            {
                                success = true;
                            }
                            break;
                        case "PT":
                            changedEmployee = SpecifyPartTimeDetails(employeeToMod, true, out quitEarly);
                            if (changedEmployee == "success")
                            //If the user quit or the change was rejected, the employee will not be changed
                            {
                                success = true;
                            }
                            break;
                        case "SN":
                            changedEmployee = SpecifySeasonalDetails(employeeToMod, true, out quitEarly);
                            if (changedEmployee == "success")
                            //If the user quit or the change was rejected, the employee will not be changed
                            {
                                success = true;
                            }
                            break;
                    }

                }
                else
                {
                    Console.WriteLine("\nCould not find employee with employeeID " + employeeID + ".");
                }
            }
            return success;
        }


        /// <summary>
        /// This method is used to get the base details for the employee from the user.
        /// It then calls another method that gets specific details. This is based on the entered
        /// employee type.
        /// </summary>
        /// ///<param name="continued">Whether the user continued from this menu to SpecifyUniqueDetails menu or not.</param>
        /// ///<param name="localEmp">If the user is changing base details, this will not be null</param>
        /// <returns>A string that contains the pipe delimited employee details. If the user
        /// decides to quit early, the string is set to null and returned.</returns>
        public string SpecifyBaseDetails(out bool continued, string localEmp)
        {
            continued = false; //This will be true if the user decides to specify type-specific details after doing base details
            bool creationSuccessful = true;
            string input;
            string prevInput;
            string empDeets = ""; //This will hold the entire employee details.
            //It is in this format: Type|FirstName|LastName|SIN|DateOfBirth
            Console.WriteLine("Specify Employee Base Details");
            Console.WriteLine("Enter 'q' at any time to quit the process.");

            if (localEmp != null)
            {
                Console.WriteLine("\n Please enter the new Employee type. Employee type is either Contract, Parttime, Fulltime, or Seasonal");
            }
            else
            {
                Console.WriteLine("\n Please enter the Employee type. Employee type is either Contract, Parttime, Fulltime, or Seasonal");
            }
            input = Console.ReadLine();
            if (input == "q")
            {
                creationSuccessful = false;
            }
            //Only continue when a valid employee type is entered
            //regex pattern doesnt work - ([Ss][Ee][Aa][Ss][Oo][Nn][Aa][Ll])|([Cc][Oo][Nn][Tt][Rr][Aa][Cc][Tt])|([Pp][Aa][Rr][Tt] ?[Tt][Ii][Mm][Ee])|([Ff][Uu][Ll]+ ?[Tt][Ii][Mm][Ee])|[CcSsPpFfq]
            input = input.ToLower();
            while (input != "seasonal" && input != "contract" && input != "parttime" && input != "fulltime" && input != "q")
            {
                Console.WriteLine("\n Employee type must be either Contract, Parttime, Fulltime, or Seasonal.");
                Console.WriteLine("Please re enter the employee type.");
                input = Console.ReadLine();
                input = input.ToLower();
            }
            //Convert the employee type to just its starting letter (example: contract converts to "C")
            string theEmpType = "";
            if (input == "contract")
            {
                input = "CT";
                theEmpType = input;
            }
            else if (input == "parttime")
            {
                input = "PT";
                theEmpType = input;
            }
            else if (input == "fulltime")
            {
                input = "FT";
                theEmpType = input;
            }
            else if (input == "seasonal")
            {
                input = "SN";
                theEmpType = input;
            }

            prevInput = input;
            if (prevInput != "q") // Quit early if they entered q
            {
                Regex name = new Regex(@"^[a-zA-Z]+(([\'\,\.\-][a-zA-Z])?[a-zA-Z]*)*$"); //By Hayk A, from http://regexlib.com/Search.aspx?k=first+name&c=-1&m=-1&ps=20
                Match nameMatch;
                empDeets += input; //Employee details starts with the employee type
                if (theEmpType != "CT") //Do not ask contract employees for first name
                {
                    if (localEmp != null)
                    {
                        Console.WriteLine("\n Please enter the new first name for the Employee (No digits or spaces).");
                    }
                    else
                    {
                        Console.WriteLine("\n Please enter the Employee's first name (No digits or spaces).");
                    }
                    input = Console.ReadLine();
                    nameMatch = name.Match(input);
                    while (!nameMatch.Success)
                    {
                        Console.WriteLine("\n Please enter a first name without any digits or spaces.");
                        input = Console.ReadLine();
                        nameMatch = name.Match(input);
                    }
                }
                else
                {
                    input = ""; //First name will not be tracked for the Contract employee
                }
                if (input == "q")
                {
                    creationSuccessful = false;
                }
                prevInput = input;
                if (prevInput != "q") // Quit early if they entered q
                {
                    empDeets += "|" + input; //Employee details string is pipe delimited. Add the first name to it.
                    if (theEmpType == "CT")
                    {
                        if (localEmp != null)
                        {
                            Console.WriteLine("\n Please enter the new company name (No digits or spaces).");
                        }
                        else
                        {
                            Console.WriteLine("\n Please enter the Employee's company name (No digits or spaces).");
                        }
                    }
                    else
                    {
                        if (localEmp != null)
                        {
                            Console.WriteLine("\n Please enter the new last name for the Employee (No digits or spaces).");
                        }
                        else
                        {
                            Console.WriteLine("\n Please enter the Employee's last name (No digits or spaces).");
                        }
                    }
                    input = Console.ReadLine();
                    nameMatch = name.Match(input);
                    while (!nameMatch.Success)
                    {
                        if (theEmpType == "CT")
                        {
                            Console.WriteLine("\n Please enter a company name without any digits or spaces.");
                        }
                        else
                        {
                            Console.WriteLine("\n Please enter a last name without any digits or spaces.");
                        }
                        input = Console.ReadLine();
                        nameMatch = name.Match(input);
                    }
                    if (input == "q")
                    {
                        creationSuccessful = false;
                    }
                    prevInput = input;
                    if (prevInput != "q") // Quit early if they entered q
                    {
                        empDeets += "|" + input; //Add the last name to the employee details string.
                        if (theEmpType == "CT")
                        {
                            if (localEmp != null)
                            {
                                Console.WriteLine("\n Please enter the new Business Number. Format: NNNNN NNNN");
                            }
                            else
                            {
                                Console.WriteLine("\n Please enter the Employee's Business Number. Format: NNNNN NNNN");
                            }
                        }
                        else
                        {
                            if (localEmp != null)
                            {
                                Console.WriteLine("\n Please enter the new SIN Number. Format: NNN NNN NNN");
                            }
                            else
                            {
                                Console.WriteLine("\n Please enter the Employee's SIN Number. Format: NNN NNN NNN");
                            }
                        }
                        input = Console.ReadLine();
                        Regex sinNum = new Regex(@"^\d{3}([- ]?)\d{3}\1\d{3}$|^0$|^\d{5}([- ]?)\d{4}$");
                        Match sinMatch = sinNum.Match(input);
                        while (!sinMatch.Success && input != "q" && (!string.IsNullOrEmpty(input)))
                        {
                            if (theEmpType == "CT")
                            {
                                Console.WriteLine("\n Please enter the Employee's Business Number in the provided format.");
                                Console.WriteLine("\n Format: NNNNN NNNN");
                            }
                            else
                            {
                                Console.WriteLine("\n Please enter the Employee's SIN Number in the provided format.");
                                Console.WriteLine("\n Format: NNN NNN NNN");
                            }
                            input = Console.ReadLine();
                            sinMatch = sinNum.Match(input);
                        }
                        if (input == "q")
                        {
                            creationSuccessful = false;
                        }
                        prevInput = input;
                        if (prevInput != "q") // Quit early if they entered q
                        {
                            //Take spaces out of the SIN
                            string[] tokenizedSIN = input.Split(' ');
                            string fullSIN = "";
                            switch (tokenizedSIN.Length)
                            {
                                case 1:
                                    fullSIN = tokenizedSIN[0];
                                    break;
                                case 2:
                                    fullSIN = tokenizedSIN[0] + tokenizedSIN[1];
                                    break;
                                case 3:
                                    fullSIN = tokenizedSIN[0] + tokenizedSIN[1] + tokenizedSIN[2];
                                    break;
                            }


                            empDeets += "|" + fullSIN; //Add the SIN number to the employee details string.
                            if (theEmpType == "CT")
                            {
                                if (localEmp != null)
                                {
                                    Console.WriteLine("\n Please enter the new Date of Incorporation. Format: YYYY/MM/DD.\n Note: For Contract employee to be valid, last two digits of the Year must match first two digits of Business Number.");
                                }
                                else
                                {
                                    Console.WriteLine("\n Please enter the Employee's Date of Incorporation. Format: YYYY/MM/DD.\n Note: For Contract employee to be valid, last two digits of the Year must match first two digits of Business Number.");
                                }
                            }
                            else
                            {
                                if (localEmp != null)
                                {
                                    Console.WriteLine("\n Please enter the new Date of Birth. Format: YYYY/MM/DD");
                                }
                                else
                                {
                                    Console.WriteLine("\n Please enter the Employee's Date of Birth. Format: YYYY/MM/DD");
                                }
                            }
                            if (theEmpType == "CT")
                            {
                                input = CheckDate("Incorporation", null);
                            }
                            else
                            {
                                input = CheckDate("Birth", null);
                            }
                            //Check if the birth date is in the correct format
                            if (input == "")
                            //This will be blank if the user quit when entering birth date
                            {
                                creationSuccessful = false;
                            }

                        }
                    }
                }
            }
            if (!creationSuccessful)
            {
                Console.WriteLine("Specify Employee base details aborted.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
            else
            {
                empDeets += "|" + input; //Add the date of birth to the employee details string.
                //Call a specific function to fill in more details based on employee type
                if (localEmp != null)
                {
                    Console.WriteLine("\nBase Details updated successfully.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey(true);
                }
                if (localEmp == null)
                {
                    Console.WriteLine("\nBase Details specified successfully.");
                    Console.WriteLine("\nWould you like to Specify Unique details for the Employee? Y/N\n");
                    ConsoleKeyInfo yesno = Console.ReadKey(true);
                    while (yesno.KeyChar != 'Y' || yesno.KeyChar != 'y' || yesno.KeyChar != 'N' || yesno.KeyChar != 'n')
                    {
                        if (yesno.KeyChar == 'Y' || yesno.KeyChar == 'y' || yesno.KeyChar == 'N' || yesno.KeyChar == 'n')
                        {
                            break;
                        }
                        yesno = Console.ReadKey(true);
                    }
                    if (yesno.KeyChar == 'N' || yesno.KeyChar == 'n')
                    {
                        continued = false;
                    }
                    else
                    {
                        continued = true;
                        empDeets = SpecifyUniqueDetails(empDeets);//continue the creation process
                    }
                }
            }
            return empDeets;
        }

        /// <summary>
        /// This menu is used to allow the user to add or edit employee type-specific details.
        /// </summary>
        /// <param name="baseDetails">The generic Employee details.</param>
        /// <returns>A string that contains the base details plus the employee-type specific details.</returns>
        public string SpecifyUniqueDetails(string baseDetails)
        {
            bool completeBase = true;
            ConsoleKeyInfo input;
            string empDeets = baseDetails;
            string[] partitions = baseDetails.Split('|'); //Get the employee type by tokeizing the pipes
            string employeeType = "";
            string employeeFname = "";
            string employeeLname = "";
            string employeeSIN = "";
            string employeeBirthday = "";
            try
            {
                employeeType = partitions[0];
                employeeFname = partitions[1];
                employeeLname = partitions[2];
                employeeSIN = partitions[3];
                employeeBirthday = partitions[4];

            }
            catch (Exception)
            {
                Console.WriteLine("\nBase Details must be fully specified before Unique Employee Details can be specified.");
                completeBase = false;
            }
            if (completeBase)
            {
                switch (employeeType)
                {
                    case "CT":
                        Console.WriteLine("\nYou will now enter a few more details for the Contract Employee.");
                        Console.WriteLine("Press any key to continue or q to exit...");
                        input = Console.ReadKey(true);
                        if (!(input.KeyChar == 'q'))
                        {
                            Console.Clear();
                            bool quitEarly = false;
                            empDeets = SpecifyContractDetails(empDeets, false, out quitEarly);
                            if (!quitEarly) //This will be true if the user entered "q" to exit early
                            {
                                Console.WriteLine("\nContract Employee details edited successfully.");
                            }
                            else
                            {
                                Console.WriteLine("\nSpecify Contract Employee Details aborted.");
                            }
                        }
                        break;
                    case "PT":
                        Console.WriteLine("\nYou will now enter a few more details for the Part Time Employee.");
                        Console.WriteLine("Press any key to continue or q to exit...");
                        input = Console.ReadKey(true);
                        if (!(input.KeyChar == 'q'))
                        {
                            Console.Clear();
                            bool quitEarly = false;
                            empDeets = SpecifyPartTimeDetails(empDeets, false, out quitEarly);
                            if (!quitEarly) //This will be null if the user entered "q" to exit early
                            {
                                Console.WriteLine("\nPart Time Employee details edited successfully.");
                            }
                            else
                            {
                                Console.WriteLine("\nSpecify Part Time Employee Details aborted.");
                            }
                        }
                        break;
                    case "FT":
                        Console.WriteLine("\nYou will now enter a few more details for the Full Time Employee.");
                        Console.WriteLine("Press any key to continue or q to exit...");
                        input = Console.ReadKey(true);
                        if (!(input.KeyChar == 'q'))
                        {
                            Console.Clear();
                            bool quitEarly = false;
                            empDeets = SpecifyFullTimeDetails(empDeets, false, out quitEarly);
                            if (!quitEarly) //This will be null if the user entered "q" to exit early
                            {
                                Console.WriteLine("\nFull Time Employee details edited successfully.");
                            }
                            else
                            {
                                Console.WriteLine("\nSpecify Full Time Employee Details aborted.");
                            }
                        }
                        break;
                    case "SN":
                        Console.WriteLine("\nYou will now enter a few more details for the Seasonal Employee.");
                        Console.WriteLine("Press any key to continue or q to exit...");
                        input = Console.ReadKey(true);
                        if (!(input.KeyChar == 'q'))
                        {
                            Console.Clear();
                            bool quitEarly = false;
                            empDeets = SpecifySeasonalDetails(empDeets, false, out quitEarly);
                            if (!quitEarly) //This will be null if the user entered "q" to exit early
                            {
                                Console.WriteLine("\nSeasonal Employee details edited successfully.");
                            }
                            else
                            {
                                Console.WriteLine("\nSpecify Seasonal Employee Details aborted.");
                            }
                        }
                        break;
                }
            }
            return empDeets;
        }

        /// <summary>
        /// This method gets the user input for attributes unique to ContractEmployee.
        /// </summary>
        /// <param name="baseDetails">The general attributes of the Employee.</param>
        /// <param name="modifying">Whether the user is modifying an employee or not.</param>
        /// <param name="userQuit">Whether the user quit or not.</param>
        /// <returns>A string containing the base details plus the details specified in this method.</returns>
        public string SpecifyContractDetails(string baseDetails, bool modifying, out bool userQuit)
        {
            //Split the base details into each attribute
            string[] pieces = baseDetails.Split('|');
            string type = pieces[0];
            string sin = pieces[3];
            string birthDate = pieces[4];

            userQuit = false;
            Console.WriteLine("");
            Console.WriteLine("Specify Contract Employee Details");
            Console.WriteLine("Enter 'q' at any time to quit the process.");
            if (modifying)
            {
                string changeSuccess = "failure";

                //If the user is modifying an employee, allow them to edit 1 attribute at a time
                Console.WriteLine("\n1. Specify Contract Start Date");
                Console.WriteLine("\n2. Specify Contract Stop Date");
                Console.WriteLine("\n3. Specify Fixed Amount");
                Console.WriteLine("\nq. Quit");
                ConsoleKeyInfo option = Console.ReadKey(true);

                string startDate = pieces[5];
                string input = "";
                bool success = false;
                switch (option.KeyChar)
                {
                    case '1': //Start date
                        Console.WriteLine("\n Please enter the Contract Start Date. Format: YYYY/MM/DD");
                        input = CheckDate("StartDate", birthDate);
                        if (input != "")//Only continue if they entered a valid start date
                        {
                            success = Employees.ModifyContractEmployee(sin, "contractStartDate", input);
                            if (success)
                            {
                                changeSuccess = "success";
                            }
                            else
                            {
                                //If the change was rejected by the container, make no change and return
                                changeSuccess = "failure";
                            }
                        }
                        else
                        {
                            //If they entered an invalid date, make no change and return
                            changeSuccess = "failure";
                        }
                        break;
                    case '2': //Stop date
                        Console.WriteLine("\n Please enter the Contract Stop Date. Format: YYYY/MM/DD");
                        input = CheckDate("StopDate", startDate);

                        if (input != "")//Only continue if they entered a valid stop date
                        {
                            success = Employees.ModifyContractEmployee(sin, "contractStopDate", input);
                            if (success)
                            {
                                changeSuccess = "success";
                            }
                            else
                            {
                                //If the change was rejected by the container, make no change and return
                                changeSuccess = "failure";
                            }
                        }
                        else
                        {
                            //If they entered an invalid date, make no change and return
                            changeSuccess = "failure";
                        }

                        break;
                    case '3': //Fixed Amount
                        Console.WriteLine("\n Please enter the Fixed amount to be paid to the Contract Employee.");
                        input = Console.ReadLine();
                        bool validAmount = false;
                        double fixedAmount;
                        while (!validAmount)
                        {
                            if (input == "q")
                            {
                                break;
                            }
                            try
                            {
                                fixedAmount = Convert.ToDouble(input); //Ensure that this value is valid
                                if (fixedAmount > 0)
                                {
                                    validAmount = true;
                                }
                            }
                            catch (Exception)
                            {
                                validAmount = false;

                            }
                            if (!validAmount)
                            {
                                Console.WriteLine("\n Please enter a non-negative number.");
                                input = Console.ReadLine();
                            }
                        }
                        if (input != "q")//Only continue if the user did not try to quit
                        {
                            success = Employees.ModifyContractEmployee(sin, "fixedContractAmount", input);
                            if (success)
                            {
                                changeSuccess = "success";
                            }
                            else
                            {
                                //If the change was rejected by the container, make no change and return
                                changeSuccess = "failure";
                            }
                        }
                        else
                        {
                            //If the user wanted to quit, make no change and return
                            changeSuccess = "failure";
                        }
                        break;
                    default:
                        changeSuccess = "failure"; //if no option was selected
                        break;
                }
                userQuit = false;
                return changeSuccess;
            }
            else
            {
                bool creationSuccessful = true;
                string fullDetails = baseDetails;
                string input = "";
                string startDate = "";
                Console.WriteLine("\n Please enter the Contract Start Date. Format: YYYY/MM/DD");
                input = CheckDate("StartDate", birthDate);
                if (input != "")//Only continue if they entered a valid start date
                {
                    fullDetails += "|" + input; //Add the start date to the details string
                    startDate = input;
                    Console.WriteLine("\n Please enter the Contract Stop Date. Format: YYYY/MM/DD");
                    input = CheckDate("StopDate", startDate);
                    if (input != "")
                    {
                        fullDetails += "|" + input; //Add the stop date to the details string

                        Console.WriteLine("\n Please enter the Fixed amount to be paid to the Contract Employee.");
                        input = Console.ReadLine();
                        bool validAmount = false;
                        double fixedAmount;
                        while (!validAmount)
                        {
                            if (input == "q")
                            {
                                break;
                            }
                            try
                            {
                                fixedAmount = Convert.ToDouble(input); //Ensure that this value is valid
                                if (fixedAmount > 0)
                                {
                                    validAmount = true;
                                }
                            }
                            catch (Exception)
                            {
                                validAmount = false;

                            }
                            if (!validAmount)
                            {
                                Console.WriteLine("\n Please enter a non-negative number.");
                                input = Console.ReadLine();
                            }
                        }
                        if (input == "q")
                        {
                            creationSuccessful = false;
                        }
                        else
                        {
                            //Input validation on the fixed amount amount needed here
                            fullDetails += "|" + input; //Add the fixed amount to the details string.
                        }
                    }
                    else
                    {
                        creationSuccessful = false;
                    }
                }
                else
                {
                    creationSuccessful = false;
                }
                if (!creationSuccessful)
                {
                    userQuit = true;
                }
                return fullDetails;
            }
        }

        /// <summary>
        /// This method gets the user input for attributes unique to ParttimeEmployee.
        /// </summary>
        /// <param name="baseDetails">The general attributes of the Employee.</param>
        /// <param name="modifying">Whether the user is modifying the Employee or not.</param>
        /// <param name="userQuit">Whether the user quit or not.</param>
        /// <returns>A string containing the base details plus the details specified in this method.</returns>
        public string SpecifyPartTimeDetails(string baseDetails, bool modifying, out bool userQuit)
        {
            //Split the base details into each attribute
            string[] pieces = baseDetails.Split('|');
            string type = pieces[0];
            string sin = pieces[3];
            string birthDate = pieces[4];
            userQuit = false;
            Console.WriteLine("");
            Console.WriteLine("Specify Part Time Employee Details");
            Console.WriteLine("Enter 'q' at any time to quit the process.");
            if (modifying)
            {
                string changeSuccess = "";
                //If the user is modifying an employee, allow them to edit 1 attribute at a time
                Console.WriteLine("\n1. Specify Hire Date");
                Console.WriteLine("\n2. Specify Termination Date");
                Console.WriteLine("\n3. Specify Hourly Pay");
                Console.WriteLine("\nq. Quit");
                ConsoleKeyInfo option = Console.ReadKey(true);


                string hireDate = pieces[5];
                string input = "";
                bool success = false;
                switch (option.KeyChar)
                {
                    case '1': //Hire date
                        Console.WriteLine("\n Please enter the Hire Date. Format: YYYY/MM/DD");
                        input = CheckDate("Hire", birthDate);
                        if (input != "")//Only continue if they entered a valid hire date
                        {
                            success = Employees.ModifyParttimeEmployee(sin, "dateOfHire", input);
                            if (success)
                            {
                                changeSuccess = "success";
                            }
                            else
                            {
                                //If the change was rejected by the container, make no change and return
                                changeSuccess = "failure";
                            }
                        }
                        else
                        {
                            //If they entered an invalid date, make no change and return
                            changeSuccess = "failure";
                        }
                        break;
                    case '2': //Termination date
                        Console.WriteLine("\n Please enter the Termination Date. Format: YYYY/MM/DD");

                        input = CheckDate("Termination", hireDate);
                        if (input != "")//Only continue if they entered a valid Termination date
                        {
                            success = Employees.ModifyParttimeEmployee(sin, "dateOfTermination", input);
                            if (success)
                            {
                                changeSuccess = "success";
                            }
                            else
                            {
                                //If the change was rejected by the container, make no change and return
                                changeSuccess = "failure";
                            }
                        }
                        else
                        {
                            //If they entered an invalid date, make no change and return
                            changeSuccess = "failure";
                        }
                        break;
                    case '3': //Hourly Amount
                        Console.WriteLine("\n Please enter the Hourly amount to be paid to the Parttime Employee.");
                        input = Console.ReadLine();
                        bool validAmount = false;
                        double hourlyAmount;
                        while (!validAmount)
                        {
                            if (input == "q")
                            {
                                break;
                            }
                            try
                            {
                                hourlyAmount = Convert.ToDouble(input); //Ensure that this value is valid
                                if (hourlyAmount > 0)
                                {
                                    validAmount = true;
                                }
                            }
                            catch (Exception)
                            {
                                validAmount = false;

                            }
                            if (!validAmount)
                            {
                                Console.WriteLine("\n Please enter a non-negative number.");
                                input = Console.ReadLine();
                            }
                        }
                        if (input != "q")//Only continue if the user did not try to quit
                        {
                            success = Employees.ModifyParttimeEmployee(sin, "hourlyRate", input);
                            if (success)
                            {
                                changeSuccess = "success";
                            }
                            else
                            {
                                //If the change was rejected by the container, make no change and return
                                changeSuccess = "failure";
                            }
                        }
                        else
                        {
                            //If the user wanted to quit, make no change and return
                            changeSuccess = "failure";
                        }
                        break;
                    default:
                        changeSuccess = "failure"; //if no option was selected
                        break;
                }
                userQuit = false;
                return changeSuccess;
            }
            else
            {
                string termDate = "";
                string hireDate = "";
                bool creationSuccessful = true;
                string fullDetails = baseDetails;
                string input = "";
                Console.WriteLine("\n Please enter the Date of Hire. Format: YYYY/MM/DD");
                input = CheckDate("Hire", birthDate);

                if (input != "")//Only continue if they entered a valid hire date
                {
                    fullDetails += "|" + input; //Add the hire date to the details string
                    hireDate = input;
                    Console.WriteLine("\n Please enter the Termination Date. Format: YYYY/MM/DD");
                    input = CheckDate("Termination", hireDate);
                    if (input != "")//Only continue if user entered a valid termination date
                    {
                        fullDetails += "|" + input; //Add the termination date to the details string
                        termDate = input;
                        Console.WriteLine("\n Please enter the Hourly amount paid to the Part Time Employee.");
                        input = Console.ReadLine();
                        bool validAmount = false;
                        double hourlyAmount;
                        while (!validAmount)
                        {
                            if (input == "q")
                            {
                                break;
                            }
                            try
                            {
                                hourlyAmount = Convert.ToDouble(input); //Ensure that this value is valid
                                if (hourlyAmount > 0)
                                {
                                    validAmount = true;
                                }
                            }
                            catch (Exception)
                            {
                                validAmount = false;

                            }
                            if (!validAmount)
                            {
                                Console.WriteLine("\n Please enter a non-negative number.");
                                input = Console.ReadLine();
                            }
                        }
                        if (input == "q")
                        {
                            creationSuccessful = false;
                        }
                        else
                        {
                            //Input validation on the hourly amount needed here
                            fullDetails += "|" + input; //Add the hourly amount to the details string.
                        }
                    }
                    else
                    {
                        creationSuccessful = false;
                    }
                }
                else
                {
                    creationSuccessful = false;
                }
                if (!creationSuccessful)
                {
                    userQuit = true;
                }
                return fullDetails;
            }
        }

        /// <summary>
        /// This method gets the user input for attributes unique to FulltimeEmployee.
        /// </summary>
        /// <param name="baseDetails">The general attributes of the Employee.</param>
        /// <param name="modifying">Whether the user is modifying the Employee or not.</param>
        /// <param name="userQuit">Whether the user quit or not.</param>
        /// <returns>A string containing the base details plus the details specified in this method.</returns>
        public string SpecifyFullTimeDetails(string baseDetails, bool modifying, out bool userQuit)
        {
            //Split the base details into each attribute
            string[] pieces = baseDetails.Split('|');
            string type = pieces[0];
            string sin = pieces[3];
            string birthDate = pieces[4];

            userQuit = false;
            Console.WriteLine("");
            Console.WriteLine("Specify Full Time Employee Details");
            Console.WriteLine("Enter 'q' at any time to quit the process.");
            if (modifying)
            {
                string changeSuccess = "";
                //If the user is modifying an employee, allow them to edit 1 attribute at a time
                Console.WriteLine("\n1. Specify Hire Date");
                Console.WriteLine("\n2. Specify Termination Date");
                Console.WriteLine("\n3. Specify Salary");
                Console.WriteLine("\nq. Quit");
                ConsoleKeyInfo option = Console.ReadKey(true);

                string hireDate = pieces[5];
                string input = "";
                bool success = false;
                switch (option.KeyChar)
                {
                    case '1': //Hire date
                        Console.WriteLine("\n Please enter the Hire Date. Format: YYYY/MM/DD");
                        input = CheckDate("Hire", birthDate);

                        if (input != "")//Only continue if they entered a valid hire date
                        {
                            success = Employees.ModifyFulltimeEmployee(sin, "dateOfHire", input);
                            if (success)
                            {
                                changeSuccess = "success";
                            }
                            else
                            {
                                //If the change was rejected by the container, make no change and return
                                changeSuccess = "failure";
                            }
                        }
                        else
                        {
                            //If they entered an invalid date, make no change and return
                            changeSuccess = "failure";
                        }

                        break;
                    case '2': //Termination date
                        Console.WriteLine("\n Please enter the Termination Date. Format: YYYY/MM/DD");
                        input = CheckDate("Termination", hireDate);

                        if (input != "")//Only continue if they entered a valid Termination date
                        {
                            success = Employees.ModifyFulltimeEmployee(sin, "dateOfTermination", input);
                            if (success)
                            {
                                changeSuccess = "success";
                            }
                            else
                            {
                                //If the change was rejected by the container, make no change and return
                                changeSuccess = "failure";
                            }
                        }
                        else
                        {
                            //If they entered an invalid date, make no change and return
                            changeSuccess = "failure";
                        }

                        break;
                    case '3': //Hourly Amount
                        Console.WriteLine("\n Please enter the Salary to be paid to the Fulltime Employee.");
                        input = Console.ReadLine();
                        bool validAmount = false;
                        double salaryAmount;
                        while (!validAmount)
                        {
                            if (input == "q")
                            {
                                break;
                            }
                            try
                            {
                                salaryAmount = Convert.ToDouble(input); //Ensure that this value is valid
                                if (salaryAmount > 0)
                                {
                                    validAmount = true;
                                }
                            }
                            catch (Exception)
                            {
                                validAmount = false;

                            }
                            if (!validAmount)
                            {
                                Console.WriteLine("\n Please enter a non-negative number.");
                                input = Console.ReadLine();
                            }
                        }
                        if (input != "q")//Only continue if the user did not try to quit
                        {
                            success = Employees.ModifyFulltimeEmployee(sin, "salary", input);
                            if (success)
                            {
                                changeSuccess = "success";
                            }
                            else
                            {
                                //If the change was rejected by the container, make no change and return
                                changeSuccess = "failure";
                            }
                        }
                        else
                        {
                            //If the user wanted to quit, make no change and return
                            changeSuccess = "failure";
                        }
                        break;
                    default:
                        changeSuccess = "failure"; //if no option was selected
                        break;
                }
                userQuit = false;
                return changeSuccess;
            }
            else
            {
                string hireDate = "";
                string termDate = "";
                bool creationSuccessful = true;
                string fullDetails = baseDetails;
                string input = "";
                Console.WriteLine("\n Please enter the Date of Hire. Format: YYYY/MM/DD");
                input = CheckDate("Hire", birthDate);
                if (input != "")//Only continue if they entered a valid hire date
                {
                    fullDetails += "|" + input; //Add the hire date to the details string
                    hireDate = input;
                    Console.WriteLine("\n Please enter the Termination Date. Format: YYYY/MM/DD");
                    input = CheckDate("Termination", hireDate);
                    if (input != "")
                    {
                        fullDetails += "|" + input; //Add the termination date to the details string
                        termDate = input;
                        Console.WriteLine("\n Please enter the Salary amount paid to the Full Time Employee.");
                        input = Console.ReadLine();
                        bool validAmount = false;
                        double salaryAmount;
                        while (!validAmount)
                        {
                            if (input == "q")
                            {
                                break;
                            }
                            try
                            {
                                salaryAmount = Convert.ToDouble(input); //Ensure that this value is valid
                                if (salaryAmount > 0)
                                {
                                    validAmount = true;
                                }
                            }
                            catch (Exception)
                            {
                                validAmount = false;

                            }
                            if (!validAmount)
                            {
                                Console.WriteLine("\n Please enter a non-negative number.");
                                input = Console.ReadLine();
                            }
                        }
                        if (input == "q")
                        {
                            creationSuccessful = false;
                        }
                        else
                        {
                            //Input validation on the salary amount needed here
                            fullDetails += "|" + input; //Add the salary amount to the details string
                        }
                    }
                    else
                    {
                        creationSuccessful = false;
                    }
                }
                else
                {
                    creationSuccessful = false;
                }
                if (!creationSuccessful)
                {
                    userQuit = true;
                }
                return fullDetails;
            }
        }

        /// <summary>
        /// This method gets the user input for attributes unique to SeasonalEmployee.
        /// </summary>
        /// <param name="baseDetails">The general attributes of the Employee.</param>
        /// <param name="modifying">Whether the user is modifying the Employee or not.</param>
        /// <param name="userQuit">Whether the user quit or not.</param>
        /// <returns>A string containing the base details plus the details specified in this method.</returns>
        public string SpecifySeasonalDetails(string baseDetails, bool modifying, out bool userQuit)
        {
            //Split the base details into each attribute
            string[] pieces = baseDetails.Split('|');
            string type = pieces[0];
            string sin = pieces[3];
            string birthDate = pieces[4];

            userQuit = false;
            Console.WriteLine("");
            Console.WriteLine("Specify Seasonal Employee Details");
            Console.WriteLine("Enter 'q' at any time to quit the process.");
            if (modifying)
            {
                string changeSuccess = "";
                //If the user is modifying an employee, allow them to edit 1 attribute at a time
                Console.WriteLine("\n1. Specify Piece Pay");
                Console.WriteLine("\n2. Specify Season");
                Console.WriteLine("\nq. Quit");
                ConsoleKeyInfo option = Console.ReadKey(true);

                string input = "";
                bool success = false;
                switch (option.KeyChar)
                {
                    case '1': //Piece Pay
                        Console.WriteLine("\n Please enter the amount to be paid per unit of work.");
                        bool validAmount = false;
                        double piecepayAmount;
                        input = Console.ReadLine();
                        while (!validAmount)
                        {
                            if (input == "q")
                            {
                                break;
                            }
                            try
                            {
                                piecepayAmount = Convert.ToDouble(input); //Ensure that this value is valid
                                if (piecepayAmount > 0)
                                {
                                    validAmount = true;
                                }
                            }
                            catch (Exception)
                            {
                                validAmount = false;

                            }
                            if (!validAmount)
                            {
                                Console.WriteLine("\n Please enter a non-negative number.");
                                input = Console.ReadLine();
                            }
                        }
                        if (input != "q")//Only continue if the user did not try to quit
                        {
                            success = Employees.ModifySeasonalEmployee(sin, "piecePay", input);
                            if (success)
                            {
                                changeSuccess = "success";
                            }
                            else
                            {
                                //If the change was rejected by the container, make no change and return
                                changeSuccess = "failure";
                            }
                        }
                        else
                        {
                            //If the user wanted to quit, make no change and return
                            changeSuccess = "failure";
                        }
                        break;
                    case '2': //Termination date
                        Console.WriteLine("\n Please enter the Season the Employee will work in. Seasons are Spring, Summer, Winter or Fall.");
                        input = Console.ReadLine();

                        if (input != "q")//Only continue if the user did not try to quit
                        {
                            success = Employees.ModifySeasonalEmployee(sin, "season", input);
                            if (success)
                            {
                                changeSuccess = "success";
                            }
                            else
                            {
                                //If the change was rejected by the container, make no change and return
                                changeSuccess = "failure";
                            }
                        }
                        else
                        {
                            //If the user wanted to quit, make no change and return
                            changeSuccess = "failure";
                        }
                        break;
                    default:
                        changeSuccess = "failure"; //if no option was selected
                        break;
                }
                userQuit = false;
                return changeSuccess;
            }
            else
            {
                bool creationSuccessful = true;
                string fullDetails = baseDetails;
                string input = "";
                Console.WriteLine("\n Please enter the Piece Pay (amount paid to the Seasonal Employee per unit of work done).");
                input = Console.ReadLine();
                bool validAmount = false;
                double piecepayAmount;
                while (!validAmount)
                {
                    if (input == "q")
                    {
                        break;
                    }
                    try
                    {
                        piecepayAmount = Convert.ToDouble(input); //Ensure that this value is valid
                        if (piecepayAmount > 0)
                        {
                            validAmount = true;
                        }
                    }
                    catch (Exception)
                    {
                        validAmount = false;

                    }
                    if (!validAmount)
                    {
                        Console.WriteLine("\n Please enter a non-negative number.");
                        input = Console.ReadLine();
                    }
                }
                if (input == "q")
                {
                    creationSuccessful = false;
                }
                else
                {
                    //Input validation on the piece pay amount needed here
                    fullDetails += "|" + input; //Add the piece pay to the details string

                    Console.WriteLine("\n Please enter the Season that the Employee will be working in. Seasons are Spring, Summer, Winter, or Fall.");
                    input = Console.ReadLine();

                    //Only continue when a valid season is entered
                    Regex seasonCheck = new Regex("([Ss][Uu][Mm]+[Ee][Rr])|([Ss][Pp][Rr][Ii][Nn][Gg])|([Ww][Ii][Nn][Tt][Ee][Rr])|([Ff][Aa][Ll]+)|q");
                    Match match = seasonCheck.Match(input);
                    while (!match.Success)
                    {
                        Console.WriteLine("\n Season must be either Spring, Summer, Winter, or Fall.");
                        Console.WriteLine("Please re enter the Season.");
                        input = Console.ReadLine();
                        match = seasonCheck.Match(input);
                    }
                    if (input == "q")
                    {
                        creationSuccessful = false;
                    }
                    else
                    {
                        //Convert the input season to the proper format (example: summer would be converted to Summer)
                        if (input == "FALL" || input == "fall")
                        {
                            input = "Fall";
                        }
                        else if (input == "SUMMER" || input == "summer")
                        {
                            input = "Summer";
                        }
                        else if (input == "SPRING" || input == "spring")
                        {
                            input = "Spring";
                        }
                        else if (input == "WINTER" || input == "winter")
                        {
                            input = "Winter";
                        }
                        fullDetails += "|" + input; //Add the season to the details string
                    }
                }
                if (!creationSuccessful)
                {
                    userQuit = true;
                }
                return fullDetails;
            }
        }

        /// <summary>
        /// This method checks a user input date and verifies that it is valid.
        /// </summary>
        /// <param name="dateType">The type of date.</param>
        /// <param name="dateStart">The preceding date to compare the date with.</param>
        /// <returns>A string with the formatted date.</returns>
        public string CheckDate(string dateType, string dateStart)
        {
            string formattedDate = "";
            string promptMessage = "";
            bool checkingForBirthday = false;
            switch (dateType)
            {
                case "Birth":
                    promptMessage = "\n Please enter the Date of Birth.";
                    checkingForBirthday = true;
                    break;
                case "Hire":
                    promptMessage = "\n Please enter the Date of Hire.";
                    break;
                case "StartDate":
                    promptMessage = "\n Please enter the Contract Start Date.";
                    break;
                case "StopDate":
                    promptMessage = "\n Please enter the Contract Stop Date.";
                    break;
                case "Termination":
                    promptMessage = "\n Please enter the Date of Termination.";
                    break;
                case "Incorporation":
                    promptMessage = "\n Please enter the Date of Incorporation. \n Note: For Contract employee to be valid, last two digits of the Year must match first two digits of Business Number.";
                    checkingForBirthday = true;
                    break;
            }
            bool correctDate = false;
            do
            {
                string input = Console.ReadLine();
                if (input == "")
                //The user entered nothing. if it is Termination date, make it the minimum value
                {
                    if (dateType == "Termination")
                    {
                        formattedDate = DateTime.MinValue.ToString("yyyy/MM/dd");
                        correctDate = true;
                        Console.WriteLine("Termination Date set to Minimum Value (" + formattedDate + ")");
                    }
                    else
                    {
                        correctDate = false;
                        Console.WriteLine("\nDate cannot be blank.");
                        Console.WriteLine(promptMessage + " Format: YYYY/MM/DD");
                    }
                }
                else if (input == "q")
                {
                    break;
                }
                else
                {
                    DateTime endDate;
                    bool valid = DateTime.TryParseExact(input, "yyyy/MM/dd", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out endDate);
                    if (!valid)
                    {
                        correctDate = false;
                        Console.WriteLine("\n Invalid date entered.");
                        Console.WriteLine(promptMessage + " Format: YYYY/MM/DD");
                    }
                    else
                    {
                        //Check that end date comes after the start date
                        if (!checkingForBirthday)
                        {
                            DateTime startDate = Convert.ToDateTime(dateStart);
                            string[] parseDateOfBirth = dateStart.Split('/');

                            if (startDate < endDate)
                            {
                                correctDate = true;
                                formattedDate = input;
                            }
                            else
                            {
                                correctDate = false;
                                Console.WriteLine("\nStart Date cannot come after End Date.");
                                Console.WriteLine(promptMessage + " Format: YYYY/MM/DD");
                            }
                        }
                        else
                        {
                            correctDate = true;
                            formattedDate = input;
                        }
                    }
                }
            } while (!correctDate);
            return formattedDate;
        }

        /// <summary>
        /// This method checks if the user's inputted SIN matches the SIN of an employee in a database.
        /// If there is a match, the SIN is retruned.
        /// </summary>
        /// <returns>The SIN number</returns>
        public string GetEmployeeSIN()
        {
            string employeeSIN = "";
            string theEmployee = "";
            Console.WriteLine("Please enter the SIN or Business number of the Employee.");
            Console.WriteLine("\n SIN Format: NNN NNN NNN");
            Console.WriteLine("\n Business Number Format: NNNNN NNNN");
            string userInput = Console.ReadLine();
            Regex sinNum = new Regex(@"^\d{3}([- ]?)\d{3}\1\d{3}$|^0$|^\d{5}([- ]?)\d{4}$");
            Match sinMatch = sinNum.Match(userInput);
            while (!sinMatch.Success && userInput != "q")
            {
                Console.WriteLine("\n Please enter the Employee's SIN or Business Number in the provided format.");
                Console.WriteLine("\n SIN Format: NNN NNN NNN");
                Console.WriteLine("\n Business Number Format: NNNNN NNNN");
                userInput = Console.ReadLine();
                sinMatch = sinNum.Match(userInput);
            }
            if (userInput != "q")
            {
                //Take spaces out of the SIN
                string[] tokenizedSIN = userInput.Split(' ');
                string fullSIN = "";
                switch (tokenizedSIN.Length)
                {
                    case 1:
                        fullSIN = tokenizedSIN[0];
                        break;
                    case 2:
                        fullSIN = tokenizedSIN[0] + tokenizedSIN[1];
                        break;
                    case 3:
                        fullSIN = tokenizedSIN[0] + tokenizedSIN[1] + tokenizedSIN[2];
                        break;
                }
                theEmployee = Employees.FindAnEmployee(fullSIN);
            }
            try
            {
                string[] parsedDetails = theEmployee.Split('|');
                employeeSIN = parsedDetails[3];
            }
            catch (Exception)
            {
                employeeSIN = "";
            }
            return employeeSIN;
        }

    }

}
