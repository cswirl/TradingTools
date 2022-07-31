using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradingTools
{
    class Presentation
    {
        private const int SECOND = 1000;
        public const int INTERNAL_TIMER_REFRESH_VALUE = SECOND * 60;

        public static void DateTimePicker_MaxDate_SafeAssign(DateTimePicker dtp, DateTime d)
        {
            dtp.MaxDate = DateTime.Today.AddDays(1).AddTicks(-1);
            dtp.Value = d;
        }
    }

    public static class BandColor
    {
        public static readonly Color Default = Color.MediumSeaGreen;
        public static readonly Color Empty = Color.Orange;
        public static readonly Color Loaded = Color.Blue;
        public static readonly Color TradeOpen = Default;
        public static readonly Color TradeClosed = Default;
    }
}