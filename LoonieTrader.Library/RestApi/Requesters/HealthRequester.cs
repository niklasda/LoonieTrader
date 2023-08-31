using System.Text.Json;
using JetBrains.Annotations;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Requesters
{
    [UsedImplicitly]
    public class HealthRequester : RequesterBase, IHealthRequester
    {
        public HealthRequester(ISettingsService settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger)
            : base(settings, fileReaderWriter, logger)
        {
        }

        public ServiceListResponse GetServiceList()
        {
            string urlServiceList = GetHttpRestUrl("service-lists");

            using (var wc = GetAnonymousWebClient())
            {
                var responseString = GetData(wc, urlServiceList);
                SaveLocalJson("service-list", "all", responseString);
          //      using (var input = new StringReader(responseString))
            //    {
                    var slr = JsonSerializer.Deserialize<ServiceListResponse>(responseString);
                    return slr;
              //  }

            }
        }

        public ServicesResponse GetServices()
        {
            string urlServices = GetHttpRestUrl("services");

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlServices);
                SaveLocalJson("services", "all", responseString);
          //      using (var input = new StringReader(responseString))
            //    {
                    var sr = JsonSerializer.Deserialize<ServicesResponse>(responseString);
                    return sr;
              //  }
            }
        }

        public ServicesResponse.Service GetService(string serviceId)
        {
            string urlService = GetHttpRestUrl("services/{0}");

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlService, serviceId);
                SaveLocalJson("services", serviceId, responseString);
             //   using (var input = new StringReader(responseString))
               // {
                    var srs = JsonSerializer.Deserialize<ServicesResponse.Service>(responseString);
                    return srs;
                //}
            }
        }

        public StatusesResponse GetStatuses()
        {
            string urlStatuses = GetHttpRestUrl("statuses");

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlStatuses);
                SaveLocalJson("statuses", "all", responseString);
           //     using (var input = new StringReader(responseString))
             //   {
                    var sr = JsonSerializer.Deserialize<StatusesResponse>(responseString);
                    return sr;
               // }
            }
        }

        public StatusesResponse.Status GetStatus(string statusId)
        {
            string urlStatus = GetHttpRestUrl("statuses/{0}");

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlStatus, statusId);
                SaveLocalJson("statuses", statusId, responseString);
            //    using (var input = new StringReader(responseString))
              //  {
                    var srs = JsonSerializer.Deserialize<StatusesResponse.Status>(responseString);
                    return srs;
                //}
            }
        }

        public ServiceEventsResponse GetServiceEvents(string serviceId)
        {
            string urlService = GetHttpRestUrl("services/{0}/events");

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlService, serviceId);
                SaveLocalJson("serviceEvents", serviceId, responseString);
           //     using (var input = new StringReader(responseString))
             //   {
                    var ser = JsonSerializer.Deserialize<ServiceEventsResponse>(responseString);
                    return ser;
               // }
            }
        }

        public ServiceEventsResponse.Event GetServiceEvent(string serviceId, string eventId)
        {
            string urlService = GetHttpRestUrl("services/{0}/events/{1}");

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlService, serviceId, eventId);
                SaveLocalJson("serviceEvent", serviceId, eventId, responseString);
        //        using (var input = new StringReader(responseString))
          //      {
                    var ser = JsonSerializer.Deserialize<ServiceEventsResponse.Event>(responseString);
                    return ser;
            //    }
            }
        }
    }
}