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

        public ServiceListResponse GetServiceList()
        {
            string urlServiceList = base.GetHttpRestUrl("service-lists");

            using (WebClient wc = GetAnonymousWebClient())
            {
                var responseString = GetData(wc, urlServiceList);
                base.SaveLocalJson("service-list", "all", responseString);
                using (var input = new StringReader(responseString))
                {
                    var slr = JSON.Deserialize<ServiceListResponse>(input);
                    return slr;
                }

            }
        }

        public ServicesResponse GetServices()
        {
            string urlServices = base.GetHttpRestUrl("services");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlServices);
                base.SaveLocalJson("services", "all", responseString);
                using (var input = new StringReader(responseString))
                {
                    var sr = JSON.Deserialize<ServicesResponse>(input);
                    return sr;
                }
            }
        }

        public ServicesResponse.Service GetService(string serviceId)
        {
            string urlService = base.GetHttpRestUrl("services/{0}");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlService, serviceId);
                base.SaveLocalJson("services", serviceId, responseString);
                using (var input = new StringReader(responseString))
                {
                    var srs = JSON.Deserialize<ServicesResponse.Service>(input);
                    return srs;
                }
            }
        }

        public StatusesResponse GetStatuses()
        {
            string urlStatuses = base.GetHttpRestUrl("statuses");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlStatuses);
                base.SaveLocalJson("statuses", "all", responseString);
                using (var input = new StringReader(responseString))
                {
                    var sr = JSON.Deserialize<StatusesResponse>(input);
                    return sr;
                }
            }
        }

        public StatusesResponse.Status GetStatus(string statusId)
        {
            string urlStatus = base.GetHttpRestUrl("statuses/{0}");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlStatus, statusId);
                base.SaveLocalJson("statuses", statusId, responseString);
                using (var input = new StringReader(responseString))
                {
                    var srs = JSON.Deserialize<StatusesResponse.Status>(input);
                    return srs;
                }
            }
        }

        public ServiceEventsResponse GetServiceEvents(string serviceId)
        {
            string urlService = base.GetHttpRestUrl("services/{0}/events");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlService, serviceId);
                base.SaveLocalJson("serviceEvents", serviceId, responseString);
                using (var input = new StringReader(responseString))
                {
                    var ser = JSON.Deserialize<ServiceEventsResponse>(input);
                    return ser;
                }
            }
        }

        public ServiceEventsResponse.Event GetServiceEvent(string serviceId, string eventId)
        {
            string urlService = base.GetHttpRestUrl("services/{0}/events/{1}");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlService, serviceId, eventId);
                base.SaveLocalJson("serviceEvent", serviceId, eventId, responseString);
                using (var input = new StringReader(responseString))
                {
                    var ser = JSON.Deserialize<ServiceEventsResponse.Event>(input);
                    return ser;
                }
            }
        }
    }
}