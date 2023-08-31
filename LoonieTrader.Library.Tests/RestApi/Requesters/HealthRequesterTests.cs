using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoonieTrader.Library.Tests.RestApi.Requesters;

[TestClass, TestCategory("Integration")]
public class HealthRequesterTests : TestClassBase
{
    //[TestInitialize]
    //public void Setup()
    //{
    //    var container = TestServiceLocator.Initialize();
    //    HealthReq = container.GetInstance<IHealthRequester>();
    //}

    //private IHealthRequester HealthReq;

    [TestMethod]
    public void TestGetServiceList()
    {
        var stats = HealthReq.GetServiceList();
        Console.WriteLine(stats);
        Assert.IsNotNull(stats);
    }

    [TestMethod]
    public void TestGetServices()
    {
        var stats = HealthReq.GetServices();
        Console.WriteLine(stats);
        Assert.IsNotNull(stats);
    }

    [TestMethod]
    public void TestGetService()
    {
        var stats1 = HealthReq.GetService("fxtrade-practice-rest-api");
        var stats2 = HealthReq.GetService("fxtrade-practice-streaming-api");
        var stats3 = HealthReq.GetService("fxtrade-rest-api");
        var stats4 = HealthReq.GetService("fxtrade-streaming-api");
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
        var stats = HealthReq.GetStatuses();
        Console.WriteLine(stats);
        Assert.IsNotNull(stats);
    }

    [TestMethod]
    public void TestGetStatus()
    {
        var stats1 = HealthReq.GetStatus("down");
        var stats2 = HealthReq.GetStatus("up");
        var stats3 = HealthReq.GetStatus("warning");
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
        var stats1 = HealthReq.GetServiceEvents("fxtrade-practice-rest-api");
        var stats2 = HealthReq.GetServiceEvents("fxtrade-practice-streaming-api");
        var stats3 = HealthReq.GetServiceEvents("fxtrade-rest-api");
        var stats4 = HealthReq.GetServiceEvents("fxtrade-streaming-api");
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
        var stats1 = HealthReq.GetServiceEvent("fxtrade-practice-rest-api", "ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgICA_L2BCgw"); // ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgIDAnoOUCgw
        var stats2 = HealthReq.GetServiceEvent("fxtrade-practice-streaming-api", "ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgIDAq5yeCgw");       // ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgIDAq5yeCgw
        var stats3 = HealthReq.GetServiceEvent("fxtrade-rest-api", "ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgIDAy_KSCgw");                // ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgIDAy_KSCgw
        var stats4 = HealthReq.GetServiceEvent("fxtrade-streaming-api", "ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgIDAy7maCgw");       // ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgIDAy7maCgw
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