using System;
using System.Collections.Generic;
using LoonieTrader.Shared.Interfaces;
using LoonieTrader.Shared.Models;
using NUnit.Framework;

namespace DemoLeadingIndicator.Tests
{
    [TestFixture]
    public class DemoLeadingIndicatorTests
    {
        [Test]
        public void TestLeadingIndicator()
        {
            var r = new Random();

            ILeadingIndicator trader = new DemoLeadingIndicator();
            var req = trader.GetRequirements();

            int numOfPoints = r.Next(req.MinPoints, req.MaxPoints);

            trader.SetSpecification(new Specification {NumberOfPoints = numOfPoints});

            var points = new List<OhlciPoint>();
            var calculatedPoints = trader.CalculatePoints(points);

            CollectionAssert.AllItemsAreInstancesOfType(calculatedPoints, typeof(PricePoint));
            Assert.AreEqual(numOfPoints, calculatedPoints.Count);
        }
    }
}
