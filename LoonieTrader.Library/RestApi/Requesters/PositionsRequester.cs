using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using LoonieTrader.Library.Extensions;
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
            string urlPositions = GetRestUrl("accounts/{0}/positions/");

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlPositions, accountId);
                //var responseBytes = wc.GetData(string.Format(urlPositions, accountId));
                //var responseString = Encoding.UTF8.GetString(responseBytes);
                SaveLocalJson("positions", accountId, responseString);
           //     using (var input = new StringReader(responseString))
             //   {
                    var apr = JsonSerializer.Deserialize<PositionsResponse>(responseString);
                    return apr;
               // }
            }
        }

        public PositionsOpenResponse GetOpenPositions(string accountId)
        {
            string urlOpenPositions = GetRestUrl("accounts/{0}/openPositions/");

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlOpenPositions, accountId);
                SaveLocalJson("positionsOpen", accountId, responseString);
           //     using (var input = new StringReader(responseString))
             //   {
                    var apr = JsonSerializer.Deserialize<PositionsOpenResponse>(responseString);
                    return apr;
               // }
            }
        }

        public PositionsInstrumentResponse GetInstrumentPositions(string accountId, string instrument)
        {
            string urlInstrumentPositions = GetRestUrl("accounts/{0}/positions/{1}");

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlInstrumentPositions, accountId, instrument);
                SaveLocalJson("positionsInstrument", accountId, instrument, responseString);
        //        using (var input = new StringReader(responseString))
          //      {
                    var apr = JsonSerializer.Deserialize<PositionsInstrumentResponse>(responseString);
                    return apr;
            //    }
            }
        }

        public PositionsCloseResponse PutClosePosition(string accountId, string instrument)
        {
            Logger.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} - {accountId} - {instrument}");

            string urlInstrumentClose = GetRestUrl("accounts/{0}/positions/{1}/close");

            using (var wc = GetAuthenticatedWebClient())
            {
                var parameters = new PositionsCloseResponse.ClosePositionParameters()
                {
                    longUnits = "1",
                    shortUnits = null
                };

                var parametersJson = JsonSerializer.Serialize(parameters, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
                Logger.Debug(parametersJson.PrettyPrintJson());
                var parametersBytes = Encoding.UTF8.GetBytes(parametersJson);

                var responseBytes = wc.UploadData(string.Format(urlInstrumentClose, accountId, instrument), "PUT", parametersBytes);
                var responseString = Encoding.UTF8.GetString(responseBytes);
                Logger.Debug(responseString.PrettyPrintJson());

         //       using (var input = new StringReader(responseString))
           //     {
                    var apr = JsonSerializer.Deserialize<PositionsCloseResponse>(responseString);
                    return apr;
             //   }
            }
        }
    }
}