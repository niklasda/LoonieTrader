using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Models;
using LoonieTrader.RestLibrary.RestApi.Interfaces;
using LoonieTrader.RestLibrary.RestApi.Responses;
using LoonieTrader.RestLibrary.Tests.Locator;

namespace LoonieTrader.RestLibrary.Tests.RestRequesters
{
    public class AccountsRequesterTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var container = TestServiceLocator.Initialize();
            _ar = container.GetInstance<IAccountsRequester>();
            _s = container.GetInstance<ISettings>();
        }

        private IAccountsRequester _ar;
        private ISettings _s;

        [Test]
        public void TestGetAccounts()
        {
            Assert.NotNull(_ar.GetAccounts());
        }


        [Test]
        public void TestGetAccountDetails()
        {
            Assert.NotNull(_ar.GetAccountDetails(_s.DefaultAccountId));
        }

        [Test]
        public void TestGetAccountSummary()
        {
            Assert.NotNull(_ar.GetAccountSummary(_s.DefaultAccountId));
        }

        [Test]
        public void TestGetAccountInstruments()
        {
            AccountInstrumentsResponse air = _ar.GetInstruments(_s.DefaultAccountId);
            Assert.NotNull(air);

            Assert.AreEqual(23, air.instruments.Count(x => x.type == "METAL"));
            Assert.AreEqual(28, air.instruments.Count(x => x.type == "CFD"));
            Assert.AreEqual(71, air.instruments.Count(x => x.type == "CURRENCY"));
        }

        [Test]
        public void TestGetAccountInstrumentHierarchy()
        {
            AccountInstrumentsResponse air = _ar.GetInstruments(_s.DefaultAccountId);
            Assert.NotNull(air);

            IEnumerable<IGrouping<string, AccountInstrumentsResponse.Instrument>> groups = air.instruments.Select(x => x).GroupBy(x => x.type);

            List<InstrumentType> its = groups.Select(x => new InstrumentType {Type = x.Key, Instruments = x.ToArray()}).ToList();

            Assert.AreEqual(3, its.Count);

            Assert.AreEqual("METAL", its[0].Type);
            Assert.AreEqual(23, its[0].Instruments.Length);

            Assert.AreEqual("CFD", its[1].Type);
            Assert.AreEqual(28, its[1].Instruments.Length);

            Assert.AreEqual("CURRENCY", its[2].Type);
            Assert.AreEqual(71, its[2].Instruments.Length);

        }
    }
}
