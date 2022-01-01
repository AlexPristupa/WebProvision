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
	[Route("api/[controller]")]
	[ApiController]
	public class TasksListsController: ControllerBase
	{
		#region General

		private readonly ILogger<PagesController> _logger;
		private readonly IDataInterface _data;
		private readonly IStringLocalizer<PagesController> _localizer;

		public TasksListsController(ILogger<PagesController> logger, IDataInterface data, IStringLocalizer<PagesController> localizer)
		{
			_logger = logger;
			_data = data;
			_localizer = localizer;
		}

		#endregion

		#region End-points
		/// <summary>
		/// Получить информацию по всем спискам заданий
		/// </summary>
		/// <returns>Список заданий</returns>
		[ProducesResponseType(typeof(List<TasksListResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				var tlists = await _data.GetAllTasksListsAsync();

				if (!tlists.Any())
				{
					return NoContent();
				}

				var summary = new TasksListSummaryResponse();

				summary.Data.AddRange(tlists.Select(x => new TasksListResponse()
				{
					Idr = x.Idr,
					Name = x.Name,
					PrivateName = x.PrivateName
				}));

				summary.Meta.Count = summary.Data.Count;
				summary.Status.Result = "success";

				return Ok(summary);
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
