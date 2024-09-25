using System;
using System.CodeDom;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DVLD_DataAccess
{
    static class clsDataAccessSettings 
    {
        //public static string ConnectionString = "Server=.;Database=DVLD;User Id=sa;Password=20202021;";


        private static EventLogEntryType ErrorType(Exception ex)
        {
            // Log an Warning event 
            if (ex is ArgumentException || ex is FormatException)
            {
                return EventLogEntryType.Warning;
            }
            else
            // Log an Error Event 
                   if (ex is InvalidOperationException || ex is NotSupportedException || ex is NullReferenceException || ex is CodeThrowExceptionStatement) 
            {
                return EventLogEntryType.Error;

            }
            else
            {
                // Log an Information event
                return EventLogEntryType.Information;
            }
        }

        public static void SendTheExceptionToTheEventLogger(Exception exception)
        {
            // Specify the source name for the event log
            string SourceName = "DVLD";

            // create the event source if it does not exist
            if (!EventLog.SourceExists(SourceName))
            {
                EventLog.CreateEventSource(SourceName, "Application");
            }
            
            // Log an Information event
            EventLog.WriteEntry(SourceName, exception.ToString(), ErrorType(exception));
        }

    }
}
