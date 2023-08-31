using System;
using System.Collections.Generic;
using JetBrains.Annotations;
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

        public IList<AccountSummaryResponse> GetAccountSummaries()
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
                    Logger.Warning(ex, "Could not get account summary for {0}", account.id);
                }
            }

            return accountSummaries;
        }

        // -------- API Below  --

        public AccountsResponse GetAccounts()
        {
            string urlAccounts = GetRestUrl("accounts");

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlAccounts);
                SaveLocalJson("accounts", "all", responseString);
            //    using (var input = new StringReader(responseString))
              //  {
                    var ar = JsonDeserialize<AccountsResponse>(responseString);
                    return ar;
                //}

            }
        }

        public AccountDetailsResponse GetAccountDetails(string accountId)
        {
            string urlAccountDetails = GetRestUrl("accounts/{0}");

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlAccountDetails, accountId);
                SaveLocalJson("accountDetails", accountId, responseString);
                //   using (var input = new StringReader(responseString))
                // {
                var ar = JsonDeserialize<AccountDetailsResponse>(responseString);

                //var ar = JsonSerializer.Deserialize<AccountDetailsResponse>(responseString, new JsonSerializerOptions(){PropertyNameCaseInsensitive = true});
                return ar;
                //}
            }
        }

        public AccountSummaryResponse GetAccountSummary(string accountId)
        {
            string urlAccountSummary = GetRestUrl("accounts/{0}/summary");

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlAccountSummary, accountId);
                SaveLocalJson("accountSummary", accountId, responseString);
         //       using (var input = new StringReader(responseString))
           //     {
                    var ar = JsonDeserialize<AccountSummaryResponse>(responseString);
                    return ar;
             //   }
            }
        }

        public AccountInstrumentsResponse GetAccountInstruments(string accountId)
        {
            string urlInstruments = GetRestUrl("accounts/{0}/instruments");

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlInstruments, accountId);
                SaveLocalJson("accountInstruments", accountId, responseString);
           //     using (var input = new StringReader(responseString))
             //   {
                    var ir = JsonDeserialize<AccountInstrumentsResponse>(responseString);
                    return ir;
               // }
            }
        }

        public AccountChangesResponse GetAccountChanges(string accountId, string transactionId)
        {
            string urlChanges = GetRestUrl("accounts/{0}/changes?sinceTransactionID={1}");

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlChanges, accountId, transactionId);
                SaveLocalJson("accountChanges", accountId, responseString);
         //       using (var input = new StringReader(responseString))
           //     {
                    var ir = JsonDeserialize<AccountChangesResponse>(responseString);
                    return ir;
             //   }
            }
        }

        public AccountInstrumentsResponse PatchAccountConfiguration(string accountId)
        {
            string urlInstruments = GetRestUrl("accounts/{0}/configuration");

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = PatchData(wc, urlInstruments, accountId);
                SaveLocalJson("accountPatch", accountId, responseString);
      //          using (var input = new StringReader(responseString))
        //        {
                    var ir = JsonDeserialize<AccountInstrumentsResponse>(responseString);
                    return ir;
          //      }
            }
        }
    }
}