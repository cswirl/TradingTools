﻿using System;
using System.Collections.Generic;
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
            string pattern = @"((\d+)((\.\d{1,6})?))$";
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
            string pattern = @"^\d$";
            Regex rg = new Regex(pattern);

            errorMsg = "";
            if (!rg.IsMatch(value))
            {
                errorMsg = message("Must be a whole number", "12");
                return false;
            }
            return true;
        }

        private static string message(string msg, string example)
        {
            return $"Incorrect format. \n\n{msg}. \n\nExample: {example}";
        }

    }
}
