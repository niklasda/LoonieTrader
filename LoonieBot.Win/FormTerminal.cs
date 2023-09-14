using System.Diagnostics;
using System.Net;
using System.Reactive.Linq;
using LoonieBot.Win.Locator;
using LoonieTrader.Library.Constants;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Models;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;
using LoonieTrader.Library.RestApi.Enums;
using FileHelpers;
using LoonieTrader.Library.Caches;
using LoonieTrader.Library.HistoricalData;
using LoonieTrader.Library.Services;
using Serilog;

namespace LoonieBot.Win
{
    public partial class FormTerminal : Form
    {
        public FormTerminal()
        {
            InitializeComponent();

            _logger = ServiceLocator.Container.GetInstance<ILogger>();

            _settingsService = ServiceLocator.Container.GetInstance<ISettingsService>();
            _txReq = ServiceLocator.Container.GetInstance<ITransactionsRequester>();

            _pricesCache = ServiceLocator.Container.GetInstance<IPricesCache>();
            _txStreamReq = ServiceLocator.Container.GetInstance<ITransactionsStreamingRequester>();
            _pricingStreamReq = ServiceLocator.Container.GetInstance<IPricingStreamingRequester>();


            _accountReq = ServiceLocator.Container.GetInstance<IAccountsRequester>();
            _instrumentReq = ServiceLocator.Container.GetInstance<IInstrumentRequester>();
            _ordersReq = ServiceLocator.Container.GetInstance<IOrdersRequester>();
            _positionReq = ServiceLocator.Container.GetInstance<IPositionsRequester>();
            _pricingReq = ServiceLocator.Container.GetInstance<IPricingRequester>();
            _tradesReq = ServiceLocator.Container.GetInstance<ITradesRequester>();

            //var list = new List<string>();
            //list.Subscribe(add => Console.WriteLine());

//            _pricesCache.Cache.Where(s=>s.Key=="EUR_USD")
   //             .SelectMany(d=>d.Value).Subscribe(d => Console.WriteLine($"{d.Symbol}: trad"));
        }

        private readonly ILogger _logger;
        private readonly IPricesCache _pricesCache;
        private readonly ISettingsService _settingsService;
        private readonly IAccountsRequester _accountReq;
        private readonly IInstrumentRequester _instrumentReq;
        private readonly IOrdersRequester _ordersReq;
        private readonly IPositionsRequester _positionReq;
        private readonly IPricingRequester _pricingReq;
        private readonly ITradesRequester _tradesReq;
        private readonly IPricingStreamingRequester _pricingStreamReq;
        private readonly ITransactionsRequester _txReq;
        private readonly ITransactionsStreamingRequester _txStreamReq;

        private void buttonConnectDemo_Click(object sender, EventArgs e)
        {
            _settingsService.CachedSettings.SelectedEnvironmentKey = Environments.Practice.Key;
            PrintCurrentEnvironment();
        }

        private void buttonConnectLive_Click(object sender, EventArgs e)
        {
            _settingsService.CachedSettings.SelectedEnvironmentKey = Environments.Live.Key;
            PrintCurrentEnvironment();
        }


        private void buttonAccount_Click(object sender, EventArgs e)
        {
            var cfg = _settingsService.CachedSettings.SelectedEnvironment;


            _logger.Information("GetAccounts");
            var accounts = _accountReq.GetAccounts();

            textBoxAcc.Clear();
            textBoxAcc.Text = cfg.EnvironmentKey + Environment.NewLine;
            foreach (var account in accounts.accounts)
            {
                var details = _accountReq.GetAccountDetails(account.id);
                textBoxAcc.Text += details + Environment.NewLine;

                var summary = _accountReq.GetAccountSummary(account.id);
                textBoxAcc.Text += summary + Environment.NewLine;

            }
        }


        private void buttonSubscribe_Click(object sender, EventArgs e)
        {
            TestTransactionStream();
        }

        private void TestTransactionStream()
        {
            var cfg = _settingsService.CachedSettings.SelectedEnvironment;

            try
            {
                ObservableStream<TransactionsResponse.Transaction> tss = _txStreamReq.GetTransactionStream(cfg.DefaultAccountId);
                tss.NewValue += Tss_NewTransaction;

            }
            catch (WebException wex)
            {
                Console.WriteLine(wex);
                using var str = new StreamReader(wex.Response.GetResponseStream());
                var content = str.ReadToEnd();
                Debug.WriteLine(wex.Response.ResponseUri.ToString());
                Debug.WriteLine(content);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());

            }

        }

        private void Tss_NewTransaction(object sender, StreamEventArgs<TransactionsResponse.Transaction> e)
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
                var cfg = _settingsService.CachedSettings.SelectedEnvironment;

                var txs = _txReq.GetTransactions(cfg.DefaultAccountId, trx.lastTransactionID);
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
            var cfg = _settingsService.CachedSettings.SelectedEnvironment;

            try
            {
                ObservableStream<PricesResponse.Price> pss = _pricingStreamReq.GetPriceStream(cfg.DefaultAccountId, "EUR_USD,USD_CAD");
                pss.NewValue += Pss_NewPrice;
            }
            catch (WebException wex)
            {
                Console.WriteLine(wex);
                using var str = new StreamReader(wex.Response.GetResponseStream());
                var content = str.ReadToEnd();
                Debug.WriteLine(wex.Response.ResponseUri.ToString());
                Debug.WriteLine(content);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());

            }

        }

        private void Pss_NewPrice(object sender, StreamEventArgs<PricesResponse.Price> e)
        {
            PricesResponse.Price pr = e.Obj;
            if (pr.EventType != AppProperties.HeartbeatName)
            {
                string line = pr.ToString();
                _pricesCache.CacheIt(pr);

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
            var cfg = _settingsService.CachedSettings.SelectedEnvironment;

            var openPos = _positionReq.GetOpenPositions(cfg.DefaultAccountId);

            textBoxPositions.Text = openPos.ToString();
            if (!openPos.positions.Any())
            {
                textBoxPositions.Text += $"{Environment.NewLine} {openPos.positions.Length} open positions";

            }


            var resp = _positionReq.GetPositions(cfg.DefaultAccountId);

            textBoxPositions.Text += $"{Environment.NewLine} {resp.positions.Length} positions";

        }

        private void FormTerminal_Load(object sender, EventArgs e)
        {
            PrintCurrentEnvironment();
        }

        private void PrintCurrentEnvironment()
        {
            var cfg = _settingsService.CachedSettings.SelectedEnvironment;

            toolStripStatusLabel1.Text = $"Using {cfg.EnvironmentKey}";
        }

        private void buttonCandle_Click(object sender, EventArgs e)
        {
            CandlesResponse candles = _instrumentReq.GetCandles("EUR_USD", CandlestickGranularity.S10, "BAM", 10);
            var asd = candles.ToString();
            Debug.WriteLine(asd);
        }

        private void loadCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var engine = new FileHelperEngine<CandleDataRecord>();
            IFileReaderWriterService frw = new FileReaderWriterService();
            string hdPath = frw.GetHistoricalDataFolderPath();
            var records = engine.ReadFile(Path.Combine(hdPath, "EURUSD2022.txt")); // todo hardcoded


            List<CandleDataRecord> recordList = records.ToList();
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFileReaderWriterService frw = new FileReaderWriterService();
            string hdPath = frw.GetHistoricalDataFolderPath();

            OpenExplorerInFolder(hdPath); 
        }

        private void OpenExplorerInFolder(string root)
        {
            Process.Start("explorer.exe", root);
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            _txStreamReq.Unsubscribe();
            _pricingStreamReq.Unsubscribe();


        }
    }
}
