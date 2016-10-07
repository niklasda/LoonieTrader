using System;
using System.IO;
using System.Net;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Models;
using LoonieTrader.Library.RestApi.Interfaces;

namespace LoonieTrader.Library.RestApi.Requesters
{
    public class PricingStreamingRequester : RequesterBase, IPricingStreamingRequester
    {
        public PricingStreamingRequester(ISettings settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger)
            : base(settings, fileReaderWriter, logger)
        {
        }

        public IObservable<string> GetPriceStream(string accountId, string instrument)
        {
            string urlPricesStream = base.GetStreamingRestUrl("accounts/{0}/pricing/stream?instruments={1}");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                Stream responseStream = wc.OpenRead(string.Format(urlPricesStream, accountId, instrument));
                var obsStream = new ObservableStream(responseStream);
                return obsStream.GetObservable();
            }
        }
    }
}