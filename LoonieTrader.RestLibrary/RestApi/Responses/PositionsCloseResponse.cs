
namespace LoonieTrader.RestLibrary.RestApi.Responses
{
    public class PositionsCloseResponse
    {
        public string lastTransactionID { get; set; }
        public Longordercanceltransaction longOrderCancelTransaction { get; set; }
        public Longordercreatetransaction longOrderCreateTransaction { get; set; }
        public string[] relatedTransactionIDs { get; set; }

        public class Longordercanceltransaction
        {
            public string accountID { get; set; }
            public string batchID { get; set; }
            public string id { get; set; }
            public string orderID { get; set; }
            public string reason { get; set; }
            public string time { get; set; }
            public string type { get; set; }
            public int userID { get; set; }
        }

        public class Longordercreatetransaction
        {
            public string accountID { get; set; }
            public string batchID { get; set; }
            public string id { get; set; }
            public string instrument { get; set; }
            public Longpositioncloseout longPositionCloseout { get; set; }
            public string positionFill { get; set; }
            public string reason { get; set; }
            public string time { get; set; }
            public string timeInForce { get; set; }
            public string type { get; set; }
            public string units { get; set; }
            public int userID { get; set; }
        }

        public class Longpositioncloseout
        {
            public string instrument { get; set; }
            public string units { get; set; }
        }

        public class ClosePositionParameters
        {
            public string longUnits { get; set; }
            public string shortUnits { get; set; }
        }
    }
}

/*
{"lastTransactionID":"343",
"longOrderCancelTransaction":{"accountID":"101-004-3904511-001","batchID":"342","id":"343","orderID":"342","reason":"MARKET_HALTED","time":"2016-09-10T17:56:13.972447759Z","type":"ORDER_CANCEL","userID":3904511},
"longOrderCreateTransaction":{"accountID":"101-004-3904511-001","batchID":"342","id":"342","instrument":"EUR_USD","longPositionCloseout":{"instrument":"EUR_USD","units":"1"},
"positionFill":"REDUCE_ONLY","reason":"POSITION_CLOSEOUT","time":"2016-09-10T17:56:13.972447759Z","timeInForce":"FOK","type":"MARKET_ORDER","units":"-1","userID":3904511},
"relatedTransactionIDs":["342","343"]}
*/
