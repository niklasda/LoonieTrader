using System.Net;
using System.CommandLine;
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
        RootCommand rootCommand = new RootCommand(description: "Converts an image file from one format to another.");

        Option inputOption = new Option<string>(aliases: new string[] { "--input", "-i" }
            , description: "The path to the image file that is to be converted.");
//            , argument: new Argument<FileInfo>());
        rootCommand.AddOption(inputOption);

        Option outputOption = new Option<string>(aliases: new string[] { "--output", "-o" }
            , description: "The target name of the output file after conversion.");
 //           , argument: new Argument<FileInfo>());
        rootCommand.AddOption(outputOption);

        Option xCropSizeOption = new Option<string>(aliases: new string[] { "--x-crop-size", "-x" }
            , description: "The x dimension size to crop the picture. The default is 0 indicating no cropping is required.");
//            , argument: new Argument<FileInfo>());
        rootCommand.AddOption(xCropSizeOption);
        
        Option yCropSizeOption = new Option<string>(aliases: new string[] { "--y-crop-size", "-y" }
            , description: "The Y dimension size to crop the picture. The default is 0 indicating no cropping is required.");
//            , argument: new Argument<FileInfo>());

        
        rootCommand.AddOption(yCropSizeOption);



        Command cmd = new Command(name: "account", description: "acc");
        rootCommand.AddCommand(cmd);
        */


//    https://github.com/dotnet/command-line-api/blob/main/docs/DragonFruit-overview.md
//
 //       rootCommand.Handler = CommandHandler.Create<FileInfo, FileInfo, int, int>(Convert);

        /*
        RootCommand rootCommand = new RootCommand(
            description: "Converts an image file from one format to another."
            , treatUnmatchedTokensAsErrors: true);
        MethodInfo method = typeof(Program).GetMethod(nameof(Convert));
        rootCommand.ConfigureFromMethod(method);
        rootCommand.Children["--input"].AddAlias("-i");
        rootCommand.Children["--output"].AddAlias("-o");
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