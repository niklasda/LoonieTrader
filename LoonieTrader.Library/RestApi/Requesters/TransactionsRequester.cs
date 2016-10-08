using System.IO;
using System.Net;
using Jil;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Requesters
{
    public class TransactionsRequester : RequesterBase, ITransactionsRequester
    {
        public TransactionsRequester(ISettings settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger)
            : base(settings, fileReaderWriter, logger)
        {
        }

        public TransactionPagesResponse GetTransactionPages(string accountId)
        {
            string urlTransactionPages = base.GetRestUrl("accounts/{0}/transactions/");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlTransactionPages, accountId);
                //var responseBytes = wc.GetData(string.Format(urlTransactionPages, accountId));
                //var responseString = Encoding.UTF8.GetString(responseBytes);
                base.SaveLocalJson("transactionPages", accountId, responseString);

                using (var input = new StringReader(responseString))
                {
                    var atpr = JSON.Deserialize<TransactionPagesResponse>(input);
                    return atpr;
                }
            }
        }

        public TransactionsResponse GetTransactions(string accountId)
        {
            string urlTransactions = base.GetRestUrl("accounts/{0}/transactions/idrange?from=3200&to=3300");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlTransactions, accountId);
                //var responseBytes = wc.GetData(string.Format(urlTransactions, accountId));
                //var responseString = Encoding.UTF8.GetString(responseBytes);
                base.SaveLocalJson("transactions", accountId, responseString);

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
                var responseString = GetData(wc, urlTransactionDetails, accountId, transactionId);
                //var responseBytes = wc.GetData(string.Format(urlTransactionDetails, accountId, transactionId));
                //var responseString = Encoding.UTF8.GetString(responseBytes);
                base.SaveLocalJson("transactionDetails", accountId, transactionId, responseString);

                using (var input = new StringReader(responseString))
                {
                    var atr = JSON.Deserialize<TransactionDetailsResponse>(input);
                    return atr;
                }
            }
        }

        public TransactionDetailsResponse GetTransactionStream(string accountId)
        {
            // todo might not be available yet
            string urlTransactionStream = base.GetRestUrl("accounts/{0}/transactions/steam/");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlTransactionStream, accountId);
                //var responseBytes = wc.GetData(string.Format(urlTransactionStream, accountId));
                //var responseString = Encoding.UTF8.GetString(responseBytes);

                // todo what will be saved here?
                base.SaveLocalJson("transactionStream", accountId, responseString);

                using (var input = new StringReader(responseString))
                {
                    var atr = JSON.Deserialize<TransactionDetailsResponse>(input);
                    return atr;
                }
            }
        }
    }
}