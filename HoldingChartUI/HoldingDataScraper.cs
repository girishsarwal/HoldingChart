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
        
        public Company HoldingCompany1;
        public Company HoldingCompany2;
        public Company HoldingCompany3;

        public Company SubsidaryLongulf;
        public Company Property1;
        public Company LongulfUK;
        public Company LCLT;
        public Company Sub2;
        public Company Sub4;
        public Company Sub5;
        public Company CEPAC;
        public Company Property2;
        public Company Property3;

        public FamilyMember FM1;
        public FamilyMember FM2;
        public FamilyMember FM3;
        public FamilyMember FM4;
        public FamilyMember FM5;

        public ThirdParty TP;



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
            try
            {
                connection.Open();
                Companies = new List<Company>();
                FamilyMembers = new List<FamilyMember>();
                Holdings = new List<Holding>();

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

        
            FM1 = new FamilyMember() { Name = "Family Member 1", Code = "FM1" };
            FM2 = new FamilyMember() { Name = "Family Member 2", Code = "FM2" };
            FM3 = new FamilyMember() { Name = "Family Member 3", Code = "FM3" };
            FM4 = new FamilyMember() { Name = "Family Member 4", Code = "FM4" };
            FM5 = new FamilyMember() { Name = "Family Member 5", Code = "FM5" };
        }

        private void BuildCompanies()
        {
            SqlCommand command = new SqlCommand("SELECT DISTINCT * FROM Hierarch_Data WHERE ShType ='Holding Company'");
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read() != null)
            {
                Companies.Add(new Company(){
                    Name=reader.GetString(0)
                });
                reader.NextResult();
            }

            HoldingCompany1 = new Company() { Name = "Holding Company 1", TotalCompanyCapital = 100000, Code = "HC0001" };
            HoldingCompany2 = new Company() { Name = "Holding Company 2", TotalCompanyCapital = 5000000, Code = "HC0002"};
            HoldingCompany3 = new Company() { Name = "Holding Company 3", TotalCompanyCapital = 0, Code = "HC0003" };

            SubsidaryLongulf = new Company() { Name = "Subsidary Longulf", TotalCompanyCapital = 50000, Code = "SLF" };
            Property1 = new Company() { Name = "Property 1", TotalCompanyCapital = 25000, Code = "PROP1" };
            LongulfUK = new Company() { Name = "LongulfUK", TotalCompanyCapital = 20000, Code = "LULFUK" };
            LCLT = new Company() { Name = "LCLT", TotalCompanyCapital = 15000, Code = "LCLT" };
            Sub2 = new Company() { Name = "Sub 2", TotalCompanyCapital = 15000, Code = "SUB2" };
            Sub4 = new Company() { Name = "Sub 4", TotalCompanyCapital = 15000, Code = "SUB4" };
            Sub5 = new Company() { Name = "Sub 5", TotalCompanyCapital = 15000, Code = "SUB5" };
            CEPAC = new Company() { Name = "CEPAC", TotalCompanyCapital = 100000, Code = "CEPAC" };
            Property2 = new Company() { Name = "Property2", TotalCompanyCapital = 1000000, Code = "PROP2" };
            Property3 = new Company() { Name = "Property3", TotalCompanyCapital = 2000000, Code = "PROP3" };
        }
        
        

        public void BuildHoldings()
        {
            Holdings.Clear();
            
            Holdings.Add(new Holding() { Comp = HoldingCompany1, ShareHoldingPercentage = 20, ShareHolder = FM1 });
            Holdings.Add(new Holding() { Comp = HoldingCompany1, ShareHoldingPercentage = 30, ShareHolder = FM2 });
            Holdings.Add(new Holding() { Comp = HoldingCompany1, ShareHoldingPercentage = 25, ShareHolder = FM3 });
            Holdings.Add(new Holding() { Comp = HoldingCompany1, ShareHoldingPercentage = 25, ShareHolder = HoldingCompany2 });
            
            Holdings.Add(new Holding() { Comp = HoldingCompany2, ShareHoldingPercentage = 5, ShareHolder = TP });
            Holdings.Add(new Holding() { Comp = HoldingCompany2, ShareHoldingPercentage = 60, ShareHolder = FM4 });
            Holdings.Add(new Holding() { Comp = HoldingCompany2, ShareHoldingPercentage = 20, ShareHolder = FM1 });
            Holdings.Add(new Holding() { Comp = HoldingCompany2, ShareHoldingPercentage = 15, ShareHolder = FM5 });
            
            Holdings.Add(new Holding() { Comp = SubsidaryLongulf, ShareHoldingPercentage = 50, ShareHolder = HoldingCompany1 });
            Holdings.Add(new Holding() { Comp = SubsidaryLongulf, ShareHoldingPercentage = 5, ShareHolder = TP });
            Holdings.Add(new Holding() { Comp = SubsidaryLongulf, ShareHoldingPercentage = 45, ShareHolder = HoldingCompany3 });
            
            Holdings.Add(new Holding() { Comp = Property1, ShareHoldingPercentage = 100, ShareHolder = SubsidaryLongulf });
            
            Holdings.Add(new Holding() { Comp = LongulfUK, ShareHoldingPercentage = 50, ShareHolder = SubsidaryLongulf });
            Holdings.Add(new Holding() { Comp = LongulfUK, ShareHoldingPercentage = 25, ShareHolder = HoldingCompany2 });
            Holdings.Add(new Holding() { Comp = LongulfUK, ShareHoldingPercentage = 25, ShareHolder = HoldingCompany1 });
            
            Holdings.Add(new Holding() { Comp = LCLT, ShareHoldingPercentage = 100, ShareHolder = SubsidaryLongulf });
            
            Holdings.Add(new Holding() { Comp = Sub2, ShareHoldingPercentage = 100, ShareHolder = SubsidaryLongulf });
            
            Holdings.Add(new Holding() { Comp = Sub4, ShareHoldingPercentage = 100, ShareHolder = SubsidaryLongulf });
            
            Holdings.Add(new Holding() { Comp = Sub5, ShareHoldingPercentage = 100, ShareHolder = SubsidaryLongulf });
            
            Holdings.Add(new Holding() { Comp = CEPAC, ShareHoldingPercentage = 100, ShareHolder = LongulfUK });
            
            Holdings.Add(new Holding() { Comp = Property2, ShareHoldingPercentage = 100, ShareHolder = CEPAC });
            
            Holdings.Add(new Holding() { Comp = Property3, ShareHoldingPercentage = 80, ShareHolder = CEPAC });
            Holdings.Add(new Holding() { Comp = Property3, ShareHoldingPercentage = 20, ShareHolder = HoldingCompany2 });


            //holdings.Add(new Holding() { Comp = HoldingCompany1, ShareHoldingPercentage = 20, ShareHolder = FM1 });
            //holdings.Add(new Holding() { Comp = SubsidaryLongulf, ShareHoldingPercentage = 50, ShareHolder = HoldingCompany1 });
            //holdings.Add(new Holding() { Comp = SubsidaryLongulf, ShareHoldingPercentage = 30, ShareHolder = FM1 });

        }
    }
}
