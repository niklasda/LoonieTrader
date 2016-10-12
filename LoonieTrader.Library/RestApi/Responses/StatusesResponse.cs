// ReSharper disable InconsistentNaming
using System.Text;
using JetBrains.Annotations;

namespace LoonieTrader.Library.RestApi.Responses
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature, ImplicitUseTargetFlags.WithMembers)]
    public class StatusesResponse
    {
        public Status[] statuses { get; set; }

        public override string ToString()
        {
            var resp = new StringBuilder();
            foreach (var status in statuses)
            {
                resp.AppendLine(status.ToString());
            }

            return resp.ToString();
        }

        public class Status
        {
            public string description { get; set; }
            public string level { get; set; }
            public bool @default { get; set; }
            public string image { get; set; }
            public string url { get; set; }
            public string id { get; set; }
            public string name { get; set; }

            public override string ToString()
            {
                return string.Format("id: {0}, name: {1}, description: {2}, level: {3}, url: {4}",  id , name , description , level , url);
            }
        }
    }
}

/*
{"statuses":
[{"description": "The service is currently down",                     "level": "NORMAL", "default": false, "image": "http://api-status.oanda.com/images/icons/fugue/cross-circle.png", "url": "http://api-status.oanda.com/api/v1/statuses/down", "id": "down", "name": "Down"},
 {"description": "The service is up",                                 "level": "NORMAL", "default": true,  "image": "http://api-status.oanda.com/images/icons/fugue/tick-circle.png",  "url": "http://api-status.oanda.com/api/v1/statuses/up", "id": "up", "name": "Up"},
 {"description": "The service is experiencing intermittent problems", "level": "NORMAL", "default": false, "image": "http://api-status.oanda.com/images/icons/fugue/exclamation.png",  "url": "http://api-status.oanda.com/api/v1/statuses/warning", "id": "warning", "name": "Warning"}]}

 {"description": "The service is currently down",                     "level": "NORMAL", "default": false, "image": "http://api-status.oanda.com/images/icons/fugue/cross-circle.png", "url": "http://api-status.oanda.com/api/v1/statuses/down", "id": "down", "name": "Down"}
*/
