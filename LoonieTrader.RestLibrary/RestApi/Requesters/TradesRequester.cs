﻿using System.IO;
using System.Net;
using System.Text;
using Jil;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Requesters
{
    public class TradesRequester : RequesterBase, ITradesRequester
    {
        public TradesRequester(ISettings settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger)
            : base(settings, fileReaderWriter, logger)
        {
        }

        public TradesResponse GetTrades(string accountId)
        {
            // OPEN The Trade is currently open, CLOSED The Trade has been fully closed, CLOSE_WHEN_TRADEABLE The Trade will be closed as soon as the trade’s instrument becomes tradeable
            string urlTrades = base.GetRestUrl("accounts/{0}/trades?state=CLOSED");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseString = DownloadData(wc, urlTrades, accountId);
                //var responseBytes = wc.DownloadData(string.Format(urlTrades, accountId));
                //var responseString = Encoding.UTF8.GetString(responseBytes);
                base.SaveLocalJson("trades", accountId, responseString);

                using (var input = new StringReader(responseString))
                {
                    var atr = JSON.Deserialize<TradesResponse>(input);
                    return atr;
                }
            }
        }
        public TradesResponse GetOpenTrades(string accountId)
        {
            string urlOpenTrades = base.GetRestUrl("accounts/{0}/openTrades");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseString = DownloadData(wc, urlOpenTrades, accountId);
                //var responseBytes = wc.DownloadData(string.Format(urlOpenTrades, accountId));
                //var responseString = Encoding.UTF8.GetString(responseBytes);
                base.SaveLocalJson("tradesOpen", accountId, responseString);

                using (var input = new StringReader(responseString))
                {
                    var atr = JSON.Deserialize<TradesResponse>(input);
                    return atr;
                }
            }
        }

        public TradeDetailsResponse GetTradeDetails(string accountId, string tradeId)
        {
            string urlTradeDetails = base.GetRestUrl("accounts/{0}/trades/{1}");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseString = DownloadData(wc, urlTradeDetails, accountId, tradeId);
                //var responseBytes = wc.DownloadData(string.Format(urlTradeDetails, accountId, tradeId));
                //var responseString = Encoding.UTF8.GetString(responseBytes);
                base.SaveLocalJson("tradeDetails", accountId, tradeId, responseString);

                using (var input = new StringReader(responseString))
                {
                    var atr = JSON.Deserialize<TradeDetailsResponse>(input);
                    return atr;
                }
            }
        }
    }
}