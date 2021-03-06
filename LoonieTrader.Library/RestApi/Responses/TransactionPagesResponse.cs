﻿// ReSharper disable InconsistentNaming
using System.Text;
using JetBrains.Annotations;

namespace LoonieTrader.Library.RestApi.Responses
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature, ImplicitUseTargetFlags.WithMembers)]
    public class TransactionPagesResponse
    {
         public int count { get; set; }
        public string from { get; set; }
        public string lastTransactionID { get; set; }
        public int pageSize { get; set; }
        public string[] pages { get; set; }
        public string to { get; set; }

        public override string ToString()
        {
            var resp = new StringBuilder();
            resp.Append("count: ");
            resp.Append(count);
            resp.Append(", lastTransactionID: ");
            resp.AppendLine(lastTransactionID);

            foreach (var page in pages)
            {
                resp.Append("page: ");
                resp.AppendLine(page);

            }

            return resp.ToString();
        }
    }
}

/*
{"count":9,
"from":"2016-07-14T14:13:47.713175329Z",
"lastTransactionID":"9",
"pageSize":100,
"pages":["https://api-fxpractice.oanda.com/v3/accounts/000-000-0000000-000/transactions/idrange?from=1&to=9"],
"to":"2016-07-17T20:38:11.366279301Z"}

*/