using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk;

namespace TradingTools.Services.UnitTests
{
    [TestFixture]
    public class FormulaTests
    {
        // LeveragedCapital
        [Test]
        [TestCase(9.9)]
        [TestCase(-0.01)]
        public void LeveragedCapital_CapitalIsLessThanTen_ThrowArgumentException(decimal capital)
        {
            Assert.That(() => Formula.LeveragedCapital(capital, 2), Throws.InstanceOf<ArgumentException>());
        }

        [Test]
        [TestCase(0.9)]
        [TestCase(-0.01)]
        public void LeveragedCapital_LeverageIsLessThanOne_ThrowArgumentException(decimal leverage)
        {
            Assert.That(() => Formula.LeveragedCapital(100, leverage), Throws.InstanceOf<ArgumentException>());
        }

        // Lot Size
        [Test]
        [TestCase(0)]
        [TestCase(-0.01)]
        public void LotSize_LeveragedCapitalIsZeroOrLess_ThrowArgumentException(decimal leveragedCapital)
        {
            Assert.That(() => Formula.LotSize(leveragedCapital, 2), Throws.InstanceOf<ArgumentException>());
        }

        [Test]
        [TestCase(0)]
        [TestCase(-0.01)]
        public void LotSize_EntryPriceIsZeroOrLess_ThrowArgumentException(decimal entryPrice)
        {
            Assert.That(() => Formula.LotSize(100, entryPrice), Throws.InstanceOf<ArgumentException>());
        }

        // Leverage
        [Test]
        public void Leverage_CapitalIsZero_ThrowArgumentException()
        {
            Assert.That(() => Formula.Leverage(10, 0), Throws.InstanceOf<ArgumentException>());

            Assert.That(() => Formula.Leverage(10, 10, 0), Throws.InstanceOf<ArgumentException>());
        }
    }
}
