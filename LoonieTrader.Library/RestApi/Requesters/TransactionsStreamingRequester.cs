using System;
using System.Collections.Concurrent;
using System.IO;
using JetBrains.Annotations;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Models;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;
using Serilog;

namespace LoonieTrader.Library.RestApi.Requesters;

[UsedImplicitly]
public class TransactionsStreamingRequester : RequesterBase, ITransactionsStreamingRequester
{
    public TransactionsStreamingRequester(ISettingsService settings, IFileReaderWriterService fileReaderWriter, ILogger logger)
        : base(settings, fileReaderWriter, logger)
    {
    }

    private ConcurrentDictionary<string, ObservableStream<TransactionsResponse.Transaction>> Subscriptions { get; } = new();

    public ObservableStream<TransactionsResponse.Transaction> GetTransactionStream(string accountId)
    {
        if (Subscriptions.ContainsKey(accountId))
        {
            if (Subscriptions.TryGetValue(accountId, out var obs))
            {
                return obs;
            }
        }

        string urlTransactionStream = GetStreamingRestUrl("accounts/{0}/transactions/stream");
        var uri = new Uri(string.Format(urlTransactionStream, accountId));

        using var wc = GetAuthenticatedWebClient();
        Stream responseStream = wc.OpenRead(uri);
        var obsStream = new ObservableStream<TransactionsResponse.Transaction>(responseStream);
        Subscriptions.TryAdd(accountId, obsStream);
        return obsStream;
    }

    public void Unsubscribe()
    {
        foreach (var kvp in Subscriptions)
        {
            kvp.Value.Unsubscribe();
        }

        Subscriptions.Clear();
    }
}
