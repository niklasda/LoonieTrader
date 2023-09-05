using LoonieTrader.Library.Models;
using LoonieTrader.Library.RestApi.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoonieTrader.Library.Tests.RestApi.Requesters;

[TestClass, TestCategory("Integration")]
public class AccountsRequesterTests : TestClassBase
{

    [TestMethod]
    public void TestGetAccounts()
    {
        Assert.IsNotNull(AccReq.GetAccounts());
    }

    [TestMethod]
    public void TestGetAccountDetails()
    {
        Assert.IsNotNull(AccReq.GetAccountDetails(EnvSettings.DefaultAccountId));
    }

    [TestMethod]
    public void TestGetAccountSummary()
    {
        Assert.IsNotNull(AccReq.GetAccountSummary(EnvSettings.DefaultAccountId));
    }

    [TestMethod]
    public void TestGetAccountInstruments()
    {
        AccountInstrumentsResponse air = AccReq.GetAccountInstruments(EnvSettings.DefaultAccountId);
        Assert.IsNotNull(air);

        Assert.AreEqual(35, air.instruments.Count(x => x.type == "CFD"));
        Assert.AreEqual(71, air.instruments.Count(x => x.type == "CURRENCY"));
        Assert.AreEqual(23, air.instruments.Count(x => x.type == "METAL"));
    }

    [TestMethod]
    public void TestGetAccountInstrumentSortedHierarchy()
    {
        AccountInstrumentsResponse air = AccReq.GetAccountInstruments(EnvSettings.DefaultAccountId);
        Assert.IsNotNull(air);

        IEnumerable<IGrouping<string, AccountInstrumentsResponse.Instrument>> groups = air.instruments.Select(x => x).OrderBy(y => y.type).GroupBy(x => x.type);

        List<InstrumentType> its = groups.Select(x => new InstrumentType {Type = x.Key, Instruments = x.ToArray()}).ToList();

        Assert.AreEqual(3, its.Count);


        Assert.AreEqual("CFD", its[0].Type);
        Assert.AreEqual(35, its[0].Instruments.Length);

        Assert.AreEqual("CURRENCY", its[1].Type);
        Assert.AreEqual(71, its[1].Instruments.Length);

        Assert.AreEqual("METAL", its[2].Type);
        Assert.AreEqual(23, its[2].Instruments.Length);

    }

    [TestMethod]
    public void TestGetAccountConfigurationChanges()
    {
        AccountChangesResponse air = AccReq.GetAccountChanges(EnvSettings.DefaultAccountId, "94"); // 3337-2920=417
        Console.WriteLine(air);
        Assert.IsNotNull(air);
    }
}