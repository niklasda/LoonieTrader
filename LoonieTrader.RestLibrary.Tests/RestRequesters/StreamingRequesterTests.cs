using System;
using NUnit.Framework;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.RestApi.Interfaces;
using LoonieTrader.RestLibrary.Tests.Locator;

namespace LoonieTrader.RestLibrary.Tests.RestRequesters
{
    public class StreamingRequesterTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var container = TestServiceLocator.Initialize();
            _txr = container.GetInstance<IStreamingRequester>();
            _s = container.GetInstance<ISettings>();
        }

        private IStreamingRequester _txr;
        private ISettings _s;

        [Test]
        public void Test()
        {
        }

    }
}
