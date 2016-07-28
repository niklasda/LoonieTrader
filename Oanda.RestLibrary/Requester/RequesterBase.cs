using Oanda.RestLibrary.Configuration;
using Oanda.RestLibrary.Interfaces;

namespace Oanda.RestLibrary.Requester
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
    }
}