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
        public void LeveragedCapital_WhenCalled_ReturnCorrectAnswer()
        {
            Assert.That(Formula.LeveragedCapital(2,3), Is.EqualTo(6));
        }

        [Test]
        [TestCase(9.9)]
        [TestCase(-0.01)]
        [Ignore("Minimum might be adjusted")]
        public void LeveragedCapital_CapitalIsLessThanTen_ThrowArgumentException(decimal capital)
        {
            Assert.That(() => Formula.LeveragedCapital(capital, 2), Throws.InstanceOf<ArgumentException>());
        }

        [Test]
        [TestCase(0.9)]
        [TestCase(-0.01)]
        [Ignore("Minimum might be adjusted")]
        public void LeveragedCapital_LeverageIsLessThanOne_ThrowArgumentException(decimal leverage)
        {
            Assert.That(() => Formula.LeveragedCapital(100, leverage), Throws.InstanceOf<ArgumentException>());
        }

        // Lot Size
        [Test]
        public void LotSize_WhenCalled_ReturnCorrectAnswer()
        {
            Assert.That(Formula.LotSize(100, 2), Is.EqualTo(50));
            Assert.That(Formula.LotSize(100, 2, 2), Is.EqualTo(100));
        }

        [Test]
        public void LotSize_EntryPriceIsZero_ReturnZero()
        {
            Assert.That(Formula.LotSize(100, 0), Is.EqualTo(0));
            Assert.That(Formula.LotSize(100, 1, 0), Is.EqualTo(0));
        }

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

        [Test]
        public void PCP_EntryPriceIsZero_ReturnZero()
        {
            Assert.That(Formula.PCP(0, 1), Is.EqualTo(0));
        }

        [Test]
        public void PCP_ExitPriceIsNull_ReturnZero()
        {
            Assert.That(Formula.PCP(1, default), Is.EqualTo(0));
        }
    }
}
