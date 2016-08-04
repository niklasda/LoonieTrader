using LoonieTrader.RestLibrary.Configuration;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.RestRequesters.v3;
using StructureMap;

namespace LoonieTrader.RestLibrary.Locator.v3
{
    public class ServiceRegistry : Registry
    {
        public ServiceRegistry()
        {
            var cr = new ConfigurationReader();
            var cfg = cr.ReadConfiguration();

            ForSingletonOf<ISettings>().Use(cfg);
            For<IAccountsRequester>().Use<AccountsRequester>();
            For<IOrdersRequester>().Use<OrdersRequester>();
            For<IPositionsRequester>().Use<PositionsRequester>();
            For<IPricingRequester>().Use<PricingRequester>();
            For<ITradesRequester>().Use<TradesRequester>();
            For<ITransactionsRequester>().Use<TransactionsRequester>();
        }
    }
}