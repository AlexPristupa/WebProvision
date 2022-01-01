using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MentolProvision.Auth;
using MentolProvision.Auth.Jwt;
using MentolProvision.Models.Request;
using MentolProvisionModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace MentolProvision.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SignOutController : ControllerBase
    {
        private readonly ILogger<TokenController> _logger;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMemoryCache _cache;

        public SignOutController(ILogger<TokenController> logger, IHttpContextAccessor httpContext, IMemoryCache cache)
        {
            _logger = logger;
            _httpContext = httpContext;
            _cache = cache;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var authorizationHeader = _httpContext
             .HttpContext.Request.Headers["authorization"];
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                bool status = false;
                if (_cache.TryGetValue(authorizationHeader.Single(), out status))
                {
                    _cache.Remove(authorizationHeader.Single());
                }
            }

            return Ok();
        }
    }
}
