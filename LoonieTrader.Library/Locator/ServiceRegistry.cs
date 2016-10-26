using LoonieTrader.Library.HistoricalData;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Logging;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Requesters;
using LoonieTrader.Library.Services;
using Serilog;
using StructureMap;

namespace LoonieTrader.Library.Locator
{
    public class ServiceRegistry : Registry
    {
        public ServiceRegistry()
        {
            IFileReaderWriterService cr = new FileReaderWriterService();
//            ISettings settings = cr.LoadConfiguration();

            IExtendedLogger exLogger = CreateExLogger(cr);

  //          ForSingletonOf<ISettings>().Use(settings);
           // ForSingletonOf<IEnvironmentSettings>().Use(settings.SelectedEnvironment);

            ForSingletonOf<ISettingsService>().Use<SettingserService>();
            ForSingletonOf<IExtendedLogger>().Use(exLogger);

            For<IHistoricalDataLoader>().Use<HistoricalDataLoader>();
            For<IFileReaderWriterService>().Use<FileReaderWriterService>();

            For<IAccountsRequester>().Use<AccountsRequester>();
            For<IOrdersRequester>().Use<OrdersRequester>();
            For<IPositionsRequester>().Use<PositionsRequester>();
            For<IPricingRequester>().Use<PricingRequester>();
            For<ITradesRequester>().Use<TradesRequester>();
            For<ITransactionsRequester>().Use<TransactionsRequester>();
            ForSingletonOf<ITransactionsStreamingRequester>().Use<TransactionsStreamingRequester>();
            ForSingletonOf<IPricingStreamingRequester>().Use<PricingStreamingRequester>();
            For<IInstrumentRequester>().Use<InstrumentRequester>();
            For<IHealthRequester>().Use<HealthRequester>();
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