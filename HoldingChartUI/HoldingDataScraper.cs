using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoldingChartUI
{
    public class HoldingDataScraper
    {
        private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sapdb"].ConnectionString);
       
        private static HoldingDataScraper instance;
        public static HoldingDataScraper Instance
        {
            get
            {
                if(instance == null)
                    instance = new HoldingDataScraper();
                return instance;
            }
        }

        
        public List<Company> Companies
        {
            get;
            private set;
        }

        public List<Holding> Holdings
        {
            get;
            private set;
        }
        public List<FamilyMember> FamilyMembers
        {
            get;
            private set;
        }

        public HoldingDataScraper()
        {
            Companies = new List<Company>();
            FamilyMembers = new List<FamilyMember>();
            Holdings = new List<Holding>();

        }
        public void BuildDataset()
        {
            try
            {
                connection.Open();
                
                BuildCompanies();
                BuildFamilyMembers();
                BuildHoldings();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
        private void BuildFamilyMembers()
        {
            FamilyMembers.Clear();
            SqlCommand command;
            SqlDataReader reader = null;
            try
            {
                command = new SqlCommand("SELECT DISTINCT ShCode, ShName FROM Hierarch_Chart WHERE ShType ='Family Member'", connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    FamilyMembers.Add(new FamilyMember()
                    {
                        Code = reader.GetString(0),
                        Name = reader.GetString(1),
                    });
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null) reader.Close();
            }

        }

        private void BuildCompanies()
        {
            Companies.Clear();
            SqlCommand command;
            SqlDataReader reader = null;
            try
            {
                command = new SqlCommand("SELECT DISTINCT CompCode, CompName, CapAmount, flag FROM CompMaster", connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Companies.Add(new Company()
                    {
                        Code = reader.GetString(0),
                        Name = reader.GetString(1),
                        TotalCompanyCapital = Convert.ToInt64(reader.GetValue(2)),
                        Recurse = (reader.IsDBNull(3) || reader.GetString(3) == "Y")
                    });
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null) reader.Close();
            }

        }
        
        

        public void BuildHoldings()
        {
            Holdings.Clear();

            SqlCommand command;
            SqlDataReader reader = null;
            try
            {
                command = new SqlCommand("SELECT CompanyCode, ShCode, Percentage FROM Hierarch_Chart", connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string companyCode = reader.GetString(0);
                    string shareHolderCode = reader.GetString(1);
                    double shareHolderPercentage = Convert.ToInt64(reader.GetValue(2));
                    Company comp = Companies.Find(c => c.Code == companyCode);
                    ShareHolder sh = GetShareHolder(shareHolderCode);
                    if (comp == null)
                    {
                        throw new Exception("ERROR: Broken Shareholder link for " + companyCode + ". \nData for this shareholder does not exist! \nPlease verify input data. \nABORTING further processing!");
                    }

                    Holdings.Add(new Holding()
                    {
                        Comp = comp,
                        ShareHolder = sh,
                        ShareHoldingPercentage = shareHolderPercentage                        
                    });
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null) reader.Close();
            }

        }

        public ShareHolder GetShareHolder(string code)
        {
            Company shCompany = Companies.Find(c => c.Code == code);
            FamilyMember shFamilyMember = FamilyMembers.Find(f => f.Code == code);
            ShareHolder sh = shCompany == null ? (ShareHolder)shFamilyMember : (ShareHolder)shCompany;
            if (sh == null)
            {
                throw new Exception("ERROR: Broken Shareholder link for " + code + ".\nMaster data for this shareholder does not exist!\nABORTING further processing!");
            }
            return sh;
        }
    }
}
