/*
* FILE : Employee.cs
* PROJECT : INFO2180 - Software Quality I - Term Project - EMS 
* PROGRAMMERS : RSkowron, DPitters, CWilson, and lpcSetProg
* FIRST VERSION : 2016-11-17
* DESCRIPTION :
* This file contains the source code for the Employee class, it is the parent class for
* all the other child employee classes. The contents in this class will be used to get the user's 
* First name, Last name, Social Insurance Number, and their date of birth.
*/

using System;
using System.Text.RegularExpressions;
using Supporting;

namespace AllEmployees
{
    /// <summary>
    /// This is the parent class for all the other child employee classes. 
    /// The contents in this class will be used to get the user's First name, Last name, 
    /// Social Insurance Number, and their date of birth.
    ///  <BR>
    ///  <B>Exception Strategy:</B> When validating the information for content towards the Parent Employee Class
    ///  each property for the specific variable will validate the incoming value. 
    ///  Throwing an exception if employee fails to  an error occurs.
    /// </summary>
    public abstract class Employee
    {
        Logging logged = new Logging();
        private string _firstName;
        private string _lastName;
        private string _socialInsuranceNumber;

        ///
        ///<summary> Gets or sets the person's First name. </summary>
        ///
        ///<exception cref="ArgumentException">  Thrown when one or more arguments have unsupported or
        ///                                      illegal values. </exception>
        ///
        ///<value>   The first name of the employee. </value>
        ///
        public string firstName
        {
            get
            {
                return this._firstName;
            }

            set
            {
                if (Regex.IsMatch(value, @"\d"))
                {
                    logged.Log("string firstName : " + value + " - INVALID", "Employee", "firstName");

                    throw new ArgumentException("First Name has a Number.");
                }
                else
                {
                    this._firstName = value;
                }
            }

        }


        ///
        ///<summary> Gets or sets the person's last name. </summary>
        ///
        ///<exception cref="ArgumentException">  Thrown when one or more arguments have unsupported or
        ///                                      illegal values. </exception>
        ///
        ///<value>   The last name of the employee. </value>
        ///
        public string lastName
        {
            get
            {
                return this._lastName;
            }
            set
            {
                if (Regex.IsMatch(value, @"\d"))
                {

                    logged.Log("string lastName : " + value + " - INVALID", "Employee", "lastName");
                    throw new ArgumentException("Last Name has a Number.");
                }
                else
                {
                    this._lastName = value;
                }
            }
        }


        ///
        ///<summary> Gets or sets the social insurance number. </summary>
        ///
        ///<exception cref="ArgumentException">  Thrown when one or more arguments have unsupported or
        ///                                      illegal values. </exception>
        ///
        ///<value>   The social insurance number. </value>
        ///
        public string socialInsuranceNumber
        {
            get
            {
                return this._socialInsuranceNumber;
            }
            set
            {
                if (Regex.IsMatch(value, "^[a-zA-Z]+$"))
                {
                    logged.Log("string socialInsuranceNumber : " + value + " - INVALID", "Employee", "socialInsuranceNumber");

                    throw new ArgumentException("Social Insurance Number has a letter.");
                }
                else
                {
                    this._socialInsuranceNumber = value;
                }
            }
        }

        ///
        ///<summary> Gets or sets the date of birth. </summary>
        ///
        ///<value>   The date of birth. </value>
        ///
        public DateTime dateOfBirth { get; set; }


        ///
        ///<summary>
        /// <summary>
        /// Default constructor for a new Employee. Sets the data members to safe values. / </summary>
        ///</summary>
        ///
        ///<remarks> Lcocarell, 11/29/2016. </remarks>
        ///
        public Employee()
        {
            this.firstName = "";
            this.lastName = "";
            this.socialInsuranceNumber = "";
            this.dateOfBirth = DateTime.MinValue;
        }


        ///
        ///<summary> Validates this object. </summary>
        ///
        ///<remarks> Lcocarell, 11/29/2016. </remarks>
        ///
        ///<returns> True if it succeeds, false if it fails. </returns>
        ///
        public abstract bool Validate();

    }
}
