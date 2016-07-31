using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.Interfaces
{
    public interface IAccountsRequester
    {
        AccountResponse GetAccounts();
        AccountSummaryResponse GetAccountSummary(string accountId);
        InstrumentsResponse GetInstruments(string accountId);

    }
}