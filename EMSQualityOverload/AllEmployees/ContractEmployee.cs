/*
* FILE : ContractEmployee.cs
* PROJECT : INFO2180 - Software Quality I - Term Project - EMS 
* PROGRAMMERS : RSkowron, DPitters, CWilson, and lpcSetProg
* FIRST VERSION : 2016-11-17
* DESCRIPTION :
* This file contains the source code for the Contract Employee class. This class inherits the first name, 
* last name, social insurance number, and date of birth from the Employee class (the parent)
* And will validate and get specific information intended for a Contract employee that will be inputed.
*/

using System;
using System.Text.RegularExpressions;
using Supporting;

namespace AllEmployees
{
    /// <summary>
    /// The contents in this class inherits the first name, 
    /// last name, social insurance number, and date of birth from the Employee class (the parent)
    /// and will validate and get specific information intended for a Contract employee that will be inputed.
    /// <BR>
    /// <B>Exception Strategy:</B> When validating the information for content towards the Contract Employee Class
    /// each property for the specific variable will validate the incoming value. Throwing an exception if an error occurs.
    /// </summary>
    public class ContractEmployee : Employee
    {
        Logging logged = new Logging();

        private double _fixedContractAmount;

        public DateTime contractStartDate { get; set; }
        public DateTime contractStopDate { get; set; }
        public double fixedContractAmount
        {
            get
            {
                return this._fixedContractAmount;
            }
            set
            {
                if (value < 0)
                {
                    logged.Log("double fixedContractAmount : " + value.ToString() + " - INVALID", "ContractEmployee", "fixedContractAmount");

                    throw new ArgumentException("Fixed Contract Amount cannot be negative");
                }
                else
                {
                    this._fixedContractAmount = value;
                }
            }
        }


        /// <summary>
        /// Default constructor for a new Contracted Employee. Sets the data members to safe values.
        /// </summary>
        public ContractEmployee()
        {
            this.contractStartDate = DateTime.MinValue;
            this.contractStopDate = DateTime.MinValue;
            this.fixedContractAmount = 0.0;
        }


        /// <summary>
        ///  A constructor that takes the employee's first and last name
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        public ContractEmployee(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }
        /// <summary>
        /// This constructor is used to get the information about the contract employee
        /// </summary>
        /// <param name="firstName">The first name of the Contract Employee.</param>
        /// <param name="lastName">The last name of the Contract Employee.</param>
        /// <param name="SIN">The Business Number of the corparation for the Contract Employee.</param>
        /// <param name="dateOfBirth">The Date of the incorparation of the Contract Employee.</param>
        /// <param name="contractStartDate">The starting date when the employee starts their contract.</param>
        /// <param name="contractStopDate">The stopping date when the employee finishes their contract.</param>
        /// <param name="fixedContractAmount"> The fixed payment they will recieve for the contract</param>
        public ContractEmployee(string firstName, string lastName, string SIN, DateTime dateOfBirth, DateTime contractStartDate, DateTime contractStopDate, double fixedContractAmount)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.socialInsuranceNumber = SIN;
            this.dateOfBirth = dateOfBirth;
            this.contractStartDate = contractStartDate;
            this.contractStopDate = contractStopDate;
            this.fixedContractAmount = fixedContractAmount;


        }

        /// <summary>
        /// This validate method checks if the Employee Object itself is valid
        /// </summary>
        /// <returns> Returns if the object is valid or not. </returns>
        public override bool Validate()
        {

            //Parse the incoming year to obtain the last two numbers of the year. Ex. 19(XX) <- get these. 
            string[] parsedDate = this.dateOfBirth.ToString("yyyy/MM/dd").Split('/'); // Split the date.
            char[] contractYear = parsedDate[0].ToCharArray(); // get the year
            string frontOfBN = contractYear[2].ToString() + contractYear[3].ToString(); // get the last two numbers of the year
            string firstTwoOfBN = this.socialInsuranceNumber.Substring(0, 2); // get the first two numbers of the business number


            bool isValid = true;

            // This is checking all values in the object again.
            // If any of these values are invalid. Then this object is invalid, returning false.

            if (!string.IsNullOrWhiteSpace(this.firstName) || // Checks if the first name string is empty
                string.IsNullOrWhiteSpace(this.lastName) || // Checks if the Company Name string is empty
                Regex.IsMatch(this.firstName, @"\d") || // Finds numbers in the user's first name
                Regex.IsMatch(this.socialInsuranceNumber, "^[a-zA-Z]+$") ||  // Checks if letters are in the business number
                this.dateOfBirth == DateTime.MinValue ||    // Check if the date of incorparation is empty
                this.socialInsuranceNumber.Length != 9 ||   // Checks if the business number is not the correct length. 
                this.fixedContractAmount < 1 ||  // Checks if the hourly rate is less than 1 dollar
                this.contractStartDate == DateTime.MinValue || // checks if the date of hire is empty..
                this.contractStopDate == DateTime.MinValue ||  // checks if the date of hire is empty..
                !firstTwoOfBN.Contains(frontOfBN)
                ) // Checks if business number has the first two numbers of the last two numbers od the year of incorparation.
            {

                isValid = false;
                logged.Log(lastName + " BN: " + socialInsuranceNumber + " - INVALID", "ContractEmployee", "Validate");
            }
            else
            {
                isValid = true;
                logged.Log(lastName + " BN: " + socialInsuranceNumber + " - VALID", "ContractEmployee", "Validate");
            }


            return isValid;
        }

        /// <summary>
        /// Displays details about the contract employee
        /// </summary>
        public string Details()
        {
            //output of business number has to be NNNNN NNNN
            string outputBN1 = this.socialInsuranceNumber[0].ToString()
                + this.socialInsuranceNumber[1].ToString()
                + this.socialInsuranceNumber[2].ToString()
                + this.socialInsuranceNumber[3].ToString()
                + this.socialInsuranceNumber[4].ToString();

            string outputBN2 = this.socialInsuranceNumber[5].ToString()
                + this.socialInsuranceNumber[6].ToString()
                + this.socialInsuranceNumber[7].ToString()
                + this.socialInsuranceNumber[8].ToString();

            string outputBN = outputBN1 + " " + outputBN2;

            string empDetails = "Contract Employee: \n"
                              + "Name: " + this.firstName + " " + this.lastName
                              + "\nBusiness Number: " + outputBN
                              + "\nDate of Incorporation: " + this.dateOfBirth.ToString("yyyy/MM/dd")
                              + "\nContract Start Date: " + this.contractStartDate.ToString("yyyy/MM/dd")
                              + "\nContract Stop Date: " + this.contractStopDate.ToString("yyyy/MM/dd")
                              + "\nFixed Contract Amount: " + this.fixedContractAmount.ToString("F");


            return empDetails;
        }
    }
}
