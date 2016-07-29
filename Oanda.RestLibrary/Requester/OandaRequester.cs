﻿using System.IO;
using System.Net;
using System.Text;
using Jil;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Responses;

namespace LoonieTrader.RestLibrary.Requester
{
    public class OandaRequester : RequesterBase, IOandaRequester
    {
        public OandaRequester(ISettings settings) : base(settings)
        {
        }

        public AccountResponse GetAccounts()
        {
            string urlAccounts =base.GetRestUrl("accounts");
            //const string urlAccounts = "https://api-fxpractice.oanda.com/v3/accounts";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.BearerApiKey);

            var responseBytes = wc.DownloadData(urlAccounts);

            var responseString = Encoding.UTF8.GetString(responseBytes);

            using (var input = new StringReader(responseString))
            {
                var ar = JSON.Deserialize<AccountResponse>(input);
                return ar;
            }
        }
        public AccountSummaryResponse GetAccountSummary(string accountId)
        {
            string urlAccountSummary = base.GetRestUrl("accounts/{0}/summary");
            //const string urlAccountSummary = "https://api-fxpractice.oanda.com/v3/accounts/{0}/summary";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.BearerApiKey);

            var responseBytes = wc.DownloadData(string.Format(urlAccountSummary, accountId));

            var responseString = Encoding.UTF8.GetString(responseBytes);

            using (var input = new StringReader(responseString))
            {
                var ar = JSON.Deserialize<AccountSummaryResponse>(input);
                return ar;
            }
        }

        public AccountPositionsResponse GetPositions(string accountId)
        {
            string urlAccountPositions = base.GetRestUrl("accounts/{0}/positions/");
            //const string urlAccountPositions = "https://api-fxpractice.oanda.com/v3/accounts/{0}/positions/";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.BearerApiKey);

            var responseBytes = wc.DownloadData(string.Format(urlAccountPositions, accountId));

            var responseString = Encoding.UTF8.GetString(responseBytes);

            using (var input = new StringReader(responseString))
            {
                var apr = JSON.Deserialize<AccountPositionsResponse>(input);
                return apr;
            }
        }


        public AccountOpenPositionsResponse GetOpenPositions(string accountId)
        {
            string urlAccountOpenPositions = base.GetRestUrl("accounts/{0}/openPositions/");
            //const string urlAccountOpenPositions = "https://api-fxpractice.oanda.com/v3/accounts/{0}/openPositions/";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.BearerApiKey);

            var responseBytes = wc.DownloadData(string.Format(urlAccountOpenPositions, accountId));

            var responseString = Encoding.UTF8.GetString(responseBytes);

            using (var input = new StringReader(responseString))
            {
                var apr = JSON.Deserialize<AccountOpenPositionsResponse>(input);
                return apr;
            }
        }

        public AccountOrdersResponse GetOrders(string accountId)
        {
            string urlAccountOrders = base.GetRestUrl("accounts/{0}/orders/");
            //const string urlAccountOrders = "https://api-fxpractice.oanda.com/v3/accounts/{0}/orders/";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.BearerApiKey);

            var responseBytes = wc.DownloadData(string.Format(urlAccountOrders, accountId));

            var responseString = Encoding.UTF8.GetString(responseBytes);

            using (var input = new StringReader(responseString))
            {
                var aor = JSON.Deserialize<AccountOrdersResponse>(input);
                return aor;
            }
        }
        public AccountPendingOrdersResponse GetPendingOrders(string accountId)
        {
            string urlPendingAccountOrders = base.GetRestUrl("accounts/{0}/pendingOrders/");
            //const string urlPendingAccountOrders = "https://api-fxpractice.oanda.com/v3/accounts/{0}/pendingOrders/";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.BearerApiKey);

            var responseBytes = wc.DownloadData(string.Format(urlPendingAccountOrders, accountId));

            var responseString = Encoding.UTF8.GetString(responseBytes);

            using (var input = new StringReader(responseString))
            {
                var aor = JSON.Deserialize<AccountPendingOrdersResponse>(input);
                return aor;
            }
        }

        public AccountTransactionPagesResponse GetTransactionPages(string accountId)
        {
            string urlAccountOrders = base.GetRestUrl("accounts/{0}/transactions/");
            //const string urlAccountOrders = "https://api-fxpractice.oanda.com/v3/accounts/{0}/transactions/";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.BearerApiKey);

            var responseBytes = wc.DownloadData(string.Format(urlAccountOrders, accountId));

            var responseString = Encoding.UTF8.GetString(responseBytes);

            using (var input = new StringReader(responseString))
            {
                var atpr = JSON.Deserialize<AccountTransactionPagesResponse>(input);
                return atpr;
            }
        }

        public AccountTransactionsResponse GetTransactions(string accountId)
        {
            string urlAccountOrders = base.GetRestUrl("accounts/{0}/transactions/idrange?from=1&to=19");
            //const string urlAccountOrders = "https://api-fxpractice.oanda.com/v3/accounts/{0}/transactions/idrange?from=1&to=19";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.BearerApiKey);

            var responseBytes = wc.DownloadData(string.Format(urlAccountOrders, accountId));

            var responseString = Encoding.UTF8.GetString(responseBytes);

            using (var input = new StringReader(responseString))
            {
                var atr = JSON.Deserialize<AccountTransactionsResponse>(input);
                return atr;
            }
        }

        public AccountTradesResponse GetTrades(string accountId)
        {
            string urlAccountOrders = base.GetRestUrl("accounts/{0}/trades");
            //const string urlAccountOrders = "https://api-fxpractice.oanda.com/v3/accounts/{0}/trades";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.BearerApiKey);

            var responseBytes = wc.DownloadData(string.Format(urlAccountOrders, accountId));

            var responseString = Encoding.UTF8.GetString(responseBytes);

            using (var input = new StringReader(responseString))
            {
                var atr = JSON.Deserialize<AccountTradesResponse>(input);
                return atr;
            }
        }
    }
}