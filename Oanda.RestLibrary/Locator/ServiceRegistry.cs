using Oanda.RestLibrary.Configuration;
using Oanda.RestLibrary.Interfaces;
using Oanda.RestLibrary.Requester;
using StructureMap;

namespace Oanda.RestLibrary.Locator
{
    public class ServiceRegistry : Registry
    {
        public ServiceRegistry()
        {
            var cr = new ConfigurationReader();
            var cfg = cr.ReadConfiguration();

            ForSingletonOf<ISettings>().Use(cfg);
            For<IOandaRequester>().Use<OandaRequester>();
            For<IOandaRequesterLive>().Use<OandaRequesterLive>();
        }
    }
}