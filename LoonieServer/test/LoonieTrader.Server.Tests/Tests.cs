using System;
using LoonieTrader.Server;
using NUnit.Framework;

namespace LoonieTrader.Server.Tests
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            LogEntry le = new LogEntry();
            Assert.True(le!=null);
        }
    }
}
