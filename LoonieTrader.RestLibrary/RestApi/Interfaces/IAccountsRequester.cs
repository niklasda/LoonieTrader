using System.Collections.Generic;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Interfaces
{
    public interface IAccountsRequester
    {
        AccountsResponse GetAccounts();
        AccountDetailsResponse GetAccountDetails(string accountId);
        AccountSummaryResponse GetAccountSummary(string accountId);
        AccountInstrumentsResponse GetInstruments(string accountId);

        IEnumerable<AccountSummaryResponse> GetAccountSummaries();
    }
}