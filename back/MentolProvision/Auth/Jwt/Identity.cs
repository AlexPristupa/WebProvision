using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MentolProvision.Auth.Jwt
{
    public class Identity
    {
        /// <summary>
        /// Token
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// Логин
        /// </summary>
        public string UserName { get; set; }

        public Identity(string login, string[] roles)
        {
            this.UserName = login;
            this.AccessToken = JwtResponse(login, roles);
        }

        private ClaimsIdentity GetClaims(string login, string[] roles)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, login));

            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, item));
            }


            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }

        private string JwtResponse(string login, string[] roles)
        {
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: GetClaims(login, roles).Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
