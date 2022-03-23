using ItRiskDev.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItRiskDev.Implementacao
{
    public class Risk : IRisk
    {
        public string CalculateRisk(ITrade trade)
        {
            if (trade.ReferenceDate.AddDays(30) > trade.NextPaymentDate)
            {
                return "EXPIRED:";
            }
            else if (trade.Value > 1000000)
            {
                if (trade.ClientSector.Equals("Private"))
                    return "HIGHRISK";
                if (trade.ClientSector.Equals("Public"))
                    return "MEDIUMRISK";
            }

            return "";
        }
    }
}
