using System;
using System.IO;
using System.Net;
using JetBrains.Annotations;
using Jil;
using LoonieTrader.Library.Extensions;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Enums;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Requesters
{
    [UsedImplicitly]
    public class InstrumentRequester : RequesterBase, IInstrumentRequester
    {
        public InstrumentRequester(ISettingsService settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger)
            : base(settings, fileReaderWriter, logger)
        {
        }

        /*
         priceComponents: M=MID A=ASK B=BID
         */
        public CandlesResponse GetCandles(string instrument, CandlestickGranularity granularity = CandlestickGranularity.S10, string priceComponents = "M", int count = 4)
        {
            string urlCandles = base.GetRestUrl("instruments/{0}/candles?granularity={1}&price={2}&count={3}");
            var url = string.Format(urlCandles, instrument, granularity, priceComponents, count);

            return GetCandlesInternal(url, $"{instrument}_{granularity}");

            /*
            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlCandles, instrument, granularity, priceComponents, count, DateTime.Now.AddHours(-10).ToRfc3339());
                base.SaveLocalJson("candles", instrument, responseString);
                using (var input = new StringReader(responseString))
                {
                    var apr = JSON.Deserialize<CandlesResponse>(input);
                    return apr;
                }
            }*/
        }

        public CandlesResponse GetCandles(string instrument, DateTime startDate, CandlestickGranularity granularity = CandlestickGranularity.S10, string priceComponents = "M", int count = 4)
        {
            string urlCandles = base.GetRestUrl("instruments/{0}/candles?granularity={1}&price={2}&count={3}&from={4}");
            var url = string.Format(urlCandles, instrument, granularity, priceComponents, count, DateTime.Now.AddHours(-10).ToRfc3339());

            return GetCandlesInternal(url, $"{instrument}_{granularity}");
        }

        private CandlesResponse GetCandlesInternal(string url, string tag)
        {
             using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, url);
                base.SaveLocalJson("candles", tag, responseString);
                using (var input = new StringReader(responseString))
                {
                    var apr = JSON.Deserialize<CandlesResponse>(input);
                    return apr;
                }
            }
        }
    }
}