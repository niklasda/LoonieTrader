using System.IO;
using System.Net;
using System.Text;
using Jil;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.RestRequesters.v3
{
    public class PricingRequester : RequesterBase, IPricingRequester
    {
        public PricingRequester(ISettings settings) : base(settings)
        {
        }

        public PricesResponse GetPrices(string instrument)
        {
            string urlPrices = base.GetRestUrl("accounts/{0}/pricing?instruments=EUR_USD%2CUSD_CAD");

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.BearerApiKey);

            var responseBytes = wc.DownloadData(string.Format(urlPrices, instrument));

            var responseString = Encoding.UTF8.GetString(responseBytes);
            //Console.WriteLine(responseString);

            using (var input = new StringReader(responseString))
            {
                var pr = JSON.Deserialize<PricesResponse>(input);
                return pr;
            }
        }
    }
}