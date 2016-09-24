using NUnit.Framework;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.Tests.Locator;

namespace LoonieTrader.Library.Tests.RestRequesters
{
    public class HealthRequesterTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var container = TestServiceLocator.Initialize();
            _ar = container.GetInstance<IHealthRequester>();
        }

        private IHealthRequester _ar;

        [Test]
        public void TestGetServiceList()
        {
            Assert.NotNull(_ar.GetServiceList());
        }

        [Test]
        public void TestGetServices()
        {
            Assert.NotNull(_ar.GetServices());
        }

        [Test]
        public void TestGetService()
        {
            Assert.NotNull(_ar.GetService("fxtrade-practice-rest-api"));
            Assert.NotNull(_ar.GetService("fxtrade-practice-streaming-api"));
            Assert.NotNull(_ar.GetService("fxtrade-rest-api"));
            Assert.NotNull(_ar.GetService("fxtrade-streaming-api"));
        }

        [Test]
        public void TestGetStatuses()
        {
            Assert.NotNull(_ar.GetStatuses());
        }

        [Test]
        public void TestGetStatus()
        {
            Assert.NotNull(_ar.GetStatus("down"));
            Assert.NotNull(_ar.GetStatus("up"));
            Assert.NotNull(_ar.GetStatus("warning"));
        }

    }
}
