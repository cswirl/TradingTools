using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk;
using TradingTools.Services;

namespace TradingTools.Model
{
    class Model
    {
    }

    public class CalculationDetails
    {
        public OpeningCost OpeningCost = new();
        public ClosingCost ClosingCost = new();
        public Position Position = new();
        public Borrow Borrow = new();

        private decimal _sPV = 0m;

        public decimal GetSpeculativePositionValue(decimal ExitPrice)
        {
            _sPV = Position.LotSize * ExitPrice;
            return _sPV;
        }

        public decimal GetSpeculativeAccountEquity(decimal ExitPrice)
        {
            return GetSpeculativePositionValue(ExitPrice) - Borrow.Amount;
        }

        //private OpeningCost _openingCost;
        //private ClosingCost _closingCost;
        //private Position _position = new();

        //public OpeningCost OpeningCost { get { return _openingCost; }}
        //public ClosingCost ClosingCost { get { return _closingCost; } }
        //public Position Position { get { return _position; } }
    }

    public class OpeningCost
    {
        public decimal TradingFee { get; set; }
    }

    public class ClosingCost
    {
        public decimal BorrowCost { get; set; }
        public decimal TradingFee { get; set; }
    }

    public record PriceChangeRecordLeveraged : PriceChangeRecord
    {
        public decimal Real { get; set; }
    }
}
