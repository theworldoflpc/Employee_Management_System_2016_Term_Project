/*
* FILE : FileIO.cs
* PROJECT : INFO2180 - Software Quality I - Term Project - EMS 
* PROGRAMMERS : RSkowron, DPitters, CWilson, and lpcSetProg
* FIRST VERSION : 2016-11-17
* DESCRIPTION :
* The file contains the source code the FileIO class, which is part of the EMS program. 
* It is constructed as an interface for reading and writing out of the database.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Supporting
{
    /// <summary>
    /// An interface for reading and writing out of the database.
    /// The class acts as the Data Access Layer. This class references and depends 
    /// on the TheCompany class. <br>
    /// <b> Exception Strategy: </b> TBA: -- essentially - will determine if file exists and 
    /// determine exceptions on that basis. 
    /// </summary>
    public class FileIO
    {

        /// <summary>
        ///  Reads the database.
        ///  Looks for text file in Dbase folder.
        ///  Opens it up. Parses the text line by line and delimits strings by '  | ' pipe. 
        ///  Depending on what flag is attached to the beginning of the new line ("S,C,F,P"),
        ///  this will determine what the file gets added to for what employee. 
        /// </summary>
        /// <returns>Returns a string array containing the database lines.</returns>
        public static string[] ReadDatabase()
        {

            List<string> employees = new List<string>();
            string line;

            //Check if the directory exists
            string dBaseFolder = @".\Dbase";
            if (!Directory.Exists(dBaseFolder))
            {
                System.IO.Directory.CreateDirectory(dBaseFolder);
            }

            //Check if the file exists
            if (!(File.Exists(@".\Dbase\EMS_FlatFileDbase.txt")))
            {
                File.CreateText(@".\Dbase\EMS_FlatFileDbase.txt").Close();  //we could CALL THE DATABASE WRITER HERE 
            }


            // Read the file and display it line by line.
            using (StreamReader file = new StreamReader(@".\Dbase\EMS_FlatFileDbase.txt"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    employees.Add(line);
                }
                //close the file after reading 
                file.Close();

                return employees.ToArray();
            }
        }

        /// <summary>
        /// Writes to the database.
        /// </summary>
        /// <param name="employees"></param>
        /// <param name="validEmployees"></param>
        /// <param name="invalidEmployees"></param>
        /// <returns>A bool. True for success, false for failure.</returns>
        public bool WriteDatabase(string[] employees, int validEmployees, int invalidEmployees)
        {
            bool success = false;        

            //Check if the directory exists
            string dBaseFolder = @".\Dbase";  
            if (!Directory.Exists(dBaseFolder))
            {
                Directory.CreateDirectory(dBaseFolder);
            }

            //Check if the file exists
            string filePath = @".\Dbase\EMS_FlatFileDbase.txt";
            if (!(File.Exists(filePath)))
            {
                File.CreateText(@".\Dbase\EMS_FlatFileDbase.txt").Close();  //we could CALL THE DATABASE WRITER HERE 
            }
            
            try
            {
                File.WriteAllLines(filePath, employees);
                Logging logger = new Logging();
                logger.Log("Total records written: " + employees.Count() + ". Total valid: " + validEmployees + ". Total invalid: " + invalidEmployees, "FileIO", "WriteDatabase");
                success = true;
                return success;
            }
            catch (Exception)
            {
                success = false;
                return success;
            }
        }
    }
}

