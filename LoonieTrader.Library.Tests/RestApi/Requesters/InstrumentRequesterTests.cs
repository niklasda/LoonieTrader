using LoonieTrader.Library.RestApi.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoonieTrader.Library.Tests.RestApi.Requesters;

[TestClass, TestCategory("Integration")]
public class InstrumentRequesterTests : TestClassBase
{

    [TestMethod]
    public void TestGetCandlesDefaults()
    {
        var resp = InstrReq.GetCandles("EUR_USD");
        Console.WriteLine(resp);
        Assert.IsNotNull(resp);
    }

    [TestMethod]
    public void TestGetCandlesError()
    {
        var resp = InstrReq.GetCandles("EUXYZSD");
        Console.WriteLine(resp);
        Assert.IsNotNull(resp);
    }

    [TestMethod]
    public void TestGetCandlesS5CountM5()
    {
        var resp = InstrReq.GetCandles("EUR_USD", CandlestickGranularity.S5, "M", 5);
        Console.WriteLine(resp);
        Assert.IsNotNull(resp);
    }

    [TestMethod]
    public void TestGetCandlesS5CountA5()
    {
        var resp = InstrReq.GetCandles("GBP_USD", CandlestickGranularity.S15, "A", 5);
        Console.WriteLine(resp);
        Assert.IsNotNull(resp);
    }

    [TestMethod]
    public void TestGetCandlesS5CountB5()
    {
        var resp = InstrReq.GetCandles("USD_SEK", CandlestickGranularity.S30, "B", 5);
        Console.WriteLine(resp);
        Assert.IsNotNull(resp);
    }

    [TestMethod]
    public void TestGetCandlesS5CountDate()
    {
        var resp = InstrReq.GetCandles("EUR_USD", DateTime.Now.AddMonths(-1));
        Console.WriteLine(resp);
        Assert.IsNotNull(resp);
    }

}