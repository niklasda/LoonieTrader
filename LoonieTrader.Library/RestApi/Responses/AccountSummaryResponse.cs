using System.Text;
using JetBrains.Annotations;

namespace LoonieTrader.Library.RestApi.Responses
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature, ImplicitUseTargetFlags.WithMembers)]
    public class AccountSummaryResponse
    {
        public string lastTransactionID { get; set; }
        public AccountSummary account { get; set; }

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

        public class AccountSummary
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
            public int pendingOrderCount { get; set; }
            public string pl { get; set; }
            public string positionValue { get; set; }
            public string resettablePL { get; set; }
            public string unrealizedPL { get; set; }
            public string withdrawalLimit { get; set; }
        }

    }
}

/*
{"account":
{"NAV":"100026.2941",
"alias":"Primary",
"balance":"100026.2941",
"createdByUserID":3904511,
"createdTime":"2016-07-14T14:13:47.713175329Z",
"currency":"EUR",
"hedgingEnabled":false,
"id":"101-004-3904511-001",
"lastTransactionID":"16",
"marginAvailable":"100026.2941",
"marginCallMarginUsed":"0.0000",
"marginCallPercent":"0.00000",
"marginCloseoutMarginUsed":"0.0000",
"marginCloseoutNAV":"100026.2941",
"marginCloseoutPercent":"0.00000",
"marginCloseoutPositionValue":"0.0000",
"marginCloseoutUnrealizedPL":"0.0000",
"marginRate":"0.02",
"marginUsed":"0.0000",
"openPositionCount":0,
"openTradeCount":0,
"pendingOrderCount":0,
"pl":"26.3639",
"positionValue":"0.0000",
"resettablePL":"26.3639",
"unrealizedPL":"0.0000",
"withdrawalLimit":"100026.2941"},
"lastTransactionID":"16"}
*/
