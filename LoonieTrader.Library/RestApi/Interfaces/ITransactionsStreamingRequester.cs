using System.IO;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Interfaces
{
    public interface ITransactionsStreamingRequester
    {
        StreamReader GetTransactionStream(string accountId);
    }
}