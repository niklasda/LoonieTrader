﻿// ReSharper disable InconsistentNaming
using System.Text;
using JetBrains.Annotations;

namespace LoonieTrader.Library.RestApi.Responses
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature, ImplicitUseTargetFlags.WithMembers)]
    public class AccountsResponse
    {
        public Account[] accounts { get; set; }

        public override string ToString()
        {
            var resp = new StringBuilder();
            foreach (var account in accounts)
            {
                resp.Append("id: ");
                resp.Append(account.id);
                resp.Append(", ");
                resp.Append("tags: ");
                resp.AppendLine(string.Concat(account.tags));
            }

            return resp.ToString();
        }

        public class Account
        {
            public string id { get; set; }

            public string[] tags { get; set; }
        }
    }
}

/*
 {"accounts":[{"id":"111-222-3333333-444","tags":[]}]}
*/