using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingTools.Trunk.Validations
{
    public static class NumericValidationExtensions
    {
        // Decimal
        public static void MustBeAbove(this decimal num, decimal value, string name = "{param}")
        {
            if (num <= value) throw new ArgumentOutOfRangeException($"{name} must be above zero");
        }

        public static void MustBeEqualOrAbove(this decimal num, decimal value, string name)
        {
            if (num < value) throw new ArgumentOutOfRangeException($"{name} must be more or equal to {value}");
        }

        public static void CannotBeZero(this decimal num, string name = "{param}")
        {
            if (num == 0) throw new ArgumentOutOfRangeException($"{name} cannot be zero");
        }
    }
}
