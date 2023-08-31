using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoonieTrader.Library.Tests.RestApi.Requesters;

[TestClass, TestCategory("Integration")]
public class PositionsRequesterTests : TestClassBase
{
    //[TestInitialize]
    //public void Setup()
    //{
    //    var container = TestServiceLocator.Initialize();
    //    PosReq = container.GetInstance<IPositionsRequester>();
    //    EnvSettings = container.GetInstance<ISettingsService>().CachedSettings.SelectedEnvironment;
    //}

    //private IPositionsRequester PosReq;
    //private IEnvironmentSettings EnvSettings;

    [TestMethod]
    public void TestGetAccountPositions()
    {
        var resp = PosReq.GetPositions(EnvSettings.DefaultAccountId);
        Console.WriteLine(resp);
        Assert.IsNotNull(resp);
    }

    [TestMethod]
    public void TestGetAccountOpenPositions()
    {
        var resp = PosReq.GetOpenPositions(EnvSettings.DefaultAccountId);
        Console.WriteLine(resp);
        Assert.IsNotNull(resp);
    }

    [TestMethod]
    public void TestGetAccountInstrumentPositions()
    {
        var resp = PosReq.GetInstrumentPositions(EnvSettings.DefaultAccountId, "EUR_USD");
        Console.WriteLine(resp);
        Assert.IsNotNull(resp);
    }

    [TestMethod]
    public void TestCloseAccountInstrumentPositions()
    {
        var resp = PosReq.PutClosePosition(EnvSettings.DefaultAccountId, "EUR_USD");
        Console.WriteLine(resp);
        Assert.IsNotNull(resp);
    }
}