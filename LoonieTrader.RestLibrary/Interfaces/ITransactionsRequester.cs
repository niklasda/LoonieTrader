
using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.Interfaces
{
    public interface ITransactionsRequester
    {
        AccountTransactionPagesResponse GetTransactionPages(string accountId);
        AccountTransactionsResponse GetTransactions(string accountId);
        AccountTransactionsResponse GetTransactionDetails(string accountId, string transactionId);
    }
}