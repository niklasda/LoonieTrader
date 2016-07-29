using System;
using System.IO;
using System.Net;
using System.Text;
using Jil;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Responses;

namespace LoonieTrader.RestLibrary.Requester
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
    }
}