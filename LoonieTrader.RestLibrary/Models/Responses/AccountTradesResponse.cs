using System.Text;

namespace LoonieTrader.RestLibrary.Models.Responses
{
    public class AccountTradesResponse
    {
        public string lastTransactionID { get; set; }
        public Position[] trades { get; set; }

        public override string ToString()
        {
            var resp = new StringBuilder();
            resp.Append("lastTransactionID: ");
            resp.AppendLine(lastTransactionID);

            foreach (var trade in trades)
            {
//                resp.AppendLine("Instrument: "+ position.instrument);
  //              resp.AppendLine("P/L: "+ position.pl);
    //            resp.AppendLine("resettable P/L: "+ position.resettablePL);
      //          resp.AppendLine("unrealized P/L: "+ position.unrealizedPL);
        //        resp.AppendLine("long P/L: "+ position.@long.pl);
          //      resp.AppendLine("short P/L: "+ position.@short.pl);
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

    /*
     * {"lastTransactionID":"17","trades":[]}
    */
}
