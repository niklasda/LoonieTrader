using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.Tests.Locator;

namespace LoonieTrader.Library.Tests.RestApi.Requesters
{
    [TestClass, TestCategory("Integration")]
    public class HealthRequesterTests
    {
        [TestInitialize]
        public void Setup()
        {
            var container = TestServiceLocator.Initialize();
            _hr = container.GetInstance<IHealthRequester>();
        }

        private IHealthRequester _hr;

        [TestMethod]
        public void TestGetServiceList()
        {
            var stats = _hr.GetServiceList();
            Console.WriteLine(stats);
            Assert.IsNotNull(stats);
        }

        [TestMethod]
        public void TestGetServices()
        {
            var stats = _hr.GetServices();
            Console.WriteLine(stats);
            Assert.IsNotNull(stats);
        }

        [TestMethod]
        public void TestGetService()
        {
            var stats1 = _hr.GetService("fxtrade-practice-rest-api");
            var stats2 = _hr.GetService("fxtrade-practice-streaming-api");
            var stats3 = _hr.GetService("fxtrade-rest-api");
            var stats4 = _hr.GetService("fxtrade-streaming-api");
            Console.WriteLine(stats1);
            Console.WriteLine(stats2);
            Console.WriteLine(stats3);
            Console.WriteLine(stats4);
            Assert.IsNotNull(stats1);
            Assert.IsNotNull(stats2);
            Assert.IsNotNull(stats3);
            Assert.IsNotNull(stats4);
        }

        [TestMethod]
        public void TestGetStatuses()
        {
            var stats = _hr.GetStatuses();
            Console.WriteLine(stats);
            Assert.IsNotNull(stats);
        }

        [TestMethod]
        public void TestGetStatus()
        {
            var stats1 = _hr.GetStatus("down");
            var stats2 = _hr.GetStatus("up");
            var stats3 = _hr.GetStatus("warning");
            Console.WriteLine(stats1);
            Console.WriteLine(stats2);
            Console.WriteLine(stats3);
            Assert.IsNotNull(stats1);
            Assert.IsNotNull(stats2);
            Assert.IsNotNull(stats3);
        }

        [TestMethod]
        public void TestGetServiceEvents()
        {
            var stats1 = _hr.GetServiceEvents("fxtrade-practice-rest-api");
            var stats2 = _hr.GetServiceEvents("fxtrade-practice-streaming-api");
            var stats3 = _hr.GetServiceEvents("fxtrade-rest-api");
            var stats4 = _hr.GetServiceEvents("fxtrade-streaming-api");
            Console.WriteLine(stats1);
            Console.WriteLine(stats2);
            Console.WriteLine(stats3);
            Console.WriteLine(stats4);
            Assert.IsNotNull(stats1);
            Assert.IsNotNull(stats2);//ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgICA_L2BCgw
            Assert.IsNotNull(stats3);
            Assert.IsNotNull(stats4);
        }

        [TestMethod]
        public void TestGetServiceEvent()
        {
            var stats1 = _hr.GetServiceEvent("fxtrade-practice-rest-api", "ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgICA_L2BCgw"); // ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgIDAnoOUCgw
            var stats2 = _hr.GetServiceEvent("fxtrade-practice-streaming-api", "ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgIDAq5yeCgw");       // ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgIDAq5yeCgw
            var stats3 = _hr.GetServiceEvent("fxtrade-rest-api", "ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgIDAy_KSCgw");                // ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgIDAy_KSCgw
            var stats4 = _hr.GetServiceEvent("fxtrade-streaming-api", "ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgIDAy7maCgw");       // ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgIDAy7maCgw
            Console.WriteLine(stats1);
            Console.WriteLine(stats2);
            Console.WriteLine(stats3);
            Console.WriteLine(stats4);
            Assert.IsNotNull(stats1);
            Assert.IsNotNull(stats2);//ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgICA_L2BCgw
            Assert.IsNotNull(stats3);
            Assert.IsNotNull(stats4);
        }

    }
}
