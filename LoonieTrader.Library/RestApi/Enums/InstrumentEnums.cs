// ReSharper disable InconsistentNaming
namespace LoonieTrader.Library.RestApi.Enums
{
    public enum InstrumentType
    {
        //UNKNOWN,
        CURRENCY,
        //INDEX,
        //BOND,
        //COMMODITY,
        //TEST,
        //BASKET,
        CFD,
        METAL
    }

    public enum AcceptDatetimeFormat
    {
        UNIX,      // “12345678.000000123” format.
        RFC3339    // “YYYY-MM-DDTHH:MM:SS.nnnnnnnnnZ” format.
    }
}
