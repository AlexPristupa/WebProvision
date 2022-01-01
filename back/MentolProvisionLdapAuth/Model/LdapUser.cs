using System;
using System.Collections.Generic;
using System.Text;

namespace MentolProvisionLdapAuth.Model
{
    /// <summary>
    /// LDAP пользователь
    /// </summary>
    public class LdapUser
    {
        public string Login { get; set; }
        public string DisplayName { get; set; }
    }
}
