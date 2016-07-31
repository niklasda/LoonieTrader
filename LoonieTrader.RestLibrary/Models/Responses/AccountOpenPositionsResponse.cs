using System.Text;

namespace LoonieTrader.RestLibrary.Models.Responses
{
    public class AccountOpenPositionsResponse
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
                resp.AppendLine("Instrument: "+ position.instrument);
                resp.AppendLine("P/L: "+ position.pl);
                resp.AppendLine("resettable P/L: "+ position.resettablePL);
                resp.AppendLine("unrealized P/L: "+ position.unrealizedPL);
                resp.AppendLine("long P/L: "+ position.@long.pl);
                resp.AppendLine("short P/L: "+ position.@short.pl);
            }

            return resp.ToString();
        }
    }

/*
{"lastTransactionID":"16","positions":[]}
*/
}
