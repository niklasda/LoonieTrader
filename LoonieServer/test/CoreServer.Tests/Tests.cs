using System;
using ConsoleApplication;
using NUnit.Framework;

namespace Tests
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
