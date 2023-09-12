using System.Collections.Generic;
using System.IO;
using FileHelpers;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Services;

namespace LoonieTrader.Library.HistoricalData;

public class HistoricalDataLoader : IHistoricalDataLoader
{
    public IList<CandleDataRecord> LoadDataFile201603()
    {
        var engine = new FileHelperEngine<CandleDataRecord>();
        IFileReaderWriterService frw = new FileReaderWriterService();
        string hdPath = frw.GetHistoricalDataFolderPath();
        CandleDataRecord[] records = engine.ReadFile(Path.Combine(hdPath, "EURUSD201603.txt")); // todo hardcoded

        return records;
    }
}