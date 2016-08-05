using System.Text;

namespace LoonieTrader.RestLibrary.Models.Responses
{
    public class AccountOpenTradesResponse
    {
        public string lastTransactionID { get; set; }
        public Trade[] trades { get; set; }

        public override string ToString()
        {
            var resp = new StringBuilder();
            resp.Append("lastTransactionID: ");
            resp.AppendLine(lastTransactionID);

            foreach (var trade in trades)
            {
                resp.AppendLine("id: " + trade.id);
                resp.AppendLine("Instrument: " + trade.instrument);
                resp.AppendLine("financing " + trade.financing);
                resp.AppendLine("price: " + trade.price);
                resp.AppendLine("unrealized P/L: " + trade.unrealizedPL);
                resp.AppendLine("currentUnits: " + trade.currentUnits);
                resp.AppendLine("state: " + trade.state);
            }

            return resp.ToString();
        }

        public class Trade
        {
            public string currentUnits { get; set; }
            public string financing { get; set; }
            public string id { get; set; }
            public string initialUnits { get; set; }
            public string instrument { get; set; }
            public string openTime { get; set; }
            public string price { get; set; }
            public string realizedPL { get; set; }
            public string state { get; set; }
            public string unrealizedPL { get; set; }
        }

    }

/*
    {"lastTransactionID":"53",
    "trades":[{"currentUnits":"1000",
    "financing":"0.0000",
    "id":"37",
    "initialUnits":"1000",
    "instrument":"EUR_USD",
    "openTime":"2016-08-05T12:55:42.268686450Z","price":"1.10852","realizedPL":"0.0000","state":"OPEN","unrealizedPL":"0.2615"},
    {"currentUnits":"1000","financing":"0.0000","id":"35","initialUnits":"1000","instrument":"EUR_USD","openTime":"2016-08-05T12:54:13.153542589Z","price":"1.10876","realizedPL":"0.0000","state":"OPEN","unrealizedPL":"0.0451"},
    {"currentUnits":"1000","financing":"0.0000","id":"33","initialUnits":"1000","instrument":"EUR_USD","openTime":"2016-08-05T12:41:33.884972931Z","price":"1.10928","realizedPL":"0.0000","state":"OPEN","unrealizedPL":"-0.4239"},
    {"currentUnits":"1000","financing":"0.0000","id":"31","initialUnits":"1000","instrument":"EUR_USD","openTime":"2016-08-05T12:39:02.397924615Z","price":"1.10931","realizedPL":"0.0000","state":"OPEN","unrealizedPL":"-0.4509"},
    {"currentUnits":"1000","financing":"0.0000","id":"29","initialUnits":"1000","instrument":"EUR_USD","openTime":"2016-08-05T12:38:32.458162450Z","price":"1.10898","realizedPL":"0.0000","state":"OPEN","unrealizedPL":"-0.1533"},
    {"currentUnits":"10000","financing":"-0.8610","id":"18","initialUnits":"10000","instrument":"EUR_USD","openTime":"2016-07-28T00:38:59.080166841Z","price":"1.10761","realizedPL":"0.0000","state":"OPEN","unrealizedPL":"10.8210"}]}
*/
}
