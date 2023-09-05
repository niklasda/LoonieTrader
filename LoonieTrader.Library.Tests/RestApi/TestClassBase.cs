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
        TestServiceLocator.Initialize();

        EnvSettings = TestServiceLocator.Container.GetInstance<ISettingsService>().CachedSettings.SelectedEnvironment;
        AccReq = TestServiceLocator.Container.GetInstance<IAccountsRequester>();
        //HealthReq = container.GetInstance<IHealthRequester>();
        InstrReq = TestServiceLocator.Container.GetInstance<IInstrumentRequester>();
        OrdersReq = TestServiceLocator.Container.GetInstance<IOrdersRequester>();
        PosReq = TestServiceLocator.Container.GetInstance<IPositionsRequester>();
        PricingReq = TestServiceLocator.Container.GetInstance<IPricingRequester>();
        TradesReq = TestServiceLocator.Container.GetInstance<ITradesRequester>();
        PricingStreamReq = TestServiceLocator.Container.GetInstance<IPricingStreamingRequester>();
        TxReq = TestServiceLocator.Container.GetInstance<ITransactionsRequester>();
        TxStreamReq = TestServiceLocator.Container.GetInstance<ITransactionsStreamingRequester>();


    }

    protected IEnvironmentSettings EnvSettings { get; private set; }
    protected IAccountsRequester AccReq { get; private set; }
    // protected IHealthRequester HealthReq;
    protected IInstrumentRequester InstrReq { get; private set; }
    protected IOrdersRequester OrdersReq { get; private set; }
    protected IPositionsRequester PosReq { get; private set; }
    protected IPricingRequester PricingReq { get; private set; }
    protected ITradesRequester TradesReq { get; private set; }
    protected IPricingStreamingRequester PricingStreamReq { get; private set; }
    protected ITransactionsRequester TxReq { get; private set; }
    protected ITransactionsStreamingRequester TxStreamReq { get; private set; }

}