using System.Collections.Generic;
using LoonieTrader.Shared.Models;
using NUnit.Framework;

namespace DemoAlgo.Tests
{
    [TestFixture]
    public class DemoAlgoTraderTests
    {
        [Test]
        public void TestAlgo()
        {
            DemoAlgoTrader trader = new DemoAlgoTrader();
            var req = trader.GetRequirements();

            var points = new List<OhlciPoint>();
            var decision = trader.Decide(points);

            Assert.AreEqual(TradeAction.None, decision);
        }
    }
}