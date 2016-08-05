using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.Interfaces
{
    public interface IAccountsRequester
    {
        AccountResponse GetAccounts();
        AccountDetailsResponse GetAccountDetails(string accountId);
        AccountSummaryResponse GetAccountSummary(string accountId);
        InstrumentsResponse GetInstruments(string accountId);

    }
}