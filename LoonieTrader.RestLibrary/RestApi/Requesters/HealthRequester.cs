using System.IO;
using System.Net;
using Jil;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Requesters
{
    public class HealthRequester : RequesterBase, IHealthRequester
    {
        public HealthRequester(ISettings settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger)
            : base(settings, fileReaderWriter, logger)
        {
        }

        public AccountsResponse GetServiceList()
        {
            string urlServiceList = base.GetHttpRestUrl("service-lists");

            using (WebClient wc = GetAnonymousWebClient())
            {
                var responseString = DownloadData(wc, urlServiceList);
                base.SaveLocalJson("service-list", "all", responseString);
                using (var input = new StringReader(responseString))
                {
                    var ar = JSON.Deserialize<AccountsResponse>(input);
                    return ar;
                }

            }
        }

        public AccountsResponse GetServices()
        {
            string urlServices = base.GetHttpRestUrl("services");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseString = DownloadData(wc, urlServices);
                base.SaveLocalJson("services", "all", responseString);
                using (var input = new StringReader(responseString))
                {
                    var ar = JSON.Deserialize<AccountsResponse>(input);
                    return ar;
                }
            }
        }

        public AccountsResponse GetService(string serviceId)
        {
            string urlService = base.GetHttpRestUrl("services/{0}");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseString = DownloadData(wc, urlService, serviceId);
                base.SaveLocalJson("services", serviceId, responseString);
                using (var input = new StringReader(responseString))
                {
                    var ar = JSON.Deserialize<AccountsResponse>(input);
                    return ar;
                }
            }
        }

        public AccountsResponse GetStatuses()
        {
            string urlStatuses = base.GetHttpRestUrl("statuses");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseString = DownloadData(wc, urlStatuses);
                base.SaveLocalJson("statuses", "all", responseString);
                using (var input = new StringReader(responseString))
                {
                    var ar = JSON.Deserialize<AccountsResponse>(input);
                    return ar;
                }
            }
        }

        public AccountsResponse GetStatus(string statusId)
        {
            string urlStatus = base.GetHttpRestUrl("statuses/{0}");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseString = DownloadData(wc, urlStatus, statusId);
                base.SaveLocalJson("statuses", statusId, responseString);
                using (var input = new StringReader(responseString))
                {
                    var ar = JSON.Deserialize<AccountsResponse>(input);
                    return ar;
                }
            }
        }

    }
}