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
        public InstrumentRequester(ISettings settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger)
            : base(settings, fileReaderWriter, logger)
        {
        }

        public CandlesResponse GetCandles(string instrument, CandlestickGranularity granularity = CandlestickGranularity.S10, string priceComponents = "M", int count = 4)
        {
            string urlCandles = base.GetRestUrl("instruments/{0}/candles?granularity={1}&price={2}&count={3}&from={4}");//2014-07-02T04:00:00.000000Z   "2016-10-24T12:17:49.0439507+02:00"

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlCandles, instrument, granularity, priceComponents, count, DateTime.Now.AddHours(-10).ToRfc3339());
                base.SaveLocalJson("candles", instrument, responseString);
                using (var input = new StringReader(responseString))
                {
                    var apr = JSON.Deserialize<CandlesResponse>(input);
                    return apr;
                }
            }
        }
    }
}