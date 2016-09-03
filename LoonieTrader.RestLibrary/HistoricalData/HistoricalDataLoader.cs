﻿using System.Collections.Generic;
using System.IO;
using FileHelpers;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Services;

namespace LoonieTrader.RestLibrary.HistoricalData
{
    public class HistoricalDataLoader : IHistoricalDataLoader
    {
        public IList<CandleDataRecord> LoadDataFile201603()
        {
            var engine = new FileHelperEngine<CandleDataRecord>();
            var frw = new FileReaderWriterService();
            var hdPath = frw.GetHistoricalDataFolderPath();
            CandleDataRecord[] records = engine.ReadFile(Path.Combine(hdPath, "EURUSD201603.txt")); // todo hardcoded

            return records;
        }
    }
}