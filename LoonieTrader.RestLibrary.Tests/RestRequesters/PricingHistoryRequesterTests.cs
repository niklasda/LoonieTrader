using System;
using NUnit.Framework;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.RestApi.Interfaces;
using LoonieTrader.RestLibrary.Tests.Locator;

namespace LoonieTrader.RestLibrary.Tests.RestRequesters
{
    public class PricingHistoryRequesterTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var container = TestServiceLocator.Initialize();
            _txr = container.GetInstance<IPricingHistoryRequester>();
            _s = container.GetInstance<ISettings>();
        }

        private IPricingHistoryRequester _txr;
        private ISettings _s;

        [Test]
        public void Test()
        {
        }

    }
}
