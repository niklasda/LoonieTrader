using System.Text;

namespace LoonieTrader.RestLibrary.Models.Responses
{
    public class InstrumentsResponse
    {
        public Instrument[] instruments { get; set; }
        public string lastTransactionID { get; set; }

        public override string ToString()
        {
            var resp = new StringBuilder();
            foreach (var instrument in instruments)
            {
                resp.Append("displayName: ");
                resp.Append(instrument.displayName);
                resp.Append(", marginRate: ");
                resp.Append(instrument.marginRate);
                resp.Append(", minimumTradeSize: ");
                resp.Append(instrument.minimumTradeSize);
                resp.Append(", maximumPositionSize: ");
                resp.Append(instrument.maximumPositionSize);
                resp.Append(", maximumOrderUnits: ");
                resp.Append(instrument.maximumOrderUnits);
                resp.Append(", name: ");
                resp.Append(instrument.name);
                resp.Append(", type: ");
                resp.AppendLine(instrument.type);
            }

            return resp.ToString();
        }
    }

    public class Instrument
    {
        public string displayName { get; set; }
        public int displayPrecision { get; set; }
        public string marginRate { get; set; }
        public string maximumOrderUnits { get; set; }
        public string maximumPositionSize { get; set; }
        public string maximumTrailingStopDistance { get; set; }
        public string minimumTradeSize { get; set; }
        public string minimumTrailingStopDistance { get; set; }
        public string name { get; set; }
        public int pipLocation { get; set; }
        public int tradeUnitsPrecision { get; set; }
        public string type { get; set; }
    }

}

/*
   {"instruments":
    [{"displayName":"France 40",
    "displayPrecision":1,
    "marginRate":"0.02",
    "maximumOrderUnits":"2000",
    "maximumPositionSize":"0",
    "maximumTrailingStopDistance":"10000.0",
    "minimumTradeSize":"1",
    "minimumTrailingStopDistance":"5.0",
    "name":"FR40_EUR",
    "pipLocation":0,
    "tradeUnitsPrecision":0,
    "type":"CFD"},
    {"displayName":"USD/SGD","displayPrecision":5,"marginRate":"0.02","maximumOrderUnits":"100000000","maximumPositionSize":"0","maximumTrailingStopDistance":"1.00000","minimumTradeSize":"1","minimumTrailingStopDistance":"0.00050","name":"USD_SGD","pipLocation":-4,"tradeUnitsPrecision":0,"type":"CURRENCY"},
    {"displayName":"EUR/SEK","displayPrecision":5,"marginRate":"0.02","maximumOrderUnits":"100000000","maximumPositionSize":"0","maximumTrailingStopDistance":"1.00000","minimumTradeSize":"1","minimumTrailingStopDistance":"0.00050","name":"EUR_SEK","pipLocation":-4,"tradeUnitsPrecision":0,"type":"CURRENCY"},
    {"displayName":"USD/ZAR","displayPrecision":5,"marginRate":"0.05","maximumOrderUnits":"100000000","maximumPositionSize":"0","maximumTrailingStopDistance":"1.00000","minimumTradeSize":"1","minimumTrailingStopDistance":"0.00050","name":"USD_ZAR","pipLocation":-4,"tradeUnitsPrecision":0,"type":"CURRENCY"},
    {"displayName":"NZD/CAD","displayPrecision":5,"marginRate":"0.02","maximumOrderUnits":"100000000","maximumPositionSize":"0","maximumTrailingStopDistance":"1.00000","minimumTradeSize":"1","minimumTrailingStopDistance":"0.00050","name":"NZD_CAD","pipLocation":-4,"tradeUnitsPrecision":0,"type":"CURRENCY"},
    {"displayName":"Gold/HKD","displayPrecision":3,"marginRate":"0.02","maximumOrderUnits":"50000","maximumPositionSize":"0","maximumTrailingStopDistance":"100.000","minimumTradeSize":"1","minimumTrailingStopDistance":"0.050","name":"XAU_HKD","pipLocation":-2,"tradeUnitsPrecision":0,"type":"METAL"},
    {"displayName":"UK 10Y Gilt","displayPrecision":3,"marginRate":"0.02","maximumOrderUnits":"60000","maximumPositionSize":"0","maximumTrailingStopDistance":"100.000","minimumTradeSize":"1","minimumTrailingStopDistance":"0.050","name":"UK10YB_GBP","pipLocation":-2,"tradeUnitsPrecision":0,"type":"CFD"}],
    "lastTransactionID":"20"}
*/
