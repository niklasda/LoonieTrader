﻿using LoonieTrader.Library.Models;
using LoonieTrader.Library.RestApi.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoonieTrader.Library.Tests.RestApi.Requesters;

[TestClass, TestCategory("Integration")]
public class TransactionsStreamingRequesterTests : TestClassBase
{

    [TestMethod]
    public void TestTransactionStream()
    {
        ObservableStream<TransactionsResponse.Transaction> tss = TxStreamReq.GetTransactionStream(EnvSettings.DefaultAccountId);
        tss.NewValue += Tss_NewTrx;

        Task.Delay(10000).Wait();
        Console.WriteLine("Done. 10s");

        tss.NewValue -= Tss_NewTrx;

        //            var price = JSON.Deserialize<TransactionsResponse.Transaction>(l1);
        //          Assert.NotNull(price);
    }

    private void Tss_NewTrx(object sender, StreamEventArgs<TransactionsResponse.Transaction> e)
    {
        Console.WriteLine("Tx1: ");
    }

    /*
    {"lastTransactionID":"2871","time":"2016-10-07T14:05:45.434095856Z","type":"HEARTBEAT"}
    {"lastTransactionID":"2871","time":"2016-10-07T14:05:50.454075397Z","type":"HEARTBEAT"}
    {"lastTransactionID":"2871","time":"2016-10-07T14:05:55.551386493Z","type":"HEARTBEAT"}
    */
}