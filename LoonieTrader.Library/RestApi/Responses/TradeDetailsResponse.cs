using System.Text;

namespace LoonieTrader.Library.RestApi.Responses
{
    public class TradeDetailsResponse
    {
        public string lastTransactionID { get; set; }
        public Trade trade { get; set; }

        public override string ToString()
        {
            var resp = new StringBuilder();
            resp.Append("lastTransactionID: ");
            resp.AppendLine(lastTransactionID);

            resp.AppendLine("id: " + trade.id);
            resp.AppendLine("Instrument: " + trade.instrument);
            resp.AppendLine("financing " + trade.financing);
            resp.AppendLine("price: " + trade.price);
            resp.AppendLine("unrealized P/L: " + trade.unrealizedPL);
            resp.AppendLine("currentUnits: " + trade.currentUnits);
            resp.AppendLine("state: " + trade.state);

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
}

/*
{"lastTransactionID":"53",
"trade":{"currentUnits":"1000",
"financing":"0.0000",
"id":"37",
"initialUnits":"1000",
"instrument":"EUR_USD",
"openTime":"2016-08-05T12:55:42.268686450Z",
"price":"1.10852",
"realizedPL":"0.0000",
"state":"OPEN",
"unrealizedPL":"0.0902"}}
*/