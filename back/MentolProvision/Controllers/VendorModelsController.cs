using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentolProvision.Models.Response;
using MentolProvisionInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace MentolProvision.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/modelsList")]
    [ApiController]
    public class VendorModelsController : ControllerBase
    {
        #region General
        private ILogger _logger;
        private IDataInterface _data;
        private readonly IStringLocalizer<VendorModelsController> _localizer;

        public VendorModelsController(ILogger<VendorModelsController> logger, IDataInterface data, IStringLocalizer<VendorModelsController> localizer)
        {
            _logger = logger;
            _data = data;
            _localizer = localizer;
        }
        #endregion

        #region End-points
        /// <summary>
        /// Получить информацию по всем моделям в разрезе вендора
        /// </summary>
        /// <param name="vendorId">ID вендора</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<VendorModelResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name = "vendorId")] int? vendorId)
        {
            try
            {
                if (vendorId == null)
                {
                    return BadRequest(_localizer["ERROR_NEED_VENDORID"].Value);
                }

                var models = await _data.GetVendorModelsAsync(vendorId.Value);

                if (models == null)
                {
                    return NotFound();
                }

                if (models.Count == 0)
                {
                    return NoContent();
                }

                var modelsResponse = models.Select(item => new VendorModelResponse{
                    Idr = item.Idr,
                    Name = item.Name
                });

                return Ok(modelsResponse.ToList());
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
