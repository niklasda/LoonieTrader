using System.Text;
using JetBrains.Annotations;

namespace LoonieTrader.Library.RestApi.Responses
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature, ImplicitUseTargetFlags.WithMembers)]
    public class ServiceListResponse
    {
        public List[] lists { get; set; }

        public override string ToString()
        {
            var resp = new StringBuilder();
            //resp.Append("lastTransactionID: ");
            // resp.AppendLine(lastTransactionID);

            foreach (var list in lists)
            {
                resp.Append("id: ");
                resp.Append(list.id);
                resp.Append(", description: ");
                resp.Append(list.description);
                resp.Append(", name: ");
                resp.Append(list.name);
                resp.Append(", url: ");
                resp.AppendLine(list.url);
            }

            return resp.ToString();
        }


        public class Rootobject
        {
            public List[] lists { get; set; }
        }

        public class List
        {
            public string url { get; set; }
            public string description { get; set; }
            public string name { get; set; }
            public string id { get; set; }
        }

    }
}

/*
{"lists":
[{"url": "http://api-status.oanda.com/api/v1/service-lists/rest", "description": " ", "name": "REST", "id": "rest"},
 {"url": "http://api-status.oanda.com/api/v1/service-lists/stream", "description": " ", "name": "Stream", "id": "stream"}]}
*/
