using System.Text;

namespace LoonieTrader.Library.RestApi.Responses
{
    public class ServicesResponse
    {
        public Service[] services { get; set; }

        public override string ToString()
        {
            var resp = new StringBuilder();
            foreach (var service in services)
            {
                resp.AppendLine(service.ToString());
            }

            return resp.ToString();
        }

        public class Service
        {
            public string description { get; set; }
            public string url { get; set; }
            public List list { get; set; }
            public CurrentEvent currentevent { get; set; }
            public string id { get; set; }
            public string name { get; set; }

            public override string ToString()
            {
                var resp = new StringBuilder();

                resp.AppendFormat("id: {0}, name: {1}, description: {2}, url: {3}", this.id, this.name, this.description, this.url);

                if (list != null)
                {
                    resp.AppendLine();
                    resp.AppendFormat("id: {0}, name: {1}, description: {2}, url: {3}", list.id, list.name, list.description, list.url);

                }
                if (currentevent != null)
                {
                    resp.AppendLine();
                    resp.AppendFormat("sid: {0}, message: {1}, timestamp: {2}, url: {3}", currentevent.sid, currentevent.message, currentevent.timestamp, currentevent.url);

                    if (currentevent.status != null)
                    {
                        resp.AppendLine();
                        resp.AppendFormat("id: {0}, name: {1}, description: {2}, url: {3}", currentevent.status.id, currentevent.status.name, currentevent.status.description, currentevent.status.url);
                    }
                }

                return resp.ToString();
            }
        }

        public class List
        {
            public string url { get; set; }
            public string description { get; set; }
            public string name { get; set; }
            public string id { get; set; }
        }

        public class CurrentEvent
        {
            public Status status { get; set; }
            public string url { get; set; }
            public string timestamp { get; set; }
            public string sid { get; set; }
            public string message { get; set; }
            public bool informational { get; set; }
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
        }

    }
}

/*
{"services":
[{"description": " ", "url": "http://api-status.oanda.com/api/v1/services/fxtrade-practice-rest-api",      "list": {"url": "http://api-status.oanda.com/api/v1/service-lists/rest",   "description": " ", "name": "REST", "id": "rest"}, "current-event": {"status": {"description": "The service is up", "level": "NORMAL", "default": true, "image": "http://api-status.oanda.com/images/icons/fugue/tick-circle.png", "url": "http://api-status.oanda.com/api/v1/statuses/up", "id": "up", "name": "Up"}, "url": "http://api-status.oanda.com/api/v1/services/fxtrade-practice-rest-api/events/ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgIDAnoOUCgw", "timestamp": "Fri, 02 Sep 2016 21:29:01 GMT", "sid": "ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgIDAnoOUCgw", "message": "Service is running without issue", "informational": false}, "id": "fxtrade-practice-rest-api", "name": "fxTrade Practice REST API"},
 {"description": " ", "url": "http://api-status.oanda.com/api/v1/services/fxtrade-practice-streaming-api", "list": {"url": "http://api-status.oanda.com/api/v1/service-lists/stream", "description": " ", "name": "Stream", "id": "stream"}, "current-event": {"status": {"description": "The service is up", "level": "NORMAL", "default": true, "image": "http://api-status.oanda.com/images/icons/fugue/tick-circle.png", "url": "http://api-status.oanda.com/api/v1/statuses/up", "id": "up", "name": "Up"}, "url": "http://api-status.oanda.com/api/v1/services/fxtrade-practice-streaming-api/events/ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgIDAq5yeCgw", "timestamp": "Thu, 08 Sep 2016 22:00:01 GMT", "sid": "ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgIDAq5yeCgw", "message": "Service is running without issue", "informational": false}, "id": "fxtrade-practice-streaming-api", "name": "fxTrade Practice Streaming API"},
 {"description": " ", "url": "http://api-status.oanda.com/api/v1/services/fxtrade-rest-api",               "list": {"url": "http://api-status.oanda.com/api/v1/service-lists/rest",   "description": " ", "name": "REST", "id": "rest"}, "current-event": {"status": {"description": "The service is up", "level": "NORMAL", "default": true, "image": "http://api-status.oanda.com/images/icons/fugue/tick-circle.png", "url": "http://api-status.oanda.com/api/v1/statuses/up", "id": "up", "name": "Up"}, "url": "http://api-status.oanda.com/api/v1/services/fxtrade-rest-api/events/ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgIDAy_KSCgw", "timestamp": "Fri, 19 Aug 2016 23:03:08 GMT", "sid": "ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgIDAy_KSCgw", "message": "Service is running without issue", "informational": false}, "id": "fxtrade-rest-api", "name": "fxTrade REST API"},
 {"description": " ", "url": "http://api-status.oanda.com/api/v1/services/fxtrade-streaming-api",          "list": {"url": "http://api-status.oanda.com/api/v1/service-lists/stream", "description": " ", "name": "Stream", "id": "stream"}, "current-event": {"status": {"description": "The service is up", "level": "NORMAL", "default": true, "image": "http://api-status.oanda.com/images/icons/fugue/tick-circle.png", "url": "http://api-status.oanda.com/api/v1/statuses/up", "id": "up", "name": "Up"}, "url": "http://api-status.oanda.com/api/v1/services/fxtrade-streaming-api/events/ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgIDAy7maCgw", "timestamp": "Wed, 24 Aug 2016 06:33:00 GMT", "sid": "ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgIDAy7maCgw", "message": "Service is running without issue", "informational": false}, "id": "fxtrade-streaming-api", "name": "fxTrade Streaming API"}
]}

{"description": " ", "url": "http://api-status.oanda.com/api/v1/services/fxtrade-practice-streaming-api", "list": {"url": "http://api-status.oanda.com/api/v1/service-lists/stream", "description": " ", "name": "Stream", "id": "stream"}, "current-event": {"status": {"description": "The service is up", "level": "NORMAL", "default": true, "image": "http://api-status.oanda.com/images/icons/fugue/tick-circle.png", "url": "http://api-status.oanda.com/api/v1/statuses/up", "id": "up", "name": "Up"}, "url": "http://api-status.oanda.com/api/v1/services/fxtrade-practice-streaming-api/events/ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgIDAq5yeCgw", "timestamp": "Thu, 08 Sep 2016 22:00:01 GMT", "sid": "ahNzfm9hbmRhLXN0YXR1cy1wYWdlchILEgVFdmVudBiAgIDAq5yeCgw", "message": "Service is running without issue", "informational": false}, "id": "fxtrade-practice-streaming-api", "name": "fxTrade Practice Streaming API"}

*/
