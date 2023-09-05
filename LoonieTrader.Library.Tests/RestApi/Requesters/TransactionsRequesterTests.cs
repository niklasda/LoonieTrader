using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoonieTrader.Library.Tests.RestApi.Requesters;

[TestClass, TestCategory("Integration")]
public class TransactionsRequesterTests : TestClassBase
{

    [TestMethod]
    public void TestGetAccountTransactionPages()
    {
        var resp = TxReq.GetTransactionPages(EnvSettings.DefaultAccountId);
        Console.WriteLine(resp);
        Assert.IsNotNull(resp);
    }

    [TestMethod]
    public void TestGetAccountTransactions()
    {
        var resp = TxReq.GetTransactions(EnvSettings.DefaultAccountId, "94");
        Console.WriteLine(resp);
        Assert.IsNotNull(resp);
    }

    [TestMethod]
    public void TestGetAllAccountTransactions()
    {
        var resp = TxReq.GetAllTransactions(EnvSettings.DefaultAccountId);
        Console.WriteLine(resp);
        Assert.IsNotNull(resp);
    }

    [TestMethod]
    public void TestGetAccountTransactionDetails()
    {
        var resp = TxReq.GetTransactionDetails(EnvSettings.DefaultAccountId, "3337");
        Console.WriteLine(resp);
        Assert.IsNotNull(resp);
    }

}