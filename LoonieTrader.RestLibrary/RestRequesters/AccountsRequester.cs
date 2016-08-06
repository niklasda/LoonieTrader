using System.IO;
using System.Net;
using System.Text;
using Jil;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.RestRequesters
{
    public class AccountsRequester : RequesterBase, IAccountsRequester
    {
        public AccountsRequester(ISettings settings) : base(settings)
        {
        }

        public AccountsResponse GetAccounts()
        {
            string urlAccounts = base.GetRestUrl("accounts");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
               // wc.Headers.Add("Authorization", base.BearerApiKey);

                var responseBytes = wc.DownloadData(urlAccounts);

                var responseString = Encoding.UTF8.GetString(responseBytes);

                using (var input = new StringReader(responseString))
                {
                    var ar = JSON.Deserialize<AccountsResponse>(input);
                    return ar;
                }
            }
        }

        public AccountDetailsResponse GetAccountDetails(string accountId)
        {
            string urlAccountDetails = base.GetRestUrl("accounts/{0}");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
              //  wc.Headers.Add("Authorization", base.BearerApiKey);

                var responseBytes = wc.DownloadData(string.Format(urlAccountDetails, accountId));

                var responseString = Encoding.UTF8.GetString(responseBytes);

                using (var input = new StringReader(responseString))
                {
                    var ar = JSON.Deserialize<AccountDetailsResponse>(input);
                    return ar;
                }
            }
        }

        public AccountSummaryResponse GetAccountSummary(string accountId)
        {
            string urlAccountSummary = base.GetRestUrl("accounts/{0}/summary");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
             //   wc.Headers.Add("Authorization", base.BearerApiKey);

                var responseBytes = wc.DownloadData(string.Format(urlAccountSummary, accountId));

                var responseString = Encoding.UTF8.GetString(responseBytes);

                using (var input = new StringReader(responseString))
                {
                    var ar = JSON.Deserialize<AccountSummaryResponse>(input);
                    return ar;
                }
            }
        }

        public AccountInstrumentsResponse GetInstruments(string accountId)
        {
            string urlInstruments = base.GetRestUrl("accounts/{0}/instruments");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
              //  wc.Headers.Add("Authorization", base.BearerApiKey);

                var responseBytes = wc.DownloadData(string.Format(urlInstruments, accountId));

                var responseString = Encoding.UTF8.GetString(responseBytes);

                using (var input = new StringReader(responseString))
                {
                    var ir = JSON.Deserialize<AccountInstrumentsResponse>(input);
                    return ir;
                }
            }
        }
    }
}