using MentolProvisionLdapAuth;
using MentolProvisionModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentolProvision.Auth.Identity
{
    /// <summary>
    /// Клас для работы с паролем для Identity
    /// Переоопределяем генерацию пароля 
    /// Добавляем остальных провайдеров
    /// </summary>
    public class PasswordHasher : IPasswordHasher<User>
    {
        private readonly ILdapAuthInterface _ldap;
        private readonly ILogger<PasswordHasher> _logger;

        public PasswordHasher()
        {
        }

        public PasswordHasher(ILdapAuthInterface ldap, ILogger<PasswordHasher> logger)
        {
            _ldap = ldap;
            _logger = logger;
        }

        /// <summary>
        /// Генерация hash пароля
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string HashPassword(User user, string password)
        {
            return Md5Password.GetMd5Hash(password);
        }

        /// <summary>
        /// Проверка пароля
        /// </summary>
        /// <param name="user"></param>
        /// <param name="hashedPassword"></param>
        /// <param name="providedPassword"></param>
        /// <returns></returns>
        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            switch (user.Provider)
            {
                case "integrated": return CheckIntegratedProvider(user, hashedPassword, providedPassword);
                case "ldap": return CheckLdapProvider(user, hashedPassword, providedPassword);
                default: return PasswordVerificationResult.Failed;
            }
        }

        /// <summary>
        /// Проверка пароля по БД
        /// </summary>
        /// <param name="user"></param>
        /// <param name="hashedPassword"></param>
        /// <param name="providedPassword"></param>
        /// <returns></returns>
        private PasswordVerificationResult CheckIntegratedProvider(User user, string hashedPassword, string providedPassword)
        {
            if (hashedPassword == CryptoManager.AesEncrypt(providedPassword))
                return PasswordVerificationResult.Success;
            else
                return PasswordVerificationResult.Failed;
        }

        /// <summary>
        /// Проверка пароля через LDAP сервер
        /// </summary>
        /// <param name="user"></param>
        /// <param name="hashedPassword"></param>
        /// <param name="providedPassword"></param>
        /// <returns></returns>
        private PasswordVerificationResult CheckLdapProvider(User user, string hashedPassword, string providedPassword)
        {
            try
            {
                _ldap.Login(user.Login, providedPassword);
                return PasswordVerificationResult.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError("Ошибка авторизации LDAP {0}", user.Login);
                return PasswordVerificationResult.Failed;
            }
        }


    }
}
