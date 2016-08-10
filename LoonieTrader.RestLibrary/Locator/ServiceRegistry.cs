using LoonieTrader.RestLibrary.Configuration;
using LoonieTrader.RestLibrary.HistoricalData;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.RestRequesters;
using Serilog;
using StructureMap;

namespace LoonieTrader.RestLibrary.Locator
{
    public class ServiceRegistry : Registry
    {
        public ServiceRegistry()
        {
            IFileReaderWriter cr = new FileReaderWriter();
            ISettings settings = cr.LoadConfiguration();

            ILogger logger = CreateLogger(cr);

            ForSingletonOf<ISettings>().Use(settings);
            ForSingletonOf<ILogger>().Use(logger);

            For<IHistoricalDataLoader>().Use<HistoricalDataLoader>();
            For<IFileReaderWriter>().Use<FileReaderWriter>();

            For<IAccountsRequester>().Use<AccountsRequester>();
            For<IOrdersRequester>().Use<OrdersRequester>();
            For<IPositionsRequester>().Use<PositionsRequester>();
            For<IPricingRequester>().Use<PricingRequester>();
            For<ITradesRequester>().Use<TradesRequester>();
            For<ITransactionsRequester>().Use<TransactionsRequester>();
        }



        private ILogger CreateLogger(IFileReaderWriter cr)
        {
            var logFilePattern = cr.GetLogFilePattern();

            var logger = new LoggerConfiguration()
               .WriteTo.LiterateConsole()
               .WriteTo.RollingFile(logFilePattern)
               .CreateLogger();

            return logger;
        }
    }
}