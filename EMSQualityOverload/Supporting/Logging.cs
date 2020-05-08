/*
* FILE : Logging.cs
* PROJECT : INFO2180 - Software Quality I - Term Project - EMS 
* PROGRAMMERS : RSkowron, DPitters, CWilson, and lpcSetProg
* FIRST VERSION : 2016-11-17
* DESCRIPTION :
* This file contains the source code the Logging class which contains a method that 
* will keep a log of messages each day the Logging class is called by the user of the
* EMS program. 
*/

using System;
using System.IO;
using System.Text;

namespace Supporting
{
    /// <summary>
    /// Creates a new log file each day that the logging class is called, and logs messages to
    /// the most up to date log file. This class depends on the FileIO class for logging files. 
    /// <br>
    /// <b>Exception Strategy:</b> Check if file exists, if not create it.
    /// </summary>
    public class Logging
    {
        static string currentLogFile; ///< Log file currently being used.

        /// <summary>
        /// Overloaded constructor, which ensures that an up to date log file exists.
        /// </summary>
        public Logging()
        {
            //Create a StringBuilder object to build the name of the log file.
            StringBuilder log = new StringBuilder();
            log.Append("ems." + DateTime.Now.ToString("yyyy-MM-dd") + ".log");

            //Change it to a string, and check if it exists. If it doesn't exist, create it.
            string logFile = log.ToString();
            if (!File.Exists(logFile))
            {
                File.Create(logFile).Close();
            }
            //Set the data member, currentLogFile, equal to the current log file.
            currentLogFile = logFile;
        }

        /// <summary>
        /// Writes a message to an up to date log file. The format is: 
        /// 'yyyy-MM-dd hh:mm:ss : classname.methodname ~ message'
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="className">The class which the message came from.</param>
        /// <param name="methodName">The method which the message came from.</param>
        public void Log(string message, string className, string methodName)
        {
            //Open the file for appending
            FileStream write = new FileStream(currentLogFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            StreamWriter sw = new StreamWriter(write);

            //Build a new string to append to the file
            string appendMessage = string.Format("{0} : {1,-30}{2, -5}{3}",
                DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"),
                (className + "." + methodName),
                " ~ ",
                message);

            //Write the log message to the file
            sw.WriteLine(appendMessage);
            sw.Close();
            write.Close();
        }
    }
}
