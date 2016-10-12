using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Models;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Requesters
{
    public class TransactionsStreamingRequester : RequesterBase, ITransactionsStreamingRequester
    {
        public TransactionsStreamingRequester(ISettings settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger)
            : base(settings, fileReaderWriter, logger)
        {
        }

        private ConcurrentDictionary<string, string> _subscriptions = new ConcurrentDictionary<string, string>();

        public IObservable<TransactionsResponse.Transaction> GetTransactionStream(string accountId)
        {
            string urlTransactionStream = base.GetStreamingRestUrl("accounts/{0}/transactions/stream");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                string url = string.Format(urlTransactionStream, accountId);
                var uri = new Uri(url);
                Stream responseStream = wc.OpenRead(uri);
                var obsStream = new ObservableStream<TransactionsResponse.Transaction>(responseStream, Logger);
                return obsStream.GetObservable();
            }
        }

    }
}