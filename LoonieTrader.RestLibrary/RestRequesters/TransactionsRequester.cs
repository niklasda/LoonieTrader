using System;
using System.IO;
using System.Net;
using System.Text;
using Jil;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.RestRequesters
{
    public class TransactionsRequester : RequesterBase, ITransactionsRequester
    {
        public TransactionsRequester(ISettings settings) : base(settings)
        {
        }

        public TransactionPagesResponse GetTransactionPages(string accountId)
        {
            string urlTransactionPages = base.GetRestUrl("accounts/{0}/transactions/");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
         //       wc.Headers.Add("Authorization", base.BearerApiKey);

                var responseBytes = wc.DownloadData(string.Format(urlTransactionPages, accountId));

                var responseString = Encoding.UTF8.GetString(responseBytes);

                using (var input = new StringReader(responseString))
                {
                    var atpr = JSON.Deserialize<TransactionPagesResponse>(input);
                    return atpr;
                }
            }
        }

        public TransactionsResponse GetTransactions(string accountId)
        {
            string urlTransactions = base.GetRestUrl("accounts/{0}/transactions/idrange?from=1&to=19");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
            //    wc.Headers.Add("Authorization", base.BearerApiKey);

                var responseBytes = wc.DownloadData(string.Format(urlTransactions, accountId));

                var responseString = Encoding.UTF8.GetString(responseBytes);

                using (var input = new StringReader(responseString))
                {
                    var atr = JSON.Deserialize<TransactionsResponse>(input);
                    return atr;
                }
            }
        }

        public TransactionDetailsResponse GetTransactionDetails(string accountId, string transactionId)
        {
            string urlTransactionDetails = base.GetRestUrl("accounts/{0}/transactions/{1}");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
            //    wc.Headers.Add("Authorization", base.BearerApiKey);

                var responseBytes = wc.DownloadData(string.Format(urlTransactionDetails, accountId, transactionId));

                var responseString = Encoding.UTF8.GetString(responseBytes);

                using (var input = new StringReader(responseString))
                {
                    var atr = JSON.Deserialize<TransactionDetailsResponse>(input);
                    return atr;
                }
            }
        }
    }
}