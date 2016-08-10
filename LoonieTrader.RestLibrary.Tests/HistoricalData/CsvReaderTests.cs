using System.IO;
using System.Net;
using System.Text;
using FileHelpers;
using Jil;
using LoonieTrader.RestLibrary.Configuration;
using LoonieTrader.RestLibrary.HistoricalData;
using LoonieTrader.RestLibrary.Models.Responses;
using NUnit.Framework;

namespace LoonieTrader.RestLibrary.Tests.HistoricalData
{
    public class CsvReaderTests
    {
        [Test]
        public void TestEurUsd()
        {

            var engine = new FileHelperEngine<CandleDataRecord>();
            var frw = new FileReaderWriter();
            var hdPath = frw.GetHistoricalDataFolderPath();
            var records = engine.ReadFile(Path.Combine(hdPath, "EURUSD2016.txt"));

            Assert.AreEqual(212445, records.Length);
        }

    }
}