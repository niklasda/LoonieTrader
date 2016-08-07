using System.IO;
using System.Net;
using System.Text;
using Jil;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Models.Responses;
using Serilog;

namespace LoonieTrader.RestLibrary.RestRequesters
{
    public class PositionsRequester : RequesterBase, IPositionsRequester
    {
        public PositionsRequester(ISettings settings, IFileReaderWriter fileReaderWriter, ILogger logger) : base(settings, fileReaderWriter, logger)
        {
        }

        public PositionsResponse GetPositions(string accountId)
        {
            string urlPositions = base.GetRestUrl("accounts/{0}/positions/");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseBytes = wc.DownloadData(string.Format(urlPositions, accountId));
                var responseString = Encoding.UTF8.GetString(responseBytes);
                base.SaveLocalJson("positions", accountId, responseString);
                using (var input = new StringReader(responseString))
                {
                    var apr = JSON.Deserialize<PositionsResponse>(input);
                    return apr;
                }
            }
        }

        public PositionsOpenResponse GetOpenPositions(string accountId)
        {
            string urlOpenPositions = base.GetRestUrl("accounts/{0}/openPositions/");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseBytes = wc.DownloadData(string.Format(urlOpenPositions, accountId));
                var responseString = Encoding.UTF8.GetString(responseBytes);
                base.SaveLocalJson("positionsOpen", accountId, responseString);
                using (var input = new StringReader(responseString))
                {
                    var apr = JSON.Deserialize<PositionsOpenResponse>(input);
                    return apr;
                }
            }
        }

        public PositionsInstrumentResponse GetInstrumentPositions(string accountId, string instrument)
        {
            string urlInstrumentPositions = base.GetRestUrl("accounts/{0}/positions/{1}");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseBytes = wc.DownloadData(string.Format(urlInstrumentPositions, accountId, instrument));
                var responseString = Encoding.UTF8.GetString(responseBytes);
                base.SaveLocalJson("positionsInstrument", accountId, instrument, responseString);
                using (var input = new StringReader(responseString))
                {
                    var apr = JSON.Deserialize<PositionsInstrumentResponse>(input);
                    return apr;
                }
            }
        }
    }
}