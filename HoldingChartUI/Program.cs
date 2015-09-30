using System;
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
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {

                HoldingChartConfiguration.ScaleDownFactor= 1.0;
                HoldingChartConfiguration.ConsiderRecurseFlag = true;
                if (args.Length <= 0)
                {
                    throw new ArgumentException("Atleast the shareholder code must be provided");
                }
                else if (args.Length <= 1)
                {
                    RootSHCode = args[0];
                    Console.WriteLine("No ScaleDownFactor provided. Will be defaulted to " + HoldingChartConfiguration.ScaleDownFactor);
                }
                else if (args.Length <= 2)
                {
                    RootSHCode = args[0];
                    Console.WriteLine("No ConsiderRecurseFlag Provided. Will be defaulted to " + HoldingChartConfiguration.ConsiderRecurseFlag);
                    HoldingChartConfiguration.ScaleDownFactor = Convert.ToDouble(args[1]);
                }
                else
                {
                    RootSHCode = args[0];
                    HoldingChartConfiguration.ScaleDownFactor = Convert.ToDouble(args[1]);
                    HoldingChartConfiguration.ConsiderRecurseFlag = args[2] == "1";
                }

                HoldingDataScraper.Instance.BuildDataset();
                HoldingProcessor.Holdings = HoldingDataScraper.Instance.Holdings;

                string path = Path.Combine(Environment.CurrentDirectory, "holdingchart.htm.template");
                string template = File.ReadAllText(path);

                ShareHolder rootShareHolder = HoldingDataScraper.Instance.GetShareHolder(RootSHCode);

                string data = HoldingProcessor.ProcessHoldings(rootShareHolder, null);
                template = template.Replace("{{ data }}", data);
                string shareHolder = HoldingDataScraper.Instance.GetShareHolder(RootSHCode).Name;
                string outputPath = Path.GetTempPath() + String.Format("{0} - Hierarchy Report", shareHolder) + ".htm";
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

                Console.WriteLine("HoldingChartUI.exe <shareholder> [downscalefactor=" + HoldingChartConfiguration.ScaleDownFactor +"]");
                Console.WriteLine("\n");
                Console.WriteLine("\n\nPress any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured while processing the request");
                Console.WriteLine(ex.Message);
                Console.WriteLine("\n\nPress any key to continue...");
                Console.ReadKey();
            }
            

        }
    }
}
