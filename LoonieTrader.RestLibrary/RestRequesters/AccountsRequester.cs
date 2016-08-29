﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Jil;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Models.Responses;
using Serilog;

namespace LoonieTrader.RestLibrary.RestRequesters
{
    public class AccountsRequester : RequesterBase, IAccountsRequester
    {
        public AccountsRequester(ISettings settings, IFileReaderWriter fileReaderWriter, IExtendedLogger logger) : base(settings, fileReaderWriter, logger)
        {
        }

        public IEnumerable<AccountSummaryResponse> GetAccountSummaries()
        {
            var accounts = GetAccounts();
            IList<AccountSummaryResponse> accountSummaries = new List<AccountSummaryResponse>();
            foreach (var account in accounts.accounts)
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
                var responseBytes = wc.DownloadData(urlAccounts);
                var responseString = Encoding.UTF8.GetString(responseBytes);
                base.SaveLocalJson("accounts", "All", responseString);
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
                var responseBytes = wc.DownloadData(string.Format(urlAccountDetails, accountId));
                var responseString = Encoding.UTF8.GetString(responseBytes);
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
                var responseBytes = wc.DownloadData(string.Format(urlAccountSummary, accountId));
                var responseString = Encoding.UTF8.GetString(responseBytes);
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
                var responseBytes = wc.DownloadData(string.Format(urlInstruments, accountId));
                var responseString = Encoding.UTF8.GetString(responseBytes);
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