using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MentolProvisionInterface;
using MentolProvisionModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace MentolProvision.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/rolesList")]
    [ApiController]
    public class RolesListController : ControllerBase
    {
        #region General
        private ILogger _logger;
        private IDataInterface _data;
        private readonly IStringLocalizer<RolesListController> _localizer;

        public RolesListController(ILogger<RolesListController> logger, IDataInterface data, IStringLocalizer<RolesListController> localizer)
        {
            _logger = logger;
            _data = data;
            _localizer = localizer;
        }
        #endregion

        #region End-points
        /// <summary>
        /// Получить информацию по всем ролям
        /// </summary>
        /// <returns>Список ролей</returns>
        [ProducesResponseType(typeof(List<Device>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var roles = await _data.GetAllRolesAsync();

                if (roles == null)
                {
                    return NotFound();
                }

                if (roles.Count == 0)
                {
                    return NoContent();
                }

                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _localizer["ERROR_METHOD_GET"].Value + ex.Message);
                return StatusCode(500);
            }
        }       
        #endregion
    }
}