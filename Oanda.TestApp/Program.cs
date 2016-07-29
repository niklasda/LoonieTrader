using System;
using System.Net;
using LoonieTrader.RestLibrary.Configuration;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Requester;
using LoonieTrader.TestApp.Locator;

namespace LoonieTrader.TespApp
{
    internal class Program
    {
        private static void Main()
        {
            // Invoke-RestMethod -H @{"Authorization"="Bearer 3e5ab0c7bd2d6584e0e02578b316577f-21e638b773cf11229fe63f0ddd6e2245"} https://api-fxpractice.oanda.com/v3/accounts

            try
            {
                var container = ServiceLocator.Initialize();
                var arl = container.GetInstance<IOandaRequesterLive>();
                var ar = container.GetInstance<IOandaRequester>();
                var cfg = container.GetInstance<ISettings>();

              //  var cr = new ConfigurationReader();
               // var cfg = cr.ReadConfiguration();

                ResizeWindow();
                

                //    var ar = new OandaRequester();
                Console.WriteLine(ar.GetAccounts());
                Console.WriteLine(ar.GetAccountSummary(cfg.DefaultAccountId));
                Console.WriteLine(ar.GetPositions(cfg.DefaultAccountId));
                Console.WriteLine(ar.GetOpenPositions(cfg.DefaultAccountId));
                Console.WriteLine(ar.GetOrders(cfg.DefaultAccountId));
                Console.WriteLine(ar.GetPendingOrders(cfg.DefaultAccountId));
                Console.WriteLine(ar.GetTransactionPages(cfg.DefaultAccountId));
                Console.WriteLine(ar.GetTransactions(cfg.DefaultAccountId));
                Console.WriteLine(ar.GetTrades(cfg.DefaultAccountId));

                //var arl = new OandaRequesterLive();
                Console.WriteLine(arl.GetInstruments(cfg.DefaultAccountId));

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
                Console.BufferHeight = 80;
                Console.BufferWidth = 200;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
