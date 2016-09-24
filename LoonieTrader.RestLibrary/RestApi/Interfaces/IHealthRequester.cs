using System.Collections.Generic;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Interfaces
{
    public interface IHealthRequester
    {
        AccountsResponse GetServiceList();
        AccountsResponse GetServices();
        AccountsResponse GetService(string serviceId);
        AccountsResponse GetStatuses();
        AccountsResponse GetStatus(string statusId);
    }
}