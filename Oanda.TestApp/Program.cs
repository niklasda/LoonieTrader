using System;
using System.Net;
using Oanda.RestLibrary.Configuration;
using Oanda.RestLibrary.Requester;

namespace Oanda.TespApp
{
    internal class Program
    {
        private static void Main()
        {
            // Invoke-RestMethod -H @{"Authorization"="Bearer 3e5ab0c7bd2d6584e0e02578b316577f-21e638b773cf11229fe63f0ddd6e2245"} https://api-fxpractice.oanda.com/v3/accounts

            try
            {
                var cr = new ConfigurationReader();
                var cfg = cr.ReadConfiguration();

                Console.WindowHeight = 80;
                Console.WindowWidth = 200;

                var ar = new OandaRequester();
                Console.WriteLine(ar.GetAccounts());
                Console.WriteLine(ar.GetAccountSummary());
                Console.WriteLine(ar.GetPositions());
                Console.WriteLine(ar.GetOpenPositions());
                Console.WriteLine(ar.GetOrders());
                Console.WriteLine(ar.GetPendingOrders());
                Console.WriteLine(ar.GetTransactionPages());
                Console.WriteLine(ar.GetTransactions());
                Console.WriteLine(ar.GetTrades());

                var arl = new OandaRequesterLive();
                Console.WriteLine(arl.GetInstruments());

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
    }
}
