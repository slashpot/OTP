﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace RsaSecureToken.Tests
{
    [TestClass()]
    public class AuthenticationServiceTests
    {
        //tips: 傳入 stub 物件的多種方式，可參考：http://www.dotblogs.com.tw/hatelove/archive/2012/11/27/learning-tdd-in-30-days-day6-several-ways-to-isolate-object-dependency-and-easy-for-testing.aspx
        // 1. 透過 target 的 constructor 傳入 interface/abstract 的 instance (DI auto-wiring)
        // 2. 透過 target 的 public property 傳入 (DI auto-wiring)
        // 3. target 方法的參數 (常看到傳入的參數型別為 interface的那種情況)
        // 4. target 將欲 stub 的方法擷取成 protected virtual 方法，使用 繼承 + override 的方式來測試原始target class

        [TestMethod()]
        public void IsValidTest()
        {
            var fakeProfile = Substitute.For<IProfile>();
            fakeProfile.GetPassword("joey").Returns("91");

            var fakeToken = Substitute.For<IToken>();
            fakeToken.GetRandom("").ReturnsForAnyArgs("000000");

            var target = new AuthenticationService(fakeProfile, fakeToken);
            var actual = target.IsValid("joey", "91000000");

            //always failed
            Assert.IsTrue(actual);
        }
    }
}