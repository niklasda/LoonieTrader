using System.Collections.Generic;
using System.Collections.ObjectModel;
using LoonieTrader.RestLibrary.HistoricalData;

namespace LoonieTrader.RestLibrary.Interfaces
{
    public interface IHistoricalDataLoader
    {
        IList<CandleDataRecord> LoadDataFile201601();
    }
}