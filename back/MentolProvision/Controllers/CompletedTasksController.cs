using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using MentolProvision.Extensions;
using MentolProvision.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using MentolProvisionModel;
using MentolProvisionInterface;
using MentolProvision.Models.Response;
using MentolProvision.Models.Response.Summaries;
using MentolProvisionModel.Infrastructure;
using MentolProvisionModel.Infrastructure.FilterModels;
using MentolProvisionRepository;
using MentolProvisionRepository.Filter;
using Serilog;

namespace MentolProvision.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/tasks/completed")]
    [ApiController]
    public class CompletedTasksController : FilteredController<TasksHistory, CompletedTasksController, CompletedTaskResponse>
    {
        #region General
        private ILogger _logger;
        private IDataInterface _data;
        private readonly IStringLocalizer<CompletedTasksController> _localizer;

        internal override Dictionary<string, List<string>> _propNames { get; set; } =
            new Dictionary<string, List<string>>()
            {
                            { nameof(TasksHistory.Task.Idr), new List<string>() { nameof(TasksHistory.Task), nameof(TasksHistory.Task.Idr) } },
                            { nameof(TasksHistory.Task.TaskList.Name), new List<string>() { nameof(TasksHistory.Task),
                                                                                            nameof(TasksHistory.Task.TaskList),
                                                                                            nameof(TasksHistory.Task.TaskList.Name) } },
                            { nameof(TasksHistory.Task.Description), new List<string>() { nameof(TasksHistory.Task), nameof(TasksHistory.Task.Description) } },
                            { nameof(TasksHistory.Task.TestServerId), new List<string>() { nameof(TasksHistory.Task), nameof(TasksHistory.Task.TestServerId) } },
                            { nameof(TasksHistory.DateRun), new List<string>() { nameof(TasksHistory.DateRun) } },
                            { nameof(TasksHistory.DateEnd), new List<string>() { nameof(TasksHistory.DateEnd) } },
                            { nameof(TasksHistory.Status), new List<string>() { nameof(TasksHistory.Status) } },
            };

        public CompletedTasksController(ILogger logger, IDataInterface data, IStringLocalizer<CompletedTasksController> localizer, ApplicationContext context)
            : base(logger, localizer, context)
        {
            _logger = logger;
            _data = data;
            _localizer = localizer;
        }
        #endregion

        #region End-points
        /// <summary>
        /// Получить информацию по всем историям
        /// </summary>
        /// <param name="offset">Смещение</param>
        /// <param name="limit">Количество на страницу</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<TasksHistory>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name = "offset")] int? offset, [FromQuery(Name = "limit")] int? limit)
        {
            try
            {
                if (offset == null || limit == null)
                {
                    return BadRequest(_localizer["ERROR_NEED_LIMIT_OFFSET"].Value);
                }

                var tasks = await _data.GetCompletedTasksAsync(offset.Value, limit.Value);

                if (!tasks.Any())
	                return NoContent();


                var tasksResponse = tasks.Select(r => 
	            new CompletedTaskResponse
                {
	                TaskCompletedId = r.TaskCompletedId,
	                TaskId = r.TaskId,
	                DevicePhoneNumber = r.DevicePhoneNumber,
	                TaskType = r.TaskType,
	                UserName = r.UserName,
	                UserLogin = r.UserLogin,
	                TaskDescription = r.TaskDescription,
	                TaskDateCreate = r.TaskDateCreate,
	                TaskDateRun = r.TaskDateRun,
	                TaskDateEnd = r.TaskDateEnd,
	                TaskStatus = r.TaskStatus,
	                TaskStatusDesc = r.TaskStatusDesc,
	                TaskCancelId = r.TaskCancelId,
	                ServerTestId = r.ServerTestId,
	                ServerFQDN = r.ServerFQDN
                }).ToList();

                return Ok(new CompletedTasksSummaryResponse {
                     Tasks = tasksResponse,
                     Meta = {Count = tasksResponse.Count, Offset = offset.Value, Limit = limit.Value}
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex, _localizer["ERROR_METHOD_GET"].Value + ex.Message);
                return StatusCode(500);
            }
        }

        public override async Task<IActionResult> GetFilteredAsync(FilterRequest request)
        {
	        try
	        {
		        var data = await ((Repository) _data).GetCompletedTasksFromProcedureAsync(request);

		        if (!data.Any())
			        return NoContent();

		        var responseData = data.Select(x => x.ToCompletedTaskResponse()).ToList();
		        return Ok(GetSelectionResponse(responseData, request));
	        }
	        catch (Exception ex)
	        {
		        _logger.Error(ex, _localizer["ERROR_METHOD_GET"].Value + ex.Message);
		        return StatusCode(500);
            }
        }


        public override async Task<IActionResult> GetFilteredCountAsync(FilterCountRequest request)
        {
	        try
	        {
		        var count = await ((Repository) _data).GetCompletedTasksCountFromProcedureAsync(request);
		        return Ok(GetCountResponse(count, request));
	        }
	        catch (Exception ex)
	        {
		        _logger.Error(ex, _localizer["ERROR_METHOD_GET"].Value + ex.Message);
		        return StatusCode(500);
	        }
        }

        [ProducesResponseType(typeof(List<TasksHistory>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("selectionDate")]
        public async Task<IActionResult> GetByDateRange([FromQuery] DateRangeRequest rangeRequest)
        {
	        try
	        {
		        if (rangeRequest.DateFrom == null || rangeRequest.DateTo == null)
		        {
			        return BadRequest(_localizer["ERROR_DATE_RANGE_IS_REQUIRED"].Value);
		        }

                if (rangeRequest.Offset == null || rangeRequest.Limit == null)
                {
	                return BadRequest(_localizer["ERROR_NEED_LIMIT_OFFSET"].Value);
                }

                rangeRequest.TableColumn = rangeRequest.TableColumn.Capitalize();

                if (!ValidateTableColumnParam(rangeRequest.TableColumn))
                {
	                return BadRequest(_localizer["ERROR_TABLE_COLUMN_IS_INCORRECT"].Value);
                }

                var data = await _data.GetCompletedTasksByDateRangeAsync(rangeRequest);

                if (!data.Any())
	                return NoContent();

				var responseData = data.Select(x => x.ToCompletedTaskResponse()).ToList();
				return Ok(GetSelectionResponse(responseData, rangeRequest));
	        }
	        catch (Exception ex)
	        {
		        _logger.Error(ex, _localizer["ERROR_METHOD_GET"].Value + ex.Message);
		        return StatusCode(500);
	        }
        }


        [ProducesResponseType(typeof(List<TasksHistory>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("totalCountDate")]
        public async Task<IActionResult> GetCountByDateRange([FromQuery] DateRangeCountRequest rangeRequest)
        {
	        try
	        {
		        if (rangeRequest.DateFrom == null || rangeRequest.DateTo == null)
		        {
			        return BadRequest(_localizer["ERROR_DATE_RANGE_IS_REQUIRED"].Value);
		        }

		        if (!ValidateTableColumnParam(rangeRequest.TableColumn.Capitalize()))
		        {
			        return BadRequest(_localizer["ERROR_TABLE_COLUMN_IS_INCORRECT"].Value);
		        }

		        rangeRequest.TableColumn = rangeRequest.TableColumn.Capitalize();
		        var count = await _data.GetTotalCountByDateRangeAsync(rangeRequest);
		        var response = new CompletedTasksDateRangeCountResponse
		        {
			        Meta = {TableColumn = rangeRequest.TableColumn, Count = count}
		        };
		        return Ok(response);
	        }
	        catch (Exception ex)
	        {
		        _logger.Error(ex, _localizer["ERROR_METHOD_GET"].Value + ex.Message);
		        return StatusCode(500);
	        }
        }



        protected override object GetCountResponse(int count, FilterCountRequest request)
        {
	        return new CompletedTasksCountResponse
	        {
		        Meta =
		        {
			        Count = count,
			        TableColumn = request.TableColumn,
			        Search = request.Search
		        }
	        };
        }



        public override Expression<Func<TasksHistory, CompletedTaskResponse>> GetSelectExpression()
        {
	        throw new NotImplementedException();
        }

        public override IOrderedQueryable<CompletedTaskResponse> GetOrderExpression(string columnName, bool desc, IQueryable<CompletedTaskResponse> _query)
        {
	        throw new NotImplementedException();
        }

        #endregion

        #region Private filtering methods

        private bool ValidateTableColumnParam(string columnName)
        {
	        if (string.IsNullOrEmpty(columnName))
		        return false;

	        return new[]
	        {
		        nameof(CompletedTaskResponse.TaskDateCreate),
		        nameof(CompletedTaskResponse.TaskDateEnd),
		        nameof(CompletedTaskResponse.TaskDateRun),
	        }.Contains(columnName);
        }

        protected override object GetSelectionResponse(List<CompletedTaskResponse> data, FilterRequest request)
        {
	        var summary = new CompletedTasksFiltrationSummaryResponse
	        {
		        Meta =
		        {
			        Offset = request.Offset,
			        Limit = request.Limit,
			        TableColumn = request.TableColumn,
			        OrderDesc = request.OrderDesc ?? true,
			        Search = request.Search
		        },
		        Tasks = data
	        };
	        return summary;
        }


        protected object GetSelectionResponse(List<CompletedTaskResponse> data, DateRangeRequest request)
        {
	        var summary = new CompletedTasksDateRangeFiltrationSummaryResponse
            {
		        Meta =
		        {
			        Offset = request.Offset ?? -1,
			        Limit = request.Limit ?? -1,
			        Count = data.Count,
			        OrderDesc = request.OrderDesc ?? false,
		        },
		        Tasks = data
	        };
	        return summary;
        }

        #endregion
    }
}
