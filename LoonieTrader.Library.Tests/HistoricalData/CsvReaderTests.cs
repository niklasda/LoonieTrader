using FileHelpers;
using LoonieTrader.Library.HistoricalData;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoonieTrader.Library.Tests.HistoricalData;

[TestClass, TestCategory("Integration")]
public class CsvReaderTests
{
    [TestMethod]
    public void TestEurUsdTxt()
    {
        //TestAutoMappings ams = new TestAutoMappings();
        //IMapper mapper = ams.CreateMapper();

        var engine = new FileHelperEngine<CandleDataRecord>();
        IFileReaderWriterService frw = new FileReaderWriterService();
        string hdPath = frw.GetHistoricalDataFolderPath();
        var records = engine.ReadFile(Path.Combine(hdPath, "EURUSD2022.txt")); // todo hardcoded

        Assert.AreEqual(366351, records.Length);

        //  var candleViewModels = mapper.Map<List<CandleDataViewModel>>(records);

        var recordList = records.ToList();

        Assert.AreEqual(366351, recordList.Count);
        Assert.IsTrue(recordList.TrueForAll(x => x.Open > 0.9m));
        Assert.IsTrue(recordList.TrueForAll(x => x.High > 0.9m));
        Assert.IsTrue(recordList.TrueForAll(x => x.Low > 0.9m));
        Assert.IsTrue(recordList.TrueForAll(x => x.Close > 0.9m));
        Assert.IsTrue(recordList.TrueForAll(x => x.Date.Length > 6));
    }
}