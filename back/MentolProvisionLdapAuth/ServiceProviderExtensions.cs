using Microsoft.Extensions.DependencyInjection;
using System;

namespace MentolProvisionLdapAuth
{
    public static class ServiceProviderExtensions
    {
        public static void AddLdapAuthService(this IServiceCollection services)
        {
            services.AddTransient<ILdapAuthInterface, LdapAuth>();
        }
    }
}
