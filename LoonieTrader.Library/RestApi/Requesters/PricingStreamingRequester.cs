using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using Jil;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Requesters
{
    public class PricingStreamingRequester : RequesterBase, IPricingStreamingRequester
    {
        public PricingStreamingRequester(ISettings settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger)
            : base(settings, fileReaderWriter, logger)
        {
        }

        public StreamReader GetPriceStream(string accountId, string instrument)
        {
            string urlPricesStream = base.GetStreamingRestUrl("accounts/{0}/pricing/stream?instruments={1}");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                Stream responseStream = wc.OpenRead(string.Format(urlPricesStream, accountId, instrument));


                    StreamReader sr = new StreamReader(responseStream);
                    //var cnt = sr.ReadLine();
                    return sr;
                    //var responseString = Encoding.UTF8.GetString(responseBytes);
                    //return responseString;

            }
        }
    }
}