using LoonieBot.Win.Locator;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;

namespace LoonieBot.Win
{
    public partial class FormTerminal : Form
    {
        public FormTerminal()
        {
            InitializeComponent();
        }

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
            var cfgs = ServiceLocator.Container.GetInstance<ISettingsService>();
            var cfg = cfgs.CachedSettings.SelectedEnvironment;

            var txr = ServiceLocator.Container.GetInstance<ITransactionsRequester>();

            var txs = txr.GetTransactions(cfg.DefaultAccountId);
            textBox2.Text = txs.ToString();
        }
    }
}
