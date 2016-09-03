using System;
using System.Net;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.RestApi.Interfaces;
using LoonieTrader.RestLibrary.RestApi.Requesters;
using LoonieTrader.TestApp.Locator;
using Serilog;

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
                IExtendedLogger logger = container.GetInstance<IExtendedLogger>();

                ResizeWindow();

                logger.Information("GetAccounts");
                Console.WriteLine(ar.GetAccounts());
                logger.Information("GetAccountDetails");
                Console.WriteLine(ar.GetAccountDetails(cfg.DefaultAccountId));
                logger.Information("GetAccountSummary");
                Console.WriteLine(ar.GetAccountSummary(cfg.DefaultAccountId));
                logger.Information("GetPositions");
                Console.WriteLine(por.GetPositions(cfg.DefaultAccountId));
                logger.Information("GetOpenPositions");
                Console.WriteLine(por.GetOpenPositions(cfg.DefaultAccountId));
                logger.Information("GetOrders");
                Console.WriteLine(or.GetOrders(cfg.DefaultAccountId));
                logger.Information("GetPendingOrders");
                Console.WriteLine(or.GetPendingOrders(cfg.DefaultAccountId));
                logger.Information("GetTransactionPages");
                Console.WriteLine(txr.GetTransactionPages(cfg.DefaultAccountId));
                logger.Information("GetTransactions");
                Console.WriteLine(txr.GetTransactions(cfg.DefaultAccountId));
                logger.Information("GetTrades");
                Console.WriteLine(tr.GetTrades(cfg.DefaultAccountId));

                logger.Information("GetInstruments");
                Console.WriteLine(ar.GetInstruments(cfg.DefaultAccountId));
                logger.Information("GetPrices");
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
