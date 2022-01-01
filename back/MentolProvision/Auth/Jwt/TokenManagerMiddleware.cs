using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MentolProvision.Auth.Jwt
{
    public class TokenManagerMiddleware : IMiddleware
    {
        private readonly IMemoryCache _cache;
        public TokenManagerMiddleware(IMemoryCache cache)
        {
            _cache = cache;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var authorizationHeader = context.Request.Headers["authorization"];

            //Если запрос без авторизации продолжаем
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                await next(context);
                return;
            };

            bool status = false;
            if (_cache.TryGetValue(authorizationHeader.Single(), out status))
            {
                await next(context);

                return;
            }

            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}
