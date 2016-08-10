using System;
using FileHelpers;

namespace LoonieTrader.RestLibrary.HistoricalData
{
    public class CandleDataConverter : ConverterBase
    {
        public override object StringToField(string from)
        {
            return Convert.ToDecimal(Decimal.Parse(from) / 100);
        }

        public override string FieldToString(object fieldValue)
        {
            return ((decimal)fieldValue).ToString("#.##").Replace(".", "");
        }

    }
    //<TICKER>,<DTYYYYMMDD>,<TIME>,<OPEN>,<HIGH>,<LOW>,<CLOSE>,<VOL>
}