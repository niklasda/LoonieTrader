using System.Text;

namespace LoonieTrader.RestLibrary.Models.Responses
{
    public class TransactionDetailsResponse
    {
        public string lastTransactionID { get; set; }
        public Transaction transaction { get; set; }

        public override string ToString()
        {
            var resp = new StringBuilder();
            resp.Append("lastTransactionID: ");
            resp.AppendLine(lastTransactionID);

            resp.Append("id: ");
            resp.Append(transaction.id);
            resp.Append(", accountID: ");
            resp.Append(transaction.accountID);
            resp.Append(", accountBalance: ");
            resp.Append(transaction.accountBalance);
            resp.Append(", type: ");
            resp.Append(transaction.type);
            resp.Append(", instrument: ");
            resp.Append(transaction.instrument);

            resp.AppendLine();

            return resp.ToString();
        }

        public class Transaction
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

        public class Tradeopened
        {
            public string tradeID { get; set; }
            public string units { get; set; }
        }
    }
}

/*
{"lastTransactionID":"53",
"transaction":{"accountBalance":"100025.4331",
"accountID":"101-004-3904511-001",
"batchID":"36",
"financing":"0.0000",
"id":"37",
"instrument":"EUR_USD",
"orderID":"36",
"pl":"0.0000",
"price":"1.10852",
"reason":"MARKET_ORDER",
"time":"2016-08-05T12:55:42.268686450Z",
"tradeOpened":{"tradeID":"37","units":"1000"},
"type":"ORDER_FILL",
"units":"1000",
"userID":3904511}}
*/
