using Oanda.RestLibrary.Responses;

namespace Oanda.RestLibrary.Interfaces
{
    public interface IOandaRequester
    {
        AccountResponse GetAccounts();
        AccountSummaryResponse GetAccountSummary(string accountId);
        AccountPositionsResponse GetPositions(string accountId);
        AccountOpenPositionsResponse GetOpenPositions(string accountId);
        AccountOrdersResponse GetOrders(string accountId);
        AccountPendingOrdersResponse GetPendingOrders(string accountId);
        AccountTransactionPagesResponse GetTransactionPages(string accountId);
        AccountTransactionsResponse GetTransactions(string accountId);
        AccountTradesResponse GetTrades(string accountId);
    }
}