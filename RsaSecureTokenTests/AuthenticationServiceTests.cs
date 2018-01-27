using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RsaSecureToken.Tests
{
    [TestClass()]
    public class AuthenticationServiceTests
    {
        [TestMethod()]
        public void IsValidTest()
        {
            IProfile profile = new TestProfile();
            IToken token = new TestToken();

            var target = new AuthenticationService(profile, token);
            var actual = target.IsValid("joey", "91000000");

            //always failed
            Assert.IsTrue(actual);
        }
    }

    public class TestProfile : IProfile
    {
        public string GetPassword(string account)
        {
            if (account == "joey")
            {
                return "91";
            }
            throw new InvalidOperationException();
        }
    }

    public class TestToken : IToken
    {
        public string GetRandom(string account)
        {
            return "000000";
        }
    }
}