using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoldingChartUI
{
    public class Company :
          ShareHolder
    {
        public Int64 TotalCompanyCapital { get; set; }
        public Boolean Recurse { get; set; }
        public Company()
        {
            Recurse = true;
        }
    }

}
