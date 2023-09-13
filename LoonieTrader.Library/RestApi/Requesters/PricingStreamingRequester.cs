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
    private readonly ConcurrentDictionary<string, ObservableStream<PricesResponse.Price>> _priceSubscriptions = new ConcurrentDictionary<string, ObservableStream<PricesResponse.Price>>();

    public ObservableStream<PricesResponse.Price> GetPriceStream(string accountId, string instrument)
    {
        if (_priceSubscriptions.ContainsKey(instrument))
        {
//                ObservableStream<PricesResponse.Price> obs;
            if (_priceSubscriptions.TryGetValue(instrument, out var obs))
            {
                return obs;
            }
        }

        string urlPricesStream = GetStreamingRestUrl("accounts/{0}/pricing/stream?instruments={1}");
        var uri = new Uri(string.Format(urlPricesStream, accountId, instrument));

        using var wc = GetAuthenticatedWebClient();
        Stream responseStream = wc.OpenRead(uri);
        var obsStream = new ObservableStream<PricesResponse.Price>(responseStream);
        _priceSubscriptions.TryAdd(instrument, obsStream);
        return obsStream;
    }
}