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

            IExtendedLogger exLogger = CreateExLogger(cr);

            ForSingletonOf<ISettings>().Use(settings);
            ForSingletonOf<IExtendedLogger>().Use(exLogger);

            For<IHistoricalDataLoader>().Use<HistoricalDataLoader>();
            For<IFileReaderWriter>().Use<FileReaderWriter>();

            For<IAccountsRequester>().Use<AccountsRequester>();
            For<IOrdersRequester>().Use<OrdersRequester>();
            For<IPositionsRequester>().Use<PositionsRequester>();
            For<IPricingRequester>().Use<PricingRequester>();
            For<ITradesRequester>().Use<TradesRequester>();
            For<ITransactionsRequester>().Use<TransactionsRequester>();
        }

        private IExtendedLogger CreateExLogger(IFileReaderWriter cr)
        {
            var logFilePattern = cr.GetLogFilePattern();

            var logger = new LoggerConfiguration()
               .WriteTo.LiterateConsole()
               .WriteTo.RollingFile(logFilePattern)
               .CreateLogger();

            IExtendedLogger exLogger = new ExtendedLogger(logger);

            return exLogger;
        }
    }
}