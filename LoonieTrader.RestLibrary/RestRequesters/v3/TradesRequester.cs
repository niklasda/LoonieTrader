using System;
using System.IO;
using System.Net;
using System.Text;
using Jil;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.RestRequesters.v3
{
    public class TradesRequester : RequesterBase, ITradesRequester
    {
        public TradesRequester(ISettings settings) : base(settings)
        {
        }

        public AccountTradesResponse GetTrades(string accountId)
        {
            string urlAccountOrders = base.GetRestUrl("accounts/{0}/trades");

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.BearerApiKey);

            var responseBytes = wc.DownloadData(string.Format(urlAccountOrders, accountId));

            var responseString = Encoding.UTF8.GetString(responseBytes);

            using (var input = new StringReader(responseString))
            {
                var atr = JSON.Deserialize<AccountTradesResponse>(input);
                return atr;
            }
        }

        public AccountTradesResponse GetOpenTrades(string accountId)
        {
            throw new NotImplementedException();
        }

        public AccountTradesResponse GetTradeDetails(string accountId, string tradeId)
        {
            throw new NotImplementedException();
        }
    }
}