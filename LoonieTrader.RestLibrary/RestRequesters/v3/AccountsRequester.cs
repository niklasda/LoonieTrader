using System.IO;
using System.Net;
using System.Text;
using Jil;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.RestRequesters.v3
{
    public class AccountsRequester : RequesterBase, IAccountsRequester
    {
        public AccountsRequester(ISettings settings) : base(settings)
        {
        }

        public AccountResponse GetAccounts()
        {
            string urlAccounts = base.GetRestUrl("accounts");

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

        public InstrumentsResponse GetInstruments(string accountId)
        {
            string urlPrices = base.GetRestUrl("accounts/{0}/instruments");

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.BearerApiKey);

            var responseBytes = wc.DownloadData(string.Format(urlPrices, accountId));

            var responseString = Encoding.UTF8.GetString(responseBytes);

            using (var input = new StringReader(responseString))
            {
                var ir = JSON.Deserialize<InstrumentsResponse>(input);
                return ir;
            }
        }
    }
}