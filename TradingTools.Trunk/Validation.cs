using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace TradingTools.Trunk.Validation
{
    public class Validation
    {
        // For Dynamic type
        public static bool HasProperty(dynamic obj, string name)
        {
            Type objType = obj.GetType();

            if (objType == typeof(ExpandoObject))
            {
                return ((IDictionary<string, object>)obj).ContainsKey(name);
            }

            return objType.GetProperty(name) != null;
        }

        public static DateTime DateExit_LateSetting_Fixer(DateTime dateExit)
        {
            if (dateExit < DateTime.Today)
            {
                dateExit = dateExit.Date.AddDays(1).AddTicks(-1);
            }

            return dateExit;
        }

        
    }

    public class Format
    {
        public static bool isDecimal(string value, out string errorMsg)
        {
            string pattern2 = @"^((\d+)?((\.\d{1,9})?))$";
            Regex rg = new Regex(pattern2);

            errorMsg = "";
            if (!rg.IsMatch(value))
            {
                errorMsg = message("Must be numeric with up-to 9 decimal place", "8.123456789");
                return false;
            }

            return true;
        }

        public static bool isMoney(string value, out string errorMsg)
        {
            // This regular expression looks for any number of digits.
            // And if dot character is added it will look for decimal places upto 2 decimals.
            //string pattern = @"^((\d+)((\.\d{1,2})?))$";


            //source: https://stackoverflow.com/questions/16242449/regex-currency-validation
            // Decimal and commas optional
            string pattern = @"^(?=.*?\d)^\$?(([1-9]\d{0,2}(,\d{3})*)|\d+)?(\.\d{1,2})?$";
            Regex rg = new Regex(pattern);

            errorMsg = "";
            if (!rg.IsMatch(value))
            {
                errorMsg = message("Must be numeric with upto 2 decimal place", "123.12 or $123.12");
                return false;
            }

            return true;
        }
        public static bool isWholeNumberAboveZero(string value, out string errorMsg)
        {
            string pattern = @"^[1-9][0-9]*$";
            Regex rg = new Regex(pattern);

            errorMsg = "";
            if (!rg.IsMatch(value))
            {
                errorMsg = message("Must be a whole number", "any number above zero");
                return false;
            }
            return true;
        }

        public static bool isTicker(string value, out string errorMsg)
        {
            // Pattern example: AAAAA/ZZZZZ 
            string pattern = @"^[a-zA-Z0-9]{1,9}[\/][a-zA-Z0-9]{1,9}$";
            Regex rg = new Regex(pattern);

            errorMsg = "";
            if (!rg.IsMatch(value))
            {
                errorMsg = message("Must be a trading pair\nMax of 9 characters for symbol", "btc/usdt or BTC/USDT or ABCDEFGHI/ABCDEFGHI");
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

        public static decimal MoneyToDecimal(string value)
        {
            NumberStyles style = NumberStyles.Currency;
            CultureInfo culture = null;
            decimal def_out;

            return decimal.TryParse(value, style, culture, out def_out) ? def_out : 0;
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
