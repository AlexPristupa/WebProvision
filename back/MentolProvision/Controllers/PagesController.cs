using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentolProvision.Models.Response;
using MentolProvisionInterface;
using MentolProvisionModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace MentolProvision.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        #region General
        private ILogger _logger;
        private IDataInterface _data;
        private readonly IStringLocalizer<PagesController> _localizer;

        public PagesController(ILogger<PagesController> logger, IDataInterface data, IStringLocalizer<PagesController> localizer)
        {
            _logger = logger;
            _data = data;
            _localizer = localizer;
        }
        #endregion

        #region End-points
        /// <summary>
        /// Получить информацию по страницам
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<PageResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                 var pages = await _data.GetAllPageAsync();

                if (pages == null)
                {
                    return NotFound();
                }

                if (pages.Count == 0)
                {
                    return NoContent();
                }

                var pagesResponse = pages.Select(item => new PageResponse
                {
                    Idr = item.Idr,
                    ViewName = item.ViewName,
                    ParentId = item.ParentId
                });

                return Ok(pagesResponse.ToList());
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
