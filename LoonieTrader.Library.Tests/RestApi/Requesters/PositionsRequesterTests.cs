using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoonieTrader.Library.Tests.RestApi.Requesters;

[TestClass, TestCategory("Integration")]
public class PositionsRequesterTests : TestClassBase
{

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