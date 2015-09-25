﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HoldingChartUI
{
    static class Program
    {
        public static string RootSHCode;
        public static double DownScaleFactor;
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {

                DownScaleFactor = 1.0;
                if (args.Length == 0)
                {
                    throw new ArgumentException("Atleast the shareholder code must be provided");
                }
                else if (args.Length == 1)
                {
                    RootSHCode = args[0];
                    Console.WriteLine("No Down Scale Factor provided. Will be defaulted to " + DownScaleFactor);
                }
                else if (args.Length == 2)
                {
                    RootSHCode = args[0];
                    DownScaleFactor = Convert.ToDouble(args[1]);
                }

                HoldingDataScraper.Instance.BuildHoldings();
                HoldingProcessor.Holdings = HoldingDataScraper.Instance.Holdings;

                string path = Path.Combine(Environment.CurrentDirectory, "holdingchart.htm.template");
                string template = File.ReadAllText(path);
                string data = HoldingProcessor.ProcessHoldings(HoldingDataScraper.Instance.FM1, null);
                template = template.Replace("{{ data }}", data);

                string outputPath = Path.GetTempPath() + Guid.NewGuid().ToString() + ".htm";
                File.WriteAllText(outputPath, template);

                Process.Start(outputPath);

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Required arguments are missing:");
                Console.WriteLine(ex.Message);

                Console.WriteLine("\n");
                Console.WriteLine("Usage"); 
                Console.WriteLine("-----");

                Console.WriteLine("HoldingChartUI.exe <shareholder> [downscalefactor=" + DownScaleFactor +"]");
                Console.WriteLine("\n");
            }

        }
    }
}
