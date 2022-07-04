using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradingTools.Trunk.Entity
{
    public class CalculatorState
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public string Ticker { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Capital { get; set; }
        [Required]
        public decimal Leverage { get; set; }
        [Required]
        public decimal EntryPriceAvg { get; set; }
        public decimal LotSize { get; set; }
        public string CounterBias { get; set; }
        public string ReasonForEntry { get; set; }
        [Required, MaxLength(20)]
        public string TradingStyle { get; set; }
        [Column(TypeName = "decimal(18, 5)")]
        public decimal ExchangeFee { get; set; }

        // Leveraged Capital
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? LeveragedCapital
        {
            get
            {
                return Formula.LeveragedCapital(Capital, Leverage);
            }
            private set { }
        }

        // Borrow
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal BorrowAmount
        {
            get
            {
                return Formula.BorrowedAmount(Leverage, Capital);
            }
            private set { }
        }

        public decimal? DayCount { get; set; }

        // Opening Cost
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal OpeningTradingFee
        {
            get
            {
                var l = LeveragedCapital ?? 0;
                return ExchangeFee * l;
            }
            private set { }
        }
        // 
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal OpeningTradingCost
        {
            get
            {
                return OpeningTradingFee;
            }
            private set { }
        }


        [Column(TypeName = "decimal(18, 5)")]
        public decimal DailyInterestRate { get; set; }

        // 
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal InterestCost
        {
            get
            {
                var d = DayCount ?? 1m;
                return BorrowAmount * DailyInterestRate * d;
            }
            private set { }
        }

        // Closing
        public string ReasonForExit { get; set; }
        public decimal? ClosingTradingFee { get; set; }
        public decimal? ClosingTradingCost { get; set; }
        //
        public decimal? PriceIncreaseTarget { get; set; }
        public decimal? PriceDecreaseTarget { get; set; }
        public decimal? PEP_ExitPrice { get; set; }
        public string PEP_Note { get; set; }
        public decimal? LEP_ExitPrice { get; set; }
        public string LEP_Note { get; set; }
        public string Note { get; set; }
        //
        public decimal? PerfectEntry_EntryPrice { get; set; }
        public decimal? PerfectEntry_ExitPrice { get; set; }
        public string PerfectEntry_Note { get; set; }
        public decimal? TradeExit_ExitPrice { get; set; }


        // Trade
        public int? TradeId { get; set; }
        public virtual Trade Trade { get; set; }
    }
}
