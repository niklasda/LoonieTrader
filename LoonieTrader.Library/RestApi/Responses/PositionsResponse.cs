// ReSharper disable InconsistentNaming
using System.Text;
using JetBrains.Annotations;

namespace LoonieTrader.Library.RestApi.Responses
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature, ImplicitUseTargetFlags.WithMembers)]
    public class PositionsResponse
    {
        public string lastTransactionID { get; set; }
        public Position[] positions { get; set; }

        public override string ToString()
        {
            var resp = new StringBuilder();
            resp.Append("lastTransactionID: ");
            resp.AppendLine(lastTransactionID);

            foreach (var position in positions)
            {
                resp.AppendLine(position.ToString());
//                resp.AppendLine("P/L: " + position.pl);
  //              resp.AppendLine("resettable P/L: " + position.resettablePL);
    //            resp.AppendLine("unrealized P/L: " + position.unrealizedPL);
      //          resp.AppendLine("long units: " + position.@long.units);
        //        resp.AppendLine("long P/L: " + position.@long.pl);
          //      resp.AppendLine("short units: " + position.@short.units);
            //    resp.AppendLine("short P/L: " + position.@short.pl);
            }

            return resp.ToString();
        }

        public class Position
        {
            public string instrument { get; set; }
            public Long @long { get; set; }
            public string pl { get; set; }
            public string resettablePL { get; set; }
            public Short @short { get; set; }
            public string unrealizedPL { get; set; }

            public override string ToString()
            {
                var resp = new StringBuilder();
         //       resp.Append("lastTransactionID: ");
           //     resp.AppendLine(lastTransactionID);

             //   foreach (var position in positions)
               // {
                    resp.Append("Instrument: " + instrument);
                    resp.Append(", P/L: " + pl);
                    resp.Append(", resettable P/L: " + resettablePL);
                    resp.Append(", unrealized P/L: " + unrealizedPL);
                    resp.Append(", long units: " + @long.units);
                    resp.Append(", long P/L: " +  @long.pl);
                    resp.Append(", short units: " + @short.units);
                    resp.Append(", short P/L: " + @short.pl);
               // }

                return resp.ToString();
            }
        }

        public class Long
        {
            public string pl { get; set; }
            public string resettablePL { get; set; }
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
    }
}

/*
{"lastTransactionID":"16",
"positions":
[{"instrument":"EUR_USD",
"long":{"pl":"0.0000","resettablePL":"0.0000","units":"0","unrealizedPL":"0.0000"},
"pl":"26.3639",
"resettablePL":"26.3639",
"short":{"pl":"26.3639","resettablePL":"26.3639","units":"0","unrealizedPL":"0.0000"},
"unrealizedPL":"0.0000"}]}
*/
