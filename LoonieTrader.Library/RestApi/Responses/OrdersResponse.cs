using System.Text;
using JetBrains.Annotations;

namespace LoonieTrader.Library.RestApi.Responses
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature, ImplicitUseTargetFlags.WithMembers)]
    public class OrdersResponse
    {
        public string lastTransactionID { get; set; }
        public Order[] orders { get; set; }

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

        public class StopLossOnFill
        {
            public string price { get; set; }
            public string timeInForce { get; set; }
        }

        public class TakeProfitOnFill
        {
            public string price { get; set; }
            public string timeInForce { get; set; }
        }

        public class Order
        {
            public string createTime { get; set; }
            public string gtdTime { get; set; }
            public string id { get; set; }
            public string instrument { get; set; }
            public string partialFill { get; set; }
            public string positionFill { get; set; }
            public string price { get; set; }
            public string state { get; set; }
            public StopLossOnFill stopLossOnFill { get; set; }
            public TakeProfitOnFill takeProfitOnFill { get; set; }
            public string timeInForce { get; set; }
            public string tradeID { get; set; }
            public string triggerCondition { get; set; }
            public string type { get; set; }
            public string units { get; set; }
        }
    }
}

/*
{"lastTransactionID":"181",
"orders":[{"createTime":"2016-08-07T21:00:00.927691378Z","id":"179","price":"1.00000","state":"PENDING","timeInForce":"GTC","tradeID":"178","triggerCondition":"TRIGGER_DEFAULT","type":"STOP_LOSS"}]}

{"lastTransactionID":"8",
"orders":
[{
"createTime":"2016-07-17T19:33:24.224580145Z",
"gtdTime":"2016-07-24T19:33:22.000000000Z",
"id":"6",
"instrument":"EUR_USD",
"partialFill":"DEFAULT_FILL",
"positionFill":"POSITION_DEFAULT",
"price":"1.10289",
"state":"PENDING",
"stopLossOnFill":{"price":"1.10500","timeInForce":"GTC"},
"takeProfitOnFill":{"price":"1.10000","timeInForce":"GTC"},
"timeInForce":"GTD",
"triggerCondition":"TRIGGER_DEFAULT",
"type":"MARKET_IF_TOUCHED",
"units":"-10000"
}]}
*/
