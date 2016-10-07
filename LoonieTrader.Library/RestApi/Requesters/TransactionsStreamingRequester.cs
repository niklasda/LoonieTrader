using System.IO;
using System.Net;
using Jil;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Requesters
{
    public class TransactionsStreamingRequester : RequesterBase, ITransactionsStreamingRequester
    {
        public TransactionsStreamingRequester(ISettings settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger)
            : base(settings, fileReaderWriter, logger)
        {
        }

        public StreamReader GetTransactionStream(string accountId)
        {
            string urlTransactionStream = base.GetStreamingRestUrl("accounts/{0}/transactions/stream");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                //var responseString = DownloadData(wc, urlTransactionStream, accountId);
                Stream responseStream = wc.OpenRead(string.Format(urlTransactionStream, accountId));
                //{

                    StreamReader sr = new StreamReader(responseStream);
                    //var cnt = sr.ReadLine();
                    return sr;
                    //var responseString = Encoding.UTF8.GetString(responseBytes);
                    //return responseString;
                //}
            }
        }
    }
}