﻿using System;
using System.IO;
using System.Net;
using System.Text;
using Jil;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.RestRequesters
{
    public class TradesRequester : RequesterBase, ITradesRequester
    {
        public TradesRequester(ISettings settings) : base(settings)
        {
        }

        public TradesResponse GetTrades(string accountId)
        {
            // OPEN The Trade is currently open, CLOSED The Trade has been fully closed, CLOSE_WHEN_TRADEABLE The Trade will be closed as soon as the trade’s instrument becomes tradeable
            string urlTrades = base.GetRestUrl("accounts/{0}/trades?state=CLOSED");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
             //   wc.Headers.Add("Authorization", base.BearerApiKey);

                var responseBytes = wc.DownloadData(string.Format(urlTrades, accountId));

                var responseString = Encoding.UTF8.GetString(responseBytes);

                using (var input = new StringReader(responseString))
                {
                    var atr = JSON.Deserialize<TradesResponse>(input);
                    return atr;
                }
            }
        }
        public TradesResponse GetOpenTrades(string accountId)
        {
            string urlOpenTrades = base.GetRestUrl("accounts/{0}/openTrades");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
           //     wc.Headers.Add("Authorization", base.BearerApiKey);

                var responseBytes = wc.DownloadData(string.Format(urlOpenTrades, accountId));

                var responseString = Encoding.UTF8.GetString(responseBytes);

                using (var input = new StringReader(responseString))
                {
                    var atr = JSON.Deserialize<TradesResponse>(input);
                    return atr;
                }
            }
        }

        public TradeDetailsResponse GetTradeDetails(string accountId, string tradeId)
        {
            string urlTradeDetails = base.GetRestUrl("accounts/{0}/trades/{1}");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
             //   wc.Headers.Add("Authorization", base.BearerApiKey);

                var responseBytes = wc.DownloadData(string.Format(urlTradeDetails, accountId, tradeId));

                var responseString = Encoding.UTF8.GetString(responseBytes);

                using (var input = new StringReader(responseString))
                {
                    var atr = JSON.Deserialize<TradeDetailsResponse>(input);
                    return atr;
                }
            }
        }
    }
}