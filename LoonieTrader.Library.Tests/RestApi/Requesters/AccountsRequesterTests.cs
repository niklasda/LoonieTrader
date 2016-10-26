using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Models;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;
using LoonieTrader.Library.Tests.Locator;

namespace LoonieTrader.Library.Tests.RestApi.Requesters
{
    [TestFixture, Category("Integration")]
    public class AccountsRequesterTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var container = TestServiceLocator.Initialize();
            _ar = container.GetInstance<IAccountsRequester>();
            _s = container.GetInstance<ISettingsService>().CachedSettings.SelectedEnvironment;
        }

        private IAccountsRequester _ar;
        private IEnvironmentSettings _s;

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
            AccountInstrumentsResponse air = _ar.GetAccountInstruments(_s.DefaultAccountId);
            Assert.NotNull(air);

            Assert.AreEqual(28, air.instruments.Count(x => x.type == "CFD"));
            Assert.AreEqual(71, air.instruments.Count(x => x.type == "CURRENCY"));
            Assert.AreEqual(23, air.instruments.Count(x => x.type == "METAL"));
        }

        [Test]
        public void TestGetAccountInstrumentSortedHierarchy()
        {
            AccountInstrumentsResponse air = _ar.GetAccountInstruments(_s.DefaultAccountId);
            Assert.NotNull(air);

            IEnumerable<IGrouping<string, AccountInstrumentsResponse.Instrument>> groups = air.instruments.Select(x => x).OrderBy(y => y.type).GroupBy(x => x.type);

            List<InstrumentType> its = groups.Select(x => new InstrumentType {Type = x.Key, Instruments = x.ToArray()}).ToList();

            Assert.AreEqual(3, its.Count);


            Assert.AreEqual("CFD", its[0].Type);
            Assert.AreEqual(28, its[0].Instruments.Length);

            Assert.AreEqual("CURRENCY", its[1].Type);
            Assert.AreEqual(71, its[1].Instruments.Length);

            Assert.AreEqual("METAL", its[2].Type);
            Assert.AreEqual(23, its[2].Instruments.Length);

        }

        [Test]
        public void TestGetAccountConfigurationChanges()
        {
            AccountChangesResponse air = _ar.GetAccountChanges(_s.DefaultAccountId, "2920"); // 3337-2920=417
            Console.WriteLine(air);
            Assert.NotNull(air);
        }
    }
}
