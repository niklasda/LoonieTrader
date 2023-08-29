using System.IO;
using System.Text.Json;
using JetBrains.Annotations;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Requesters
{
    [UsedImplicitly]
    public class TradesRequester : RequesterBase, ITradesRequester
    {
        public TradesRequester(ISettingsService settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger)
            : base(settings, fileReaderWriter, logger)
        {
        }

        public TradesResponse GetTrades(string accountId)
        {
            // OPEN The Trade is currently open, CLOSED The Trade has been fully closed, CLOSE_WHEN_TRADEABLE The Trade will be closed as soon as the trade’s instrument becomes tradeable
            string urlTrades = base.GetRestUrl("accounts/{0}/trades?state=CLOSED");

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlTrades, accountId);
                base.SaveLocalJson("trades", accountId, responseString);

          //      using (var input = new StringReader(responseString))
            //    {
                    var atr = JsonSerializer.Deserialize<TradesResponse>(responseString);
                    return atr;
              //  }
            }
        }
        public TradesResponse GetOpenTrades(string accountId)
        {
            string urlOpenTrades = base.GetRestUrl("accounts/{0}/openTrades");

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlOpenTrades, accountId);
                base.SaveLocalJson("tradesOpen", accountId, responseString);

          //      using (var input = new StringReader(responseString))
            //    {
                    var atr = JsonSerializer.Deserialize<TradesResponse>(responseString);
                    return atr;
              //  }
            }
        }

        public TradeDetailsResponse GetTradeDetails(string accountId, string tradeId)
        {
            string urlTradeDetails = base.GetRestUrl("accounts/{0}/trades/{1}");

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlTradeDetails, accountId, tradeId);
                base.SaveLocalJson("tradeDetails", accountId, tradeId, responseString);

            //    using (var input = new StringReader(responseString))
              //  {
                    var atr = JsonSerializer.Deserialize<TradeDetailsResponse>(responseString);
                    return atr;
                //}
            }
        }
    }
}