using LoonieBot.Win.Locator;
using LoonieTrader.Library.Constants;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Models;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieBot.Win
{
    public partial class FormTerminal : Form
    {
        public FormTerminal()
        {
            InitializeComponent();

            SettingsService = ServiceLocator.Container.GetInstance<ISettingsService>();
            TxReq = ServiceLocator.Container.GetInstance<ITransactionsRequester>();

            TxStreamReq = ServiceLocator.Container.GetInstance<ITransactionsStreamingRequester>();
            PricingStreamReq = ServiceLocator.Container.GetInstance<IPricingStreamingRequester>();


            AccReq = ServiceLocator.Container.GetInstance<IAccountsRequester>();
            HealthReq = ServiceLocator.Container.GetInstance<IHealthRequester>();
            InstrReq = ServiceLocator.Container.GetInstance<IInstrumentRequester>();
            OrdersReq = ServiceLocator.Container.GetInstance<IOrdersRequester>();
            PosReq = ServiceLocator.Container.GetInstance<IPositionsRequester>();
            PricingReq = ServiceLocator.Container.GetInstance<IPricingRequester>();
            TradesReq = ServiceLocator.Container.GetInstance<ITradesRequester>();
            //PricingStreamReq = container.GetInstance<IPricingStreamingRequester>();
          //  TxReq = container.GetInstance<ITransactionsRequester>();
          //  TxStreamReq = container.GetInstance<ITransactionsStreamingRequester>();
        }

        protected ISettingsService SettingsService;
       // protected IEnvironmentSettings EnvSettings;
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


        private void buttonAccount_Click(object sender, EventArgs e)
        {


            IExtendedLogger logger = ServiceLocator.Container.GetInstance<IExtendedLogger>();

            var ar = ServiceLocator.Container.GetInstance<IAccountsRequester>();
            //var cfgs = ServiceLocator.Container.GetInstance<ISettingsService>();
            //var cfg = cfgs.CachedSettings.SelectedEnvironment;

            logger.Information("GetAccounts");
            var accounts = ar.GetAccounts();

            textBoxAcc.Clear();
            foreach (var account in accounts.accounts)
            {
                var details = ar.GetAccountDetails(account.id);
                textBoxAcc.Text += details.ToString() + Environment.NewLine;

                var summary = ar.GetAccountSummary(account.id);
                textBoxAcc.Text += summary.ToString() + Environment.NewLine;

            }

            //            logger.Information($"GetAccountDetails {cfg.DefaultAccountId}");
            //          Console.WriteLine(ar.GetAccountDetails(cfg.DefaultAccountId));

            //        logger.Information("GetAccountSummary");
            //      Console.WriteLine(ar.GetAccountSummary(cfg.DefaultAccountId));
        }

        //private void buttonSubscribeOld_Click(object sender, EventArgs e)
        //{
        //    //var cfgs = ServiceLocator.Container.GetInstance<ISettingsService>();
        //    var cfg = SettingsService.CachedSettings.SelectedEnvironment;

        //    var txr = ServiceLocator.Container.GetInstance<ITransactionsRequester>();

        //    var txFirst = txr.GetTransactionPages(cfg.DefaultAccountId);
        //    //   txs.lastTransactionID

        //    var txs = txr.GetTransactions(cfg.DefaultAccountId, txFirst.lastTransactionID);

        //    textBoxTrx.Text = txs.ToString();
        //}

        private void buttonSubscribe_Click(object sender, EventArgs e)
        {
            TestTransactionStream();
        }

        private void TestTransactionStream()
        {
            var cfg = SettingsService.CachedSettings.SelectedEnvironment;
            //    EnvSettings = container.GetInstance<ISettingsService>().CachedSettings.SelectedEnvironment;

            ObservableStream<TransactionsResponse.Transaction> tss = TxStreamReq.GetTransactionStream(cfg.DefaultAccountId);
            tss.NewValue += Tss_NewTtrx;
            //IDisposable l1 = tss.Subscribe(x => Console.WriteLine("Tx1: {0}", x));

            // Task.Delay(10000).Wait();
            // Console.WriteLine("Done. 10s");

            //       tss.NewValue -= Tss_NewTtrx;
            // l1.Dispose();

            //            var price = JSON.Deserialize<TransactionsResponse.Transaction>(l1);
            //          Assert.NotNull(price);
        }

        private void Tss_NewTtrx(object sender, StreamEventArgs<TransactionsResponse.Transaction> e)
        {
            TransactionsResponse.Transaction trx = e.Obj;
            if (trx.type != AppProperties.HeartbeatName)
            {
                string line = trx.ToString();
                if (InvokeRequired)
                {
                    BeginInvoke(() => textBoxTrx.Text = line);
                }
                else
                {
                    textBoxTrx.Text = line;
                }
            }
            else
            {
                string line = $"Heartbeat: {trx.lastTransactionID} - {trx.time} {Environment.NewLine}";
                var cfg = SettingsService.CachedSettings.SelectedEnvironment;

                var txs = TxReq.GetTransactions(cfg.DefaultAccountId, trx.lastTransactionID);
                line += txs.ToString();
                if (InvokeRequired)
                {
                    BeginInvoke(() => textBoxTrx.Text = line);
                }
                else
                {
                    textBoxTrx.Text = line;
                }
            }

            //            Console.WriteLine("Tx1: ");
        }

        private void buttonConnectDemo_Click(object sender, EventArgs e)
        {
            //  var cfgs = ServiceLocator.Container.GetInstance<ISettingsService>();
            // var cfgBefore = SettingsService.CachedSettings.SelectedEnvironment;

            SettingsService.CachedSettings.SelectedEnvironmentKey = Environments.Practice.Key;

            //  var cfgAfter = SettingsService.CachedSettings.SelectedEnvironment;


        }

        private void buttonConnectLive_Click(object sender, EventArgs e)
        {
            //var cfgs = ServiceLocator.Container.GetInstance<ISettingsService>();
            //  var cfgBefore = SettingsService.CachedSettings.SelectedEnvironment;

            SettingsService.CachedSettings.SelectedEnvironmentKey = Environments.Live.Key;

            // var cfgAfter = SettingsService.CachedSettings.SelectedEnvironment;

        }

        private void buttonSymbol_Click(object sender, EventArgs e)
        {
            TestPricingStream();
        }

        private void TestPricingStream()
        {
            var cfg = SettingsService.CachedSettings.SelectedEnvironment;

            ObservableStream<PricesResponse.Price> pss = PricingStreamReq.GetPriceStream(cfg.DefaultAccountId, "EUR_USD");
            pss.NewValue += Pss_NewPrice;
            //            var l1 = pss.Subscribe(x => Console.WriteLine("Price1: {0}", x));

            // Task.Delay(10000).Wait();
            // Console.WriteLine("Done 10s");

            // pss.NewValue -= Pss_NewPrice;
            //          l1.Dispose();

            // var price = JSON.Deserialize<PricesResponse.Price>(l1);
            // Assert.NotNull(price);
        }

        private void Pss_NewPrice(object sender, StreamEventArgs<PricesResponse.Price> e)
        {
            PricesResponse.Price pr = e.Obj;
            if (pr.type != AppProperties.HeartbeatName)
            {
                string line = pr.ToString();
                if (InvokeRequired)
                {
                    BeginInvoke(() => textBoxSymbol.Text = line);
                }
                else
                {
                    textBoxSymbol.Text = line;
                }
            }
            else
            {
                string line = $"Heartbeat: {pr.instrument} - {pr.time} {Environment.NewLine}";
                //            var cfg = SettingsService.CachedSettings.SelectedEnvironment;

                //                var txs = TxReq.GetTransactions(cfg.DefaultAccountId, trx.lastTransactionID);
                //              line += txs.ToString();
                if (InvokeRequired)
                {
                    BeginInvoke(() => textBoxSymbol.Text = line);
                }
                else
                {
                    textBoxSymbol.Text = line;
                }
            }
        }

        private void buttonPos_Click(object sender, EventArgs e)
        {
            var cfg = SettingsService.CachedSettings.SelectedEnvironment;

            var openPos = PosReq.GetOpenPositions(cfg.DefaultAccountId);

            textBoxPositions.Text = openPos.ToString();
            if (!openPos.positions.Any())
            {
                textBoxPositions.Text += $"{Environment.NewLine} {openPos.positions.Length} open positions";

            }


            var resp = PosReq.GetPositions(cfg.DefaultAccountId);
            //if (!pos.positions.Any())
            //{
                textBoxPositions.Text += $"{Environment.NewLine} {resp.positions.Length} positions";

            //}
            //foreach (var position in pos.positions)
            //{
            //   textBoxPositions.Text +=  Environment.NewLine + position.ToString();

            //}
        }
    }
}
