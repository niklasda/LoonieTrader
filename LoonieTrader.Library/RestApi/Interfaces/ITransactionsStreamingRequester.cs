using System;

namespace LoonieTrader.Library.RestApi.Interfaces
{
    public interface ITransactionsStreamingRequester
    {
        IObservable<string> GetTransactionStream(string accountId);
    }
}