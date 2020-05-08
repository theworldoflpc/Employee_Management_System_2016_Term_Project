/*
* FILE : ParttimeEmployee.cs
* PROJECT : INFO2180 - Software Quality I - Term Project - EMS 
* PROGRAMMERS : RSkowron, DPitters, CWilson, and lpcSetProg
* FIRST VERSION : 2016-11-17
* DESCRIPTION :
* This file contains the source code for the ParttimeEmployee class, this class inherits the first name, 
* last name, social insurance number, and date of birth from the Employee class (the parent)
* And will validate and get specific information intended for a Part time Employee that will be inputed.
*/

using System;
using System.Text.RegularExpressions;
using Supporting;

namespace AllEmployees
{
    /// <summary>
    /// The contents in this class inherits the first name, 
    /// last name, social insurance number, and date of birth from the Employee class (the parent)
    /// and will validate and get specific information intended for a Part time Employee that will be inputed.
    /// <BR>
    /// <B>Exception Strategy:</B> When validating the information for content towards the ParttimeEmployee Class
    /// each property for the specific variable will validate the incoming value. Throwing an exception if an error occurs.
    /// </summary>
    public class ParttimeEmployee : Employee
    {
        Logging logged = new Logging();

        private double _hourlyRate;

        public DateTime dateOfHire { get; set; }
        public DateTime dateOfTermination { get; set; }
        public double hourlyRate
        {
            get
            {
                return this._hourlyRate;
            }
            set
            {
                if (value < 0)
                {
                    logged.Log("double hourlyRate : " + value.ToString() + " - INVALID", "PartTimeEmployee", "hourlyRate");
                    throw new ArgumentException("Hourly Rate cannot be negative");
                }
                else
                {
                    this._hourlyRate = value;
                }
            }
        }

        /// <summary>
        /// Default constructor for a new Part Time Employee. Sets the data members to safe values.
        /// </summary>

        public ParttimeEmployee()
        {
            this.dateOfHire = DateTime.MinValue;
            this.dateOfTermination = DateTime.MinValue;
            this.hourlyRate = 0;
        }


        /// <summary>
        ///  A constructor that takes the employee's first and last name
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        public ParttimeEmployee(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }

        /// <summary>
        /// This constructor takes a set of parameters that specify a new Part Time Employee's details.
        /// </summary>
        /// <param name="firstName">The first name of the Part Time Employee.</param>
        /// <param name="lastName">The last name of the Part Time Employee.</param>
        /// <param name="SIN">The Social Insurance Number of the Part Time Employee.</param>
        /// <param name="dateOfBirth">The Date of Birth of the Part Time Employee.</param>
        /// <param name="dateOfHire">The Date that the Part Time Employee was hired.</param>
        /// <param name="dateOfTerm">The Date that the Part Time Employee was terminated.</param>
        /// <param name="hourlyRate">The amount paid to the Part Time Employee per hour of work.</param>

        public ParttimeEmployee(string firstName, string lastName, string SIN, DateTime dateOfBirth, DateTime dateOfHire, DateTime dateOfTerm, double hourlyRate)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.socialInsuranceNumber = SIN;
            this.dateOfBirth = dateOfBirth;
            this.dateOfHire = dateOfHire;
            this.dateOfTermination = dateOfTerm;
            this.hourlyRate = hourlyRate;
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
                this.hourlyRate < 1 ||  // Checks if the hourly rate is less than 1 dollar
                this.dateOfHire == DateTime.MinValue) // checks if the date of hire is empty..
            {
                isValid = false;
                logged.Log(lastName + ", " + firstName + " SIN: " + socialInsuranceNumber + " - INVALID", "PartTimeEmployee", "Validate");
            }
            else
            {
                isValid = true;
                logged.Log(lastName + ", " + firstName + " SIN: " + socialInsuranceNumber + " - VALID", "PartTimeEmployee", "Validate");
            }
            return isValid;
        }

        /// <summary>
        /// Displays details about the part time employee
        /// </summary>
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
            string empDetails = "Part Time Employee: \n"
                              + "Name: " + this.firstName + " " + this.lastName
                              + "\nSocial Insurance Number: " + outputSIN
                              + "\nDate of Birth: " + this.dateOfBirth.ToString("yyyy/MM/dd")
                              + "\nDate of Hire: " + this.dateOfHire.ToString("yyyy/MM/dd")
                              + "\nDate of Termination: " + outputDate
                              + "\nHourly Rate: " + this.hourlyRate.ToString("F");


            return empDetails;

        }
    }
}
