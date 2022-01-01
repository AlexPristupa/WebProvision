using System;
using System.Collections.Generic;
using System.Text;

namespace MentolProvisionLdapAuth.Model
{
    /// <summary>
    /// Конфигурация LDAP сервера
    /// </summary>
    public class LdapConfig
    {
        public string Url { get; set; }
        public int Port { get; set; }
        public string BindDn { get; set; }
        public string BindCredentials { get; set; }
        public string SearchBase { get; set; }
        public string SearchFilter { get; set; }
        public string AdminCn { get; set; }
    }
}
