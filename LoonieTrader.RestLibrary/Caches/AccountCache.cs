using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.Caches
{
    public static class AccountCache
    {
        // persist the caches as yaml

        public static AccountsResponse Accounts { get; set; }
    }
}