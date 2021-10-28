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
        [Required, MaxLength(20)]
        public string TradingStyle { get; set; }
        [Required]
        public DateTime DateEnter { get; set; }

        // Position
        [Required]
        [Column(TypeName = "money")]
        public decimal Capital { get; set; }
        [Required]
        public decimal EntryPriceAvg { get; set; }
        [Required]
        public decimal LotSize { get; set; }
        [Required]
        public decimal Leverage { get; set; }

        // Leveraged Capital
        [Column(TypeName = "money")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? LeveragedCapital 
        { 
            get 
            {
                return Capital * Leverage;
            }
            private set { } 
        }

        // Borrow
        public decimal? BorrowAmount
        {
            get
            {
                var l = LeveragedCapital ?? Capital;
                return l - Capital;
            }
            private set { }
        }

        // Day Count
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? DayCount
        {
            get
            {
                int d = Convert.ToInt32(Trade_Utility.GetTrading_ElaspsedTime_Days(DateEnter, DateExit ?? DateTime.Now));
                return d == 0 ? 1 : d;
            }
            private set { }
        }

        // Closing
        public DateTime? DateExit { get; set; }
        public decimal? ExitPriceAvg { get; set; }
        public decimal? FinalCapital { get; set; }

        // PnL
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? PnL
        {
            get
            {
                return FinalCapital - Capital;
            }
            private set { }
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? PnL_percentage
        {
            get
            {
                return (FinalCapital - Capital) / Capital * 100;
            }
            private set { }
        }

        [Required, MaxLength(20), DefaultValue("open")]
        public string Status { get; set; }

        // CalculatorState
        public virtual CalculatorState CalculatorState { get; set; }

    }
}
