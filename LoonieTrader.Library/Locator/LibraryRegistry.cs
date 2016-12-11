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
    public class LibraryRegistry : Registry
    {
        public LibraryRegistry()
        {
            IFileReaderWriterService cr = new FileReaderWriterService();
            IExtendedLogger exLogger = CreateExLogger(cr);

            //Scan(_ =>
           // {
                // Declare which assemblies to scan
            //    _.TheCallingAssembly();
                //_.AssemblyContainingType<LiveChartsPartViewModel>();
           // });

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
            For<IInstrumentRequester>().Use<InstrumentRequester>();
            For<IHealthRequester>().Use<HealthRequester>();

            ForSingletonOf<ITransactionsStreamingRequester>().Use<TransactionsStreamingRequester>();
            ForSingletonOf<IPricingStreamingRequester>().Use<PricingStreamingRequester>();
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