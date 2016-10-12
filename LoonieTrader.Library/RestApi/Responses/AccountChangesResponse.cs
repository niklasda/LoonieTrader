// ReSharper disable InconsistentNaming
using System.Text;
using JetBrains.Annotations;

namespace LoonieTrader.Library.RestApi.Responses
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature, ImplicitUseTargetFlags.WithMembers)]
    public class AccountChangesResponse
    {
        public Changes changes { get; set; }
        public string lastTransactionID { get; set; }
        public State state { get; set; }

        public override string ToString()
        {
            var resp = new StringBuilder();
            //foreach (var change in changes)
            // {
            resp.Append("ordersCancelled#: ");
            resp.Append(changes.ordersCancelled.Length);
            resp.Append(", ordersCreated#: ");
            resp.Append(changes.ordersCreated.Length);
            resp.Append(", ordersFilled#: ");
            resp.Append(changes.ordersFilled.Length);
            resp.Append(", ordersTriggered#: ");
            resp.Append(changes.ordersTriggered.Length);
            resp.Append(", positions#: ");
            resp.Append(changes.positions.Length);
            resp.Append(", tradesClosed#: ");
            resp.Append(changes.tradesClosed.Length);
            resp.Append(", tradesOpened#: ");
            resp.Append(changes.tradesOpened.Length);
            resp.Append(", tradesReduced#: ");
            resp.Append(changes.tradesReduced.Length);
            resp.Append(", transactions#: ");
            resp.Append(changes.transactions.Length);

            resp.AppendLine();

            resp.Append(", orders#: ");
            resp.Append(state.orders.Length);
            resp.Append(", positions#: ");
            resp.Append(state.positions.Length);
            resp.Append(", trades#: ");
            resp.Append(state.trades.Length);
            resp.AppendLine();
            //  }

            return resp.ToString();
        }



        public class Changes
        {
            public Orderscancelled[] ordersCancelled { get; set; }
            public object[] ordersCreated { get; set; }
            public object[] ordersFilled { get; set; }
            public object[] ordersTriggered { get; set; }
            public object[] positions { get; set; }
            public object[] tradesClosed { get; set; }
            public object[] tradesOpened { get; set; }
            public object[] tradesReduced { get; set; }
            public Transaction[] transactions { get; set; }
        }

        public class Orderscancelled
        {
            public string cancelledTime { get; set; }
            public string cancellingTransactionID { get; set; }
            public string createTime { get; set; }
            public string id { get; set; }
            public string price { get; set; }
            public string state { get; set; }
            public string timeInForce { get; set; }
            public string tradeID { get; set; }
            public string triggerCondition { get; set; }
            public string type { get; set; }
        }

        public class Transaction
        {
            public string accountID { get; set; }
            public string batchID { get; set; }
            public string closedTradeID { get; set; }
            public string id { get; set; }
            public string orderID { get; set; }
            public string reason { get; set; }
            public string time { get; set; }
            public string tradeCloseTransactionID { get; set; }
            public string type { get; set; }
            public int userID { get; set; }
        }

        public class State
        {
            public string NAV { get; set; }
            public string marginAvailable { get; set; }
            public string marginCallMarginUsed { get; set; }
            public string marginCallPercent { get; set; }
            public string marginCloseoutMarginUsed { get; set; }
            public string marginCloseoutNAV { get; set; }
            public string marginCloseoutPercent { get; set; }
            public string marginCloseoutUnrealizedPL { get; set; }
            public string marginUsed { get; set; }
            public object[] orders { get; set; }
            public string positionValue { get; set; }
            public object[] positions { get; set; }
            public object[] trades { get; set; }
            public string unrealizedPL { get; set; }
            public string withdrawalLimit { get; set; }
        }


    }
}

/*
{"changes":
{
"ordersCancelled":[
    {"cancelledTime":"2016-10-07T18:46:04.262320207Z","cancellingTransactionID":"3301","createTime":"2016-10-05T19:57:29.008810202Z","id":"2655","price":"1.00000","state":"CANCELLED","timeInForce":"GTC","tradeID":"2654","triggerCondition":"TRIGGER_DEFAULT","type":"STOP_LOSS"},
    {"cancelledTime":"2016-10-07T18:46:04.262320207Z","cancellingTransactionID":"3302","createTime":"2016-10-06T17:30:27.695822967Z","id":"2663","price":"1.00000","state":"CANCELLED","timeInForce":"GTC","tradeID":"2662","triggerCondition":"TRIGGER_DEFAULT","type":"STOP_LOSS"},
    {"cancelledTime":"2016-10-07T18:46:04.262320207Z","cancellingTransactionID":"3303","createTime":"2016-10-06T17:34:44.981860968Z","id":"2670","price":"1.00000","state":"CANCELLED","timeInForce":"GTC","tradeID":"2669","triggerCondition":"TRIGGER_DEFAULT","type":"STOP_LOSS"},
"ordersCreated":[],
"ordersFilled":[],
"ordersTriggered":[],
"positions":[],
"tradesClosed":[],
"tradesOpened":[],
"tradesReduced":[],
"transactions":[
    {"accountID":"101-004-3904511-001","batchID":"2922","closedTradeID":"2654","id":"3301","orderID":"2655","reason":"LINKED_TRADE_CLOSED","time":"2016-10-07T18:46:04.262320207Z","tradeCloseTransactionID":"2923","type":"ORDER_CANCEL","userID":3904511},
    {"accountID":"101-004-3904511-001","batchID":"2922","closedTradeID":"2662","id":"3302","orderID":"2663","reason":"LINKED_TRADE_CLOSED","time":"2016-10-07T18:46:04.262320207Z","tradeCloseTransactionID":"2923","type":"ORDER_CANCEL","userID":3904511},
    {"accountID":"101-004-3904511-001","batchID":"2922","closedTradeID":"2915","id":"3337","orderID":"2916","reason":"LINKED_TRADE_CLOSED","time":"2016-10-07T18:46:04.262320207Z","tradeCloseTransactionID":"2923","type":"ORDER_CANCEL","userID":3904511}]},
"lastTransactionID":"3337",
"state":{
    "NAV":"97692.3471","marginAvailable":"97692.3471","marginCallMarginUsed":"0.0000","marginCallPercent":"0.00000","marginCloseoutMarginUsed":"0.0000","marginCloseoutNAV":"97692.3471","marginCloseoutPercent":"0.00000","marginCloseoutUnrealizedPL":"0.0000","marginUsed":"0.0000",
    "orders":[],
    "positionValue":"0.0000",
    "positions":[],
    "trades":[],
    "unrealizedPL":"0.0000",
    "withdrawalLimit":"97692.3471"}
}
*/
