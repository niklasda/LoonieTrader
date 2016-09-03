using System.Collections.Generic;
using LoonieTrader.RestLibrary.RestApi.Responses;

namespace LoonieTrader.RestLibrary.RestApi.Interfaces
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