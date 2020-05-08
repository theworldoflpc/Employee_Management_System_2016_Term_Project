/*
* FILE : SeasonalEmployee.cs
* PROJECT : INFO2180 - Software Quality I - Term Project - EMS 
* PROGRAMMERS : RSkowron, DPitters, CWilson, and lpcSetProg
* FIRST VERSION : 2016-11-17
* DESCRIPTION :
* This file contains the source code for the SeasonalEmployee class, 
* this class inherits the first name, last name, social insurance number, and date 
* of birth from the Employee class (the parent), it will validate and get specific information intended
* for a Seasonal Employee that will be inputed.
*/

using System;
using System.Text.RegularExpressions;
using Supporting;

namespace AllEmployees
{
    /// <summary>
    ///  When validating the information for content towards the Seasonal Employee Class
    ///  each property for the specific variable will validate the incoming value.
    ///  <BR>
    ///  <B>Exception Strategy:</B> Throwing an exception if an error occurs.
    /// </summary>
    public class SeasonalEmployee : Employee
    {
        Logging logged = new Logging();

        private double _piecePay;
        private string _season;
        public string season {
            get
            {
                return this._season;
            }
            set
            {
                string incomingSeason = value;
                incomingSeason = incomingSeason.ToLower();
                if (incomingSeason == "winter" || incomingSeason == "fall" || incomingSeason == "summer" || incomingSeason == "spring")
                {
                    this._season = value;
                }
                else
                {
                    throw new ArgumentException("Season must be Spring, Summer, Winter or Fall.");
                }
            }
        }

        public double piecePay
        {
            get
            {
                return this._piecePay;
            }
            set
            {
                if (value < 0)
                {
                    logged.Log("double piecePay : " + value.ToString() + " - INVALID", "SeasonalEmployee", "piecePay");

                    throw new ArgumentException("PiecePay cannot be negative");
                }
                else
                {
                    this._piecePay = value;
                }
            }
        }

        /// <summary>
        /// Default constructor for a new Seasonal Employee. Sets the data members to safe values.
        /// </summary>
        /// <returns>Nothing (void).</returns>
        public SeasonalEmployee()
        {
            this.piecePay = 0.0;
            this.season = string.Empty;
        }

        /// <summary>
        /// Constructor which sets only the first and last name.
        /// </summary>
        /// <param name="firstName">The first name of the Seasonal Employee.</param>
        /// <param name="lastName">The last name of the Seasonal Employee.</param>
        public SeasonalEmployee(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.piecePay = 0.0;
            this.season = string.Empty;
        }

        /// <summary>
        /// This constructor takes a set of parameters that specify a new Seasonal Employee's details.
        /// </summary>
        /// <param name="firstName">The first name of the Seasonal Employee.</param>
        /// <param name="lastName">The last name of the Seasonal Employee.</param>
        /// <param name="SIN">The Social Insurance Number of the Seasonal Employee.</param>
        /// <param name="dateOfBirth">The Date of Birth of the Seasonal Employee.</param>
        /// <param name="piecePay">The amount paid to the Seasonal Employee per unit-of-work done.</param>
        /// <param name="season">The season that the Seasonal Employee will be working in.</param>
        public SeasonalEmployee(string firstName, string lastName, string SIN, DateTime dateOfBirth, double piecePay, string season)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.socialInsuranceNumber = SIN;
            this.dateOfBirth = dateOfBirth;
            this.season = season;
            this.piecePay = piecePay;
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
                string.IsNullOrWhiteSpace(this.lastName) || // Checks if the Company Name string is empty
                Regex.IsMatch(this.firstName, @"\d") || // Finds numbers in the user's first name
                Regex.IsMatch(this.socialInsuranceNumber, "^[a-zA-Z]+$") ||  // Checks if letters are in the social insurance number
                this.dateOfBirth == DateTime.MinValue ||    // Check if the date of birth is not set
                this.socialInsuranceNumber.Length != 9 ||   // Checks if the SIN number is not the correct length. 
                this.piecePay < 1 || // Checks if the piece pay is less than 1 dollar
                !(string.Equals(this.season, "winter", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(this.season, "spring", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(this.season, "summer", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(this.season, "fall", StringComparison.OrdinalIgnoreCase)))
            {
                isValid = false;
                logged.Log(lastName + ", " + firstName + " SIN: " + socialInsuranceNumber + " - INVALID", "SeasonalEmployee", "Validate");
            }
            else
            {
                isValid = true;
                logged.Log(lastName + ", " + firstName + " SIN: " + socialInsuranceNumber + " - VALID", "SeasonalEmployee", "Validate");
            }
            return isValid;
        }

        /// <summary>
        /// Displays the details for the Seasonal Employee
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
            string empDetails = "Seasonal Employee: \n"
                              + "Name: " + this.firstName + " " + this.lastName
                              + "\nSocial Insurance Number: " + outputSIN
                              + "\nDate of Birth: " + this.dateOfBirth.ToString("yyyy/MM/dd")
                              + "\nSeason: " + this.season
                              + "\nPiece Pay: " + this.piecePay.ToString("F");


            return empDetails;

        }


    }
}

