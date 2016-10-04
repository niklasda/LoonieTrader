using NUnit.Framework;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.Tests.Locator;

namespace LoonieTrader.Library.Tests.RestRequesters
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
