using System;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using MentolProvision.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using MentolProvisionModel;
using MentolProvisionInterface;
using MentolProvision.Models.Response;
using MentolProvision.Models.Response.Summaries;
using MentolProvisionModel.Exceptions;
using MentolProvisionModel.CustomQueries;
using MentolProvisionModel.Infrastructure;
using MentolProvisionRepository;
using MentolProvisionRepository.Filter;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace MentolProvision.Controllers
{
	//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Route("api/[controller]")]
	[ApiController]
	public class DevicesController : FilteredController<Device, DevicesController, DeviceResponse>
	{
		#region General

		private ILogger _logger;
		private IDataInterface _data;
		private readonly IStringLocalizer<DevicesController> _localizer;

		internal override Dictionary<string, List<string>> _propNames { get; set; } =
			new Dictionary<string, List<string>>()
			{
				{nameof(Device.Idr), new List<string>() {nameof(Device.Idr)}},
				{nameof(Device.Name), new List<string>() {nameof(Device.Name)}},
				{nameof(Device.Description), new List<string>() {nameof(Device.Description)}},
				{nameof(Device.IpAddress), new List<string>() {nameof(Device.IpAddress)}},
			};

		public DevicesController(ILogger logger, IDataInterface data, IStringLocalizer<DevicesController> localizer,
			ApplicationContext context)
			: base(logger, localizer, context)
		{
			_logger = logger;
			_data = data;
			_localizer = localizer;
		}

		#endregion

		#region End-points

		/// <summary>
		/// Получить информацию по всем устройствам или по одному конкретному
		/// </summary>
		/// <param name="id">Идентификатор</param>
		/// <param name="offset">Смещение</param>
		/// <param name="limit">Количество на страницу</param>
		/// <returns></returns>
		[ProducesResponseType(typeof(List<Device>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpGet]
		public async Task<IActionResult> Get([FromQuery(Name = "offset")] int? offset,
			[FromQuery(Name = "limit")] int? limit)
		{
			try
			{
				if (offset == null || limit == null)
					return BadRequest(_localizer["ERROR_NEED_ID_OR_LIMIT_OFFSET"].Value);

				var devices = await _data.GetDevicesAsync(offset.Value, limit.Value);

				if (!devices.Any())
					return NoContent();

				var devicesResponse = devices.Select(item => new DeviceResponse
				{
					PhoneId = item.PhoneId,
					PhoneName = item.PhoneName,
					PhoneDescription = item.PhoneDescription,
					PhoneIpAddress = item.PhoneIpAddress,
					LinePhoneNumber = item.LinePhoneNumber,
				}).ToList();


				var deviceIds = devicesResponse.Select(x => x.PhoneId).ToArray();
				var lines = await _data.GetLinesByDeviceIdsAsync(deviceIds);

				AddLinesToDevices(devicesResponse, lines);

				var summary = new DevicesListSummaryResponse {Meta = {Count = devicesResponse.Count, Limit = limit.Value, Offset = offset.Value}};
				summary.Devices.AddRange(devicesResponse);
				return Ok(summary);
			}
			catch (Exception ex)
			{
				_logger.Error(ex, _localizer["ERROR_METHOD_GET"].Value + ex.Message);
				return StatusCode(500);
			}
		}

		[ProducesResponseType(typeof(List<Device>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public override async Task<IActionResult> GetFilteredAsync(FilterRequest request)
		{
			try
			{
				if (request?.Offset == null || request.Limit == null)
					return BadRequest(_localizer["ERROR_NEED_LIMIT_OFFSET"].Value);

				request.TableColumn = request.TableColumn.Capitalize();

				var devices = await ((Repository) _data).GetDevicesFilteredAsync(request);

				var dResponse = devices.Select(d => new DeviceResponse
				{
					PhoneId = d.PhoneId,
					PhoneName = d.PhoneName,
					PhoneIpAddress = d.PhoneIpAddress,
					PhoneDescription = d.PhoneDescription,
					LinePhoneNumber = d.LinePhoneNumber
				}).ToList();

				var deviceIds = dResponse.Select(x => x.PhoneId).ToArray();
				var lines = await _data.GetLinesByDeviceIdsAsync(deviceIds);

				AddLinesToDevices(dResponse, lines);

				return Ok(GetSelectionResponse(dResponse, request));
			}
			catch (InvalidColumnNameException e)
			{
				_logger.Error(e, string.Format(_localizer["ERROR_INVALID_COLUMN_NAME"].Value, e.ColumnName));
				return BadRequest(string.Format(_localizer["ERROR_INVALID_COLUMN_NAME"].Value, e.ColumnName));
			}
			catch (Exception e)
			{
				_logger.Error(e, _localizer["ERROR_METHOD_GET"].Value + e.Message);
				return StatusCode(500);
			}
		}

		private void AddLinesToDevices(IEnumerable<DeviceResponse> devicesResponse, IEnumerable<LineRow> lines)
		{
			foreach (var device in devicesResponse)
			{
				device.Lines.AddRange(lines.Where(l => l.DeviceId.Equals(device.PhoneId)).Select(l => new LineResponse()
				{
					LineId = l.LineId,
					LineASCIIDisplayCallerId = l.LineASCIIDisplayCallerId,
					LineDisplayCallerId = l.LineDisplayCallerId,
					LineDescription = l.LineDescription,
					LineAlertingName = l.LineAlertingName,
					LineASCIIAlertingName = l.LineASCIIAlertingName,
					LineUserAssociatedLine = l.LineUserAssociatedLine,
					LinePhoneNumber = l.LinePhoneNumber
				}));
			}
		}

		[ProducesResponseType(typeof(List<Device>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public override async Task<IActionResult> GetFilteredCountAsync(FilterCountRequest request)
		{
			try
			{
				var count = await ((Repository) _data).GetDevicesFilteredCountAsync(request.Search,
					request.TableColumn.Capitalize());
				var response = new DevicesCountResponse()
				{
					Meta = {Count = count, TableColumn = request.TableColumn, Search = request.Search}
				};
				return Ok(response);
			}
			catch (InvalidColumnNameException e)
			{
				_logger.Error(e, string.Format(_localizer["ERROR_INVALID_COLUMN_NAME"].Value, e.ColumnName));
				return BadRequest(string.Format(_localizer["ERROR_INVALID_COLUMN_NAME"].Value, e.ColumnName));
			}
			catch (Exception e)
			{
				_logger.Error(e, _localizer["ERROR_METHOD_GET"].Value + e.Message);
				return StatusCode(500);
			}
		}

		#endregion

		#region Private filtering methods

		public override Expression<Func<Device, DeviceResponse>> GetSelectExpression()
		{
			Expression<Func<Device, DeviceResponse>> expression = t => new DeviceResponse
			{
				PhoneId = t.Idr,
				PhoneName = t.Name,
				PhoneDescription = t.Description,
				PhoneIpAddress = t.IpAddress,
			};

			return expression;
		}

		protected override object GetSelectionResponse(List<DeviceResponse> data, FilterRequest request)
		{
			return new DevicesFiltrationSummaryResponse
			{
				Devices = data,
				Meta =
				{
					Search = request.Search,
					OrderDesc = request.OrderDesc ?? true,
					Limit = request.Limit,
					Offset = request.Offset,
					TableColumn = request.TableColumn
				}
			};
		}

		[NonAction]
		public override IOrderedQueryable<DeviceResponse> GetOrderExpression(string columnName, bool desc,
			IQueryable<DeviceResponse> _query)
		{
			return columnName switch
			{
				nameof(Device.Idr) => !desc ? _query.OrderBy(r => r.PhoneId) : _query.OrderByDescending(r => r.PhoneId),
				nameof(Device.Name) => !desc ? _query.OrderBy(r => r.PhoneName) : _query.OrderByDescending(r => r.PhoneName),
				nameof(Device.Description) => !desc
					? _query.OrderBy(r => r.PhoneDescription)
					: _query.OrderByDescending(r => r.PhoneDescription),
				nameof(Device.IpAddress) => !desc
					? _query.OrderBy(r => r.PhoneIpAddress)
					: _query.OrderByDescending(r => r.PhoneIpAddress),
				_ => throw new ArgumentException("invalid order column name", nameof(columnName)),
			};
		}

		#endregion
	}
}