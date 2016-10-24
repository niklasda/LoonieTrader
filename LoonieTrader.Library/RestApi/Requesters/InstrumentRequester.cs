using System.IO;
using System.Net;
using JetBrains.Annotations;
using Jil;
using LoonieTrader.Library.Interfaces;
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

        public CandlesResponse GetCandles(string instrument)
        {
            string urlPositions = base.GetRestUrl("instruments/{0}/candles/");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlPositions, instrument);
                //var responseBytes = wc.GetData(string.Format(urlPositions, accountId));
                //var responseString = Encoding.UTF8.GetString(responseBytes);
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