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

[UsedImplicitly] // Registered as singleton in StructureMap
public class PricingStreamingRequester : RequesterBase, IPricingStreamingRequester
{
    public PricingStreamingRequester(ISettingsService settings, IFileReaderWriterService fileReaderWriter, ILogger logger)
        : base(settings, fileReaderWriter, logger)
    {
    }

    // todo the stream does not support concurrent reads
    private ConcurrentDictionary<string, ObservableStream<PricesResponse.Price>> Subscriptions { get; } = new();

    public ObservableStream<PricesResponse.Price> GetPriceStream(string accountId, string instrument)
    {
        if (Subscriptions.ContainsKey(instrument))
        {
            if (Subscriptions.TryGetValue(instrument, out var obs))
            {
                return obs;
            }
        }

        string urlPricesStream = GetStreamingRestUrl("accounts/{0}/pricing/stream?instruments={1}");
        var uri = new Uri(string.Format(urlPricesStream, accountId, instrument));

        using var wc = GetAuthenticatedWebClient();
        Stream responseStream = wc.OpenRead(uri);
        var obsStream = new ObservableStream<PricesResponse.Price>(responseStream);
        Subscriptions.TryAdd(instrument, obsStream);
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
