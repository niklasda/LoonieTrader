using System.Text;

namespace LoonieTrader.RestLibrary.Models.Responses
{
    public class PositionsResponse
    {
        public string lastTransactionID { get; set; }
        public Position[] positions { get; set; }

        public override string ToString()
        {
            var resp = new StringBuilder();
            resp.Append("lastTransactionID: ");
            resp.AppendLine(lastTransactionID);

            foreach (var position in positions)
            {
                resp.AppendLine("Instrument: " + position.instrument);
                resp.AppendLine("P/L: " + position.pl);
                resp.AppendLine("resettable P/L: " + position.resettablePL);
                resp.AppendLine("unrealized P/L: " + position.unrealizedPL);
                resp.AppendLine("long units: " + position.@long.units);
                resp.AppendLine("long P/L: " + position.@long.pl);
                resp.AppendLine("short units: " + position.@short.units);
                resp.AppendLine("short P/L: " + position.@short.pl);
            }

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
            public string pl { get; set; }
            public string resettablePL { get; set; }
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
}

/*
{"lastTransactionID":"16",
"positions":
[{"instrument":"EUR_USD",
"long":{"pl":"0.0000","resettablePL":"0.0000","units":"0","unrealizedPL":"0.0000"},
"pl":"26.3639",
"resettablePL":"26.3639",
"short":{"pl":"26.3639","resettablePL":"26.3639","units":"0","unrealizedPL":"0.0000"},
"unrealizedPL":"0.0000"}]}
*/
