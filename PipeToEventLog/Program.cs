using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeToEventLog
{
    class Program
    {
        static bool running = true;
        static bool verbosedebug = false;
        static int counter = 0;
        static int Main(string[] args)
        {

            string input = "";
            while (running) // OUTERLOOP
            {
                if (!IsPipedInput())
                    return 0;
                Console.SetIn(new StreamReader(Console.OpenStandardInput(8192))); // This will allow input >256 chars
                while (Console.In.Peek() != -1) // DATALOOP
                {
                    input = Console.In.ReadLine();
                    myWriteEvent(input);
                    Debug.WriteLine("Data read was " + input);
                    counter = 0; // reset counter 
                    input = "";
                }
            }
            return 0;
        }

        private static void myWriteEvent(string strz)
        {
            EventLog myLog = new EventLog();
            myLog.Log = "JUNIPER-FW";
            myLog.Source = "JUNIPER-FW";
            myLog.WriteEntry(strz, EventLogEntryType.Information, 96);
            myLog.Close();
            //myLog.Dispose(); // Analysis by ms tools said this was bad!
        }

        private static bool IsPipedInput()
        {
            try
            {
                bool redirected = Console.IsInputRedirected; // is always true
                int peekVal = Console.In.Peek();
                if (redirected == false) { Debug.WriteLine("Redirected was false"); }
                if (peekVal != -1)
                {
                    Debug.WriteLine("Has Data"); // only hit on startup
                }
                else
                {
                    Debug.WriteLine("No Data"); // mostly hit but the data loop keeps count low like LT 1
                    counter++;
                }
                if(counter >= 100) { Debug.WriteLine("Counter hit >= 100"); running = false; }
                bool isKey = Console.KeyAvailable; // intentionally throws an error
                return false;
            }
            catch(Exception e)
            {   
                if(verbosedebug) { Debug.WriteLine(e.Message); }
                return true;
            }
        }
    }
}
