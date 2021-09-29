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
        [Required]
        [Column(TypeName = "money")]
        public decimal Capital { get; set; }
        [Required]
        public decimal Leverage { get; set; }
        [Required]
        public decimal EntryPriceAvg { get; set; }
        public int DayCount { get; set; }
        [Column(TypeName = "decimal(18, 5)")]
        public decimal DailyInterestRate { get; set; }
        public decimal PriceIncreaseTarget { get; set; }
        public decimal PriceDecreaseTarget { get; set; }
        public decimal PEP_ExitPrice { get; set; }
        public string PEP_Note { get; set; }
        public decimal LEP_ExitPrice { get; set; }
        public string LEP_Note { get; set; }

        [MaxLength(20)]
        public string Ticker { get; set; }
        public string Strategy { get; set; }
        public string Note { get; set; }
    }
}
