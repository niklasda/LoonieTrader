using System.Collections.Generic;
using System.IO;
using AutoMapper;
using FileHelpers;
using LoonieTrader.Library.HistoricalData;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Services;
using LoonieTrader.Library.Tests.Locator;
using NUnit.Framework;

namespace LoonieTrader.Library.Tests.HistoricalData
{
    [TestFixture, Category("Integration")]
    public class CsvReaderTests
    {
        [Test]
        public void TestEurUsdTxt()
        {
            TestAutoMappings ams = new TestAutoMappings();
            IMapper mapper = ams.CreateMapper();

            var engine = new FileHelperEngine<CandleDataRecord>();
            IFileReaderWriterService frw = new FileReaderWriterService();
            string hdPath = frw.GetHistoricalDataFolderPath();
            var records = engine.ReadFile(Path.Combine(hdPath, "EURUSD2016.txt")); // todo hardcoded

            Assert.AreEqual(212445, records.Length);

            var candleViewModels = mapper.Map<List<CandleDataViewModel>>(records);
            Assert.AreEqual(212445, candleViewModels.Count);
            Assert.IsTrue(candleViewModels.TrueForAll(x => x.Open > 1));
            Assert.IsTrue(candleViewModels.TrueForAll(x => x.High > 1));
            Assert.IsTrue(candleViewModels.TrueForAll(x => x.Low > 1));
            Assert.IsTrue(candleViewModels.TrueForAll(x => x.Close > 1));
            Assert.IsTrue(candleViewModels.TrueForAll(x => x.DatePlusTime.Year > 2001));
        }
    }
}