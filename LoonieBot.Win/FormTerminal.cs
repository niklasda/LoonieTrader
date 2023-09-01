using LoonieBot.Win.Locator;
using LoonieTrader.Library.Constants;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.Services;

namespace LoonieBot.Win
{
    public partial class FormTerminal : Form
    {
        public FormTerminal()
        {
            InitializeComponent();

            SettingsService = ServiceLocator.Container.GetInstance<ISettingsService>();
        }

        protected ISettingsService SettingsService;
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


        private void buttonAccount_Click(object sender, EventArgs e)
        {


            IExtendedLogger logger = ServiceLocator.Container.GetInstance<IExtendedLogger>();

            var ar = ServiceLocator.Container.GetInstance<IAccountsRequester>();
            //var cfgs = ServiceLocator.Container.GetInstance<ISettingsService>();
            //var cfg = cfgs.CachedSettings.SelectedEnvironment;

            logger.Information("GetAccounts");
            var accounts = ar.GetAccounts();

            textBox1.Clear();
            foreach (var account in accounts.accounts)
            {
                var details = ar.GetAccountDetails(account.id);
                textBox1.Text += details.ToString() + Environment.NewLine;

                var summary = ar.GetAccountSummary(account.id);
                textBox1.Text += summary.ToString() + Environment.NewLine;

            }

            //            logger.Information($"GetAccountDetails {cfg.DefaultAccountId}");
            //          Console.WriteLine(ar.GetAccountDetails(cfg.DefaultAccountId));

            //        logger.Information("GetAccountSummary");
            //      Console.WriteLine(ar.GetAccountSummary(cfg.DefaultAccountId));
        }

        private void buttonSubscribe_Click(object sender, EventArgs e)
        {
            //var cfgs = ServiceLocator.Container.GetInstance<ISettingsService>();
            var cfg = SettingsService.CachedSettings.SelectedEnvironment;

            var txr = ServiceLocator.Container.GetInstance<ITransactionsRequester>();

            var txFirst = txr.GetTransactionPages(cfg.DefaultAccountId);
         //   txs.lastTransactionID

            var txs = txr.GetTransactions(cfg.DefaultAccountId, txFirst.lastTransactionID);

            textBox2.Text = txs.ToString();
        }

        private void buttonConnectDemo_Click(object sender, EventArgs e)
        {
          //  var cfgs = ServiceLocator.Container.GetInstance<ISettingsService>();
            var cfgBefore = SettingsService.CachedSettings.SelectedEnvironment;

            SettingsService.CachedSettings.SelectedEnvironmentKey = Environments.Practice.Key;

            var cfgAfter = SettingsService.CachedSettings.SelectedEnvironment;


        }

        private void buttonConnectLive_Click(object sender, EventArgs e)
        {
            //var cfgs = ServiceLocator.Container.GetInstance<ISettingsService>();
            var cfgBefore = SettingsService.CachedSettings.SelectedEnvironment;

            SettingsService.CachedSettings.SelectedEnvironmentKey = Environments.Live.Key;

            var cfgAfter = SettingsService.CachedSettings.SelectedEnvironment;

        }
    }
}
