using System;
using System.Collections.Generic;
using LoonieTrader.Shared.Interfaces;
using LoonieTrader.Shared.Models;
using NUnit.Framework;

namespace DemoLaggingIndicator.Tests
{
    [TestFixture]
    public class DemoLaggingIndicatorTests
    {
        [Test]
        public void TestLaggingIndicator()
        {
            ILaggingIndicator trader = new DemoLaggingIndicator();
            var req = trader.GetRequirements();

            var points = new List<OhlciPoint>();
            var p = trader.CalculatePoints(points);

            CollectionAssert.AllItemsAreInstancesOfType(p, typeof(PricePoint));
        }
    }
}
