using System.Collections.Generic;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Interfaces
{
    public interface IAccountsRequester
    {
        AccountsResponse GetAccounts();
        AccountDetailsResponse GetAccountDetails(string accountId);
        AccountSummaryResponse GetAccountSummary(string accountId);

        IEnumerable<AccountSummaryResponse> GetAccountSummaries();
        AccountInstrumentsResponse GetAccountInstruments(string accountId);
        AccountChangesResponse GetAccountChanges(string accountId, string transactionId);
        AccountInstrumentsResponse PatchAccountConfiguration(string accountId);
    }
}