using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OnlineTester.data;
using OnlineTester.io;

namespace OnlineTester
{
    /// <summary>
    /// Checking all the IP-addresses given and displaying there status with color code
    /// </summary>
    internal class PingManager
    {
        #region Local properties
        private static Pinger _pinger;
        private static IList<Bestemming> _pingList, _readList;
        private static FileReader _reader;
        private const string Path = "C:/OnlineTester/addresslijst.txt";
        private const char Splitter = ';';
        #endregion
        /// <summary>
        /// Initializing all the vars
        /// </summary>
        private static void PingManagerInit()
        {
            Console.Title = "Pingmanager";
            Console.CursorVisible = false;
            _pingList = new List<Bestemming>();
            _readList = new List<Bestemming>();
            _pinger = new Pinger();
            _reader = new FileReader(Path, Splitter);
        }
        private static void Main(string[] args)
        {
            PingManagerInit();
            FillPingList();
            PrintInitList();
            PrintLoopList();
        }
        /// <summary>
        /// Checks every address and writes them at the correct spot
        /// </summary>
        private static void PrintLoopList()
        {
            while (true)
            {
                foreach (var bestemming in _pingList)
                {
                    bestemming.Status = "Testing";
                    GetToLineOf(bestemming);
                    EmptyTheLineFor(bestemming);
                    Print(bestemming);
                    Thread.Sleep(1000);
                    bestemming.Status = Convert.ToString(_pinger.PingTo(bestemming.Address).Status);
                    GetToLineOf(bestemming);
                    EmptyTheLineFor(bestemming);
                    Print(bestemming);
                }
            }
        }
        /// <summary>
        /// Sets the cursor to the beginning of the line of the "bestemming"
        /// </summary>
        /// <param name="bestemming"></param>
        private static void GetToLineOf(Bestemming bestemming)
        {
            Console.SetCursorPosition(0, _pingList.IndexOf(bestemming));
        }
        /// <summary>
        /// Emptying the line for the designated "bestemming", this way a previous longer line will be cleared completly (No residu from the previous status)
        /// </summary>
        /// <param name="bestemming"></param>
        private static void EmptyTheLineFor(Bestemming bestemming)
        {
            Console.WriteLine();
            Console.SetCursorPosition(0, _pingList.IndexOf(bestemming));
        }
        /// <summary>
        /// Print out the IP-list in a default gray color
        /// </summary>
        private static void PrintInitList()
        {
            foreach (var bestemming in _pingList)
            {
                Print(bestemming);
                Console.ForegroundColor = ConsoleColor.Gray; // if Ctrl+C is used
            }
        }
        /// <summary>
        /// Print a single IP-address with the designated color code
        /// </summary>
        /// <param name="bestemming"></param>
        private static void Print(Bestemming bestemming)
        {
            var space = "\t\t\t\t";
            var spacer = "\t\t";
            if (bestemming.Status == "Testing")
                Console.ForegroundColor = ConsoleColor.Yellow;

            if (bestemming.Status == "Success")
                Console.ForegroundColor = ConsoleColor.Green;

            if (bestemming.Status != "Testing" && bestemming.Status != "Success")
                Console.ForegroundColor = ConsoleColor.Red;
            
            // first time, have everything gray instead of red
            if (bestemming.Status == "Await")
                Console.ForegroundColor = ConsoleColor.Gray;

            if (bestemming.Status.Length > 12)
                space = "\t";

            if (bestemming.Address.Length > 7)
                spacer = "\t";


            Console.WriteLine(bestemming.Status + space + bestemming.Address + spacer + bestemming.Plaats);
        }
        /// <summary>
        /// Fill the internal list with all the read addresses
        /// </summary>
        private static void FillPingList()
        {
            _readList = _reader.ReadFile();

            if (_readList.Count == _pingList.Count) return; // If the lists are the same

            _pingList = _readList;
        }
    }
}