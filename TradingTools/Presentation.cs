using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingTools
{
    class Presentation
    {
        private const int SECOND = 1000;
        public const int INTERNAL_TIMER_REFRESH_VALUE = SECOND * 15;
    }

    public enum RiskRewardCalcState
    {
        Empty,
        Loaded,
        OpenPosition,
        ClosedPosition
    }

    public static class BandColor
    {
        public static readonly Color Default = Color.MediumSeaGreen;
        public static readonly Color Empty = Color.Orange;
        public static readonly Color Loaded = Color.Blue;
        public static readonly Color OpenPosition = Default;
        public static readonly Color ClosedPosition = Color.Orange;
    }

}
