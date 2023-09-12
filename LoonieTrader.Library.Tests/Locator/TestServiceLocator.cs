using Lamar;
using LoonieTrader.Library.HistoricalData;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Logging;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Requesters;
using LoonieTrader.Library.Services;
using Serilog;

namespace LoonieTrader.Library.Tests.Locator;

public static class TestServiceLocator
{
    private static IContainer Initialize()
    {
        var container = new Container(c =>
        {
            IFileReaderWriterService cr = new FileReaderWriterService();
            IExtendedLogger exLogger = CreateExLogger(cr);

            c.ForSingletonOf<ISettingsService>().Use<SettingsService>();
            c.ForSingletonOf<IExtendedLogger>().Use(exLogger);

            c.For<IHistoricalDataLoader>().Use<HistoricalDataLoader>();
            c.For<IFileReaderWriterService>().Use<FileReaderWriterService>();

            c.For<IAccountsRequester>().Use<AccountsRequester>();
            c.For<IOrdersRequester>().Use<OrdersRequester>();
            c.For<IPositionsRequester>().Use<PositionsRequester>();
            c.For<IPricingRequester>().Use<PricingRequester>();
            c.For<ITradesRequester>().Use<TradesRequester>();
            c.For<ITransactionsRequester>().Use<TransactionsRequester>();
            c.For<IInstrumentRequester>().Use<InstrumentRequester>();

            c.ForSingletonOf<ITransactionsStreamingRequester>().Use<TransactionsStreamingRequester>();
            c.ForSingletonOf<IPricingStreamingRequester>().Use<PricingStreamingRequester>();
        });

        return container;
    }

    public static IContainer Container { get; } = Initialize();

    private static IExtendedLogger CreateExLogger(IFileReaderWriterService cr)
    {
        var logFilePattern = cr.GetTestLogFilePattern();

        var logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(logFilePattern)
            .MinimumLevel.Debug()
            .CreateLogger();

        IExtendedLogger exLogger = new ExtendedLogger(logger);

        return exLogger;
    }
}