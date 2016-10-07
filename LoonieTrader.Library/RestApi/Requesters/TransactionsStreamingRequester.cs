using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Jil;
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

        public IObservable<string> GetTransactionStream(string accountId)
        {
            string urlTransactionStream = base.GetStreamingRestUrl("accounts/{0}/transactions/stream");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                Stream responseStream = wc.OpenRead(string.Format(urlTransactionStream, accountId));
                var obsStream = new ObservableStream(responseStream);
                return obsStream.GetObservable();
            }
        }

        //private IObservable<string> ObserveLines(Stream inputStream)
        //{
        //    return ReadLines(inputStream).ToObservable(Scheduler.Default);
        //}

        //private IEnumerable<string> ReadLines(Stream stream)
        //{
        //    using (StreamReader reader = new StreamReader(stream))
        //    {
        //        while (!reader.EndOfStream)
        //        {
        //            yield return reader.ReadLine();
        //        }
        //    }
        //}
    }
}