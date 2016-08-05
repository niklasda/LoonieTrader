using System;
using System.Net;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.RestRequesters.v3;
using LoonieTrader.TestApp.Locator;

namespace LoonieTrader.TestApp
{
    internal class Program
    {
        private static void Main()
        {
            // Invoke-RestMethod -H @{"Authorization"="Bearer 3e5ab0c7bd2d6584e0e02578b316577f-21e638b773cf11229fe63f0ddd6e2245"} https://api-fxpractice.oanda.com/v3/accounts

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

              //  var cr = new ConfigurationReader();
               // var cfg = cr.ReadConfiguration();

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
