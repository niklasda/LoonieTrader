using System.Text;

namespace LoonieTrader.RestLibrary.Models.Responses
{
    public class OrderDetailsResponse
    {
        public string lastTransactionID { get; set; }
        public Order order { get; set; }

        public override string ToString()
        {
            var resp = new StringBuilder();
            resp.Append("lastTransactionID: ");
            resp.AppendLine(lastTransactionID);

            resp.Append("id: ");
            resp.Append(order.id);
            resp.Append(", instrument: ");
            resp.Append(order.instrument);
            resp.Append(", type: ");
            resp.AppendLine(order.type);

            return resp.ToString();
        }

        public class Order
        {
            public string createTime { get; set; }
            public string filledTime { get; set; }
            public string fillingTransactionID { get; set; }
            public string id { get; set; }
            public string instrument { get; set; }
            public string partialFill { get; set; }
            public string positionFill { get; set; }
            public string price { get; set; }
            public string state { get; set; }
            public Stoplossonfill stopLossOnFill { get; set; }
            public string timeInForce { get; set; }
            public string tradeOpenedID { get; set; }
            public string triggerCondition { get; set; }
            public string type { get; set; }
            public string units { get; set; }
        }

        public class Stoplossonfill
        {
            public string price { get; set; }
            public string timeInForce { get; set; }
        }
    }
}

/*
 {"lastTransactionID":"63","order":{"createTime":"2016-08-05T20:16:51.295686437Z","filledTime":"2016-08-05T20:16:51.295686437Z","fillingTransactionID":"62","id":"61","instrument":"EUR_USD","partialFill":"DEFAULT_FILL","positionFill":"POSITION_DEFAULT","price":"1.20000","state":"FILLED","stopLossOnFill":{"price":"1.00000","timeInForce":"GTC"},"timeInForce":"GTC","tradeOpenedID":"62","triggerCondition":"TRIGGER_DEFAULT","type":"LIMIT","units":"1000"}}
*/
