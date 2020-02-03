using AspStudy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using Microsoft.Ajax.Utilities;

namespace AspStudy.Models
{
    public class CustomMemberShipProvider : MembershipProvider
    {
        public override bool EnablePasswordRetrieval => throw new NotImplementedException();

        public override bool EnablePasswordReset => throw new NotImplementedException();

        public override bool RequiresQuestionAndAnswer => throw new NotImplementedException();

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override int MaxInvalidPasswordAttempts => throw new NotImplementedException();

        public override int PasswordAttemptWindow => throw new NotImplementedException();

        public override bool RequiresUniqueEmail => throw new NotImplementedException();

        public override MembershipPasswordFormat PasswordFormat => throw new NotImplementedException();

        public override int MinRequiredPasswordLength => throw new NotImplementedException();

        public override int MinRequiredNonAlphanumericCharacters => throw new NotImplementedException();

        public override string PasswordStrengthRegularExpression => throw new NotImplementedException();

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// User名とPasswordを引数に、ログインできるか否か
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public override bool ValidateUser(string username, string password)
        {
            // Hash化したパスワードを取得する。
            string hashPass = GeneratePasswordHash(username, password);

            // Modelを使ってdbとやり取りをするときは、Contextをnewする。
            using(var db = new AspStudyContext())
            {
                var user = db.Users.
                    Where(u => u.UserName == username && u.Password == hashPass).
                    FirstOrDefault();

                if (user != null)
                {
                    return true;
                }
            }

            if (username.Equals("admin") && password.Equals("pass"))
            {
                return true;
            }


            return false;
        }

        public string GeneratePasswordHash(string userId, string password)
        {
            // 頭に特定の文字列を加え長い文字にする。
            string rawSalt = $"seacret_{userId}";
            // sha256のhashインスタンスをnewする
            var sha256 = new SHA256CryptoServiceProvider();

            // 上記rawsaltを実際にハッシュ化する。
            var salt = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(rawSalt));

            // パスワードと上記saltを使い10000回ストレッチング（ハッシュ化を１万回連続で行う）
            var pdkdf2 = new Rfc2898DeriveBytes(password,salt,10000);
            var hash = pdkdf2.GetBytes(32);

            // 戻り値のbyteの配列を文字列にしてかえす。
            return Convert.ToBase64String(hash);
        }
    }
}