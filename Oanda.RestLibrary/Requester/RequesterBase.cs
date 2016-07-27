using Oanda.RestLibrary.Configuration;

namespace Oanda.RestLibrary.Requester
{
    public abstract class RequesterBase
    {
        public string ApiKey { get; set; }

        public string GetRestUrlStart()
        {
            return string.Format("https://{0}.oanda.com/v3/", Environments.Practice);
        }
    }
}