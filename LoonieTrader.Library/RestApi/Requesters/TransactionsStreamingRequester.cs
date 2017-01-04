using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using JetBrains.Annotations;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Models;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Requesters
{
    [UsedImplicitly]
    public class TransactionsStreamingRequester : RequesterBase, ITransactionsStreamingRequester
    {
        public TransactionsStreamingRequester(ISettingsService settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger)
            : base(settings, fileReaderWriter, logger)
        {
        }

        //private ConcurrentDictionary<string, string> _transactionSubscriptions = new ConcurrentDictionary<string, string>();
        private readonly ConcurrentDictionary<string, ObservableStream<TransactionsResponse.Transaction>> _transactionSubscriptions = new ConcurrentDictionary<string, ObservableStream<TransactionsResponse.Transaction>>();

        public ObservableStream<TransactionsResponse.Transaction> GetTransactionStream(string accountId)
        {
            if (_transactionSubscriptions.ContainsKey(accountId))
            {
                ObservableStream<TransactionsResponse.Transaction> obs;
                if (_transactionSubscriptions.TryGetValue(accountId, out obs))
                {
                    return obs;
                }
            }

            string urlTransactionStream = base.GetStreamingRestUrl("accounts/{0}/transactions/stream");
            var uri = new Uri(string.Format(urlTransactionStream, accountId));

            using (var wc = GetAuthenticatedWebClient())
            {
                Stream responseStream = wc.OpenRead(uri);
                var obsStream = new ObservableStream<TransactionsResponse.Transaction>(responseStream, Logger);
                _transactionSubscriptions.TryAdd(accountId, obsStream);
                return obsStream;
            }
        }
    }
}