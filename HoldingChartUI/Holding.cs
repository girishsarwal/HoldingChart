using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoldingChartUI
{
    public class Holding
    {
        public Holding Parent { get; set; }
        public Company Comp { get; set; }
        public double ShareHoldingPercentage { get; set; }

        public double EffectiveShareHoldingPercentage
        {
            get
            {
                if (Parent == null)
                    return ShareHoldingPercentage;
                return Parent.EffectiveShareHoldingPercentage * (ShareHoldingPercentage /100.0f);
            }
        }
        public double ShareHolderCapital
        {
            get
            {
               return (EffectiveShareHoldingPercentage / 100.0f) * Comp.TotalCompanyCapital;
            }
        }

        public String PathCode
        {
            get
            {
                if (Parent == null)
                {
                    return Comp.Code;
                }
                return Parent.PathCode + "_" + Comp.Code;
            }
        }
        public ShareHolder ShareHolder { get; set; }
    }
}
