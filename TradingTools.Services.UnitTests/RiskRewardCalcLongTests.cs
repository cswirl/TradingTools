using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Services.Dto;
using TradingTools.Trunk;

namespace TradingTools.Services.UnitTests
{
    [TestFixture]
    public class RiskRewardCalcLongTests
    {
        [Test]
        [TestCase(0)]
        [TestCase(-0.99)]
        public void ComputeTradeExit_ExitPriceIsZeroOrBelow_ThrowArgumentException(decimal exitPrice)
        {
            var pos = new Position();
            var rrc = new RiskRewardCalcLong(pos);

            Assert.That(() => rrc.ComputeTradeExit(exitPrice, pos), Throws.InstanceOf<ArgumentException>());
        }
    }
}
