using System;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Interfaces
{
    public interface ITransactionsStreamingRequester
    {
        IObservable<TransactionsResponse.Transaction> GetTransactionStream(string accountId);
    }
}