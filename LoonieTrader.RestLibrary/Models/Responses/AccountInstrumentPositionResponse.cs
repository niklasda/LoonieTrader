using System.Text;

namespace LoonieTrader.RestLibrary.Models.Responses
{
    public class AccountInstrumentPositionResponse
    {
        public string lastTransactionID { get; set; }
        public Position position { get; set; }

        public override string ToString()
        {
            var resp = new StringBuilder();
            resp.Append("lastTransactionID: ");
            resp.AppendLine(lastTransactionID);

            resp.AppendLine("Instrument: " + position.instrument);
            resp.AppendLine("P/L: " + position.pl);
            resp.AppendLine("resettable P/L: " + position.resettablePL);
            resp.AppendLine("unrealized P/L: " + position.unrealizedPL);
            resp.AppendLine("long P/L: " + position.@long.pl);
            resp.AppendLine("short P/L: " + position.@short.pl);

            return resp.ToString();
        }

        public class Position
        {
            public string instrument { get; set; }
            public Long @long { get; set; }
            public string pl { get; set; }
            public string resettablePL { get; set; }
            public Short @short { get; set; }
            public string unrealizedPL { get; set; }
        }

        public class Long
        {
            public string averagePrice { get; set; }
            public string pl { get; set; }
            public string resettablePL { get; set; }
            public string[] tradeIDs { get; set; }
            public string units { get; set; }
            public string unrealizedPL { get; set; }
        }

        public class Short
        {
            public string pl { get; set; }
            public string resettablePL { get; set; }
            public string units { get; set; }
            public string unrealizedPL { get; set; }
        }

    }

    /*

    {"lastTransactionID":"53","position":{"instrument":"EUR_USD","long":{"averagePrice":"1.10806","pl":"-1.7538","resettablePL":"-1.7538","tradeIDs":["18","29","31","33","35","37"],"units":"15000","unrealizedPL":"14.4239"},"pl":"24.6101","resettablePL":"24.6101","short":{"pl":"26.3639","resettablePL":"26.3639","units":"0","unrealizedPL":"0.0000"},"unrealizedPL":"14.4239"}}

    */
}