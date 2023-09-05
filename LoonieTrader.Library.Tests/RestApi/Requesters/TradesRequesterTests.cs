using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoonieTrader.Library.Tests.RestApi.Requesters;

[TestClass, TestCategory("Integration")]
public class TradesRequesterTests : TestClassBase
{

    [TestMethod]
    public void TestGetAccountTrades()
    {
        var resp = TradesReq.GetTrades(EnvSettings.DefaultAccountId);
        Console.WriteLine(resp);
        Assert.IsNotNull(resp);
    }

    [TestMethod]
    public void TestGetAccountOpenTrades()
    {
        var resp = TradesReq.GetOpenTrades(EnvSettings.DefaultAccountId);
        Console.WriteLine(resp);
        Assert.IsNotNull(resp);
    }

    [TestMethod]
    public void TestGetAccountTradeDetails()
    {
        var resp = TradesReq.GetTradeDetails(EnvSettings.DefaultAccountId, "37");
        Console.WriteLine(resp);
        Assert.IsNotNull(resp);
    }
}