using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MentolProvisionInterface;
using MentolProvisionModel.CustomQueries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Serilog;

namespace MentolProvision.Controllers
{
	[Route("api/about")]
	[ApiController]
	public class AboutController: ControllerBase
	{
		private readonly ILogger _logger;
		private readonly IDataInterface _data;
		private readonly IStringLocalizer<AboutController> _localizer;

		public AboutController(ILogger logger, IDataInterface data, IStringLocalizer<AboutController> localizer)
		{
			_logger = logger;
			_data = data;
			_localizer = localizer;
		}

        #region EndPoints
        /// <summary>
        /// Получить информацию о продукте
        /// </summary>
        /// <returns>Информация о продукте</returns>
        [ProducesResponseType(typeof(ProductInfoRow), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<ProductInfoRow>> Get()
        {
	        try
	        {
		        var info = await _data.GetProductInfo("PROVISION");

		        if (info == null)
			        return NoContent();

		        return Ok(info);
	        }
	        catch (Exception ex)
	        {
		        _logger.Error(ex, _localizer["ERROR_METHOD_GET"].Value + ex.Message);
		        return StatusCode(500);
	        }
        }

        #endregion
    }
}