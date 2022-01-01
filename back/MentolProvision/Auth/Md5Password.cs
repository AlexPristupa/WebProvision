using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MentolProvision.Auth
{
    /// <summary>
    /// Для работы с MD5 паролями
    /// </summary>
    public static class Md5Password
    {
        /// <summary>
        /// Получить md5 hash пароля
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string GetMd5Hash(string password)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }

        /// <summary>
        /// Сверить пароль с хэшем
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hashPassword"></param>
        /// <returns></returns>
        public static bool Verify(string password, string hashPassword)
        {
            string hashOfInput = GetMd5Hash(password);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hashPassword))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
