using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradingTools.Trunk.Entity
{
    public class TradeChallenge
    {
        public int Id { get; set; }
        
        [Required]
        public int TradeCap { get; set; }   // soft cap - just a reference for a trade limit
        [DefaultValue(true)]
        public bool IsOpen { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string Title
        {
            get { return $"{TradeCap} Trading Challenge"; }
            private set { }
        }
        public string Description { get; set; }


        // Navigation Properties
        public virtual ICollection<TradeThread> TradeThreads { get; set; }

    }
}
