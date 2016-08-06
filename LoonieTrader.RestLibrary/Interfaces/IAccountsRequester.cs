using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.Interfaces
{
    public interface IAccountsRequester
    {
        AccountsResponse GetAccounts();
        AccountDetailsResponse GetAccountDetails(string accountId);
        AccountSummaryResponse GetAccountSummary(string accountId);
        AccountInstrumentsResponse GetInstruments(string accountId);

    }
}