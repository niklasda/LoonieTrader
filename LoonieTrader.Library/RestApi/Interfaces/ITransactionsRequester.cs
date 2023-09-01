using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Interfaces
{
    public interface ITransactionsRequester
    {
        TransactionPagesResponse GetTransactionPages(string accountId);
        TransactionsResponse GetTransactions(string accountId, string transactionId);
        TransactionsResponse GetAllTransactions(string accountId);
        TransactionDetailsResponse GetTransactionDetails(string accountId, string transactionId);
    }
}