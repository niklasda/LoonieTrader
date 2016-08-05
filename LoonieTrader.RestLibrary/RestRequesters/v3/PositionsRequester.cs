using System.IO;
using System.Net;
using System.Text;
using Jil;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.RestRequesters.v3
{
    public class PositionsRequester : RequesterBase, IPositionsRequester
    {
        public PositionsRequester(ISettings settings) : base(settings)
        {
        }

        public AccountPositionsResponse GetPositions(string accountId)
        {
            string urlAccountPositions = base.GetRestUrl("accounts/{0}/positions/");

            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("Authorization", base.BearerApiKey);

                var responseBytes = wc.DownloadData(string.Format(urlAccountPositions, accountId));

                var responseString = Encoding.UTF8.GetString(responseBytes);

                using (var input = new StringReader(responseString))
                {
                    var apr = JSON.Deserialize<AccountPositionsResponse>(input);
                    return apr;
                }
            }
        }

        public AccountOpenPositionsResponse GetOpenPositions(string accountId)
        {
            string urlAccountOpenPositions = base.GetRestUrl("accounts/{0}/openPositions/");

            using (WebClient wc = new WebClient())
            {
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

        public AccountInstrumentPositionResponse GetInstrumentPositions(string accountId, string instrument)
        {
            string urlAccountOpenPositions = base.GetRestUrl("accounts/{0}/positions/{1}");

            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("Authorization", base.BearerApiKey);

                var responseBytes = wc.DownloadData(string.Format(urlAccountOpenPositions, accountId, instrument));

                var responseString = Encoding.UTF8.GetString(responseBytes);

                using (var input = new StringReader(responseString))
                {
                    var apr = JSON.Deserialize<AccountInstrumentPositionResponse>(input);
                    return apr;
                }
            }
        }
    }
}