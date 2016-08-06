using System;
using System.Net;
using LoonieTrader.RestLibrary.Configuration;
using LoonieTrader.RestLibrary.Interfaces;

namespace LoonieTrader.RestLibrary.RestRequesters
{
    public abstract class RequesterBase
    {
        protected RequesterBase(ISettings settings)
        {
            ApiKey = settings.ApiKey;
        }

        private string ApiKey { get; }

        protected string BearerApiKey { get { return string.Format("Bearer {0}", ApiKey); } }

        protected string GetRestUrl(string arg)
        {
            return string.Format("https://{0}.oanda.com/v3/{1}", Environments.Practice.Value, arg);
        }

        protected WebClient GetAuthenticatedWebClient()
        {
            var wc = new WebClient();
            wc.Headers.Add("Authorization", BearerApiKey);
            wc.Headers.Add("Content-Type", "application/json");
            return wc;
        }
    }
}