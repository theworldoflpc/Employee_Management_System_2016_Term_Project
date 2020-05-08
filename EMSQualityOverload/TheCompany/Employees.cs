/*
* FILE : Employees.cs
* PROJECT : INFO2180 - Software Quality I - Term Project - EMS 
* PROGRAMMERS : RSkowron, DPitters, CWilson, and lpcSetProg
* FIRST VERSION : 2016-11-17
* DESCRIPTION :
* This file contains the source for the Employee classes which the EMS program is 
* dependent on for functionality. It contains multiple-types of employees which are 
* stored in list collections.  
*/

using System;
using System.Collections.Generic;
using AllEmployees;
using Supporting;
using System.Linq;

namespace TheCompany
{
    /// <summary>
    /// A collection of lists which contain each type of the accepted Employees (ContractEmployee, FulltimeEmployee, 
    /// ParttimeEmployee, and SeasonalEmployee). This class is dependent on the AllEmployees class and its 
    /// inheritance for functionality. 
    /// <br>
    /// <b> Exception Strategy: </b> The Add and Modify methods all contain  try catch blocks, which can 
    /// catch exceptions from the AllEmployees class library classes.
    /// The Remove methods check to see if the employee exists before deletion. 
    /// 
    /// </summary>
    public class Employees
    {
        static List<FulltimeEmployee> FE = new List<FulltimeEmployee>(); ///< A  a list of all the Full time employees. />
        static List<ParttimeEmployee> PE = new List<ParttimeEmployee>(); ///< A  a list of all the Part time employees. />
        static List<ContractEmployee> CE = new List<ContractEmployee>(); ///< A  a list of all the Contract employees. />
        static List<SeasonalEmployee> SE = new List<SeasonalEmployee>(); ///< A  a list of all the Seasonal employees.  />
        static List<string> IE = new List<string>();

        /// <summary>
        /// Called by the UIMenu. It calls the FileIO ReadDatabase() method to get the data, and then stores it.
        /// </summary>
        public static void ReadDatabase()
        {
            //string name variables from text file 
            string employee_ID_temp;
            string fName_temp;
            string lName_temp;
            string SIN_temp;
            string season_temp;


            //dates variables from text file 
            DateTime dob_temp;
            DateTime doh_temp;
            DateTime dot_temp;
            DateTime contract_start_date;
            DateTime contract_stop_date;


            //wages for import from text file 
            double salary_temp;
            double hourly_rate_temp;
            double fixed_Contract_Amount_temp;
            double piecePay_temp;

            string[] employees = FileIO.ReadDatabase();

            //Get each line from the flatfile database and add it to a proper list.
            foreach (string emp in employees)
            {
                try
                {
                    //If it's a semicolon, ignore it
                    if (emp[0] == ';')
                    {
                        continue;
                    }

                    char[] delimiters = new char[] { '|' };
                    string[] parts = emp.Split(delimiters, StringSplitOptions.None); //can also be .none

                    for (int i = 0; i < parts.Length; i++)
                    {
                        //temp string array to hold the parts parsed from the text file 
                        employee_ID_temp = parts[0];

                        //Check for, and try to add, a fulltime employee
                        if (employee_ID_temp.Contains("FT"))
                        {
                            employee_ID_temp = parts[0];
                            lName_temp = parts[1];
                            fName_temp = parts[2];
                            SIN_temp = parts[3];
                            dob_temp = Convert.ToDateTime(TryParse(parts[4])); //wrapped in the try parse in case DOB = N/A, datetime nullable value 
                            doh_temp = Convert.ToDateTime(TryParse(parts[5])); //wrapped in the try parse in case DOB = N/A, datetime nullable value

                            if (parts[6] == "N/A")
                            {
                                dot_temp = DateTime.MinValue;
                            }
                            else
                            {
                                dot_temp = Convert.ToDateTime(TryParse(parts[6])); //wrapped in the try parse in case DOB = N/A, datetime nullable value 
                            }
                            salary_temp = Convert.ToDouble(parts[7]); //double incase salary has decimals             

                            Employees.AddFulltimeEmployee(fName_temp, lName_temp, SIN_temp, dob_temp, doh_temp, dot_temp, salary_temp);
                            break;
                        }
                        //Check for, and try to add, a parttime employee
                        else if (employee_ID_temp.Contains("PT"))
                        {
                            employee_ID_temp = parts[0];
                            lName_temp = parts[1];
                            fName_temp = parts[2];
                            SIN_temp = parts[3];
                            dob_temp = Convert.ToDateTime(TryParse(parts[4])); //wrapped in the try parse in case DOB = N/A, datetime nullable value 
                            doh_temp = Convert.ToDateTime(TryParse(parts[5])); //wrapped in the try parse in case DOB = N/A, datetime nullable value 
                            if (parts[6] == "N/A")
                            {
                                dot_temp = DateTime.MinValue;
                            }
                            else
                            {
                                dot_temp = Convert.ToDateTime(TryParse(parts[6])); //wrapped in the try parse in case DOB = N/A, datetime nullable value 
                            }
                            hourly_rate_temp = Convert.ToDouble(parts[7]); //double incase salary has decimals   

                            AddParttimeEmployee(fName_temp, lName_temp, SIN_temp, dob_temp, doh_temp, dot_temp, hourly_rate_temp);
                            break;

                        }
                        //Check for, and try to add, a contract employee
                        else if (employee_ID_temp.Contains("CT"))
                        {
                            employee_ID_temp = parts[0];
                            lName_temp = parts[1];
                            fName_temp = parts[2];
                            SIN_temp = parts[3];
                            dob_temp = Convert.ToDateTime(TryParse(parts[4])); //wrapped in the try parse in case DOB = N/A, datetime nullable value 
                            contract_start_date = Convert.ToDateTime(TryParse(parts[5])); //wrapped in the try parse in case DOB = N/A, datetime nullable value 
                            contract_stop_date = Convert.ToDateTime(TryParse(parts[6])); //wrapped in the try parse in case DOB = N/A, datetime nullable value 
                            fixed_Contract_Amount_temp = Convert.ToDouble(parts[7]); //double incase salary has decimals   

                            AddContractEmployee(fName_temp, lName_temp, SIN_temp, dob_temp, contract_start_date, contract_stop_date, fixed_Contract_Amount_temp);
                            break;
                        }
                        //Check for, and try to add, a seasonal employee
                        else if (employee_ID_temp.Contains("SN"))
                        {
                            employee_ID_temp = parts[0];
                            lName_temp = parts[1];
                            fName_temp = parts[2];
                            SIN_temp = parts[3];
                            dob_temp = Convert.ToDateTime(TryParse(parts[4])); //wrapped in the try parse in case DOB = N/A, datetime nullable value 
                            season_temp = parts[5]; //wrapped in the try parse in case DOB = N/A, datetime nullable value 
                            piecePay_temp = Convert.ToDouble(parts[6]); //double incase salary has decimals           

                            AddSeasonalEmployee(fName_temp, lName_temp, SIN_temp, dob_temp, season_temp, piecePay_temp);
                            break;
                        }
                        //If there are none, add it as an invalid employee.
                        else
                        {
                            AddInvalidEmployee(emp);
                        }
                    }
                }
                catch (Exception)
                {
                    Logging logger = new Logging();
                    logger.Log("Invalid data read from database - Error", "Employees", "ReadDatabase");
                    AddInvalidEmployee(emp);
                }
            }
            Logging log = new Logging();
            log.Log("Total records read: " + employees.Count() + ". Total valid: " + (employees.Count() - IE.Count()) + ". Total invalid: " + IE.Count, "Employees", "ReadDatabase");
        }

        ///<summary> Try parse. Need this to parse null dates for year. </summary>
        ///
        ///<remarks> Lcocarell, 12/10/2016. </remarks>
        ///
        ///<param name="text">The text.</param>
        ///
        ///<returns> A DateTime? </returns>
        public static DateTime? TryParse(string text)
        {
            DateTime date;
            if (DateTime.TryParse(text, out date))
            {
                return date;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// This method adds a new Full Time Employee to the database.
        /// </summary>
        /// <param name="fName">The first name of the Full Time Employee.</param>
        /// <param name="lName">The last name of the Full Time Employee.</param>
        /// <param name="SIN">The Social Insurance Number of the Full Time Employee.</param>
        /// <param name="dateOfBirth">The Date of Birth of the Full Time Employee.</param>
        /// <param name="dateOfHire">The Date that the Full Time Employee was hired.</param>
        /// <param name="dateOfTermination">The Date that the Full Time Employee was terminated.</param>
        /// <param name="salary">The total amount paid to the Full Time Employee over the course of a year.</param>
        /// <returns>A bool. true if employee was added, false otherwise.</returns>
        public static bool AddFulltimeEmployee(string fName, string lName, string SIN, DateTime dateOfBirth, DateTime dateOfHire, DateTime dateOfTermination, double salary)
        {
            bool empAdded = false;

            try
            {
                //Check if an employee with that sin number already exists
                bool employeeExists = FE.Any(x => x.socialInsuranceNumber == SIN);
                if (employeeExists)
                {
                    throw new Exception(); //The employee exists, it can't be added
                }
                else
                {
                    FulltimeEmployee emp = new FulltimeEmployee(fName, lName, SIN, dateOfBirth, dateOfHire, dateOfTermination, salary);
                    bool valid = emp.Validate();
                    if (valid)
                    {
                        FE.Add(emp);
                        Logging logger = new Logging();
                        logger.Log(lName + ", " + fName + " SIN: " + SIN + " - ADDED", "Employees", "AddFulltimeEmployee");
                        empAdded = true;
                    }
                }
            }

            catch (Exception)
            {
                empAdded = false;
            }

            return empAdded;
        }

        /// <summary>
        /// This method adds a new Part Time Employee to the database.
        /// </summary>
        /// <param name="fName">The first name of the Part Time Employee.</param>
        /// <param name="lName">The last name of the Part Time Employee.</param>
        /// <param name="SIN">The Social Insurance Number of the Part Time Employee.</param>
        /// <param name="dateOfBirth">The Date of Birth of the Part Time Employee.</param>
        /// <param name="dateOfHire">The Date that the Part Time Employee was hired.</param>
        /// <param name="dateOfTermination">The Date that the Part Time Employee was terminated.</param>
        /// <param name="hourlyRate">The amount paid to the Part Time Employee per hour of work.</param>
        /// <returns>A bool. true if employee was added, false otherwise.</returns>
        public static bool AddParttimeEmployee(string fName, string lName, string SIN, DateTime dateOfBirth, DateTime dateOfHire, DateTime dateOfTermination, double hourlyRate)
        {
            bool empAdded = false;

            try
            {
                //Check if an employee with that sin number already exists
                bool employeeExists = PE.Any(x => x.socialInsuranceNumber == SIN);
                if (employeeExists)
                {
                    throw new Exception(); //The employee exists, it can't be added
                }
                else
                {
                    ParttimeEmployee emp = new ParttimeEmployee(fName, lName, SIN, dateOfBirth, dateOfHire, dateOfTermination, hourlyRate);
                    bool valid = emp.Validate();
                    if (valid)
                    {
                        PE.Add(emp);
                        Logging logger = new Logging();
                        logger.Log(lName + ", " + fName + " SIN: " + SIN + " - ADDED", "Employees", "AddParttimeEmployee");
                        empAdded = true;
                    }
                }
            }

            catch (Exception)
            {
                empAdded = false;
            }

            return empAdded;
        }

        /// <summary>
        /// This method adds a new Contract Employee to the database.
        /// </summary>
        /// <param name="fName">The first name of the Contract Employee.</param>
        /// <param name="lName">The last name of the Contract Employee.</param>
        /// <param name="SIN">The Social Insurance Number of the Contract Employee.</param>
        /// <param name="dateOfBirth">The Date of Birth of the Contract Employee.</param>
        /// <param name="contractStartDate">The Date that the Contract Employee was signed.</param>
        /// <param name="contractStopDate">The Date that the Contract Employee's contract expires.</param>
        /// <param name="fixedContractAmount">The amount paid to the Contract Employee.</param>
        /// <returns>A bool. true if employee was added, false otherwise.</returns>
        public static bool AddContractEmployee(string fName, string lName, string SIN, DateTime dateOfBirth, DateTime contractStartDate, DateTime contractStopDate, double fixedContractAmount)
        {
            bool empAdded = false;

            try
            {
                //Check if an employee with that sin number already exists
                bool employeeExists = CE.Any(x => x.socialInsuranceNumber == SIN);
                if (employeeExists)
                {
                    throw new Exception(); //The employee exists, it can't be added
                }
                else
                {
                    ContractEmployee emp = new ContractEmployee(fName, lName, SIN, dateOfBirth, contractStartDate, contractStopDate, fixedContractAmount);
                    bool valid = emp.Validate();
                    if (valid)
                    {
                        CE.Add(emp);
                        Logging logger = new Logging();
                        logger.Log(lName + " BN: " + SIN + " - ADDED", "Employees", "AddContractEmployee");
                        empAdded = true;
                    }
                }
            }

            catch (Exception)
            {
                empAdded = false;
            }

            return empAdded;
        }

        /// <summary>
        /// This method adds a new Seasonal Employee to the database.
        /// </summary>
        /// <param name="fName">The first name of the Seasonal Employee.</param>
        /// <param name="lName">The last name of the Seasonal Employee.</param>
        /// <param name="SIN">The Social Insurance Number of the Seasonal Employee.</param>
        /// <param name="dateOfBirth">The Date of Birth of the Seasonal Employee.</param>
        /// <param name="season">The season that the Seasonal Employee will be working in.</param>
        /// <param name="piecePay">The amount paid to the Seasonal Employee per unit-of-work done.</param>
        /// <returns>A bool. true if employee was added, false otherwise.</returns>
        public static bool AddSeasonalEmployee(string fName, string lName, string SIN, DateTime dateOfBirth, string season, double piecePay)
        {
            bool empAdded = false;

            try
            {
                //Check if an employee with that sin number already exists
                bool employeeExists = SE.Any(x => x.socialInsuranceNumber == SIN);
                if (employeeExists)
                {
                    throw new Exception(); //The employee exists, it can't be added
                }
                else
                {
                    SeasonalEmployee emp = new SeasonalEmployee(fName, lName, SIN, dateOfBirth, piecePay, season);
                    bool valid = emp.Validate();
                    if (valid)
                    {
                        SE.Add(emp);
                        Logging logger = new Logging();
                        logger.Log(lName + ", " + fName + " SIN: " + SIN + " - ADDED", "Employees", "AddSeasonalEmployee");
                        empAdded = true;
                    }
                }
            }
            catch (Exception)
            {
                empAdded = false;
            }

            return empAdded;
        }

        /// <summary>
        /// This method adds a new Invalid Employee to the database.
        /// </summary>
        /// <param name="fName">The first name of the Seasonal Employee.</param>
        /// <param name="lName">The last name of the Seasonal Employee.</param>
        /// <param name="SIN">The Social Insurance Number of the Seasonal Employee.</param>
        /// <param name="dateOfBirth">The Date of Birth of the Seasonal Employee.</param>
        /// <param name="season">The season that the Seasonal Employee will be working in.</param>
        /// <param name="piecePay">The amount paid to the Seasonal Employee per unit-of-work done.</param>
        /// <returns>A bool. true if employee was added, false otherwise.</returns>
        public static void AddInvalidEmployee(string invalidEmployee)
        {
            IE.Add(invalidEmployee);
        }

        /// <summary>
        /// This method removes the given FulltimeEmployee from the database
        /// </summary>
        /// <param name="SIN"></param>
        /// <returns>A bool. true if employee was removed, false otherwise.</returns>
        public static bool RemoveFulltimeEmployee(string SIN)
        {
            bool empRemoved = false;

            foreach (FulltimeEmployee employee in FE)
            {
                if (employee.socialInsuranceNumber == SIN)
                {
                    Logging logger = new Logging();
                    logger.Log(employee.lastName + ", " + employee.firstName + " SIN: " + SIN + " - REMOVED", "Employees", "RemoveFulltimeEmployee");
                    FE.Remove(employee); //Remove the employee if it exists
                    empRemoved = true;
                    break;
                }
            }

            return empRemoved;
        }

        /// <summary>
        /// This method removes the given ParttimeEmployee from the database
        /// </summary>
        /// <param name="SIN"></param>
        /// <returns>A bool. true if employee was removed, false otherwise.</returns>
        public static bool RemoveParttimeEmployee(string SIN)
        {
            bool empRemoved = false;

            foreach (ParttimeEmployee employee in PE)
            {
                if (employee.socialInsuranceNumber == SIN)
                {
                    Logging logger = new Logging();
                    logger.Log(employee.lastName + ", " + employee.firstName + " SIN: " + SIN + " - REMOVED", "Employees", "RemoveParttimeEmployee");
                    PE.Remove(employee); //Remove the employee if it exists
                    empRemoved = true;
                    break;
                }
            }

            return empRemoved;
        }

        /// <summary>
        /// This method removes the given ContractEmployee from the database
        /// </summary>
        /// <param name="SIN"></param>
        /// <returns>A bool. true if employee was removed, false otherwise.</returns>
        public static bool RemoveContractEmployee(string SIN)
        {
            bool empRemoved = false;

            foreach (ContractEmployee employee in CE)
            {
                if (employee.socialInsuranceNumber == SIN)
                {
                    Logging logger = new Logging();
                    logger.Log(employee.lastName + " BN: " + SIN + " - REMOVED", "Employees", "RemoveContractEmployee");
                    CE.Remove(employee); //Remove the employee if it exists
                    empRemoved = true;
                    break;
                }
            }

            return empRemoved;
        }

        /// <summary>
        /// This method removes the given SeasonalEmployee from the database
        /// </summary>
        /// <param name="SIN"></param>
        /// <returns>A bool. true if employee was removed, false otherwise.</returns>
        public static bool RemoveSeasonalEmployee(string SIN)
        {
            bool empRemoved = false;

            foreach (SeasonalEmployee employee in SE)
            {
                if (employee.socialInsuranceNumber == SIN)
                {
                    Logging logger = new Logging();
                    logger.Log(employee.lastName + ", " + employee.firstName + " SIN: " + SIN + " - REMOVED", "Employees", "RemoveSeasonalEmployee");
                    SE.Remove(employee); //Remove the employee if it exists
                    empRemoved = true;
                    break;
                }
            }

            return empRemoved;
        }

        /// <summary>
        /// This method modifies the given FulltimeEmployee with the given change.
        /// </summary>
        /// <param name="SIN"></param>
        /// <param name="nameOfChange">The name of the attribute to be changed.</param>
        /// <param name="change">The value of the change.</param>
        /// <returns>A bool. true if employee was modified, false otherwise.</returns>
        public static bool ModifyFulltimeEmployee(string SIN, string nameOfChange, string change)
        {
            bool empModified = false;

            //Attempt to modify the fulltime employee
            foreach (FulltimeEmployee emp in FE)
            {
                if (emp.socialInsuranceNumber == SIN)
                {
                    //Check which attribute to change, and attempt to change it.
                    if (nameOfChange == "firstName")
                    {
                        try
                        {
                            emp.firstName = change;
                            empModified = true;
                        }
                        catch (Exception)
                        {
                            empModified = false;
                            return empModified;
                        }
                    }
                    else if (nameOfChange == "lastName")
                    {
                        try
                        {
                            emp.lastName = change;
                            empModified = true;
                        }
                        catch (Exception)
                        {
                            empModified = false;
                        }
                    }
                    else if (nameOfChange == "dateOfBirth")
                    {
                        try
                        {
                            emp.dateOfBirth = Convert.ToDateTime(change);
                            empModified = true;
                        }
                        catch (Exception)
                        {
                            empModified = false;
                        }
                    }
                    else if (nameOfChange == "dateOfHire")
                    {
                        try
                        {
                            emp.dateOfHire = Convert.ToDateTime(change);
                            empModified = true;
                        }
                        catch (Exception)
                        {
                            empModified = false;
                            return empModified;
                        }
                    }
                    else if (nameOfChange == "dateOfTermination")
                    {
                        try
                        {
                            emp.dateOfTermination = Convert.ToDateTime(change);
                            empModified = true;
                        }
                        catch (Exception)
                        {
                            empModified = false;
                            return empModified;
                        }
                    }
                    else if (nameOfChange == "salary")
                    {
                        try
                        {
                            emp.salary = Convert.ToDouble(change);
                            empModified = true;
                        }
                        catch (Exception)
                        {
                            empModified = false;
                            return empModified;
                        }
                    }

                    empModified = emp.Validate();

                    Logging logger = new Logging();
                    logger.Log("SIN: " + SIN + " - MODIFIED " + nameOfChange + " TO " + change,
                        "Employees", "ModifyFulltimeEmployee");
                    return empModified;
                }
            }

            return empModified;
        }

        /// <summary>
        /// This method modifies the given ParttimeEmployee with the given change.
        /// </summary>
        /// <param name="SIN"></param>
        /// <param name="nameOfChange">The name of the attribute to be changed.</param>
        /// <param name="change">The value of the change.</param>
        /// <returns>A bool. true if employee was modified, false otherwise.</returns>
        public static bool ModifyParttimeEmployee(string SIN, string nameOfChange, string change)
        {
            bool empModified = false;

            //Attempt to modify the parttime employee
            foreach (ParttimeEmployee emp in PE)
            {
                if (emp.socialInsuranceNumber == SIN)
                {
                    //Check which attribute to change, and attempt to change it.
                    if (nameOfChange == "firstName")
                    {
                        try
                        {
                            emp.firstName = change;
                            empModified = true;
                        }
                        catch (Exception)
                        {
                            empModified = false;
                            return empModified;
                        }
                    }
                    else if (nameOfChange == "lastName")
                    {
                        try
                        {
                            emp.lastName = change;
                            empModified = true;
                        }
                        catch (Exception)
                        {
                            empModified = false;
                            return empModified;
                        }
                    }
                    else if (nameOfChange == "dateOfBirth")
                    {
                        try
                        {
                            emp.dateOfBirth = Convert.ToDateTime(change);
                            empModified = true;
                        }
                        catch (Exception)
                        {
                            empModified = false;
                            return empModified;
                        }
                    }
                    else if (nameOfChange == "dateOfHire")
                    {
                        try
                        {
                            emp.dateOfHire = Convert.ToDateTime(change);
                            empModified = true;
                        }
                        catch (Exception)
                        {
                            empModified = false;
                            return empModified;
                        }
                    }
                    else if (nameOfChange == "dateOfTermination")
                    {
                        try
                        {
                            emp.dateOfTermination = Convert.ToDateTime(change);
                            empModified = true;
                        }
                        catch (Exception)
                        {
                            empModified = false;
                            return empModified;
                        }
                    }
                    else if (nameOfChange == "hourlyRate")
                    {
                        try
                        {
                            emp.hourlyRate = Convert.ToDouble(change);
                            empModified = true;
                        }
                        catch (Exception)
                        {
                            empModified = false;
                            return empModified;
                        }
                    }

                    empModified = emp.Validate();

                    Logging logger = new Logging();
                    logger.Log("SIN: " + SIN + " - MODIFIED " + nameOfChange + " TO " + change,
                        "Employees", "ModifyParttimeEmployee");
                    return empModified;
                }
            }

            return empModified;
        }

        /// <summary>
        /// This method modifies the given ContractEmployee with the given change.
        /// </summary>
        /// <param name="SIN"></param>
        /// <param name="nameOfChange">The name of the attribute to be changed.</param>
        /// <param name="change">The value of the change.</param>
        /// <returns>A bool. true if employee was modified, false otherwise.</returns>
        public static bool ModifyContractEmployee(string SIN, string nameOfChange, string change)
        {
            bool empModified = false;

            //Attempt to modify the parttime employee
            foreach (ContractEmployee emp in CE)
            {
                if (emp.socialInsuranceNumber == SIN)
                {
                    //Check which attribute to change, and attempt to change it.
                    if (nameOfChange == "firstName")
                    {
                        try
                        {
                            emp.firstName = change;
                            empModified = true;
                        }
                        catch (Exception)
                        {
                            empModified = false;
                            return empModified;
                        }
                    }
                    else if (nameOfChange == "lastName")
                    {
                        try
                        {
                            emp.lastName = change;
                            empModified = true;
                        }
                        catch (Exception)
                        {
                            empModified = false;
                            return empModified;
                        }
                    }
                    else if (nameOfChange == "dateOfBirth")
                    {
                        try
                        {
                            emp.dateOfBirth = Convert.ToDateTime(change);
                            empModified = true;
                        }
                        catch (Exception)
                        {
                            empModified = false;
                            return empModified;
                        }
                    }
                    else if (nameOfChange == "contractStartDate")
                    {
                        try
                        {
                            emp.contractStartDate = Convert.ToDateTime(change);
                            empModified = true;
                        }
                        catch (Exception)
                        {
                            empModified = false;
                            return empModified;
                        }
                    }
                    else if (nameOfChange == "contractStopDate")
                    {
                        try
                        {
                            emp.contractStopDate = Convert.ToDateTime(change);
                            empModified = true;
                        }
                        catch (Exception)
                        {
                            empModified = false;
                            return empModified;
                        }
                    }
                    else if (nameOfChange == "fixedContractAmount")
                    {
                        try
                        {
                            emp.fixedContractAmount = Convert.ToDouble(change);
                            empModified = true;
                        }
                        catch (Exception)
                        {
                            empModified = false;
                            return empModified;
                        }
                    }

                    empModified = emp.Validate();

                    Logging logger = new Logging();
                    logger.Log("BN: " + SIN + " - MODIFIED " + nameOfChange + " TO " + change,
                        "Employees", "ModifyContractEmployee");
                    return empModified;
                }
            }

            return empModified;
        }

        /// <summary>
        /// This method modifies the given SeasonalEmployee with the given change.
        /// </summary>
        /// <param name="SIN"></param>
        /// <param name="nameOfChange">The name of the attribute to be changed.</param>
        /// <param name="change">The value of the change.</param>
        /// <returns>A bool. true if employee was modified, false otherwise.</returns>
        public static bool ModifySeasonalEmployee(string SIN, string nameOfChange, string change)
        {
            bool empModified = false;

            //Attempt to modify the parttime employee
            foreach (SeasonalEmployee emp in SE)
            {
                if (emp.socialInsuranceNumber == SIN)
                {
                    //Check which attribute to change, and attempt to change it.
                    if (nameOfChange == "firstName")
                    {
                        try
                        {
                            emp.firstName = change;
                            empModified = true;
                        }
                        catch (Exception)
                        {
                            empModified = false;
                            return empModified;
                        }
                    }
                    else if (nameOfChange == "lastName")
                    {
                        try
                        {
                            emp.lastName = change;
                            empModified = true;
                        }
                        catch (Exception)
                        {
                            empModified = false;
                            return empModified;
                        }
                    }
                    else if (nameOfChange == "dateOfBirth")
                    {
                        try
                        {
                            emp.dateOfBirth = Convert.ToDateTime(change);
                            empModified = true;
                        }
                        catch (Exception)
                        {
                            empModified = false;
                            return empModified;
                        }
                    }
                    else if (nameOfChange == "season")
                    {
                        try
                        {
                            emp.season = change;
                            empModified = true;
                        }
                        catch (Exception)
                        {
                            empModified = false;
                            return empModified;
                        }
                    }
                    else if (nameOfChange == "piecePay")
                    {
                        try
                        {
                            emp.piecePay = Convert.ToDouble(change);
                            empModified = true;
                        }
                        catch (Exception)
                        {
                            empModified = false;
                            return empModified;
                        }
                    }

                    empModified = emp.Validate();

                    Logging logger = new Logging();
                    logger.Log("SIN: " + SIN + " - MODIFIED " + nameOfChange + " TO " + change,
                        "Employees", "ModifySeasonalEmployee");
                    return empModified;
                }
            }
            return empModified;
        }

        /// <summary>
        /// Gets and returns the list of fulltime employees so the UI can print them to the user.
        /// </summary>
        /// <returns>The list of all fulltime employees</returns>
        public static List<FulltimeEmployee> GetFulltimeEmployees()
        {
            //First, sort the list
            FE = FE.OrderBy(x => x.socialInsuranceNumber).ToList();

            return FE;
        }

        /// <summary>
        /// Gets and returns the list of parttime employees so the UI can print them to the user.
        /// </summary>
        /// <returns>The list of all parttime employees</returns>
        public static List<ParttimeEmployee> GetParttimeEmployees()
        {
            //First, sort the list
            PE = PE.OrderBy(x => x.socialInsuranceNumber).ToList();

            return PE;
        }

        /// <summary>
        /// Gets and returns the list of contract employees so the UI can print them to the user.
        /// </summary>
        /// <returns>The list of all contract employees</returns>
        public static List<ContractEmployee> GetContractEmployees()
        {
            //First, sort the list
            CE = CE.OrderBy(x => x.socialInsuranceNumber).ToList();

            return CE;
        }

        /// <summary>
        /// Gets and returns the list of seasonal employees so the UI can print them to the user.
        /// </summary>
        /// <returns>The list of all seasonal employees</returns>
        public static List<SeasonalEmployee> GetSeasonalEmployees()
        {
            //First, sort the list
            SE = SE.OrderBy(x => x.socialInsuranceNumber).ToList();

            return SE;
        }

        /// <summary>
        /// Sorts the employees by SIN number, and calls the FileIO method to write to the database.
        /// </summary>
        /// <returns>A bool which represents whether or not the employees were successfully written to the database.</returns>
        public static bool WriteEmployeesToDatabase()
        {
            List<string> employees = new List<string>();
            bool dbWrittenTo = true;

            //First, sort the list
            FE = FE.OrderBy(x => x.socialInsuranceNumber).ToList();
            PE = PE.OrderBy(x => x.socialInsuranceNumber).ToList();
            CE = CE.OrderBy(x => x.socialInsuranceNumber).ToList();
            SE = SE.OrderBy(x => x.socialInsuranceNumber).ToList();

            //Add the fulltime employees to the list
            foreach (FulltimeEmployee emp in FE)
            {
                string employee = "FT" + "|";                                    //The fulltime status
                employee = employee + emp.lastName + "|" + emp.firstName + "|"; //The name
                employee = employee + emp.socialInsuranceNumber + "|";          //The SIN number (primary key)
                employee = employee + emp.dateOfBirth.ToString() + "|";         //The date of birth
                employee = employee + emp.dateOfHire.ToString() + "|";          //The date of hire
                if (emp.dateOfTermination == DateTime.MinValue)
                {
                    employee = employee + "N/A" + "|";   //The date of termination
                }
                else
                {
                    employee = employee + emp.dateOfTermination.ToString() + "|";   //The date of termination
                }
                employee = employee + emp.salary.ToString();                    //The salary
                employees.Add(employee);
            }

            //Add the parttime employees to the list
            foreach (ParttimeEmployee emp in PE)
            {
                string employee = "PT" + "|";                                    //The fulltime status
                employee = employee + emp.lastName + "|" + emp.firstName + "|"; //The name
                employee = employee + emp.socialInsuranceNumber + "|";          //The SIN number (primary key)
                employee = employee + emp.dateOfBirth.ToString() + "|";         //The date of birth
                employee = employee + emp.dateOfHire.ToString() + "|";          //The date of hire
                if (emp.dateOfTermination == DateTime.MinValue)
                {
                    employee = employee + "N/A" + "|";   //The date of termination
                }
                else
                {
                    employee = employee + emp.dateOfTermination.ToString() + "|";   //The date of termination
                }
                employee = employee + emp.hourlyRate.ToString("F");             //The hourly rate
                employees.Add(employee);
            }

            //Add the contract employees to the list
            foreach (ContractEmployee emp in CE)
            {
                string employee = "CT" + "|";                                    //The fulltime status
                employee = employee + emp.lastName + "|" + emp.firstName + "|"; //The name
                employee = employee + emp.socialInsuranceNumber + "|";          //The SIN number (primary key)
                employee = employee + emp.dateOfBirth.ToString() + "|";         //The date of birth
                employee = employee + emp.contractStartDate.ToString() + "|";   //The contract start date
                employee = employee + emp.contractStopDate.ToString() + "|";    //The contract stop date
                employee = employee + emp.fixedContractAmount.ToString();       //The fixed contract amount
                employees.Add(employee);
            }

            //Add the contract employees to the list
            foreach (SeasonalEmployee emp in SE)
            {
                string employee = "SN" + "|";                                    //The fulltime status
                employee = employee + emp.lastName + "|" + emp.firstName + "|"; //The name
                employee = employee + emp.socialInsuranceNumber + "|";          //The SIN number (primary key)
                employee = employee + emp.dateOfBirth.ToString() + "|";         //The date of birth
                employee = employee + emp.season + "|";                         //The season of work
                employee = employee + emp.piecePay.ToString("F");               //The piecepay
                employees.Add(employee);
            }

            //Add the invalid employees to the list
            foreach (string emp in IE)
            {
                employees.Add(emp);
            }

            //Convert the list to a string array
            FileIO fileWrite = new FileIO();
            fileWrite.WriteDatabase(employees.ToArray(), employees.Count() - IE.Count(), IE.Count());

            return dbWrittenTo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SIN"></param>
        /// <returns></returns>
        public static string FindAnEmployee(string SIN)
        {
            string employee = "";
            //Add the fulltime employees to the list
            foreach (FulltimeEmployee emp in FE)
            {
                if (emp.socialInsuranceNumber == SIN)
                {
                    employee = "FT" + "|";                                           //The fulltime status
                    employee = employee + emp.lastName + "|" + emp.firstName + "|"; //The name
                    employee = employee + emp.socialInsuranceNumber + "|";          //The SIN number (primary key)
                    employee = employee + emp.dateOfBirth.ToString() + "|";         //The date of birth
                    employee = employee + emp.dateOfHire.ToString() + "|";          //The date of hire
                    employee = employee + emp.dateOfTermination.ToString() + "|";   //The date of termination
                    employee = employee + emp.salary.ToString() + "|";              //The salary
                    return employee;
                }
            }

            //Add the parttime employees to the list
            foreach (ParttimeEmployee emp in PE)
            {
                if (emp.socialInsuranceNumber == SIN)
                {
                    employee = "PT" + "|";                                           //The fulltime status
                    employee = employee + emp.lastName + "|" + emp.firstName + "|"; //The name
                    employee = employee + emp.socialInsuranceNumber + "|";          //The SIN number (primary key)
                    employee = employee + emp.dateOfBirth.ToString() + "|";         //The date of birth
                    employee = employee + emp.dateOfHire.ToString() + "|";          //The date of hire
                    employee = employee + emp.dateOfTermination.ToString() + "|";   //The date of termination
                    employee = employee + emp.hourlyRate.ToString("F") + "|";       //The hourly rate
                    return employee;
                }
            }

            //Add the contract employees to the list
            foreach (ContractEmployee emp in CE)
            {
                if (emp.socialInsuranceNumber == SIN)
                {
                    employee = "CT" + "|";                                           //The fulltime status
                    employee = employee + emp.lastName + "|" + emp.firstName + "|"; //The name
                    employee = employee + emp.socialInsuranceNumber + "|";          //The SIN number (primary key)
                    employee = employee + emp.dateOfBirth.ToString() + "|";         //The date of birth
                    employee = employee + emp.contractStartDate.ToString() + "|";   //The contract start date
                    employee = employee + emp.contractStopDate.ToString() + "|";    //The contract stop date
                    employee = employee + emp.fixedContractAmount.ToString() + "|"; //The fixed contract amount
                    return employee;
                }
            }

            //Add the seasonal employees to the list
            foreach (SeasonalEmployee emp in SE)
            {
                if (emp.socialInsuranceNumber == SIN)
                {
                    employee = "SN" + "|";                                           //The fulltime status
                    employee = employee + emp.lastName + "|" + emp.firstName + "|"; //The name
                    employee = employee + emp.socialInsuranceNumber + "|";          //The SIN number (primary key)
                    employee = employee + emp.dateOfBirth.ToString() + "|";         //The date of birth
                    employee = employee + emp.season + "|";                         //The season of work
                    employee = employee + emp.piecePay.ToString("F") + "|";         //The piecepay
                    return employee;
                }
            }

            return employee;
        }
    }
}
