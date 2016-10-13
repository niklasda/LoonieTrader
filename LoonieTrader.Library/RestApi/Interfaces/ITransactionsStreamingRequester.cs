using System;
using LoonieTrader.Library.Models;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Interfaces
{
    public interface ITransactionsStreamingRequester
    {
        ObservableStream<TransactionsResponse.Transaction> GetTransactionStream(string accountId);
    }
}