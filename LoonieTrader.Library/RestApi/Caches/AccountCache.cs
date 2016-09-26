using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Caches
{
    public static class AccountCache
    {
        // persist the caches as yaml

        public static AccountsResponse Accounts { get; set; }
    }
}