using LoonieTrader.RestLibrary.HistoricalData;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Logging;
using LoonieTrader.RestLibrary.RestApi.Interfaces;
using LoonieTrader.RestLibrary.RestApi.Requesters;
using LoonieTrader.RestLibrary.Services;
using Serilog;
using StructureMap;

namespace LoonieTrader.RestLibrary.Locator
{
    public class ServiceRegistry : Registry
    {
        public ServiceRegistry()
        {
            IFileReaderWriterService cr = new FileReaderWriterService();
            ISettings settings = cr.LoadConfiguration();

            IExtendedLogger exLogger = CreateExLogger(cr);

            ForSingletonOf<ISettings>().Use(settings);
            ForSingletonOf<IExtendedLogger>().Use(exLogger);

            For<IHistoricalDataLoader>().Use<HistoricalDataLoader>();
            For<IFileReaderWriterService>().Use<FileReaderWriterService>();

            For<IAccountsRequester>().Use<AccountsRequester>();
            For<IOrdersRequester>().Use<OrdersRequester>();
            For<IPositionsRequester>().Use<PositionsRequester>();
            For<IPricingRequester>().Use<PricingRequester>();
            For<ITradesRequester>().Use<TradesRequester>();
            For<ITransactionsRequester>().Use<TransactionsRequester>();
        }

        private IExtendedLogger CreateExLogger(IFileReaderWriterService cr)
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