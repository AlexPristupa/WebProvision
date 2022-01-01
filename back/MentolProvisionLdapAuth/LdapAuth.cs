using MentolProvisionLdapAuth.Model;
using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolProvisionLdapAuth
{
    public class LdapAuth : ILdapAuthInterface
    {
        private readonly LdapConfig _config;
        /// <summary>
        /// Инициализируем подключение в конструкторе с LDAP сервером
        /// </summary>        
        public LdapAuth(IOptions<LdapConfig> config)
        {
            _config = config.Value;
        }

        public LdapUser Login(string login, string password)
        {
            LdapUser user = new LdapUser();

            using (var connection = new LdapConnection())
            {

                connection.Connect(_config.Url, _config.Port);
                connection.Bind(_config.BindDn, _config.BindCredentials);

                var searchFilter = string.Format(_config.SearchFilter, login);


                var result = connection.Search(
                    _config.SearchBase,
                    LdapConnection.ScopeSub,
                    searchFilter,
                    new[] { "displayName", "sAMAccountName" },
                    false
                );

                var bufUser = result.Next();

                if (bufUser != null)
                {
                    connection.Bind(bufUser.Dn, password);
                    if (connection.Bound)
                    {
                        user.Login = bufUser.GetAttribute("sAMAccountName").StringValue;
                        user.DisplayName = bufUser.GetAttribute("displayName").StringValue;
                    }
                }

                return null;
            }
        }

        public List<LdapUser> Search(string search)
        {
            List<LdapUser> users = new List<LdapUser>();
            using (var connection = new LdapConnection())
            {
                connection.Connect(_config.Url, _config.Port);
                connection.Bind(_config.BindDn, _config.BindCredentials);

                var result = connection.Search(
                                _config.SearchBase,
                                LdapConnection.ScopeSub,
                                "(objectclass=*)",
                                new[] { "displayName", "sAMAccountName" },
                                false
                                );


                while (result.HasMore())
                {
                    var ldapUser = result.Next();
                    var login = "";
                    var displayName = "";

                    try
                    {
                        login = ldapUser.GetAttribute("sAMAccountName").StringValue;
                        displayName = ldapUser.GetAttribute("displayName").StringValue;
                    }
                    catch (Exception ex)
                    {

                    }

                    if (login.ToLower().Contains("search") || displayName.ToLower().Contains(search))
                    {
                        users.Add(new LdapUser
                        {
                            Login = login,
                            DisplayName = displayName
                        });
                    }
                }
            }
            return users;
        }
    }
}
