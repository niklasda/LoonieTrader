using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.Tests.Locator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoonieTrader.Library.Tests.RestApi;

public class TestClassBase
{
    [TestInitialize]
    public void Setup()
    {
        var container = TestServiceLocator.Initialize();

        EnvSettings = container.GetInstance<ISettingsService>().CachedSettings.SelectedEnvironment;
        AccReq = container.GetInstance<IAccountsRequester>();
        HealthReq = container.GetInstance<IHealthRequester>();
        InstrReq = container.GetInstance<IInstrumentRequester>();
        OrdersReq = container.GetInstance<IOrdersRequester>();
        PosReq = container.GetInstance<IPositionsRequester>();
        PricingReq = container.GetInstance<IPricingRequester>();
        TradesReq = container.GetInstance<ITradesRequester>();
        PricingStreamReq = container.GetInstance<IPricingStreamingRequester>();
        TxReq = container.GetInstance<ITransactionsRequester>();
        TxStreamReq = container.GetInstance<ITransactionsStreamingRequester>();


    }

    protected IEnvironmentSettings EnvSettings;
    protected IAccountsRequester AccReq;
    protected IHealthRequester HealthReq;
    protected IInstrumentRequester InstrReq;
    protected IOrdersRequester OrdersReq;
    protected IPositionsRequester PosReq;
    protected IPricingRequester PricingReq;
    protected ITradesRequester TradesReq;
    protected IPricingStreamingRequester PricingStreamReq;
    protected ITransactionsRequester TxReq;
    protected ITransactionsStreamingRequester TxStreamReq;

}