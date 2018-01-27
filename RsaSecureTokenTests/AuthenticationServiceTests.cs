using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RsaSecureToken.Tests
{
    [TestClass()]
    public class AuthenticationServiceTests
    {
        [TestMethod()]
        public void IsValidTest()
        {
            IProfile profile = new ProfileDao();
            IToken token = new TestToken();

            var target = new AuthenticationService(profile, token);
            var actual = target.IsValid("joey", "91000000");

            //always failed
            Assert.IsTrue(actual);
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