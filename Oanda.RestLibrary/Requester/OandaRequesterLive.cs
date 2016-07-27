using System;
using System.IO;
using System.Net;
using System.Text;
using Jil;
using Oanda.RestLibrary.Interfaces;
using Oanda.RestLibrary.Responses;

namespace Oanda.RestLibrary.Requester
{
    public class OandaRequesterLive : RequesterBase, IOandaRequesterLive
    {
        public PricesResponse GetPrices(string instrument)
        {
            const string urlPrices = "https://api-fxpractice.oanda.com/v3/prices?instruments={0}";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.ApiKey);

            var responseBytes = wc.DownloadData(string.Format(urlPrices, instrument));

            var responseString = Encoding.UTF8.GetString(responseBytes);
            Console.WriteLine(responseString);

            using (var input = new StringReader(responseString))
            {
                var pr = JSON.Deserialize<PricesResponse>(input);
                return pr;
//                Console.WriteLine(pr.ToString());
            }
        }

        public PricesResponse GetCandles(string instrument)
        {
            const string urlPrices = "https://api-fxpractice.oanda.com/v3/candles?instruments={0}&count=2&candleFormat=midpoint";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.ApiKey);

            var responseBytes = wc.DownloadData(string.Format(urlPrices, instrument));

            var responseString = Encoding.UTF8.GetString(responseBytes);
            Console.WriteLine(responseString);

            using (var input = new StringReader(responseString))
            {
                var pr = JSON.Deserialize<PricesResponse>(input);
                return pr;
  //              Console.WriteLine(pr.ToString());
            }
        }

        public PricesResponse GetInstruments(string accountId)
        {
            const string urlPrices = "https://api-fxtrade.oanda.com/v3/instruments?accountId={0}&instruments=AUD_CAD";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.ApiKey);

            //string instruments = "EUR_USD";
            var responseBytes = wc.DownloadData(string.Format(urlPrices, accountId));

            var responseString = Encoding.UTF8.GetString(responseBytes);
            Console.WriteLine(responseString);

            using (var input = new StringReader(responseString))
            {
                var pr = JSON.Deserialize<PricesResponse>(input);
                return pr;
    //            Console.WriteLine(pr.ToString());
            }
        }
    }
}