using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingTools.Trunk.Entity
{
    public class Trade
    {
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string Ticker { get; set; }
        [Required, MaxLength(20)]
        public string PositionSide { get; set; }      //long or short
        [Required]
        public DateTime DateEnter { get; set; }
        // Position
        [Required]
        [Column(TypeName = "money")]
        public decimal Capital { get; set; }
        [Required]
        public decimal Leverage { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal LeveragedCapital { get; set; }
        [Required]
        public decimal EntryPriceAvg { get; set; }
        [Required]
        public decimal LotSize { get; set; }
        // Opening Cost
        [Required]
        public decimal OpeningTradingFee { get; set; }
        [Required]
        public decimal OpeningTradingCost { get; set; }
        // Borrow
        [Required]
        public decimal BorrowAmount { get; set; }
        [Required]
        public int DayCount { get; set; }
        [Required]
        public decimal DailyInterestRate { get; set; }
        [Required]
        public decimal InterestCost { get; set; }
        // Closing
        public DateTime? DateExit { get; set; }
        public decimal? ExitPriceAvg { get; set; }
        // Closing Cost
        public decimal? ClosingTradingFee { get; set; }
        public decimal? ClosingTradingCost { get; set; }

        public decimal? PnL { get; set; }

        [Required, MaxLength(20), DefaultValue("open")]
        public string Status { get; set; }

        // CalculatorState
        public virtual CalculatorState CalculatorState { get; set; }

    }
}
