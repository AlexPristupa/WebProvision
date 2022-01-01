using MentolProvisionLdapAuth.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolProvisionLdapAuth
{
    public interface ILdapAuthInterface
    {
        LdapUser Login(string login, string password);
        List<LdapUser> Search(string search);
    }
}
