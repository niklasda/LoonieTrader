
using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.Interfaces
{
    public interface ITransactionsRequester
    {
        AccountTransactionPagesResponse GetTransactionPages(string accountId);
        AccountTransactionsResponse GetTransactions(string accountId);
        AccountTransactionDetailsResponse GetTransactionDetails(string accountId, string transactionId);
    }
}