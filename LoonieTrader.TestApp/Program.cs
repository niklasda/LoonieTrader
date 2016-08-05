using System;
using System.Net;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.RestRequesters;
using LoonieTrader.TestApp.Locator;

namespace LoonieTrader.TestApp
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                var container = ServiceLocator.Initialize();
                var ar = container.GetInstance<IAccountsRequester>();
                var or = container.GetInstance<IOrdersRequester>();
                var por = container.GetInstance<IPositionsRequester>();
                var prr = container.GetInstance<IPricingRequester>();
                var tr = container.GetInstance<TradesRequester>();
                var txr = container.GetInstance<ITransactionsRequester>();

                var cfg = container.GetInstance<ISettings>();


                ResizeWindow();

                Console.WriteLine(ar.GetAccounts());
                Console.WriteLine(ar.GetAccountDetails(cfg.DefaultAccountId));
                Console.WriteLine(ar.GetAccountSummary(cfg.DefaultAccountId));
                Console.WriteLine(por.GetPositions(cfg.DefaultAccountId));
                Console.WriteLine(por.GetOpenPositions(cfg.DefaultAccountId));
                Console.WriteLine(or.GetOrders(cfg.DefaultAccountId));
                Console.WriteLine(or.GetPendingOrders(cfg.DefaultAccountId));
                Console.WriteLine(txr.GetTransactionPages(cfg.DefaultAccountId));
                Console.WriteLine(txr.GetTransactions(cfg.DefaultAccountId));
                Console.WriteLine(tr.GetTrades(cfg.DefaultAccountId));

                Console.WriteLine(ar.GetInstruments(cfg.DefaultAccountId));
                Console.WriteLine(prr.GetPrices(cfg.DefaultAccountId, "EUR_USD"));

                //GetPrices();
                //GetCandles();
                //GetInstruments();
            }
            catch (WebException wex)
            {
                Console.WriteLine(wex.Message);

                HttpWebResponse resp = (HttpWebResponse)wex.Response;

                Console.WriteLine(resp.ResponseUri);
                Console.WriteLine("{0} ({1})", resp.StatusCode, (int)resp.StatusCode);

                Console.WriteLine(resp.Server);
            }

            Console.ReadLine();
        }

        private static void ResizeWindow()
        {
            try
            {
                Console.WindowHeight = 80;
                Console.WindowWidth = 200;
                Console.BufferHeight = 800;
                Console.BufferWidth = 200;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
