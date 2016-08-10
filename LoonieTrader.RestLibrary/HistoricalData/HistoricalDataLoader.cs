using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using FileHelpers;
using LoonieTrader.RestLibrary.Configuration;
using LoonieTrader.RestLibrary.Interfaces;

namespace LoonieTrader.RestLibrary.HistoricalData
{
    public class HistoricalDataLoader : IHistoricalDataLoader
    {
        public IList<CandleDataRecord> LoadDataFile201601()
        {
            var engine = new FileHelperEngine<CandleDataRecord>();
            var frw = new FileReaderWriter();
            var hdPath = frw.GetHistoricalDataFolderPath();
            CandleDataRecord[] records = engine.ReadFile(Path.Combine(hdPath, "EURUSD201601.txt"));

            return records;
        }
    }
}