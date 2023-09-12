using LoonieTrader.Library.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoonieTrader.Library.Tests.Extensions;

[TestClass]
public class DateTimeExTests
{
    [TestMethod]
    public void TestEurUsdTxt()
    {

        var date = new DateTime(2022, 12, 2, 8, 9, 7);
        date = date.AddMilliseconds(123);
        var timeString = date.ToRfc3339();
        Assert.AreEqual("2022-12-02T08:09:07.123000Z", timeString);

    }
}