// ReSharper disable InconsistentNaming
using System.Text;
using JetBrains.Annotations;

namespace LoonieTrader.Library.RestApi.Responses
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature, ImplicitUseTargetFlags.WithMembers)]
    public class CandlesResponse
    {
        public Candle[] candles { get; set; }
        public string granularity { get; set; }
        public string instrument { get; set; }

        public override string ToString()
        {
            var resp = new StringBuilder();

            resp.Append("instrument: ");
            resp.AppendLine(instrument);

            foreach (var candle in candles)
            {
                if (candle.mid != null)
                    resp.Append("MID ");
                if (candle.ask != null)
                    resp.Append("ASK ");
                if (candle.bid != null)
                    resp.Append("BID ");

                var cd = candle.mid ?? candle.ask ?? candle.bid;
                resp.Append("complete: ");
                resp.Append(candle.complete);
                resp.Append(", time: ");
                resp.Append(candle.time);
                resp.Append(", open: ");
                resp.Append(cd.o);
                resp.Append(", high: ");
                resp.Append(cd.h);
                resp.Append(", low: ");
                resp.Append(cd.l);
                resp.Append(", close: ");
                resp.Append(cd.c);
                resp.Append(", volume: ");
                resp.AppendLine(candle.volume.ToString());
            }

            return resp.ToString();
        }

        public class Candle
        {
            public bool complete { get; set; }
            public CandleData mid { get; set; }
            public CandleData ask { get; set; }
            public CandleData bid { get; set; }
            public string time { get; set; }
            public int volume { get; set; }
        }

        public class CandleData
        {
            public string c { get; set; }
            public string h { get; set; }
            public string l { get; set; }
            public string o { get; set; }
        }
    }
}

/*

{"candles":[{"complete":true,"mid":{"c":"1.08766","h":"1.08766","l":"1.08766","o":"1.08766"},"time":"2016-10-24T16:58:40.000000000Z","volume":1},
{"complete":true,"mid":{"c":"1.08763","h":"1.08763","l":"1.08763","o":"1.08763"},"time":"2016-10-24T16:59:05.000000000Z","volume":1},
{"complete":true,"mid":{"c":"1.08756","h":"1.08760","l":"1.08756","o":"1.08760"},"time":"2016-10-24T16:59:10.000000000Z","volume":3},
{"complete":true,"mid":{"c":"1.08759","h":"1.08759","l":"1.08759","o":"1.08759"},"time":"2016-10-24T16:59:15.000000000Z","volume":1},
{"complete":true,"mid":{"c":"1.08762","h":"1.08762","l":"1.08762","o":"1.08762"},"time":"2016-10-24T16:59:20.000000000Z","volume":1},
{"complete":true,"mid":{"c":"1.08764","h":"1.08764","l":"1.08759","o":"1.08759"},"time":"2016-10-24T16:59:25.000000000Z","volume":3},
{"complete":true,"mid":{"c":"1.08772","h":"1.08772","l":"1.08770","o":"1.08770"},"time":"2016-10-24T16:59:40.000000000Z","volume":2},
{"complete":true,"mid":{"c":"1.08776","h":"1.08776","l":"1.08776","o":"1.08776"},"time":"2016-10-24T16:59:45.000000000Z","volume":1},
{"complete":true,"mid":{"c":"1.08781","h":"1.08781","l":"1.08781","o":"1.08781"},"time":"2016-10-24T16:59:55.000000000Z","volume":1},
{"complete":true,"mid":{"c":"1.08788","h":"1.08788","l":"1.08785","o":"1.08785"},"time":"2016-10-24T17:00:20.000000000Z","volume":2},
{"complete":false,"mid":{"c":"1.08781","h":"1.08781","l":"1.08781","o":"1.08781"},"time":"2016-10-24T19:04:25.000000000Z","volume":1}],
"granularity":"S5","instrument":"EUR/USD"}

*/
