﻿using JetBrains.Annotations;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Requesters;

[UsedImplicitly]
public class TradesRequester : RequesterBase, ITradesRequester
{
    public TradesRequester(ISettingsService settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger)
        : base(settings, fileReaderWriter, logger)
    {
    }

    public TradesResponse GetTrades(string accountId)
    {
        // OPEN The Trade is currently open, CLOSED The Trade has been fully closed, CLOSE_WHEN_TRADEABLE The Trade will be closed as soon as the trade’s instrument becomes tradeable
        string urlTrades = GetRestUrl("accounts/{0}/trades?state=CLOSED");

        using (var wc = GetAuthenticatedWebClient())
        {
            var responseString = GetData(wc, urlTrades, accountId);
            SaveLocalJson("trades", accountId, responseString);

            //      using (var input = new StringReader(responseString))
            //    {
            var atr = JsonDeserialize<TradesResponse>(responseString);
            return atr;
            //  }
        }
    }
    public TradesResponse GetOpenTrades(string accountId)
    {
        string urlOpenTrades = GetRestUrl("accounts/{0}/openTrades");

        using (var wc = GetAuthenticatedWebClient())
        {
            var responseString = GetData(wc, urlOpenTrades, accountId);
            SaveLocalJson("tradesOpen", accountId, responseString);

            //      using (var input = new StringReader(responseString))
            //    {
            var atr = JsonDeserialize<TradesResponse>(responseString);
            return atr;
            //  }
        }
    }

    public TradeDetailsResponse GetTradeDetails(string accountId, string tradeId)
    {
        string urlTradeDetails = GetRestUrl("accounts/{0}/trades/{1}");

        using (var wc = GetAuthenticatedWebClient())
        {
            var responseString = GetData(wc, urlTradeDetails, accountId, tradeId);
            SaveLocalJson("tradeDetails", accountId, tradeId, responseString);

            //    using (var input = new StringReader(responseString))
            //  {
            var atr = JsonDeserialize<TradeDetailsResponse>(responseString);
            return atr;
            //}
        }
    }
}