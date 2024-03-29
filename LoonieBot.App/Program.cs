﻿using System.Net;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Requesters;
using LoonieBot.App.Locator;

namespace LoonieBot.App;

public static class Program
{
    private static void Main()
    {
        /*
        */


//    https://github.com/dotnet/command-line-api/blob/main/docs/DragonFruit-overview.md
//
 //       rootCommand.Handler = CommandHandler.Create<FileInfo, FileInfo, int, int>(Convert);

        /*
        */

        //return await rootCommand.InvokeAsync(args);



        Console.ReadLine();
    }

    private static void DoAllTheStuffs()
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

            var cfgs = container.GetInstance<ISettingsService>();
            var cfg = cfgs.CachedSettings.SelectedEnvironment;

            IExtendedLogger logger = container.GetInstance<IExtendedLogger>();



            logger.Information("GetAccounts");
            Console.WriteLine(ar.GetAccounts());

            logger.Information($"GetAccountDetails {cfg.DefaultAccountId}");
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
            Console.WriteLine(txr.GetTransactions(cfg.DefaultAccountId, "94"));

            logger.Information("GetTrades");
            Console.WriteLine(tr.GetTrades(cfg.DefaultAccountId));

            logger.Information("GetInstruments");
            Console.WriteLine(ar.GetAccountInstruments(cfg.DefaultAccountId));
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

    }


}