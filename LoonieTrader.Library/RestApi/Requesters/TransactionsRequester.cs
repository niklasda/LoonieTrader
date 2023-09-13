using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;
using Serilog;

namespace LoonieTrader.Library.RestApi.Requesters;

[UsedImplicitly]
public class TransactionsRequester : RequesterBase, ITransactionsRequester
{
    public TransactionsRequester(ISettingsService settings, IFileReaderWriterService fileReaderWriter, ILogger logger)
        : base(settings, fileReaderWriter, logger)
    {
    }

    public TransactionPagesResponse GetTransactionPages(string accountId)
    {
        string urlTransactionPages = GetRestUrl("accounts/{0}/transactions");
        //string urlTransactionPages = GetRestUrl("accounts/{0}/transactions?from=2021-08-23T21:12:24.357321399Z");

        using (var wc = GetAuthenticatedWebClient())
        {
            var responseString = GetData(wc, urlTransactionPages, accountId);
            SaveLocalJson("transactionPages", accountId, responseString);

            //        using (var input = new StringReader(responseString))
            //      {
            var atpr = JsonDeserialize<TransactionPagesResponse>(responseString);
            return atpr;
            //    }
        }
    }

    public TransactionsResponse GetTransactions(string accountId, string transactionId)
    {
        if (int.TryParse(transactionId, out int txId))
        {
            string urlTransactions = GetRestUrl($"accounts/{{0}}/transactions/idrange?from={txId - 10}&to={txId}");

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlTransactions, accountId);
                SaveLocalJson("transactions", accountId, responseString);

                //         using (var input = new StringReader(responseString))
                //       {
                var atr = JsonDeserialize<TransactionsResponse>(responseString);
                return atr;
                //     }
            }

        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(transactionId));
        }

    }

    public TransactionsResponse GetAllTransactions(string accountId)
    {
        string urlTransactionPages = GetRestUrl("accounts/{0}/transactions/");

        using (var wc = GetAuthenticatedWebClient())
        {
            var responsePagesString = GetData(wc, urlTransactionPages, accountId);
            //base.SaveLocalJson("transactionPages", accountId, responseString);
            TransactionsResponse responseAll = new TransactionsResponse();
            List<TransactionsResponse.Transaction> transactions = new List<TransactionsResponse.Transaction>();

            //    using (var inputPages = new StringReader(responsePagesString))
            //  {
            var atpr = JsonDeserialize<TransactionPagesResponse>(responsePagesString);
            foreach (var p in atpr.pages)
            {
                var responseString = GetData(wc, p, accountId);
                //           using (var input = new StringReader(responseString))
                //        {
                var atr = JsonDeserialize<TransactionsResponse>(responseString);
                //Console.WriteLine(atr);

                transactions.AddRange(atr.transactions);
                responseAll.lastTransactionID = atr.lastTransactionID;
                //      }
            }
            //}

            responseAll.transactions = transactions.ToArray();
            return responseAll;
        }
    }

    public TransactionDetailsResponse GetTransactionDetails(string accountId, string transactionId)
    {
        string urlTransactionDetails = GetRestUrl("accounts/{0}/transactions/{1}");

        using (var wc = GetAuthenticatedWebClient())
        {
            var responseString = GetData(wc, urlTransactionDetails, accountId, transactionId);
            SaveLocalJson("transactionDetails", accountId, transactionId, responseString);

            //      using (var input = new StringReader(responseString))
            //    {
            var atr = JsonDeserialize<TransactionDetailsResponse>(responseString);
            return atr;
            //  }
        }
    }

}