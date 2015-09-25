using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoldingChartUI
{
    public class HoldingProcessor
    {
        static int reentry = 0;
        static StringBuilder sb = new StringBuilder();
        public static List<Holding> Holdings
        {
            get;
            set;
        }

        public static string ProcessHoldings(ShareHolder s, Holding parent)
        {
            sb.Clear();
            sb.Append("[");
            double totalHoldings = RecurseHoldings(s, parent);
            if (s is FamilyMember)
            {
                sb.Append("[{v:'" + s.Code + "', f:'<div class=\"bluebg shareholdername\">" + s.Name + "</div><div class=\"bluebg shareholdercapital\">" + (totalHoldings / HoldingChartConfiguration.ScaleDownFactor)+ "</div>'}, '', '' ],");
            }
            else
            {
                sb.Append("[{v:'" + s.Code + "', f:'<div class=\"greenbg shareholdername\">" + s.Name + "</div><div class=\"greenbg shareholdercapital\">" + (totalHoldings / HoldingChartConfiguration.ScaleDownFactor) + "</div>'}, '', '' ],");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");


            return sb.ToString();
        }
        public static double RecurseHoldings(ShareHolder s, Holding parent)
        {
            reentry++;
            List<Holding> shareHoldings = Holdings.FindAll(h => h.ShareHolder == s);
            double shareHoldingsAggregated = 0;
            
                foreach (Holding item in shareHoldings)
                {
                    item.Parent = parent;
                    shareHoldingsAggregated += item.ShareHolderCapital;
                    String parentPathCode = item.Parent == null? s.Code: item.Parent.PathCode;
                    sb.Append("[{v:'" + item.PathCode + "', f:'<div class=\"greenbg shareholdername\">" + item.Comp.Name + "</div><div class=\"greenbg companycapital\">" + (item.Comp.TotalCompanyCapital/HoldingChartConfiguration.ScaleDownFactor) + "</div><div class=\"greenbg shareholdercapital\">" + (item.ShareHolderCapital/HoldingChartConfiguration.ScaleDownFactor) + "</div><div class=\"greenbg shareholderpercent\">" + item.EffectiveShareHoldingPercentage + "%</div>'}, '" + parentPathCode + "', '' ],");
                    double childAggregated = 0;
                    if (item.Comp != null)
                    {
                        if (item.Comp.Recurse)
                        {
                            childAggregated = RecurseHoldings(item.Comp, item);
                        }
                    }
                    shareHoldingsAggregated +=  childAggregated;
                }
            reentry--;
            return shareHoldingsAggregated;
        }
    }
    
}
