using LoonieTrader.RestLibrary.Configuration;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Requester;
using StructureMap;

namespace LoonieTrader.RestLibrary.Locator
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