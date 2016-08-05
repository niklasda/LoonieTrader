using System;
using System.IO;
using System.Net;
using System.Text;
using Jil;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.RestRequesters
{
    public class TradesRequester : RequesterBase, ITradesRequester
    {
        public TradesRequester(ISettings settings) : base(settings)
        {
        }

        public AccountTradesResponse GetTrades(string accountId)
        {
            // OPEN The Trade is currently open, CLOSED The Trade has been fully closed, CLOSE_WHEN_TRADEABLE The Trade will be closed as soon as the trade’s instrument becomes tradeable
            string urlAccountOrders = base.GetRestUrl("accounts/{0}/trades?state=CLOSED");

            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("Authorization", base.BearerApiKey);

                var responseBytes = wc.DownloadData(string.Format(urlAccountOrders, accountId));

                var responseString = Encoding.UTF8.GetString(responseBytes);

                using (var input = new StringReader(responseString))
                {
                    var atr = JSON.Deserialize<AccountTradesResponse>(input);
                    return atr;
                }
            }
        }
        public AccountTradesResponse GetOpenTrades(string accountId)
        {
            string urlAccountOrders = base.GetRestUrl("accounts/{0}/openTrades");

            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("Authorization", base.BearerApiKey);

                var responseBytes = wc.DownloadData(string.Format(urlAccountOrders, accountId));

                var responseString = Encoding.UTF8.GetString(responseBytes);

                using (var input = new StringReader(responseString))
                {
                    var atr = JSON.Deserialize<AccountTradesResponse>(input);
                    return atr;
                }
            }
        }

        public AccountTradeDetailsResponse GetTradeDetails(string accountId, string tradeId)
        {
            string urlAccountOrders = base.GetRestUrl("accounts/{0}/trades/{1}");

            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("Authorization", base.BearerApiKey);

                var responseBytes = wc.DownloadData(string.Format(urlAccountOrders, accountId, tradeId));

                var responseString = Encoding.UTF8.GetString(responseBytes);

                using (var input = new StringReader(responseString))
                {
                    var atr = JSON.Deserialize<AccountTradeDetailsResponse>(input);
                    return atr;
                }
            }
        }
    }
}