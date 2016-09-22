using System.IO;
using System.Net;
using System.Text;
using Jil;
using JsonPrettyPrinterPlus;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Requesters
{
    public class PositionsRequester : RequesterBase, IPositionsRequester
    {
        public PositionsRequester(ISettings settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger) : base(settings, fileReaderWriter, logger)
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

        public PositionsCloseResponse PutClosePosition(string accountId, string instrument)
        {
            string urlInstrumentClose = base.GetRestUrl("accounts/{0}/positions/{1}/close");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var parameters = new PositionsCloseResponse.ClosePositionParameters()
                {
                    longUnits = "1",
                    shortUnits = null
                };

                var parametersJson = JSON.Serialize(parameters, new Options(excludeNulls: true));
                base.Logger.Debug(parametersJson.PrettyPrintJson());
                var parametersBytes = Encoding.UTF8.GetBytes(parametersJson);

                var responseBytes = wc.UploadData(string.Format(urlInstrumentClose, accountId, instrument), "PUT", parametersBytes);
                var responseString = Encoding.UTF8.GetString(responseBytes);
                base.Logger.Debug(responseString.PrettyPrintJson());

                using (var input = new StringReader(responseString))
                {
                    var apr = JSON.Deserialize<PositionsCloseResponse>(input);
                    return apr;
                }
            }
        }
    }
}