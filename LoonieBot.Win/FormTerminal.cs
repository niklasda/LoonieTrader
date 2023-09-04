using LoonieBot.Win.Locator;
using LoonieTrader.Library.Constants;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Models;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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
        }

        private ISettingsService SettingsService;
        private IAccountsRequester AccReq;
        private IHealthRequester HealthReq;
        private IInstrumentRequester InstrReq;
        private IOrdersRequester OrdersReq;
        private IPositionsRequester PosReq;
        private IPricingRequester PricingReq;
        private ITradesRequester TradesReq;
        private IPricingStreamingRequester PricingStreamReq;
        private ITransactionsRequester TxReq;
        private ITransactionsStreamingRequester TxStreamReq;

        private void buttonConnectDemo_Click(object sender, EventArgs e)
        {
            SettingsService.CachedSettings.SelectedEnvironmentKey = Environments.Practice.Key;
        }

        private void buttonConnectLive_Click(object sender, EventArgs e)
        {
            SettingsService.CachedSettings.SelectedEnvironmentKey = Environments.Live.Key;
        }


        private void buttonAccount_Click(object sender, EventArgs e)
        {
            IExtendedLogger logger = ServiceLocator.Container.GetInstance<IExtendedLogger>();
            var cfg = SettingsService.CachedSettings.SelectedEnvironment;

            var ar = ServiceLocator.Container.GetInstance<IAccountsRequester>();

            logger.Information("GetAccounts");
            var accounts = ar.GetAccounts();

            textBoxAcc.Clear();
            textBoxAcc.Text = cfg.EnvironmentKey + Environment.NewLine;
            foreach (var account in accounts.accounts)
            {
                var details = ar.GetAccountDetails(account.id);
                textBoxAcc.Text += details.ToString() + Environment.NewLine;

                var summary = ar.GetAccountSummary(account.id);
                textBoxAcc.Text += summary.ToString() + Environment.NewLine;

            }
        }


        private void buttonSubscribe_Click(object sender, EventArgs e)
        {
            TestTransactionStream();
        }

        private void TestTransactionStream()
        {
            var cfg = SettingsService.CachedSettings.SelectedEnvironment;

            ObservableStream<TransactionsResponse.Transaction> tss = TxStreamReq.GetTransactionStream(cfg.DefaultAccountId);
            tss.NewValue += Tss_NewTtrx;

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
                string line = $".";
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

        }



        private void buttonSymbol_Click(object sender, EventArgs e)
        {
            TestPricingStream();
        }

        private void TestPricingStream()
        {
            var cfg = SettingsService.CachedSettings.SelectedEnvironment;

            ObservableStream<PricesResponse.Price> pss = PricingStreamReq.GetPriceStream(cfg.DefaultAccountId, "EUR_USD,USD_CAD");
            pss.NewValue += Pss_NewPrice;

        }

        private void Pss_NewPrice(object sender, StreamEventArgs<PricesResponse.Price> e)
        {
            PricesResponse.Price pr = e.Obj;
            if (pr.type != AppProperties.HeartbeatName)
            {
                string line = pr.ToString();
                if (InvokeRequired)
                {
                    BeginInvoke(() => textBoxSymbol.Text += line);
                }
                else
                {
                    textBoxSymbol.Text = line;
                }
            }
            else
            {
                string line = $".";
                if (InvokeRequired)
                {
                    BeginInvoke(() => textBoxSymbol.Text += line);
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

            textBoxPositions.Text += $"{Environment.NewLine} {resp.positions.Length} positions";

        }
    }
}
