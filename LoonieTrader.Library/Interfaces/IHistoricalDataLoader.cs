using System.Collections.Generic;
using LoonieTrader.Library.HistoricalData;

namespace LoonieTrader.Library.Interfaces
{
    public interface IHistoricalDataLoader
    {
        IList<CandleDataRecord> LoadDataFile201603();
    }
}