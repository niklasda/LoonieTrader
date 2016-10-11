using System.Text;
using JetBrains.Annotations;

namespace LoonieTrader.Library.RestApi.Responses
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature, ImplicitUseTargetFlags.WithMembers)]
    public class TransactionsResponse
    {
        public string lastTransactionID { get; set; }
        public Transaction[] transactions { get; set; }

        public override string ToString()
        {
            var resp = new StringBuilder();
            resp.Append("lastTransactionID: ");
            resp.AppendLine(lastTransactionID);

            foreach (var transaction in transactions)
            {
                resp.AppendLine(transaction.ToString());
            }

            return resp.ToString();
        }

        public class Transaction
        {
            public string accountID { get; set; }
            public int accountNumber { get; set; }
            public int accountUserID { get; set; }
            public string batchID { get; set; }
            public int divisionID { get; set; }
            public string homeCurrency { get; set; }
            public string id { get; set; }
            public int siteID { get; set; }
            public string time { get; set; }
            public string type { get; set; }
            public int userID { get; set; }
            public string alias { get; set; }
            public string marginRate { get; set; }
            public string accountBalance { get; set; }
            public string amount { get; set; }
            public string fundingReason { get; set; }
            public string instrument { get; set; }
            public string positionFill { get; set; }
            public string reason { get; set; }
            public string timeInForce { get; set; }
            public string units { get; set; }
            public string orderID { get; set; }
            public string gtdTime { get; set; }
            public string price { get; set; }
            public Stoplossonfill stopLossOnFill { get; set; }
            public Takeprofitonfill takeProfitOnFill { get; set; }
            public string triggerCondition { get; set; }

            public override string ToString()
            {
                var resp = new StringBuilder();

                resp.Append("id: ");
                resp.Append(id);
                resp.Append(", accountID: ");
                resp.Append(accountID);
                resp.Append(", accountNumber: ");
                resp.Append(accountNumber);
                resp.Append(", accountUserID: ");
                resp.Append(accountUserID);
                resp.Append(", accountBalance: ");
                resp.Append(accountBalance);
                resp.Append(", type: ");
                resp.Append(type);
                resp.Append(", instrument: ");
                resp.Append(instrument);
                resp.Append(", amount: ");
                resp.Append(amount);

                return resp.ToString();
            }
        }

        public class Stoplossonfill
        {
            public string price { get; set; }
            public string timeInForce { get; set; }
        }

        public class Takeprofitonfill
        {
            public string price { get; set; }
            public string timeInForce { get; set; }
        }
    }
}

/*
{"lastTransactionID":"9",
"transactions":
[{"accountID":"111-222-3333333-444",
"accountNumber":1,
"accountUserID":3904511,
"batchID":"1",
"divisionID":4,
"homeCurrency":
"EUR","id":"1",
"siteID":101,
"time":"2016-07-14T14:13:47.713175329Z",
"type":"CREATE",
"userID":3904511},
{"accountID":"111-222-3333333-444","alias":"Primary","batchID":"1","id":"2","marginRate":"0.02","time":"2016-07-14T14:13:47.713175329Z","type":"CLIENT_CONFIGURE","userID":3904511},
{"accountBalance":"100000.0000","accountID":"101-004-3904511-001","amount":"100000","batchID":"3","fundingReason":"CLIENT_FUNDING","id":"3","time":"2016-07-14T14:13:48.525550922Z","type":"TRANSFER_FUNDS","userID":3904511},
{"accountID":"111-222-3333333-444","batchID":"4","id":"4","instrument":"EUR_USD","positionFill":"DEFAULT","reason":"CLIENT_ORDER","time":"2016-07-17T19:32:11.183017417Z","timeInForce":"FOK","type":"MARKET_ORDER","units":"-10000","userID":3904511},
{"accountID":"111-222-3333333-444","batchID":"4","id":"5","orderID":"4","reason":"MARKET_HALTED","time":"2016-07-17T19:32:11.183017417Z","type":"ORDER_CANCEL","userID":3904511},
{"accountID":"111-222-3333333-444","batchID":"6","gtdTime":"2016-07-24T19:33:22.000000000Z","id":"6","instrument":"EUR_USD","positionFill":"DEFAULT","price":"1.10289","reason":"CLIENT_ORDER",
    "stopLossOnFill":{"price":"1.10500","timeInForce":"GTC"},
    "takeProfitOnFill":{"price":"1.10000","timeInForce":"GTC"},
    "time":"2016-07-17T19:33:24.224580145Z","timeInForce":"GTD","triggerCondition":"TRIGGER_DEFAULT","type":"MARKET_IF_TOUCHED_ORDER","units":"-10000","userID":3904511},
{"accountID":"111-222-3333333-444","batchID":"7","id":"7","instrument":"EUR_JPY","positionFill":"DEFAULT","reason":"CLIENT_ORDER",
    "stopLossOnFill":{"price":"115.822","timeInForce":"GTC"},
    "takeProfitOnFill":{"price":"115.476","timeInForce":"GTC"},
    "time":"2016-07-17T19:35:15.097656369Z","timeInForce":"FOK","type":"MARKET_ORDER","units":"-10000","userID":3904511},
{"accountID":"111-222-3333333-444","batchID":"7","id":"8","orderID":"7","reason":"MARKET_HALTED","time":"2016-07-17T19:35:15.097656369Z","type":"ORDER_CANCEL","userID":3904511},
{"accountID":"111-222-3333333-444","batchID":"9","gtdTime":"2016-07-17T22:56:25.000000000Z","id":"9","instrument":"EUR_USD","positionFill":"DEFAULT","price":"1.11163","reason":"CLIENT_ORDER",
    "takeProfitOnFill":{"price":"1.10996","timeInForce":"GTC"},
    "time":"2016-07-17T19:56:27.899077807Z","timeInForce":"GTD","triggerCondition":"TRIGGER_DEFAULT","type":"MARKET_IF_TOUCHED_ORDER","units":"-11000","userID":3904511}]}
*/
