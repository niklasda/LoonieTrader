using System.IO;
using System.Net;
using System.Text;
using Jil;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Requesters
{
    public class PricingRequester : RequesterBase, IPricingRequester
    {
        public PricingRequester(ISettings settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger) 
            : base(settings, fileReaderWriter, logger)
        {
        }

        public PricesResponse GetPrices(string accountId, string instrument)
        {
            string urlPrices = base.GetRestUrl("accounts/{0}/pricing?instruments={1}&since={2}");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseBytes =
                    wc.DownloadData(string.Format(urlPrices, accountId, instrument, "2016-08-05T04:00:00.000000Z"));

                var responseString = Encoding.UTF8.GetString(responseBytes);
                base.SaveLocalJson("prices", accountId, instrument, responseString);
                using (var input = new StringReader(responseString))
                {
                    var pr = JSON.Deserialize<PricesResponse>(input);
                    return pr;
                }
            }
        }
    }
}