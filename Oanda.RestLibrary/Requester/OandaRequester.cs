using System.IO;
using System.Net;
using System.Text;
using Jil;
using Oanda.RestLibrary.Interfaces;
using Oanda.RestLibrary.Responses;

namespace Oanda.RestLibrary.Requester
{
    public class OandaRequester : RequesterBase, IOandaRequester
    {
        public AccountResponse GetAccounts()
        {
            const string urlAccounts = "https://api-fxpractice.oanda.com/v3/accounts";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.ApiKey);

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
            const string urlAccountSummary = "https://api-fxpractice.oanda.com/v3/accounts/{0}/summary";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.ApiKey);

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
            const string urlAccountPositions = "https://api-fxpractice.oanda.com/v3/accounts/{0}/positions/";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.ApiKey);

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
            const string urlAccountOpenPositions = "https://api-fxpractice.oanda.com/v3/accounts/{0}/openPositions/";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.ApiKey);

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
            const string urlAccountOrders = "https://api-fxpractice.oanda.com/v3/accounts/{0}/orders/";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.ApiKey);

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
            const string urlPendingAccountOrders = "https://api-fxpractice.oanda.com/v3/accounts/{0}/pendingOrders/";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.ApiKey);

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
            const string urlAccountOrders = "https://api-fxpractice.oanda.com/v3/accounts/{0}/transactions/";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.ApiKey);

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
            const string urlAccountOrders = "https://api-fxpractice.oanda.com/v3/accounts/{0}/transactions/idrange?from=1&to=19";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.ApiKey);

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
            const string urlAccountOrders = "https://api-fxpractice.oanda.com/v3/accounts/{0}/trades";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.ApiKey);

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