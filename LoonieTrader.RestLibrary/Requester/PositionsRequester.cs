using System;
using System.IO;
using System.Net;
using System.Text;
using Jil;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Responses;

namespace LoonieTrader.RestLibrary.Requester
{
    public class PositionsRequester : RequesterBase, IPositionsRequester
    {
        public PositionsRequester(ISettings settings) : base(settings)
        {
        }

        public AccountPositionsResponse GetPositions(string accountId)
        {
            string urlAccountPositions = base.GetRestUrl("accounts/{0}/positions/");

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.BearerApiKey);

            var responseBytes = wc.DownloadData(string.Format(urlAccountPositions, accountId));

            var responseString = Encoding.UTF8.GetString(responseBytes);

            using (var input = new StringReader(responseString))
            {
                var apr = JSON.Deserialize<AccountPositionsResponse>(input);
                return apr;
            }
        }

        public AccountOpenPositionsResponse GetOpenPositions(string accountId)
        {
            string urlAccountOpenPositions = base.GetRestUrl("accounts/{0}/openPositions/");
            //const string urlAccountOpenPositions = "https://api-fxpractice.oanda.com/v3/accounts/{0}/openPositions/";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.BearerApiKey);

            var responseBytes = wc.DownloadData(string.Format(urlAccountOpenPositions, accountId));

            var responseString = Encoding.UTF8.GetString(responseBytes);

            using (var input = new StringReader(responseString))
            {
                var apr = JSON.Deserialize<AccountOpenPositionsResponse>(input);
                return apr;
            }
        }
    }
}