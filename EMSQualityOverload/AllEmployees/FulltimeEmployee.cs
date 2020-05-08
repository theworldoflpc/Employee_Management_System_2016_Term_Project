/*
* FILE : FulltimeEmployee.cs
* PROJECT : INFO2180 - Software Quality I - Term Project - EMS 
* PROGRAMMERS : RSkowron, DPitters, CWilson, and lpcSetProg
* FIRST VERSION : 2016-11-17
* DESCRIPTION :
* This file contains the source code for the FulltimeEmployee
*/

using System;
using System.Text.RegularExpressions;
using Supporting;

namespace AllEmployees
{
    /// <summary>
    ///  The contents in this class inherits the first name, 
    /// last name, social insurance number, and date of birth from the Employee class (the parent)
    /// and will validate and get specific information intended for a Full time employee that will be inputed.
    ///  <BR>
    ///  <B>Exception Strategy:</B> When validating the information for content towards the Full Time Employee Class,  
    ///  each property for the specific variable will validate the incoming value.
    ///  Throwing an exception if an error occurs.
    /// </summary>
    public class FulltimeEmployee : Employee
    {


        Logging logged = new Logging();
        private double _salary;
        public DateTime dateOfHire { get; set; }

        public DateTime dateOfTermination { get; set; }

        public double salary
        {
            get
            {
                return this._salary;
            }
            set
            {
                if (value < 0)
                {

                    logged.Log("int salary : " + value.ToString() + " - INVALID", "FullTimeEmployee", "salary");
                    throw new ArgumentException("Salary cannot be negative");
                }
                else
                {
                    this._salary = value;
                }
            }
        }

        /// <summary>
        /// Default constructor for a new Full Time Employee. Sets the data members to safe values.
        /// </summary>

        public FulltimeEmployee()
        {
            dateOfHire = DateTime.MinValue;
            dateOfTermination = DateTime.MinValue;
            salary = 0;
        }

        /// <summary>
        ///  A constructor that takes the employee's first and last name
        /// </summary>
        /// <param name="firstName">The first name of the Full Time Employee.</param>
        /// <param name="lastName">The last name of the Full Time Employee.</param>
        public FulltimeEmployee(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }


        /// <summary>
        /// This constructor takes a set of parameters that specify a new Full Time Employee's details.
        /// </summary>
        /// <param name="firstName">The first name of the Full Time Employee.</param>
        /// <param name="lastName">The last name of the Full Time Employee.</param>
        /// <param name="SIN">The Social Insurance Number of the Full Time Employee.</param>
        /// <param name="dateOfBirth">The Date of Birth of the Full Time Employee.</param>
        /// <param name="dateOfHire">The Date that the Full Time Employee was hired.</param>
        /// <param name="dateOfTerm">The Date that the Full Time Employee was terminated.</param>
        /// <param name="newSalary">The total amount paid to the Full Time Employee over the course of a year.</param>
        public FulltimeEmployee(string firstName, string lastName, string SIN, DateTime dateOfBirth, DateTime dateOfHire, DateTime dateOfTerm, double newSalary)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.socialInsuranceNumber = SIN;
            this.dateOfBirth = dateOfBirth;

            this.dateOfHire = dateOfHire;
            this.dateOfTermination = dateOfTerm;
            this.salary = newSalary;
        }

        /// <summary>
        /// This validate method checks if the Employee Object itself is valid
        /// </summary>
        /// <returns> Returns if the object is valid or not. </returns>
        public override bool Validate()
        {
            bool isValid = true;

            // This is checking all values in the object again.
            // If any of these values are invalid. Then this object is invalid, returning false.
            if (string.IsNullOrWhiteSpace(this.firstName) || // Checks if the first name string is empty
                string.IsNullOrWhiteSpace(this.lastName) || // Checks if the last name string is empty
                Regex.IsMatch(this.firstName, @"\d") || // Finds numbers in the user's first name
                Regex.IsMatch(this.lastName, @"\d") || // Finds numbers in the user's last name
                Regex.IsMatch(this.socialInsuranceNumber, "^[a-zA-Z]+$") ||  // Checks if letters are in the social insurance number
                this.dateOfBirth == DateTime.MinValue ||    // Check if the date of birth is not set
                this.socialInsuranceNumber.Length != 9 ||   // Checks if the SIN number is not the correct length. 
                this.salary < 1 ||  // Checks if the salary is less than 1 dollar
                this.dateOfHire == DateTime.MinValue) // checks if the date of hire is empty..
            {
                isValid = false;
                logged.Log(lastName + ", " + firstName + " SIN: " + socialInsuranceNumber + " - INVALID", "FullTimeEmployee", "Validate");
            }
            else
            {
                isValid = true;
                logged.Log(lastName + ", " + firstName + " SIN: " + socialInsuranceNumber + " - VALID", "FullTimeEmployee", "Validate");
            }
            
            return isValid;
        }


        ///
        ///<summary>
        /// &lt;summary&gt;
        /// Displays the details about the Full time employee.
        ///</summary>
        ///
        ///
        ///
        public string Details()
        {
            string outputSIN = this.socialInsuranceNumber[0].ToString()
                + this.socialInsuranceNumber[1].ToString()
                + this.socialInsuranceNumber[2].ToString()
                + " "
                + this.socialInsuranceNumber[3].ToString()
                + this.socialInsuranceNumber[4].ToString()
                + this.socialInsuranceNumber[5].ToString()
                + " "
                + this.socialInsuranceNumber[6].ToString()
                + this.socialInsuranceNumber[7].ToString()
                + this.socialInsuranceNumber[8].ToString();
            string outputDate = "";
            DateTime checkDate = this.dateOfTermination;
            if (checkDate == DateTime.MinValue)
            {
                outputDate = "N/A";
            }
            else
            {
                outputDate = this.dateOfTermination.ToString("yyyy/MM/dd");
            }
            string empDetails = "Full Time Employee: \n"
                              + "Name: " + this.firstName + " " + this.lastName
                              + "\nSocial Insurance Number: " + outputSIN
                              + "\nDate of Birth: " + this.dateOfBirth.ToString("yyyy/MM/dd")
                              + "\nDate of Hire: " + this.dateOfHire.ToString("yyyy/MM/dd")
                              + "\nDate of Termination: " + outputDate
                              + "\nSalary: " + this.salary.ToString("F");

            return empDetails;
        }


    }
}
