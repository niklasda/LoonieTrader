﻿using System;
using NUnit.Framework;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.Tests.Locator;

namespace LoonieTrader.Library.Tests.RestRequesters
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