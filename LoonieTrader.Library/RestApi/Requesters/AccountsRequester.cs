using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using JetBrains.Annotations;
using Jil;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Requesters
{
    [UsedImplicitly]
    public class AccountsRequester : RequesterBase, IAccountsRequester
    {
        public AccountsRequester(ISettingsService settingService, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger) 
            : base(settingService, fileReaderWriter, logger)
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

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlAccounts);
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

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlAccountDetails, accountId);
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

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlAccountSummary, accountId);
                base.SaveLocalJson("accountSummary", accountId, responseString);
                using (var input = new StringReader(responseString))
                {
                    var ar = JSON.Deserialize<AccountSummaryResponse>(input);
                    return ar;
                }
            }
        }

        public AccountInstrumentsResponse GetAccountInstruments(string accountId)
        {
            string urlInstruments = base.GetRestUrl("accounts/{0}/instruments");

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlInstruments, accountId);
                base.SaveLocalJson("accountInstruments", accountId, responseString);
                using (var input = new StringReader(responseString))
                {
                    var ir = JSON.Deserialize<AccountInstrumentsResponse>(input);
                    return ir;
                }
            }
        }

        public AccountChangesResponse GetAccountChanges(string accountId, string transactionId)
        {
            string urlChanges = base.GetRestUrl("accounts/{0}/changes?sinceTransactionID={1}");

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlChanges, accountId, transactionId);
                base.SaveLocalJson("accountChanges", accountId, responseString);
                using (var input = new StringReader(responseString))
                {
                    var ir = JSON.Deserialize<AccountChangesResponse>(input);
                    return ir;
                }
            }
        }

        public AccountInstrumentsResponse PatchAccountConfiguration(string accountId)
        {
            string urlInstruments = base.GetRestUrl("accounts/{0}/configuration");

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = PatchData(wc, urlInstruments, accountId);
                base.SaveLocalJson("accountPatch", accountId, responseString);
                using (var input = new StringReader(responseString))
                {
                    var ir = JSON.Deserialize<AccountInstrumentsResponse>(input);
                    return ir;
                }
            }
        }
    }
}