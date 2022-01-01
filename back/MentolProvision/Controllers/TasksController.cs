using System;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using MentolProvision.Extensions;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MentolProvisionModel;
using MentolProvisionInterface;
using MentolProvision.Models.Response;
using MentolProvisionRepository;
using MentolProvisionRepository.Filter;
using MentolProvision.Models.Request;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using MentolProvision.Enums;
using MentolProvision.Models;
using MentolProvision.Models.Response.Summaries;
using MentolProvisionModel.Infrastructure;
using Microsoft.Extensions.Configuration;
using ILogger = Serilog.ILogger;
using TaskStatus = MentolProvisionModel.Infrastructure.Constants.TaskStatus;

namespace MentolProvision.Controllers
{
	//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Route("api/[controller]")]
	[ApiController]
	public class TasksController : FilteredController<ServerTask, TasksController, PoolTaskResponse>
	{
		#region General

		private ILogger _logger;
		private IDataInterface _data;
		private readonly IStringLocalizer<TasksController> _localizer;
		private readonly IMemoryCache _memoryCache;
		private readonly ApplicationContext _context;
		private const int _taskCacheLifeTimeInMinutes = 5;
		private readonly IConfiguration _configuration;

		internal override Dictionary<string, List<string>> _propNames { get; set; } =
			new Dictionary<string, List<string>>()
			{
				{
					nameof(ServerTask.Idr),
					new List<string> {nameof(ServerTask.Idr)}
				},
				{
					nameof(ServerTask.Description),
					new List<string>()
					{
						nameof(ServerTask.Description)
					}
				},
				{
					nameof(ServerTask.Line.PhoneNumber),
					new List<string>
					{
						nameof(ServerTask.Line),
						nameof(ServerTask.Line.PhoneNumber)
					}
				},
				{
					nameof(ServerTask.User.Login),
					new List<string>
					{
						nameof(ServerTask.User),
						nameof(ServerTask.User.Login)
					}
				},
				{
					nameof(ServerTask.TaskList.Name),
					new List<string>
					{
						nameof(ServerTask.TaskList),
						nameof(ServerTask.TaskList.Name),
					}
				},
				{
					"DeviceName",
					new List<string>
					{
						nameof(ServerTask.Device),
						nameof(ServerTask.Device.Name),
					}
				},
				{
					"ProductionServerName",
					new List<string>
					{
						nameof(ServerTask.ProductionServer),
						nameof(ServerTask.ProductionServer.FQDN),
					}
				},
				{
					"ServerTestName",
					new List<string>
					{
						nameof(ServerTask.TestServer),
						nameof(ServerTask.TestServer.FQDN),
					}
				},
				{
					"UserDisplayName",
					new List<string>
					{
						nameof(ServerTask.User),
						nameof(ServerTask.User.DisplayName),
					}
				},
			};

		public TasksController(ILogger logger, IDataInterface data, IStringLocalizer<TasksController> localizer,
			ApplicationContext context, IMemoryCache memoryCache, IConfiguration configuration) : base(logger, localizer, context)
		{
			_logger = logger;
			_data = data;
			_context = context;
			_localizer = localizer;
			_memoryCache = memoryCache;
			_configuration = configuration;
		}

		#endregion

		#region End-points

		/// <summary>
		/// Получить информацию по всем задачам
		/// </summary>
		/// <param name="offset">Смещение</param>
		/// <param name="limit">Количество на страницу</param>
		/// <param name="filter">Фильтр</param>
		/// <returns></returns>
		[ProducesResponseType(typeof(TasksSummaryResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpGet]
		public async Task<IActionResult> Get([FromQuery(Name = "offset")] int? offset,
			[FromQuery(Name = "limit")] int? limit, string filter)
		{
			try
			{
				if (offset == null || limit == null)
					return BadRequest(_localizer["ERROR_NEED_LIMIT_OFFSET"].Value);

				var tasks = await _data.GetTasksAsync(offset.Value, limit.Value, filter);

				if (tasks.Count == 0)
					return NoContent();

				var summary = new TasksSummaryResponse(tasks);
				summary.Status.Result = "success";
				summary.Meta.Offset = offset.Value;
				summary.Meta.Limit = limit.Value;
				return Ok(summary);
			}
			catch (Exception ex)
			{
				_logger.Error(ex, _localizer["ERROR_METHOD_GET"].Value + ex.Message);
				return StatusCode(500);
			}
		}


		/// <summary>
		/// Получить информацию по всем задачам
		/// </summary>
		/// <param name="taskId">PK задачи</param>
		/// <param name="taskStatus">Статус задачи</param>
		/// <returns></returns>
		[ProducesResponseType(typeof(TasksSummaryResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpGet("detailTask")]
		public async Task<IActionResult> GetTaskDetails(int? taskId, string taskStatus)
		{
			try
			{
				if (!taskId.HasValue)
					return BadRequest(_localizer["ERROR_METHOD_GET_TASK_DETAILS_REQUIRED_TASK_ID"].Value);

				if (!ValidateTaskStatus(taskStatus, out var normalized))
					return BadRequest(_localizer["ERROR_METHOD_GET_TASK_DETAILS_INCORRECT_STATUS"].Value);

				var details = await _data.GetTaskDetailsAsync(taskId.Value, normalized);
				return Ok(details);
			}
			catch (Exception ex)
			{
				_logger.Error(ex, _localizer["ERROR_METHOD_GET"].Value + ex.Message);
				return StatusCode(500);
			}
		}

		/// <summary>
		/// Добавить задачу
		/// </summary>
		/// <param name="task"></param>
		/// <returns></returns>
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[ProducesResponseType(typeof(TaskPostResponse), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] TaskPostRequest task)
		{
			try
			{
				if (!RequestTaskActions.IsTaskAction(task.Meta.Action))
					ModelState.AddModelError(nameof(task.Meta.Action),
						_localizer["OPERATION_NAME_MUST_BE_ONE_OF_FOLLOWING",
							string.Join(", ", RequestTaskActions.TaskActions)]);

				var requestTaskDevice = await _data.GetDeviceWithLinesAsync(task.Task.DeviceId);
				if (requestTaskDevice == null)
					ModelState.AddModelError(string.Empty, _localizer["NO_DEVICE_WITH_ID", task.Task.DeviceId].Value);

				if (task.Meta.Action.Is(RequestTaskActions.Create))
				{
					if (task.Task.TaskDateRun == null)
						ModelState.AddModelError(string.Empty,
							_localizer["WHEN_TASK_IS_CREATED_TASKDATERUN_MUST_BE_FILLED"].Value);

					if (task.Task.TaskJsonNew == null)
						ModelState.AddModelError(string.Empty,
							_localizer["WHEN_TASK_IS_CREATED_TASKJSONNEW_MUST_BE_FILLED"].Value);

					if (await _data.GetTasksListAsync(task.Task.TaskTypeId) is null)
						ModelState.AddModelError(string.Empty,
							_localizer["NO_TASK_LIST_WITH_THE_SPECIFIELD_ID", task.Task.TaskTypeId].Value);

					if (task.Task.ServerTestId != null &&
					    await _data.GetServerAsync(task.Task.ServerTestId.Value) == null)
						ModelState.AddModelError(string.Empty,
							_localizer["NO_SERVER_WITH_THE_SPECIFIELD_SERVERTESTID", task.Task.ServerTestId]);
				}

				if (!ModelState.IsValid) return BadRequest(ModelState);

				var responseBuilder = new TaskPostResponseBuilder(_localizer);
				var login = HttpContext.User.FindFirstValue(ClaimTypes.Name);

				var taskInfo = new TaskInfo
				{
					UUID = task.Meta.UUID, 
					DeviceId = task.Task.DeviceId, 
					TaskListId = task.Task.TaskTypeId,
					UserLogin = login
				};
				var phoneNumbers = requestTaskDevice.Lines.Select(line => line.PhoneNumber).ToArray();
				var buildData = new TaskPostResponseBuildData
				{
					UUID = task.Meta.UUID, 
					PhoneNumbers = phoneNumbers, 
					UsersBooking = new[] {login}
				};

				IActionResult response = BadRequest();
				if (task.Meta.Action.Is(RequestTaskActions.Preview))
				{
					var existTasks =
						await _data.GetTasksFromTaskPoolByDeviceIdAndTaskListId(task.Task.DeviceId,
							task.Task.TaskTypeId);

					if (TryGetCachedTask(taskInfo, out var cashedTask))
					{
						if (existTasks.Any())
						{
							var device =
								buildData.TaskIds = existTasks.Select(task => task.Idr).ToArray();
							buildData.UsersBooking = new[] {cashedTask.UserLogin};

							var builtResponse =
								responseBuilder.BuildByCode(
									TaskPostResponseCode.TaskIsExistAndResourceBlockedByAnotherUser, buildData);
							response = StatusCode(builtResponse.HttpCode, builtResponse.Response);
						}
						else
						{
							var builtResponse =
								responseBuilder.BuildByCode(TaskPostResponseCode.ResourceBlockedByAnotherUser,
									buildData);
							response = StatusCode(builtResponse.HttpCode, builtResponse.Response);
						}
					}
					else
					{
						AddTaskToCache(taskInfo);

						if (existTasks.Any())
						{
							buildData.TaskIds = existTasks.Select(task => task.Idr).ToArray();
							buildData.UsersBooking = existTasks.Where(user => user.User != null)
								.Select(task => task.User.Login).ToArray();

							var builtResponse = responseBuilder.BuildByCode(
								TaskPostResponseCode.TaskIsExistAndResourceNotBlockedByAnotherUser, buildData);
							response = StatusCode(builtResponse.HttpCode, builtResponse.Response);
						}
						else
						{
							var builtResponse =
								responseBuilder.BuildByCode(TaskPostResponseCode.TaskNotExistAndNotBlockedByAnotherUser,
									buildData);
							response = StatusCode(builtResponse.HttpCode, builtResponse.Response);
						}
					}
				}
				else if (task.Meta.Action.Is(RequestTaskActions.Continue))
				{
					if (CacheContainsUUID(task.Meta.UUID))
					{
						var builtResponse =
							responseBuilder.BuildByCode(TaskPostResponseCode.TaskIsExistAndNotBlockedByAnotherUser,
								buildData);
						response = StatusCode(builtResponse.HttpCode, builtResponse.Response);
					}
					else
					{
						response = BadRequest();
					}
				}
				else if (task.Meta.Action.Is(RequestTaskActions.Create))
				{
					if (CacheContainsUUID(task.Meta.UUID))
					{
						var user = await _data.GetUserAsync(login);
						var newTask = new ServerTask
						{
							TaskListId = task.Task.TaskTypeId,
							Description = task.Task.TaskDescription,
							TestServerId = task.Task.ServerTestId,
							JsonNew = task.Task.TaskJsonNew,
							UsersId = user.Idr,
							DeviceId = task.Task.DeviceId
						};

						var newTaskPool = new TasksPool
						{
							DateRun = task.Task.TaskDateRun,
							DateCreated = DateTime.Now
						};
						await _data.AddTaskInTaskPoolAsync(newTask, newTaskPool);

						RemoveTaskFromCache(taskInfo);

						buildData.TaskIds = new[] {newTask.Idr};
						var builtResponse =
							responseBuilder.BuildByCode(TaskPostResponseCode.TaskCreatedAndNotBlockedByAnotherUser,
								buildData);
						response = StatusCode(builtResponse.HttpCode, builtResponse.Response);
					}
					else
					{
						response = BadRequest();
					}
				}
				else if (task.Meta.Action.Is(RequestTaskActions.Cancel))
				{
					RemoveTaskFromCache(taskInfo);

					var builtResponse = responseBuilder.BuildByCode(TaskPostResponseCode.ActionIsCanceled, buildData);
					response = StatusCode(builtResponse.HttpCode, builtResponse.Response);
				}

				return response;
			}
			catch (Exception ex)
			{
				_logger.Error(ex, _localizer["ERROR_METHOD_POST"].Value + ex.Message);
				return StatusCode(500);
			}
		}

		#endregion

		public override Expression<Func<ServerTask, PoolTaskResponse>> GetSelectExpression()
		{
			Expression<Func<ServerTask, PoolTaskResponse>> expression = task => new PoolTaskResponse()
			{
				TaskId = task.Idr.ToString(),
				DevicePhoneNumber = task.Line.PhoneNumber,
				TaskType = task.TaskList.Name,
				UserName = task.User.DisplayName,
				TaskDescription = task.Description,
				TaskDateRun = task.TasksPool.Max(tp => tp.DateRun).ToString(),
				ServerId = task.ServerId.ToString(),
				ServerTestId = task.TestServerId.ToString(),
				ServerFQDN = task.TestServer.FQDN,
			};
			return expression;
		}

		public override IOrderedQueryable<PoolTaskResponse> GetOrderExpression(string columnName, bool desc,
			IQueryable<PoolTaskResponse> _query)
		{
			switch (columnName)
			{
				case nameof(PoolTaskResponse.TaskId):
					return !desc ? _query.OrderBy(r => r.TaskId) : _query.OrderByDescending(t => t.TaskId);
				case nameof(Line.PhoneNumber):
				case nameof(PoolTaskResponse.DevicePhoneNumber):
					return !desc
						? _query.OrderBy(t => t.DevicePhoneNumber)
						: _query.OrderByDescending(t => t.DevicePhoneNumber);
				case nameof(PoolTaskResponse.TaskType):
					return !desc
						? _query.OrderBy(t => t.TaskType)
						: _query.OrderByDescending(t => t.TaskType);
				case nameof(PoolTaskResponse.UserName):
					return !desc
						? _query.OrderBy(t => t.UserName)
						: _query.OrderByDescending(t => t.UserName);
				case nameof(PoolTaskResponse.ServerId):
					return !desc
						? _query.OrderBy(t => t.ServerId)
						: _query.OrderByDescending(t => t.ServerId);
				case nameof(PoolTaskResponse.TaskDescription):
					return !desc
						? _query.OrderBy(t => t.TaskDescription)
						: _query.OrderByDescending(t => t.TaskDescription);
				case nameof(PoolTaskResponse.ServerFQDN):
					return !desc
						? _query.OrderBy(t => t.ServerFQDN)
						: _query.OrderByDescending(t => t.ServerFQDN);
				case nameof(PoolTaskResponse.ServerTestId):
					return !desc
						? _query.OrderBy(t => t.ServerTestId)
						: _query.OrderByDescending(r => r.ServerTestId);
				case nameof(PoolTaskResponse.UserLogin):
					return !desc
						? _query.OrderBy(t => t.UserLogin)
						: _query.OrderByDescending(t => t.UserLogin);
				case nameof(PoolTaskResponse.TaskDateRun):
					return !desc ? _query.OrderBy(r => r.TaskDateRun) : _query.OrderByDescending(t => t.TaskDateRun);
				default:
					throw new ArgumentException("invalid order column name", nameof(columnName));
			}
		}

		public override List<string> GeIncludes()
		{
			return new List<string>
			{
				nameof(ServerTask.TasksPool),
				nameof(ServerTask.TaskList),
				nameof(ServerTask.User),
				nameof(ServerTask.Device),
				nameof(ServerTask.Line),
			};
		}

		protected override async Task<int> GetCountExpressionAsync(IQueryable<ServerTask> query,
			FilterCountRequest request)
		{
			var count = await _data.GetTasksCountAsync(request);
			return count;
		}


		[ProducesResponseType(typeof(TaskPostResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public override async Task<IActionResult> GetFilteredAsync(FilterRequest request)
		{
			try
			{
				var result = await ((Repository) _data).GetFilteredTasksWithProcedureAsync(request);

				if (!result.Any())
					return NoContent();

				return Ok(GetSelectionResponse(result, request));
			}
			catch (Exception ex)
			{
				_logger.Error(ex, _localizer["ERROR_METHOD_GET"].Value + ex.Message);
				return StatusCode(500);
			}
		}

		[ProducesResponseType(typeof(TaskPostResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public override async Task<IActionResult> GetFilteredCountAsync(FilterCountRequest request)
		{
			try
			{
				var result = await ((Repository) _data).GetTasksCountAsync(request);
				return Ok(GetCountResponse(result, request));
			}
			catch (Exception ex)
			{
				_logger.Error(ex, _localizer["ERROR_METHOD_GET"].Value + ex.Message);
				return StatusCode(500);
			}
		}

		[Authorize]
		[ProducesResponseType(typeof(CancelTaskResponse<>), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(CancelTaskResponse<>), StatusCodes.Status202Accepted)]
		[ProducesResponseType(typeof(CancelTaskResponse<>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(CancelTaskResponse<>), StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(typeof(CancelTaskResponse<>), StatusCodes.Status403Forbidden)]
		[ProducesResponseType(typeof(CancelTaskResponse<>), StatusCodes.Status410Gone)]
		[ProducesResponseType(typeof(CancelTaskResponse<>), StatusCodes.Status500InternalServerError)]
		[HttpPost("return")]
		public async Task<IActionResult> CancelTask(CancelTaskRequest request)
		{
			if (request?.Meta.UUID == null)
				return BadRequest(_localizer["ERROR_NEED_TRANSACTION_UUID"].Value);

			if (string.IsNullOrEmpty(request.Meta.Action) || !RequestTaskActions.IsTaskAction(request.Meta.Action))
				return BadRequest(_localizer["OPERATION_NAME_MUST_BE_ONE_OF_FOLLOWING",
					string.Join(", ", RequestTaskActions.TaskActions.Except(new[] {RequestTaskActions.Continue}))]);

			var taskId = request.Task.TaskId;

			if (taskId == null)
				return BadRequest(_localizer["ERROR_METHOD_CANCEL_TASK_ NEED_TASK_ID"].Value);

			var task = await _data.GetTaskAsync(taskId.Value);
			if (task == null)
				return BadRequest(_localizer["ERROR_NO_TASK_WITH_THE_SPECIFIED_ID"].Value);

			var userLogin = HttpContext.User.FindFirstValue(ClaimTypes.Name);
			var transactionId = request.Meta.UUID;

			CacheEntry<TasksPool> cacheEntry;

			if (request.Meta.Action.Is(RequestTaskActions.Preview))
			{
				if (request.Task.TaskDateRun == null)
					return BadRequest(_localizer["WHEN_TASK_IS_CREATED_TASKDATERUN_MUST_BE_FILLED"].Value);

				if (request.Task.ServerTestId != null && await _data.GetServerAsync(request.Task.ServerTestId.Value) == null)
					return BadRequest(_localizer["NO_SERVER_WITH_THE_SPECIFIELD_SERVERTESTID", request.Task.ServerTestId].Value);

				if (!_memoryCache.TryGetValue(taskId.Value, out cacheEntry))
				{
					var taskPool = await _data.GetTasksPoolByTaskIdAsync(taskId.Value);
					if (taskPool == null)
					{
						var response = new CancelTaskResponse<AcceptedTaskData>
						{
							Meta = {UUID = transactionId, UserBooking = userLogin},
							Status = {Code = (int)TaskCancelResponseCode.TaskNotExistAndNotBlockedByAnotherUser, Message = null, Result = "success"},
							Task =
							{
								TaskDescription = task.Description,
								DeviceId = task.DeviceId,
								TaskTypeId = task.TaskList.Idr,
								TaskDateRun = request.Task.TaskDateRun,
								ServerTestId = request.Task.ServerTestId,
								TaskJsonNew = task.JsonNew,
								TaskJsonOld = task.JsonOld
							}
						};

						CreateCacheEntry(request.Task, transactionId, userLogin, DateTime.Now);
						return AcceptedAtAction(nameof(CancelTask), response);
					}
					else
					{
						var response = new CancelTaskResponse<ForbiddenTaskData>
						{
							Meta = {UUID = transactionId, UserBooking = userLogin},
							Status =
							{
								Code = (int)TaskCancelResponseCode.TaskAlreadyExistsAndNonBlockedByAnotherUser,
								Message = _localizer["ERROR_TASKPOOL_FOR_CANCEL_WITH_TASKID_ALREADY_EXISTS", taskId.Value].Value,
								Result = "error"
							},
							Task = {TaskId = taskId.Value}
						};
						return StatusCode(StatusCodes.Status403Forbidden, response); 
					}
				}

				var taskPoolFromDb = await _data.GetTasksPoolByTaskIdAsync(taskId.Value);
				if (taskPoolFromDb == null)
				{
					if (task.DeviceId == null)
						return BadRequest($"Task with Idr: {task.Idr} has no DeviceId field specified");

					var lines = await _data.GetLinesByDeviceIdsAsync(new[] {task.DeviceId.Value});

					string message;

					if (lines.Any())
						message = _localizer["ERROR_TASK_FOR_PHONENUMBER_IS_BEING_ALREADY_CREATED_BY_LOGIN",
							lines.First().LinePhoneNumber, userLogin ?? "NULL"].Value;
					else
						message = _localizer["ERROR_TASK_FOR_DEVICE_IS_BEING_ALREADY_CREATED_BY_LOGIN", task.Device,
							task.User.Login].Value;

					var response = new CancelTaskResponse<ForbiddenTaskData>
					{
						Meta = {UUID = transactionId, UserBooking = userLogin},
						Status = {Code = (int)TaskCancelResponseCode.TaskIsBlockedByAnotherUser, Message = message, Result = "error"},
						Task = {TaskId = taskId.Value}
					};
					return StatusCode(StatusCodes.Status403Forbidden, response);
				}
				else
				{
					var response = new CancelTaskResponse<ForbiddenTaskData>
					{
						Meta = {UUID = transactionId, UserBooking = userLogin},
						Status =
						{
							Code = (int)TaskCancelResponseCode.TaskAlreadyExistsAndBlockedByAnotherUser,
							Message = _localizer["ERROR_TASKPOOL_FOR_CANCEL_WITH_TASKID_ALREADY_EXISTS_AND_EDITED_BY_LOGIN", task.Idr, task.User.Login].Value,
							Result = "error"
						},
						Task = {TaskId = taskId.Value}
					};
					return StatusCode(StatusCodes.Status403Forbidden, response);
				}
			}

			if (!_memoryCache.TryGetValue(taskId.Value, out cacheEntry))
				return BadRequest(_localizer["ERROR_CACHE_ENTRY_WAS_NOT_FOUND", taskId.Value].Value);

			if (request.Meta.Action.Is(RequestTaskActions.Create))
			{
				var login = HttpContext.User.FindFirstValue(ClaimTypes.Name);

				var user = await _data.GetUserAsync(login);

				var taskForCancel = new ServerTask
				{
					TestServerId = cacheEntry.TestServerId,
					JsonNew = task.JsonOld ?? string.Empty,
					Description = cacheEntry.TaskDescription,
					TaskListId = task.TaskListId,
					UsersId = user.Idr
				};

				taskForCancel.TasksPool.Add(cacheEntry.Data);

				var key = await _data.AddTaskAsync(taskForCancel);
				task.CancelTaskId = key;
				_context.Tasks.Update(task);
				await _context.SaveChangesAsync();

				_memoryCache.Remove(taskId.Value);

				var response = new CancelTaskResponse<CreatedTaskData>
				{
					Meta =
					{
						UUID = transactionId,
						UserBooking = userLogin
					},
					Status = {Code = (int)TaskCancelResponseCode.TaskCreatedAndNotBlockedByAnotherUser, Result = "success", Message = $"Задание {key} для отмены успешно создано"},
					Task =
					{
						TaskId = taskId.Value
					}
				};
				return CreatedAtAction(nameof(CancelTask), response);
			}

			//cancel action
			var cancelTaskResponse = new CancelTaskResponse<CancelTaskData>
			{
				Meta =
				{
					UUID = transactionId,
					UserBooking = userLogin
				},
				Status = {Code = (int)TaskCancelResponseCode.ActionIsCanceled, Result = "error", Message = null},
				Task =
				{
					TaskId = taskId.Value
				}
			};

			_memoryCache.Remove(taskId.Value);
			return StatusCode(StatusCodes.Status410Gone, cancelTaskResponse);
		}

		protected override object GetSelectionResponse(List<PoolTaskResponse> data, FilterRequest request)
		{
			var response = new TasksSummaryResponse(data)
			{
				Meta =
				{
					Offset = request.Offset,
					OrderDesc = request.OrderDesc ?? true,
					TableColumn = request.TableColumn,
					Limit = request.Limit,
					Search = request.Search
				},
				Status = {Result = "success"}
			};
			return response;
		}

		#region Private methods

		private void CreateCacheEntry(CancelTaskData taskData, string transactionKey, string userLogin, DateTime timeTrace)
		{
			var lifeTimeConfig = _configuration.GetSection("CachingConfig:ExpirationSpanInMinutes")?.Value;

			if (!int.TryParse(lifeTimeConfig, out int entrylifeTime))
				entrylifeTime = 5;

			var cacheValue = new CacheEntry<TasksPool>(new TasksPool {DateCreated = timeTrace, DateRun = taskData.TaskDateRun}, transactionKey, taskData,  userLogin);
			_memoryCache.Set(taskData.TaskId, cacheValue, new DateTimeOffset(timeTrace).AddMinutes(entrylifeTime));
		}

		private bool ValidateTaskStatus(string status, out string normalized)
		{
			normalized = null;
			if (string.IsNullOrEmpty(status))
				return false;

			normalized = status.ToLower();
			return new[] {TaskStatus.Current, TaskStatus.Done}.Contains(normalized);
		}

		private bool CacheContainsUUID(string uuid)
		{
			return _memoryCache.TryGetValue(uuid, out _);
		}

		private bool TryGetCachedTask(TaskInfo taskInfo, out TaskInfo task)
		{
			var success = _memoryCache.TryGetValue(taskInfo.ToString(), out var cachedTask);
			if (success)
				task = cachedTask as TaskInfo;
			else
				task = null;

			return success;
		}

		private void AddTaskToCache(TaskInfo taskInfo)
		{
			var expirationTime = DateTimeOffset.Now.AddMinutes(_taskCacheLifeTimeInMinutes);

			_memoryCache.Set(taskInfo.ToString(), taskInfo, expirationTime);
			_memoryCache.Set(taskInfo.UUID, taskInfo, expirationTime);
		}

		private void RemoveTaskFromCache(TaskInfo taskInfo)
		{
			_memoryCache.Remove(taskInfo.UUID);
			_memoryCache.Remove(taskInfo.ToString());
		}

		#endregion

		public class TaskInfo
		{
			public string UUID { get; set; }
			public int TaskListId { get; set; }
			public int DeviceId { get; set; }
			public string UserLogin { get; set; }

			public override string ToString()
			{
				return DeviceId.ToString() + TaskListId.ToString();
			}
		}

		public static class RequestTaskActions
		{
			public const string Preview = "preview";
			public const string Continue = "continue";
			public const string Create = "create";
			public const string Cancel = "cancel";

			public static IReadOnlyCollection<string> TaskActions { get; } = new[] {Preview, Continue, Create, Cancel};

			public static bool IsTaskAction(string action)
			{
				return TaskActions.Any(taskAction => taskAction.Is(action));
			}
		}
	}
}