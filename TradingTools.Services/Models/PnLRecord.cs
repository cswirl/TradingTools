using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingTools.Services.Models
{
    public class PnLRecord
    {
        public decimal PCP { get; set; }   // Price Change Percentage
        public decimal PnL { get; set; }
        public decimal PnL_Percentage { get; set; }
        public decimal ExitPrice { get; init; }

        public static PnLRecord Create(decimal exitPrice, decimal entryPrice, decimal lotSize, decimal capital)
        {
            try
            {
                decimal pip = (exitPrice - entryPrice) / entryPrice * 100;
                decimal pnl = entryPrice * lotSize * pip / 100;
                return new PnLRecord
                {
                    PCP = pip,
                    PnL = pnl,
                    PnL_Percentage = pnl / capital * 100
                };
            }
            catch (DivideByZeroException dex)
            {

            }
            return null;
        }
    }
}
