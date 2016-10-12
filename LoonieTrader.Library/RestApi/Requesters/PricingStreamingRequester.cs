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
    public class PricingStreamingRequester : RequesterBase, IPricingStreamingRequester
    {
        public PricingStreamingRequester(ISettings settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger)
            : base(settings, fileReaderWriter, logger)
        {
        }

        // todo the stram does not support concurrent reads
        private readonly ConcurrentDictionary<string, ObservableStream<PricesResponse.Price>> _subscriptions = new ConcurrentDictionary<string, ObservableStream<PricesResponse.Price>>();

        public IObservable<PricesResponse.Price> GetPriceStream(string accountId, string instrument)
        {
            if (_subscriptions.ContainsKey(instrument))
            {
                ObservableStream<PricesResponse.Price> obs;
                if (_subscriptions.TryGetValue(instrument, out obs))
                {
                    return obs.GetObservable();
                }
            }

            string urlPricesStream = base.GetStreamingRestUrl("accounts/{0}/pricing/stream?instruments={1}");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                string url = string.Format(urlPricesStream, accountId, instrument);
                var uri = new Uri(url);
                Stream responseStream = wc.OpenRead(uri);
                var obsStream = new ObservableStream<PricesResponse.Price>(responseStream, Logger);
                _subscriptions.TryAdd(instrument, obsStream);
                return obsStream.GetObservable();
            }

        }
    }
}