using System;
using NUnit.Framework;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Tests.Locator;

namespace LoonieTrader.RestLibrary.Tests.RestRequesters.v3
{
    public class AccountsRequesterTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var container = ServiceLocator.Initialize();
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
            var air = _ar.GetInstruments(_s.DefaultAccountId);
            Assert.NotNull(air);
            Console.WriteLine(air);
        }
    }
}
