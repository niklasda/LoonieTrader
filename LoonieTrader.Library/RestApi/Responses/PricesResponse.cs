using System.Text;

namespace LoonieTrader.Library.RestApi.Responses
{
    public class PricesResponse
    {
        public Price[] prices { get; set; }

        public override string ToString()
        {
            var resp = new StringBuilder();
            foreach (var price in prices)
            {
                resp.Append("instrument: ");
                resp.Append(price.instrument);
                resp.Append(", status: ");
                resp.Append(price.status);
                resp.Append(", asks: ");
                resp.Append(price.asks[0].price);
                resp.Append(", bids: ");
                resp.Append(price.bids[0].price);
                resp.Append(", time: ");
                resp.AppendLine(price.time);
            }

            return resp.ToString();
        }

        public class Price
        {
            public Ask[] asks { get; set; }
            public Bid[] bids { get; set; }
            public string closeoutAsk { get; set; }
            public string closeoutBid { get; set; }
            public string instrument { get; set; }
            public Quotehomeconversionfactors quoteHomeConversionFactors { get; set; }
            public string status { get; set; }
            public string time { get; set; }
            public Unitsavailable unitsAvailable { get; set; }
        }

        public class Quotehomeconversionfactors
        {
            public string negativeUnits { get; set; }
            public string positiveUnits { get; set; }
        }

        public class Unitsavailable
        {
            public Default @default { get; set; }
            public Openonly openOnly { get; set; }
            public Reducefirst reduceFirst { get; set; }
            public Reduceonly reduceOnly { get; set; }
        }

        public class Default
        {
            public string @long { get; set; }
            public string @short { get; set; }
        }

        public class Openonly
        {
            public string @long { get; set; }
            public string @short { get; set; }
        }

        public class Reducefirst
        {
            public string @long { get; set; }
            public string @short { get; set; }
        }

        public class Reduceonly
        {
            public string @long { get; set; }
            public string @short { get; set; }
        }

        public class Ask
        {
            public int liquidity { get; set; }
            public string price { get; set; }
        }

        public class Bid
        {
            public int liquidity { get; set; }
            public string price { get; set; }
        }
    }
}

/*
  {"prices":
  [{"asks":
  [{"liquidity":10000000,"price":"1.11796"},
  {"liquidity":10000000,"price":"1.11798"}],
  "bids":
  [{"liquidity":10000000,"price":"1.11696"},
  {"liquidity":10000000,"price":"1.11694"}],
  "closeoutAsk":"1.11800",
  "closeoutBid":"1.11692",
  "instrument":"EUR_USD",
  "quoteHomeConversionFactors":{"negativeUnits":"0.89528721","positiveUnits":"0.89448639"},
  "status":"non-tradeable",
  "time":"2016-07-29T20:59:59.059711797Z",
  "unitsAvailable":
  {"default":{"long":"4991009","short":"5019967"},
  "openOnly":{"long":"4991009","short":"0"},
  "reduceFirst":{"long":"4991009","short":"5019967"},
  "reduceOnly":{"long":"0","short":"10000"}}},
  {"asks":
  [{"liquidity":1000000,"price":"1.30451"},
  {"liquidity":2000000,"price":"1.30452"},
  {"liquidity":5000000,"price":"1.30453"},
  {"liquidity":10000000,"price":"1.30455"}],
  "bids":
  [{"liquidity":1000000,"price":"1.30351"},
  {"liquidity":2000000,"price":"1.30350"},
  {"liquidity":5000000,"price":"1.30349"},
  {"liquidity":10000000,"price":"1.30347"}],
  "closeoutAsk":"1.30455",
  "closeoutBid":"1.30347",
  "instrument":"USD_CAD",
  "quoteHomeConversionFactors":
  {"negativeUnits":"0.68676130","positiveUnits":"0.68581931"},
  "status":"non-tradeable",
  "time":"2016-07-29T20:59:59.062206413Z",
  "unitsAvailable":
  {"default":{"long":"5575958","short":"5588010"},
  "openOnly":{"long":"5575958","short":"5588010"},
  "reduceFirst":{"long":"5575958","short":"5588010"},
  "reduceOnly":{"long":"0","short":"0"}}}]}
*/
