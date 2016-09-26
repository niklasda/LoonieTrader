using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Jil;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Requesters
{
    public class AccountsRequester : RequesterBase, IAccountsRequester
    {
        public AccountsRequester(ISettings settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger) 
            : base(settings, fileReaderWriter, logger)
        {
        }

        public IEnumerable<AccountSummaryResponse> GetAccountSummaries()
        {
            var accounts = GetAccounts();
            IList<AccountSummaryResponse> accountSummaries = new List<AccountSummaryResponse>();
            foreach (var account  in accounts.accounts)
            {
                try
                {
                    accountSummaries.Add(GetAccountSummary(account.id));
                }
                catch (Exception ex)
                {
                    base.Logger.Warning(ex, "Could not get  account summary for {0}", account.id);
                }
            }

            return accountSummaries;
        }

        // -------- API Below  --

        public AccountsResponse GetAccounts()
        {
            string urlAccounts = base.GetRestUrl("accounts");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseString = DownloadData(wc, urlAccounts);
                //var responseBytes = wc.DownloadData(urlAccounts);
                //var responseString = Encoding.UTF8.GetString(responseBytes);
                base.SaveLocalJson("accounts", "all", responseString);
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
                var responseString = DownloadData(wc, urlAccountDetails, accountId);
                //var responseBytes = wc.DownloadData(string.Format(urlAccountDetails, accountId));
                //var responseString = Encoding.UTF8.GetString(responseBytes);
                base.SaveLocalJson("accountDetails", accountId, responseString);
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
                var responseString = DownloadData(wc, urlAccountSummary, accountId);
                //var responseBytes = wc.DownloadData(string.Format(urlAccountSummary, accountId));
                //var responseString = Encoding.UTF8.GetString(responseBytes);
                base.SaveLocalJson("accountSummary", accountId, responseString);
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
                var responseString = DownloadData(wc, urlInstruments, accountId);
                //var responseBytes = wc.DownloadData(string.Format(urlInstruments, accountId));
                //var responseString = Encoding.UTF8.GetString(responseBytes);
                base.SaveLocalJson("accountInstruments", accountId, responseString);
                using (var input = new StringReader(responseString))
                {
                    var ir = JSON.Deserialize<AccountInstrumentsResponse>(input);
                    return ir;
                }
            }
        }
    }
}