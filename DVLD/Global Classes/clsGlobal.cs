using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using DVLD.User;
using DVLD_Bisnesess;
using Microsoft.Win32;


namespace DVLD.Classes
{
    internal static  class clsGlobal
    {
        public static clsUser CurrentUser;
        public static string ValueName = "ValueName";
        private static EventLogEntryType ErrorType(Exception ex)
        {
            if (ex is ArgumentException || ex is FormatException) 
            {
                return EventLogEntryType.Warning;
            }
            else
                   if (ex is InvalidOperationException || ex is NotSupportedException || ex is NullReferenceException) 
            {
                return EventLogEntryType.Error;

            }
            else
            {
                return EventLogEntryType.Information;
            }
        }

        public static void SendTheExceptionToTheEventLogger(Exception exception)
        {
            string SourceName = "DVLD";

            if (!EventLog.SourceExists(SourceName)) 
            {
                EventLog.CreateEventSource(SourceName, "Application");
            }

            EventLog.WriteEntry(SourceName, exception.ToString(), ErrorType(exception));
        }

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {

            try
            {
                /*
                //this will get the current project directory folder.
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();


                // Define the path to the text file where you want to save the data
                string filePath = currentDirectory + "\\data.txt";

                //incase the username is empty, delete the file
                if (Username == "" && File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;

                }

                // concatenate username and password withe separator.
                string dataToSave = Username + "#//#" + Password;

                // Create a StreamWriter to write to the file
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write the data to the file
                    writer.WriteLine(dataToSave);

                }
                return true;
                */
                // Save UserName and Password in windows Registry 
            
                // Specify the Registry key and the path
                string KeyPath = "HKEY_CURRENT_USER\\Software\\DVLD_Project";
                string dataToSave = Username + "#//#" + Password;

                // Write the value to the Registry 
                Registry.SetValue(KeyPath, ValueName, dataToSave, RegistryValueKind.String);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
           
        }
        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            //this will get the stored username and password and will return true if found and false if not found.
           /*
           
            try
            {
                //gets the current project's directory
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();

                // Path for the file that contains the credential.
                string filePath  = currentDirectory + "\\data.txt";

                // Check if the file exists before attempting to read it
                if (File.Exists(filePath))
                {
                    // Create a StreamReader to read from the file
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        // Read data line by line until the end of the file
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                           // Console.WriteLine(line); // Output each line of data to the console
                            string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);

                            Username = result[0];
                            Password = result[1];
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show ($"An error occurred: {ex.Message}");
                return false;   
            }
          */

            string KeyPath = @"HKEY_CURRENT_USER\Software\DVLD_Project";

            try
            {
                string ValueData = null;
                
                ValueData = Registry.GetValue(KeyPath, ValueName,null) as string;
                //Read the value from the Registry
                if (ValueData != null)
                {
                    string[] value = ValueData.Split(new string[] { "#//#" }, StringSplitOptions.None);
                    Username = value[0];
                    Password = value[1];

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred :{ex.Message} ");
                return false;
            }
        }

        public static string ComputeHash(string Input)
        {
            // SHA is security hash algorithm
            // create as instance of the SHA-256 algorithm
            using (SHA256 sHA256 = SHA256.Create())
            {
                // compute the hash value from UTF8 encoded input string 
                byte[] hashBytes = sHA256.ComputeHash(Encoding.UTF8.GetBytes(Input));

                // Convert the byte array to lowercase hexadecimal string 
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

    }
}
