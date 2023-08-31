using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.Tests.Locator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoonieTrader.Library.Tests.RestApi.Requesters;

[TestClass, TestCategory("Integration")]
public class TransactionsRequesterTests
{
    [TestInitialize]
    public void Setup()
    {
        var container = TestServiceLocator.Initialize();
        _txr = container.GetInstance<ITransactionsRequester>();
        _s = container.GetInstance<ISettingsService>().CachedSettings.SelectedEnvironment;
    }

    private ITransactionsRequester _txr;
    private IEnvironmentSettings _s;

    [TestMethod]
    public void TestGetAccountTransactionPages()
    {
        var resp = _txr.GetTransactionPages(_s.DefaultAccountId);
        Console.WriteLine(resp);
        Assert.IsNotNull(resp);
    }

    [TestMethod]
    public void TestGetAccountTransactions()
    {
        var resp = _txr.GetTransactions(_s.DefaultAccountId);
        Console.WriteLine(resp);
        Assert.IsNotNull(resp);
    }

    [TestMethod]
    public void TestGetAllAccountTransactions()
    {
        var resp = _txr.GetAllTransactions(_s.DefaultAccountId);
        Console.WriteLine(resp);
        Assert.IsNotNull(resp);
    }

    [TestMethod]
    public void TestGetAccountTransactionDetails()
    {
        var resp = _txr.GetTransactionDetails(_s.DefaultAccountId, "3337");
        Console.WriteLine(resp);
        Assert.IsNotNull(resp);
    }

}