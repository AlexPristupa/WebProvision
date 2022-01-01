using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MentolProvision.Auth;
using MentolProvision.Auth.Jwt;
using MentolProvision.Models.Request;
using MentolProvisionModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace MentolProvision.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ILogger<TokenController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IMemoryCache _cache;

        public TokenController(ILogger<TokenController> logger, UserManager<User> userManager, IMemoryCache cache)
        {
            _logger = logger;
            _userManager = userManager;
            _cache = cache;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Identity), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(AuthRequest item)
        {
            if(string.IsNullOrWhiteSpace(item.Login)) return NotFound();

            var user = await _userManager.FindByNameAsync(item.Login);

            if (user == null) return NotFound();

            var checkPass = await _userManager.CheckPasswordAsync(user, item.Password);

            if (!checkPass) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            var result = new Identity(user.Login, roles.ToArray());

            _cache.Set($"Bearer {result.AccessToken}", true);

            return Ok(result);
        }
    }
}
