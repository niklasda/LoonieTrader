﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lamar;
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
            var cfgs = ServiceLocator.Container.GetInstance<ISettingsService>();
            var cfg = cfgs.CachedSettings.SelectedEnvironment;

            logger.Information("GetAccounts");
            var accounts = ar.GetAccounts();

            textBox1.Clear();
            foreach (var account in accounts.accounts)
            {
                var details = ar.GetAccountDetails(account.id);
                textBox1.Text += details.ToString() + Environment.NewLine;

                var summary = ar.GetAccountSummary(cfg.DefaultAccountId);
                textBox1.Text += summary.ToString() + Environment.NewLine;

            }

            //            logger.Information($"GetAccountDetails {cfg.DefaultAccountId}");
            //          Console.WriteLine(ar.GetAccountDetails(cfg.DefaultAccountId));

            //        logger.Information("GetAccountSummary");
            //      Console.WriteLine(ar.GetAccountSummary(cfg.DefaultAccountId));
        }
    }
}