using System.IO;
using System.Net;
using System.Text;
using JetBrains.Annotations;
using Jil;
using JsonPrettyPrinterPlus;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Requesters
{
    [UsedImplicitly]
    public class PositionsRequester : RequesterBase, IPositionsRequester
    {
        public PositionsRequester(ISettingsService settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger) 
            : base(settings, fileReaderWriter, logger)
        {
        }

        public PositionsResponse GetPositions(string accountId)
        {
            string urlPositions = base.GetRestUrl("accounts/{0}/positions/");

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlPositions, accountId);
                //var responseBytes = wc.GetData(string.Format(urlPositions, accountId));
                //var responseString = Encoding.UTF8.GetString(responseBytes);
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

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlOpenPositions, accountId);
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

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlInstrumentPositions, accountId, instrument);
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

            using (var wc = GetAuthenticatedWebClient())
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