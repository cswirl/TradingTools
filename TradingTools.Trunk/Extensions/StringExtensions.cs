﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingTools.Trunk.Extensions
{
    public static class StringExtensions
    {
        // Convert string and currency ($10.0) - Return 0 if all fails
        public static decimal ToDecimal(this string str)
        {
            try
            {
                return Convert.ToDecimal(str);

            } 
            catch (FormatException)
            {
                NumberStyles style = NumberStyles.Currency;
                CultureInfo culture = null;
                decimal def_out;

                return decimal.TryParse(str, style, culture, out def_out) ? def_out : 0;
            }
        }
    }

    public static class DecimalExtensions 
    { 
        public static string ToMoney(this decimal d) => d.ToString(Constant.MONEY_FORMAT);

        public static string ToLeverageDecimalPlace(this decimal d) => d.ToString(Constant.LEVERAGE_DECIMAL_PLACE);

        public static string ToMaxDecimalPlace(this decimal d) => d.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);

    }


}
