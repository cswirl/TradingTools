using System;
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

        public static int ToInteger(this string str)
        {
            try
            {
                return Convert.ToInt32(str);

            }
            catch (FormatException)
            {
                NumberStyles style = NumberStyles.Currency;
                CultureInfo culture = null;
                int def_out;

                return int.TryParse(str, style, culture, out def_out) ? def_out : 0;
            }
        }
    }

    public static class DecimalExtensions 
    {
        public static string ToMoney(this decimal d) => d.ToString(Constant.MONEY_FORMAT);
        public static string ToPercentageSingle(this decimal d) => d.ToString(Constant.PERCENTAGE_FORMAT_SINGLE);

        public static string ToString_WholeNumber(this decimal d) => d.ToString(Constant.WHOLE_NUMBER);
        public static string ToString_OneDecimal(this decimal d) => d.ToString(Constant.DECIMAL_ONE);
        public static string ToString_TwoDecimal(this decimal d) => d.ToString(Constant.DECIMAL_TWO);
        public static string ToString_UptoOneDecimal(this decimal d) => d.ToString(Constant.DECIMAL_UPTO_ONE);
        public static string ToString_UptoTwoDecimal(this decimal d) => d.ToString(Constant.DECIMAL_UPTO_TWO);
        public static string ToString_UptoMaxDecimal(this decimal d) => d.ToString(Constant.DECIMAL_UPTO_MAX);

    }

    public static class DoubleExtensions
    {
        public static decimal ToDecimalSafe(this double input)
        {
            if (input < (double)decimal.MinValue)
                return decimal.MinValue;
            else if (input > (double)decimal.MaxValue)
                return decimal.MaxValue;
            else
                return (decimal)input;
        }
    }
}
