using System.Text;

namespace LoonieTrader.RestLibrary.Models.Responses
{
    public class AccountCreateOrdersResponse
    {
        public string lastTransactionID { get; set; }
        public Ordercreatetransaction orderCreateTransaction { get; set; }
        public Orderfilltransaction orderFillTransaction { get; set; }
        public Ordercanceltransaction orderCancelTransaction { get; set; }
        public string[] relatedTransactionIDs { get; set; }

        public override string ToString()
        {
            var resp = new StringBuilder();
            resp.Append("lastTransactionID: ");
            resp.AppendLine(lastTransactionID);
            resp.Append("relatedTransactionIDs: ");
            resp.AppendLine("#" + relatedTransactionIDs.Length);
            if (orderCreateTransaction != null)
            {
                resp.Append("orderCreateTransaction: ");
                resp.AppendLine(orderCreateTransaction.id);
            }
            if (orderFillTransaction != null)
            {
                resp.Append("orderFillTransaction: ");
                resp.AppendLine(orderFillTransaction.id);
            }
            if (orderCancelTransaction != null)
            {
                resp.Append("orderCancelTransaction: ");
                resp.AppendLine(orderCancelTransaction.orderID);
            }

            return resp.ToString();
        }

        public class OrderDefinition
        {
            public OrderDefinition()
            {
                order = new Order();
                order.stopLossOnFill = new StopLossOnFill();
            }

            public Order order { get; set; }

            public class Order
            {
                public string price { get; set; } // not for market orders
                public string units { get; set; }
                public string instrument { get; set; }
                public string timeInForce { get; set; }
                public string type { get; set; }
                public string positionFill { get; set; }
                public StopLossOnFill stopLossOnFill { get; set; }
            }

            public class StopLossOnFill
            {
                public string timeInForce { get; set; }
                public string price { get; set; }
            }
        }


        public class Ordercreatetransaction
        {
            public string accountID { get; set; }
            public string batchID { get; set; }
            public string id { get; set; }
            public string instrument { get; set; }
            public string positionFill { get; set; }
            public string reason { get; set; }
            public string time { get; set; }
            public string timeInForce { get; set; }
            public string type { get; set; }
            public string units { get; set; }
            public int userID { get; set; }
        }

        public class Orderfilltransaction
        {
            public string accountBalance { get; set; }
            public string accountID { get; set; }
            public string batchID { get; set; }
            public string financing { get; set; }
            public string id { get; set; }
            public string instrument { get; set; }
            public string orderID { get; set; }
            public string pl { get; set; }
            public string price { get; set; }
            public string reason { get; set; }
            public string time { get; set; }
            public Tradeopened tradeOpened { get; set; }
            public string type { get; set; }
            public string units { get; set; }
            public int userID { get; set; }
        }

        public class Ordercanceltransaction
        {
            //"accountID":"101-004-3904511-001","batchID":"40","id":"41","orderID":"40","reason":"STOP_LOSS_ON_FILL_LOSS","time":"2016-08-05T13:29:31.477223240Z","type":"ORDER_CANCEL","userID":3904511}
            public string accountID { get; set; }
            public string batchID { get; set; }
            public string orderID { get; set; }
            public string reason { get; set; }
            public string time { get; set; }
            public string type { get; set; }
            public int userID { get; set; }
        }

        public class Tradeopened
        {
            public string tradeID { get; set; }
            public string units { get; set; }
        }

    }



}

/*
  {"lastTransactionID":"31",
  "orderCreateTransaction":
  {"accountID":"101-004-3904511-001",
      "batchID":"30",
      "id":"30",
      "instrument":"EUR_USD",
      "positionFill":"DEFAULT",
      "reason":"CLIENT_ORDER",
      "time":"2016-08-05T12:39:02.397924615Z",
      "timeInForce":"FOK",
      "type":"MARKET_ORDER","units":"1000","userID":3904511},
  "orderFillTransaction":
  {"accountBalance":"100025.4331",
      "accountID":"101-004-3904511-001",
      "batchID":"30",
      "financing":"0.0000",
      "id":"31","instrument":
      "EUR_USD","orderID":"30",
      "pl":"0.0000",
      "price":"1.10931",
      "reason":"MARKET_ORDER",
      "time":"2016-08-05T12:39:02.397924615Z",
      "tradeOpened":
          {"tradeID":"31",
          "units":"1000"},
      "type":"ORDER_FILL",
      "units":"1000",
      "userID":3904511},
  "relatedTransactionIDs":["30","31"]}
*/
/*
 {"lastTransactionID":"41",
 "orderCancelTransaction":
 {"accountID":"101-004-3904511-001","batchID":"40","id":"41","orderID":"40","reason":"STOP_LOSS_ON_FILL_LOSS","time":"2016-08-05T13:29:31.477223240Z","type":"ORDER_CANCEL","userID":3904511},
 "orderCreateTransaction":
 {"accountID":"101-004-3904511-001","batchID":"40","id":"40","instrument":"EUR_USD","positionFill":"DEFAULT","price":"1.50000","reason":"CLIENT_ORDER",
 "stopLossOnFill":
 {"price":"1.45000","timeInForce":"GTC"},
 "time":"2016-08-05T13:29:31.477223240Z","timeInForce":"GTC","triggerCondition":"TRIGGER_DEFAULT","type":"LIMIT_ORDER","units":"1000","userID":3904511},
 "relatedTransactionIDs":["40","41"]}

 * */
