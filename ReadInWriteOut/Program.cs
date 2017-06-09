using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadInWriteOut
{
    class Program
    {
        static void Main(string[] args)
        {
            // This is just a test app, pipe it to real app and watch debug when you type input and press enter.
            //  callthis.exe | pipetoeventlog.exe 
            string s;
            while ((s = Console.ReadLine()) != null)
            {
                Console.WriteLine(s);
            }
        }
    }
}
