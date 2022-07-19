using Newtonsoft.Json;
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
        public string Side { get; set; }      //long or short
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

        #region "Computed Data"
        // Leveraged Capital
        [Column(TypeName = "money")]
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
        public decimal? BorrowAmount
        {
            get
            {
                return Formula.BorrowedAmount(Leverage, Capital);
            }
            private set { }
        }

        // Day Count
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? DayCount
        {
            get
            {
                return Formula.DayCount(DateEnter, DateExit ?? DateTime.Now);
            }
            private set { }
        }
        #endregion

        #region "Closing"
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
                return Formula.PnL(Capital, FinalCapital ?? 0);
            }
            private set { }
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? PnL_percentage
        {
            get
            {
                return Formula.PnL_percentage(Capital, FinalCapital ?? 0);
            }
            private set { }
        }

        [Required, MaxLength(20), DefaultValue("open")]
        public string Status { get; set; }
        #endregion

        // Navigation Properties
        public virtual CalculatorState CalculatorState { get; set; }
        public virtual ICollection<TradeThread> TradeThreads { get; set; }
    }
}
