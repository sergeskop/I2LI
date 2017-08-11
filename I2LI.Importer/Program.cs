using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace I2LI.Importer
{
    class Program
    {
        static void Main(string[] args)
        {
            string importPath = @"G:\AppData\I2LI Imports";
            string fileName;
            try
            {
                string[] files = Directory.GetFiles(importPath, "*.csv");
                if (files.Length == 0)
                {
                    throw new InvalidDataException("No *.csv files in path: " + importPath);
                }
                if (files.Length > 1)
                {
                    throw new InvalidDataException("More than 1 *.csv files in path: " + importPath);
                }

                //Success finding file
                fileName = files[0];

                CsvImporter.ImportFile(fileName);

                //Success reading file
                Console.WriteLine("Successfully impirted file: " + fileName);

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            finally
            {
                Console.WriteLine("Press Enter to exit...");
                Console.ReadLine();
            }
        }
    }
}
