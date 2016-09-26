using System.Collections.Generic;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Interfaces
{
    public interface IHealthRequester
    {
        ServiceListResponse GetServiceList();
        ServicesResponse GetServices();
        ServicesResponse.Service GetService(string serviceId);
        StatusesResponse GetStatuses();
        StatusesResponse.Status GetStatus(string statusId);
        ServiceEventsResponse GetServiceEvents(string serviceId);
        ServiceEventsResponse.Event GetServiceEvent(string serviceId, string eventId);
    }
}