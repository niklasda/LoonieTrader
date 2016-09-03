using System.Collections.Generic;
using System.IO;
using AutoMapper;
using FileHelpers;
using LoonieTrader.RestLibrary.Configuration;
using LoonieTrader.RestLibrary.HistoricalData;
using LoonieTrader.RestLibrary.Services;
using LoonieTrader.RestLibrary.Tests.Locator;
using NUnit.Framework;

namespace LoonieTrader.RestLibrary.Tests.HistoricalData
{
    public class CsvReaderTests
    {
        [Test]
        public void TestEurUsdTxt()
        {
            TestAutoMappings ams = new TestAutoMappings();
            IMapper mapper = ams.CreateMapper();

            var engine = new FileHelperEngine<CandleDataRecord>();
            var frw = new FileReaderWriterService();
            var hdPath = frw.GetHistoricalDataFolderPath();
            var records = engine.ReadFile(Path.Combine(hdPath, "EURUSD2016.txt"));

            Assert.AreEqual(212445, records.Length);

            var candleViewModels = mapper.Map<List<CandleDataViewModel>>(records);
            Assert.AreEqual(212445, candleViewModels.Count);
            Assert.IsTrue(candleViewModels.TrueForAll(x => x.Open > 1));
            Assert.IsTrue(candleViewModels.TrueForAll(x => x.High > 1));
            Assert.IsTrue(candleViewModels.TrueForAll(x => x.Low > 1));
            Assert.IsTrue(candleViewModels.TrueForAll(x => x.Close > 1));
            Assert.IsTrue(candleViewModels.TrueForAll(x => x.DatePlusTime.Year > 2001));
        }

        [Test]
        public void TestEurUsdCsv()
        {
            TestAutoMappings ams = new TestAutoMappings();
            IMapper mapper = ams.CreateMapper();

            var engine = new FileHelperEngine<CandleDataType2Record>();
            var frw = new FileReaderWriterService();
            var hdPath = frw.GetHistoricalDataFolderPath();
            var records = engine.ReadFile(Path.Combine(hdPath, "EURUSD_M1_UTC+0_00.csv"));

            Assert.AreEqual(1139, records.Length);

            var candleType2ViewModels = mapper.Map<List<CandleDataType2ViewModel>>(records);
            Assert.AreEqual(1139, candleType2ViewModels.Count);
            Assert.IsTrue(candleType2ViewModels.TrueForAll(x => x.Open > 1));
            Assert.IsTrue(candleType2ViewModels.TrueForAll(x => x.High > 1));
            Assert.IsTrue(candleType2ViewModels.TrueForAll(x => x.Low > 1));
            Assert.IsTrue(candleType2ViewModels.TrueForAll(x => x.Close > 1));
            Assert.IsTrue(candleType2ViewModels.TrueForAll(x => x.DatePlusTime.Year > 2001));
        }

    }
}