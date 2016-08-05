﻿using System;
using System.IO;
using System.Net;
using System.Text;
using Jil;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.RestRequesters.v3
{
    public class TransactionsRequester : RequesterBase, ITransactionsRequester
    {
        public TransactionsRequester(ISettings settings) : base(settings)
        {
        }

        public AccountTransactionPagesResponse GetTransactionPages(string accountId)
        {
            string urlAccountOrders = base.GetRestUrl("accounts/{0}/transactions/");

            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("Authorization", base.BearerApiKey);

                var responseBytes = wc.DownloadData(string.Format(urlAccountOrders, accountId));

                var responseString = Encoding.UTF8.GetString(responseBytes);

                using (var input = new StringReader(responseString))
                {
                    var atpr = JSON.Deserialize<AccountTransactionPagesResponse>(input);
                    return atpr;
                }
            }
        }

        public AccountTransactionsResponse GetTransactions(string accountId)
        {
            string urlAccountOrders = base.GetRestUrl("accounts/{0}/transactions/idrange?from=1&to=19");

            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("Authorization", base.BearerApiKey);

                var responseBytes = wc.DownloadData(string.Format(urlAccountOrders, accountId));

                var responseString = Encoding.UTF8.GetString(responseBytes);

                using (var input = new StringReader(responseString))
                {
                    var atr = JSON.Deserialize<AccountTransactionsResponse>(input);
                    return atr;
                }
            }
        }

        public AccountTransactionDetailsResponse GetTransactionDetails(string accountId, string transactionId)
        {
            string urlAccountOrders = base.GetRestUrl("accounts/{0}/transactions/{1}");

            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("Authorization", base.BearerApiKey);

                var responseBytes = wc.DownloadData(string.Format(urlAccountOrders, accountId, transactionId));

                var responseString = Encoding.UTF8.GetString(responseBytes);

                using (var input = new StringReader(responseString))
                {
                    var atr = JSON.Deserialize<AccountTransactionDetailsResponse>(input);
                    return atr;
                }
            }
        }
    }
}