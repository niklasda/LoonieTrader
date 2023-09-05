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
            
            _logger = ServiceLocator.Container.GetInstance<IExtendedLogger>();
            
            SettingsService = ServiceLocator.Container.GetInstance<ISettingsService>();
            TxReq = ServiceLocator.Container.GetInstance<ITransactionsRequester>();

            TxStreamReq = ServiceLocator.Container.GetInstance<ITransactionsStreamingRequester>();
            PricingStreamReq = ServiceLocator.Container.GetInstance<IPricingStreamingRequester>();


            AccReq = ServiceLocator.Container.GetInstance<IAccountsRequester>();
           // HealthReq = ServiceLocator.Container.GetInstance<IHealthRequester>();
            InstrReq = ServiceLocator.Container.GetInstance<IInstrumentRequester>();
            OrdersReq = ServiceLocator.Container.GetInstance<IOrdersRequester>();
            PosReq = ServiceLocator.Container.GetInstance<IPositionsRequester>();
            PricingReq = ServiceLocator.Container.GetInstance<IPricingRequester>();
            TradesReq = ServiceLocator.Container.GetInstance<ITradesRequester>();
        }

        private readonly IExtendedLogger _logger;
        private readonly ISettingsService SettingsService;
        private readonly IAccountsRequester AccReq;
        //private IHealthRequester HealthReq;
        private readonly IInstrumentRequester InstrReq;
        private readonly IOrdersRequester OrdersReq;
        private readonly IPositionsRequester PosReq;
        private readonly IPricingRequester PricingReq;
        private readonly ITradesRequester TradesReq;
        private readonly IPricingStreamingRequester PricingStreamReq;
        private readonly ITransactionsRequester TxReq;
        private readonly ITransactionsStreamingRequester TxStreamReq;

        private void buttonConnectDemo_Click(object sender, EventArgs e)
        {
            SettingsService.CachedSettings.SelectedEnvironmentKey = Environments.Practice.Key;
            PrintCurrentEnvironment();
        }

        private void buttonConnectLive_Click(object sender, EventArgs e)
        {
            SettingsService.CachedSettings.SelectedEnvironmentKey = Environments.Live.Key;
            PrintCurrentEnvironment();
        }


        private void buttonAccount_Click(object sender, EventArgs e)
        {
            var cfg = SettingsService.CachedSettings.SelectedEnvironment;

         //   var ar = ServiceLocator.Container.GetInstance<IAccountsRequester>();

            _logger.Information("GetAccounts");
            var accounts = AccReq.GetAccounts();

            textBoxAcc.Clear();
            textBoxAcc.Text = cfg.EnvironmentKey + Environment.NewLine;
            foreach (var account in accounts.accounts)
            {
                var details = AccReq.GetAccountDetails(account.id);
                textBoxAcc.Text += details.ToString() + Environment.NewLine;

                var summary = AccReq.GetAccountSummary(account.id);
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
            if (trx.EventType != AppProperties.HeartbeatName)
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
            if (pr.EventType != AppProperties.HeartbeatName)
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

        private void FormTerminal_Load(object sender, EventArgs e)
        {
            PrintCurrentEnvironment();
        }

        private void PrintCurrentEnvironment()
        {
            var cfg = SettingsService.CachedSettings.SelectedEnvironment;

            toolStripStatusLabel1.Text = $"Using {cfg.EnvironmentKey}";
        }
    }
}
