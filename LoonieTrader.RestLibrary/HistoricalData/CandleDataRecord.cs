using System;
using FileHelpers;

namespace LoonieTrader.RestLibrary.HistoricalData
{
    [DelimitedRecord(",")]
    [IgnoreFirst]
    public class CandleDataRecord
    {
        public string Ticker;

        public string Date;

        public string Time;

        [FieldConverter(ConverterKind.Decimal, ".")]
        public decimal Open;

        [FieldConverter(ConverterKind.Decimal, ".")]
        public decimal High;

        [FieldConverter(ConverterKind.Decimal, ".")]
        public decimal Low;

        [FieldConverter(ConverterKind.Decimal, ".")]
        public decimal Close;

        [FieldConverter(ConverterKind.Int32)]
        public int Vol;

    }
    //<TICKER>,<DTYYYYMMDD>,<TIME>,<OPEN>,<HIGH>,<LOW>,<CLOSE>,<VOL>
}