using System.IO;
using System.Text;
using JetBrains.Annotations;
using Jil;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Requesters
{
    [UsedImplicitly]
    public class PricingRequester : RequesterBase, IPricingRequester
    {
        public PricingRequester(ISettingsService settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger)
            : base(settings, fileReaderWriter, logger)
        {
        }

        public PricesResponse GetPrices(string accountId, string instrument)
        {
            var responseString = GetPricesJson(accountId, instrument);
            base.SaveLocalJson("prices", accountId, instrument, responseString);
            using (var input = new StringReader(responseString))
            {
                var pr = JSON.Deserialize<PricesResponse>(input);
                return pr;
            }
        }

        public string GetPricesJson(string accountId, string instrument)
        {
            string urlPrices = base.GetRestUrl("accounts/{0}/pricing?instruments={1}&since={2}");

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseBytes = wc.DownloadData(string.Format(urlPrices, accountId, instrument, "2016-08-05T04:00:00.000000Z"));

                var responseString = Encoding.UTF8.GetString(responseBytes);
                return responseString;
            }
        }
    }
}