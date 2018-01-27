using System;
using System.Collections.Generic;

namespace RsaSecureToken
{
    public class AuthenticationService
    {
        private IProfile _profileDao;
        private IToken _rsaToken;

        public AuthenticationService(IProfile profile, IToken token)
        {
            _profileDao = profile;
            _rsaToken = token;
        }

        public AuthenticationService()
        {
            _profileDao = new ProfileDao();
            _rsaToken = new RsaTokenDao();
        }

        public bool IsValid(string account, string passCode)
        {
            // 根據 account 取得自訂密碼
            var passwordFromDao = _profileDao.GetPassword(account);

            // 根據 account 取得 RSA token 目前的亂數
            var randomCode = _rsaToken.GetRandom(account);

            // 驗證傳入的 passCode 是否等於自訂密碼 + RSA token亂數
            var validPasscode = passwordFromDao + randomCode;
            var isValid = passCode == validPasscode;

            return isValid;
        }
    }

    public interface IToken
    {
        string GetRandom(string account);
    }

    public interface IProfile
    {
        string GetPassword(string account);
    }

    public class ProfileDao : IProfile
    {
        public string GetPassword(string account)
        {
            return Context.GetPassword(account);
        }
    }

    public static class Context
    {
        public static Dictionary<string, string> profiles;

        static Context()
        {
            profiles = new Dictionary<string, string>();
            profiles.Add("joey", "91");
            profiles.Add("mei", "99");
        }

        public static string GetPassword(string key)
        {
            return profiles[key];
        }
    }

    public class RsaTokenDao : IToken
    {
        public string GetRandom(string account)
        {
            var seed = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            var result = seed.Next(0, 999999).ToString("000000");
            Console.WriteLine("randomCode:{0}", result);

            return result;
        }
    }
}