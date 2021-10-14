using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace TradingTools.Trunk.Validation
{
    public class Validation
    {

    }

    public class Format
    {
        public static bool isDecimal(string value, out string errorMsg)
        {
            string pattern = @"^((\d+)((\.\d{1,6})?))$";
            Regex rg = new Regex(pattern);

            errorMsg = "";
            if (!rg.IsMatch(value))
            {
                errorMsg = message("Must be numeric with up-to 6 decimal place", "8.123456");
                return false;
            }

            return true;
        }

        public static bool isMoney(string value, out string errorMsg)
        {
            // This regular expression looks for any number of digits.
            // And if dot character is added it will look for decimal places upto 2 decimals.
            string pattern = @"^((\d+)((\.\d{1,2})?))$";
            Regex rg = new Regex(pattern);

            errorMsg = "";
            if (!rg.IsMatch(value))
            {
                errorMsg = message("Must be numeric with upto 2 decimal place", "123.12");
                return false;
            }

            return true;
        }
        public static bool isInteger(string value, out string errorMsg)
        {
            string pattern = @"^[1-9]{1,3}$";
            Regex rg = new Regex(pattern);

            errorMsg = "";
            if (!rg.IsMatch(value))
            {
                errorMsg = message("Must be a whole number", "any number from 1-999");
                return false;
            }
            return true;
        }

        public static bool isTicker(string value, out string errorMsg)
        {
            //TODO - needs to validate
            string pattern = @"^[a-zA-Z]{1,5}[\/][a-zA-Z]{1,5}$";
            Regex rg = new Regex(pattern);

            errorMsg = "";
            if (!rg.IsMatch(value))
            {
                errorMsg = message("Must be a trading pair", "btc/usdt or BTC/USDT");
                return false;
            }
            return true;
        }
        private static string message(string msg, string example)
        {
            return $"Incorrect format. \n\n{msg}. \n\nExample: {example}";
        }

        
    }
    public class InputConverter
    {
        public static decimal Decimal(string value)
        {
            decimal def_out;
            return decimal.TryParse(value, out def_out) ? def_out : 0;
        }

    }

    public class StringToNumeric
    {
        public static decimal MoneyToDecimal(string value)
        {
            NumberStyles style = NumberStyles.Currency;
            CultureInfo culture = null;
            decimal def_out;

            return decimal.TryParse(value, style, culture, out def_out) ? def_out : 0;
        }

    }

    public class SafeConvert
    {
        public static decimal ToDecimalSafe(double input)
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
