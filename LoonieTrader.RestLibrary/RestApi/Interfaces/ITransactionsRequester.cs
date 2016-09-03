using LoonieTrader.RestLibrary.RestApi.Responses;

namespace LoonieTrader.RestLibrary.RestApi.Interfaces
{
    public interface ITransactionsRequester
    {
        TransactionPagesResponse GetTransactionPages(string accountId);
        TransactionsResponse GetTransactions(string accountId);
        TransactionDetailsResponse GetTransactionDetails(string accountId, string transactionId);
    }
}