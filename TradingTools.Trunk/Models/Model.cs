using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk;

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
}
