using System.Text;

namespace LoonieTrader.RestLibrary.Models.Responses
{
    public class OrdersPendingResponse
    {
        public string lastTransactionID { get; set; }
        public PendingOrder[] orders { get; set; }

        public override string ToString()
        {
            var resp = new StringBuilder();
            resp.Append("lastTransactionID: ");
            resp.AppendLine(lastTransactionID);

            foreach (var order in orders)
            {
                resp.Append("id: ");
                resp.Append(order.id);
                resp.Append(", tradeId: ");
                resp.Append(order.tradeID);
                resp.Append(", instrument: ");
                resp.Append(order.instrument);
                resp.Append(", type: ");
                resp.Append(order.type);
                resp.Append(", state: ");
                resp.AppendLine(order.state);
            }

            return resp.ToString();
        }

        public class PendingOrder
        {
            public string createTime { get; set; }
            public string gtdTime { get; set; }
            public string id { get; set; }
            public string instrument { get; set; }
            public string partialFill { get; set; }
            public string positionFill { get; set; }
            public string price { get; set; }
            public string state { get; set; }
            public string timeInForce { get; set; }
            public string tradeID { get; set; }
            public string triggerCondition { get; set; }
            public string type { get; set; }
            public string units { get; set; }
        }
    }
}

/*

{"lastTransactionID":"181","orders":[{"createTime":"2016-08-07T21:00:00.927691378Z","id":"177","price":"1.00000","state":"PENDING","timeInForce":"GTC","tradeID":"176","triggerCondition":"TRIGGER_DEFAULT","type":"STOP_LOSS"}]}

{"lastTransactionID":"16","orders":[]}

{"lastTransactionID":"17","orders":[
{"createTime":"2016-07-23T16:38:56.395363771Z",
"gtdTime":"2016-07-30T16:38:52.000000000Z",
"id":"17",
"instrument":"EUR_USD",
"partialFill":"DEFAULT_FILL",
"positionFill":"POSITION_DEFAULT",
"price":"1.10760",
"state":"PENDING",
"timeInForce":"GTD",
"triggerCondition":"TRIGGER_DEFAULT",
"type":"MARKET_IF_TOUCHED","units":"10000"}]}
*/
