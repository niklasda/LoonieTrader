using System.IO;
using System.Net;
using System.Text;
using Jil;
using Oanda.RestLibrary.Configuration;
using Oanda.RestLibrary.Responses;

namespace Oanda.RestLibrary.Requester
{
    public class OandaRequester
    {
        public AccountResponse GetAccounts()
        {
            const string urlAccounts = "https://api-fxpractice.oanda.com/v3/accounts";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", Constants.Key);

            var responseBytes = wc.DownloadData(urlAccounts);

            var responseString = Encoding.UTF8.GetString(responseBytes);

            using (var input = new StringReader(responseString))
            {
                var ar = JSON.Deserialize<AccountResponse>(input);
                return ar;
            }
        }
        public AccountSummaryResponse GetAccountSummary()
        {
            const string urlAccountSummary = "https://api-fxpractice.oanda.com/v3/accounts/{0}/summary";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", Constants.Key);

            var responseBytes = wc.DownloadData(string.Format(urlAccountSummary, Constants.AccountId));

            var responseString = Encoding.UTF8.GetString(responseBytes);

            using (var input = new StringReader(responseString))
            {
                var ar = JSON.Deserialize<AccountSummaryResponse>(input);
                return ar;
            }
        }

        public AccountPositionsResponse GetPositions()
        {
            const string urlAccountPositions = "https://api-fxpractice.oanda.com/v3/accounts/{0}/positions/";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", Constants.Key);

            var responseBytes = wc.DownloadData(string.Format(urlAccountPositions, Constants.AccountId));

            var responseString = Encoding.UTF8.GetString(responseBytes);

            using (var input = new StringReader(responseString))
            {
                var apr = JSON.Deserialize<AccountPositionsResponse>(input);
                return apr;
            }
        }


        public AccountOpenPositionsResponse GetOpenPositions()
        {
            const string urlAccountOpenPositions = "https://api-fxpractice.oanda.com/v3/accounts/{0}/openPositions/";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", Constants.Key);

            var responseBytes = wc.DownloadData(string.Format(urlAccountOpenPositions, Constants.AccountId));

            var responseString = Encoding.UTF8.GetString(responseBytes);

            using (var input = new StringReader(responseString))
            {
                var apr = JSON.Deserialize<AccountOpenPositionsResponse>(input);
                return apr;
            }
        }

        public AccountOrdersResponse GetOrders()
        {
            const string urlAccountOrders = "https://api-fxpractice.oanda.com/v3/accounts/{0}/orders/";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", Constants.Key);

            var responseBytes = wc.DownloadData(string.Format(urlAccountOrders, Constants.AccountId));

            var responseString = Encoding.UTF8.GetString(responseBytes);

            using (var input = new StringReader(responseString))
            {
                var aor = JSON.Deserialize<AccountOrdersResponse>(input);
                return aor;
            }
        }
        public AccountPendingOrdersResponse GetPendingOrders()
        {
            const string urlPendingAccountOrders = "https://api-fxpractice.oanda.com/v3/accounts/{0}/pendingOrders/";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", Constants.Key);

            var responseBytes = wc.DownloadData(string.Format(urlPendingAccountOrders, Constants.AccountId));

            var responseString = Encoding.UTF8.GetString(responseBytes);

            using (var input = new StringReader(responseString))
            {
                var aor = JSON.Deserialize<AccountPendingOrdersResponse>(input);
                return aor;
            }
        }

        public AccountTransactionPagesResponse GetTransactionPages()
        {
            const string urlAccountOrders = "https://api-fxpractice.oanda.com/v3/accounts/{0}/transactions/";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", Constants.Key);

            var responseBytes = wc.DownloadData(string.Format(urlAccountOrders, Constants.AccountId));

            var responseString = Encoding.UTF8.GetString(responseBytes);

            using (var input = new StringReader(responseString))
            {
                var atpr = JSON.Deserialize<AccountTransactionPagesResponse>(input);
                return atpr;
            }
        }

        public AccountTransactionsResponse GetTransactions()
        {
            const string urlAccountOrders = "https://api-fxpractice.oanda.com/v3/accounts/{0}/transactions/idrange?from=1&to=19";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", Constants.Key);

            var responseBytes = wc.DownloadData(string.Format(urlAccountOrders, Constants.AccountId));

            var responseString = Encoding.UTF8.GetString(responseBytes);

            using (var input = new StringReader(responseString))
            {
                var atr = JSON.Deserialize<AccountTransactionsResponse>(input);
                return atr;
            }
        }

        public AccountTradesResponse GetTrades()
        {
            const string urlAccountOrders = "https://api-fxpractice.oanda.com/v3/accounts/{0}/trades";

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", Constants.Key);

            var responseBytes = wc.DownloadData(string.Format(urlAccountOrders, Constants.AccountId));

            var responseString = Encoding.UTF8.GetString(responseBytes);

            using (var input = new StringReader(responseString))
            {
                var atr = JSON.Deserialize<AccountTradesResponse>(input);
                return atr;
            }
        }
    }
}