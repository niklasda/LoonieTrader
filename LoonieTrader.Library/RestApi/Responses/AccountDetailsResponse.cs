using System.Text;

namespace LoonieTrader.Library.RestApi.Responses
{
    public class AccountDetailsResponse
    {
        public Account account { get; set; }
        public string lastTransactionID { get; set; }

        public override string ToString()
        {
            var resp = new StringBuilder();

            resp.Append("alias: ");
            resp.Append(account.alias);
            resp.Append(", balance: ");
            resp.Append(account.balance);
            resp.Append(", ccy: ");
            resp.Append(account.currency);
            resp.Append(", by user: ");
            resp.Append(account.createdByUserID);
            resp.Append(", at time: ");
            resp.Append(account.createdTime);

            return resp.ToString();
        }



        public class Account
        {
            public string NAV { get; set; }
            public string alias { get; set; }
            public string balance { get; set; }
            public int createdByUserID { get; set; }
            public string createdTime { get; set; }
            public string currency { get; set; }
            public bool hedgingEnabled { get; set; }
            public string id { get; set; }
            public string lastTransactionID { get; set; }
            public string marginAvailable { get; set; }
            public string marginCallMarginUsed { get; set; }
            public string marginCallPercent { get; set; }
            public string marginCloseoutMarginUsed { get; set; }
            public string marginCloseoutNAV { get; set; }
            public string marginCloseoutPercent { get; set; }
            public string marginCloseoutPositionValue { get; set; }
            public string marginCloseoutUnrealizedPL { get; set; }
            public string marginRate { get; set; }
            public string marginUsed { get; set; }
            public int openPositionCount { get; set; }
            public int openTradeCount { get; set; }
            public object[] orders { get; set; }
            public int pendingOrderCount { get; set; }
            public string pl { get; set; }
            public string positionValue { get; set; }
            public Position[] positions { get; set; }
            public string resettablePL { get; set; }
            public Trade[] trades { get; set; }
            public string unrealizedPL { get; set; }
            public string withdrawalLimit { get; set; }
        }

        public class Position
        {
            public string instrument { get; set; }
            public Long @long { get; set; }
            public string pl { get; set; }
            public string resettablePL { get; set; }
            public Short @short { get; set; }
            public string unrealizedPL { get; set; }
        }

        public class Long
        {
            public string averagePrice { get; set; }
            public string pl { get; set; }
            public string resettablePL { get; set; }
            public string[] tradeIDs { get; set; }
            public string units { get; set; }
            public string unrealizedPL { get; set; }
        }

        public class Short
        {
            public string pl { get; set; }
            public string resettablePL { get; set; }
            public string units { get; set; }
            public string unrealizedPL { get; set; }
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
{"account":{"NAV":"100071.9086","alias":"Primary","balance":"100025.5437","createdByUserID":3904511,"createdTime":"2016-07-14T14:13:47.713175329Z","currency":"EUR","hedgingEnabled":false,"id":"101-004-3904511-001","lastTransactionID":"26","marginAvailable":"99871.8834","marginCallMarginUsed":"200.0000","marginCallPercent":"0.00200","marginCloseoutMarginUsed":"200.0000","marginCloseoutNAV":"100072.5406","marginCloseoutPercent":"0.00100","marginCloseoutPositionValue":"10000.0000","marginCloseoutUnrealizedPL":"46.9969","marginRate":"0.02","marginUsed":"200.0252","openPositionCount":1,"openTradeCount":1,"orders":[],"pendingOrderCount":0,"pl":"26.3639","positionValue":"10001.2581","positions":[{"instrument":"EUR_USD","long":{"averagePrice":"1.10761","pl":"0.0000","resettablePL":"0.0000","tradeIDs":["18"],"units":"10000","unrealizedPL":"46.3649"},"pl":"26.3639","resettablePL":"26.3639","short":{"pl":"26.3639","resettablePL":"26.3639","units":"0","unrealizedPL":"0.0000"},"unrealizedPL":"46.3649"}],"resettablePL":"26.3639","trades":[{"currentUnits":"10000","financing":"-0.7504","id":"18","initialUnits":"10000","instrument":"EUR_USD","openTime":"2016-07-28T00:38:59.080166841Z","price":"1.10761","realizedPL":"0.0000","state":"OPEN","unrealizedPL":"46.3649"}],"unrealizedPL":"46.3649","withdrawalLimit":"99871.8834"},"lastTransactionID":"26"}
*/
