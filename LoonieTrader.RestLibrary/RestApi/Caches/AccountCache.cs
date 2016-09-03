using LoonieTrader.RestLibrary.RestApi.Responses;

namespace LoonieTrader.RestLibrary.RestApi.Caches
{
    public static class AccountCache
    {
        // persist the caches as yaml

        public static AccountsResponse Accounts { get; set; }
    }
}