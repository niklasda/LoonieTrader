using LoonieTrader.RestLibrary.Responses;

namespace LoonieTrader.RestLibrary.Interfaces
{
    public interface ITransactionsRequester
    {
        AccountTransactionPagesResponse GetTransactionPages(string accountId);
        AccountTransactionsResponse GetTransactions(string accountId);
    }
}