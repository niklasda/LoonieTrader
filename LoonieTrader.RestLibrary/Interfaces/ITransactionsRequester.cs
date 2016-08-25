using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.Interfaces
{
    public interface ITransactionsRequester
    {
        TransactionPagesResponse GetTransactionPages(string accountId);
        TransactionsResponse GetTransactions(string accountId);
        TransactionDetailsResponse GetTransactionDetails(string accountId, string transactionId);
    }
}