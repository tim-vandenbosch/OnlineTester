using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTester;
using OnlineTester.data;

namespace OnlineTester.io
{
    /// <summary>
    /// Reading the designated file and returning all the IP-addresses.
    /// </summary>
    public class FileReader
    {
        #region Local vars
        private readonly IList<Bestemming> _pingList;
        private readonly string _path;
        private readonly char _splitter;
        #endregion
        /// <summary>
        /// Initializing the FileReader
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="stringsplitter"></param>
        public FileReader(string filepath, char stringsplitter)
        {
            _path = filepath;
            _splitter = stringsplitter;
            _pingList = new List<Bestemming>();
        }
        /// <summary>
        /// Reading the file and adding it to the pinglist
        /// </summary>
        /// <returns>Ilist(bestemming)</returns>
        public IList<Bestemming> ReadFile()
        {
            try
            {
                using (var sr = File.OpenText(_path))
                {
                    string readLine;
                    while ((readLine = sr.ReadLine()) != null)
                    {
                        var splitLine = readLine.Split(_splitter);
                        var bestemming = new Bestemming
                        {
                            Address = splitLine[0],
                            Plaats = splitLine[1],
                            Status = "Await" // IMPORTANT at first cycle have everything gray instead of red
                        };
                        _pingList.Add(bestemming);
                    }
                    sr.Close();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Het bestand is niet gevonden. (maak C:/OnlineTester/addresslijst.txt aan)");
                Console.WriteLine("Druk op enter om de applicatie af te sluiten.");
                Console.ReadLine();
                Environment.Exit(0);
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("De folder is niet gevonden. (Je hebt geen C schijf?)");
                Console.WriteLine("Druk op enter om de applicatie af te sluiten.");
                Console.ReadLine();
                Environment.Exit(0);
            }
            catch (Exception)
            {
                Console.WriteLine("Een onverwachte fout is opgetreden");
                Console.WriteLine("Druk op enter om de applicatie af te sluiten.");
                Console.ReadLine();
                Environment.Exit(0);
            }
            return _pingList;
        }
    }
}
