using MentolProvisionInterface;
using MentolProvisionModel;
using MentolProvisionModel.CustomQueries;
using MentolProvisionModel.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MentolProvision.Models.Request;
using MentolProvisionModel.Exceptions;
using MentolProvisionRepository.Filter;
using Microsoft.Data.SqlClient;
using MentolProvisionModel.Infrastructure.FilterModels;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Version = MentolProvisionModel.Version;

namespace MentolProvisionRepository
{
	public class Repository : IDataInterface
	{
		#region General

		/// <summary>
		/// Контекст данных
		/// </summary>
		private readonly ApplicationContext db;

		/// <summary>
		/// Инициализация репозитория контекстом
		/// </summary>
		/// <param name="_db"></param>
		/// <returns></returns>
		public Repository(ApplicationContext _db)
		{
			db = _db;
		}

		#endregion

		#region ActionList

		/// <summary>
		/// Получить данные действий по идентификаторам
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public async Task<List<ActionList>> GetActionListAsync(IEnumerable<int> ids)
		{
			return await db.ActionLists.Where(al => ids.Contains(al.Idr)).ToListAsync();
		}

		#endregion

		#region Device

		/// <summary>
		/// Получить данные устройства
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<Device> GetDeviceAsync(int id)
		{
			var device = await db.Devices.SingleOrDefaultAsync(p => p.Idr == id);
			return device;
		}

		//<inheritdoc/>
		public async Task<Device> GetDeviceWithLinesAsync(int id)
		{
			return await db.Devices.Include(device => device.Lines).FirstOrDefaultAsync(p => p.Idr == id);
		}

		/// <summary>
		/// Получить информацию по всем устройствам
		/// </summary>
		/// <param name="offset">Смещение</param>
		/// <param name="limit">Количество на страницу</param>
		/// <returns></returns>
		public async Task<List<DeviceRow>> GetDevicesAsync(int offset, int limit)
		{
			using var connection = new SqlConnection(db.Database.GetDbConnection().ConnectionString);
			using var command = connection.CreateCommand();

			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "dbo.api_get_devices_phones";
			command.Parameters.AddWithValue("@__p_1", offset);
			command.Parameters.AddWithValue("@__p_2", limit);

			var list = new List<DeviceRow>();

			await connection.OpenAsync();
			using var reader = await command.ExecuteReaderAsync();
			while (await reader.ReadAsync())
				list.Add(MapToDeviceRow(reader));

			return list;
		}

		


		public async Task<List<DeviceRow>> GetDevicesFilteredAsync(FilterRequest request)
		{
			using var connection = new SqlConnection(db.Database.GetDbConnection().ConnectionString);
			using var command = connection.CreateCommand();

			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "dbo.api_get_devices_phones_selection";
			command.Parameters.AddWithValue("@__filter_0", request.Search ?? string.Empty);
			command.Parameters.AddWithValue("@__filter_1", request.TableColumn ?? string.Empty);
			command.Parameters.AddWithValue("@__p_1", request.Offset);
			command.Parameters.AddWithValue("@__p_2", request.Limit);
			command.Parameters.AddWithValue("@__sort_direction_p_3", request.OrderDesc ?? true ? "desc": "asc");

			var list = new List<DeviceRow>();

			await connection.OpenAsync();
			using var reader = await command.ExecuteReaderAsync();
			while (await reader.ReadAsync())
				  list.Add(MapToDeviceRow(reader));
			return list;
		}

		private DeviceRow MapToDeviceRow(IDataReader reader)
		{
			var row = new DeviceRow();
			row.PhoneId = reader.GetInt32(0);
			row.PhoneName = reader.GetString(1);
			row.PhoneDescription = reader.IsDBNull(2) ? null : reader.GetString(2);
			row.PhoneIpAddress = reader.IsDBNull(3) ? null : reader.GetString(3);
			row.LinePhoneNumber = reader.GetString(4);
			return row;
		}

		public async Task<int> GetDevicesFilteredCountAsync(string search, string tableColumn)
		{
			using var connection = new SqlConnection(db.Database.GetDbConnection().ConnectionString);
			var command = connection.CreateCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "dbo.api_get_devices_phones_totalcount";
			command.Parameters.AddWithValue("@__filter_0", search ?? string.Empty);
			command.Parameters.AddWithValue("@__filter_1", tableColumn ?? string.Empty);
			await connection.OpenAsync();
			return (int) await command.ExecuteScalarAsync();
		}

		public Task<List<LineRow>> GetLinesByDeviceIdsAsync(int[] devicesIds)
		{
			if (devicesIds == null)
				 throw new ArgumentException(nameof(devicesIds));

			if (!devicesIds.Any())
				return Task.FromResult(new List<LineRow>());

			var query =
				from l in db.Lines
				where devicesIds.Contains(l.DeviceId)
				select new LineRow
				{
					LineId = l.Idr,
					DeviceId = l.DeviceId,
					LinePhoneNumber = l.PhoneNumber,
					LineDescription = l.Description,
					LineDisplayCallerId = l.DisplayCallerId,
					LineASCIIDisplayCallerId = l.AsciiDisplayCallerId,
					LineAlertingName = l.AlertingName,
					LineASCIIAlertingName = l.AsciiAlertingName,
					LineUserAssociatedLine = l.UserAssociatedLine
				};
			return query.ToListAsync();
		}


		private IOrderedQueryable<DeviceRow> GetOrderExpression(string columnName, bool isDesc,
			IQueryable<DeviceRow> query)
		{
			return columnName switch
			{
				nameof(DeviceRow.PhoneId) => !isDesc
					? query.OrderBy(x => x.PhoneId)
					: query.OrderByDescending(x => x.PhoneId),
				nameof(DeviceRow.PhoneName) => !isDesc
					? query.OrderBy(x => x.PhoneName)
					: query.OrderByDescending(x => x.PhoneName),
				nameof(DeviceRow.PhoneDescription) => !isDesc
					? query.OrderBy(x => x.PhoneDescription)
					: query.OrderByDescending(x => x.PhoneDescription),
				nameof(DeviceRow.PhoneIpAddress) => !isDesc
					? query.OrderBy(x => x.PhoneIpAddress)
					: query.OrderByDescending(x => x.PhoneIpAddress),
				_ => throw new ArgumentException("invalid order column name", nameof(columnName)),
			};
		}

		#endregion

		#region ActionList

		/// <summary>
		/// Получить данные объектов по идентификаторам
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public async Task<List<ObjectList>> GetObjectListAsync(IEnumerable<int> ids)
		{
			return await db.ObjectLists.Where(ol => ids.Contains(ol.Idr)).ToListAsync();
		}

		#endregion

		#region Page

		/// <summary>
		/// Получить страницу по идентификатору
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<Page> GetPageByIdAsync(int id)
		{
			return await db.Pages.FirstOrDefaultAsync(page => page.Idr == id);
		}

		/// <summary>
		/// Получить все страницы
		/// </summary>
		/// <returns></returns>
		public async Task<List<Page>> GetAllPageAsync()
		{
			return await db.Pages.AsNoTracking().ToListAsync();
		}

		/// <summary>
		/// Получить страницы для указанного пользователя
		/// </summary>
		/// <param name="userId">Идентификатор пользователя</param>
		/// <returns></returns>
		public async Task<List<Page>> GetPagesByUserIdAsync(int userId)
		{
			//
			return await (from users in db.Users
					join utr in db.UsersToRoles on users.Idr equals utr.UserId
					join r in db.Roles on utr.RoleId equals r.Idr
					join rp in db.PagesToRoles on r.Idr equals rp.RoleId
					join pages in db.Pages on rp.PageId equals pages.Idr
					where users.Idr == userId
					select pages)
				.ToListAsync();
		}

		#endregion

		#region PageToRole

		/// <summary>
		/// Получить данные о страницах в разрезе роли
		/// </summary>
		/// <param name="roleId"></param>
		/// <returns></returns>
		public async Task<List<PageToRole>> GetPagesToRolesAsync(int roleId)
		{
			return await db.PagesToRoles.Where(item => item.RoleId == roleId).ToListAsync();
		}

		#endregion

		#region Role

		/// <summary>
		/// Получить информацию по ролям
		/// </summary>
		/// <param name="id">ID</param>
		/// <param name="name">Название</param>
		/// <returns></returns>
		public async Task<List<Role>> GetRolesAsync(int? id, string name)
		{
			if (id != null && name != null)
				return await db.Roles.Where(x => x.Name.Contains(name) && x.Idr == id).AsNoTracking().ToListAsync();

			if (id != null) return await db.Roles.Where(x => x.Idr == id).AsNoTracking().ToListAsync();

			if (name != null) return await db.Roles.Where(x => x.Name.Contains(name)).AsNoTracking().ToListAsync();

			return await db.Roles.AsNoTracking().ToListAsync();
		}

		public async Task<List<Role>> GetUserRolesAsync(int userId)
		{
			var query = from p in db.Roles
				join x in db.UsersToRoles on p.Idr equals x.RoleId
				where x.UserId == userId
				select new Role {Idr = p.Idr, Name = p.Name, Description = p.Description};

			return await query.ToListAsync();
		}

		/// <summary>
		/// Получить информацию по всем ролям
		/// </summary>
		/// <returns>Список ролей</returns>
		public async Task<List<Role>> GetAllRolesAsync()
		{
			return await db.Roles.AsNoTracking().ToListAsync();
		}

		/// <summary>
		/// Изменить информацию для роли
		/// </summary>
		/// <param name="role"></param>
		/// <returns></returns>
		public async Task<int?> UpdateRoleAsynс(Role role)
		{
			db.Roles.Update(role);

			return await db.SaveChangesAsync();
		}

		#endregion

		#region RoleAction

		/// <summary>
		/// Заменяет доступы роли к объектам для указанной роли
		/// </summary>
		/// <param name="roleId">Идентификатор роли</param>
		/// <param name="roleActions">Доступы роли к объектам</param>
		/// <returns></returns>
		public async Task<int?> ReplaceRoleActionForRoleIdAsync(int roleId, IEnumerable<RoleAction> roleActions)
		{
			var previousRoleActions = await db.RoleActions.Where(pr => pr.RoleId == roleId).ToListAsync();

			var toRemove = previousRoleActions.Except(roleActions, new ActionRoleSameRoleIdObjectId());
			var toInsert = roleActions.Except(previousRoleActions, new ActionRoleSameRoleIdObjectId());
			var toUpdate = new List<RoleAction>();

			var roleActionDictionary = roleActions.ToDictionary(rl => new {rl.RoleId, rl.ObjectId});
			foreach (var roleAction in previousRoleActions.Intersect(roleActions, new ActionRoleSameRoleIdObjectId()))
			{
				var newRoleAction = roleActionDictionary[new {roleAction.RoleId, roleAction.ObjectId}];
				if (roleAction.ActionId != newRoleAction.ActionId)
				{
					roleAction.ActionId = newRoleAction.ActionId;
					toUpdate.Add(roleAction);
				}
			}

			db.RoleActions.RemoveRange(toRemove);
			db.RoleActions.AddRange(toInsert);
			db.RoleActions.UpdateRange(toUpdate);

			return await db.SaveChangesAsync();
		}

		#endregion

		#region PageToRole

		/// <summary>
		/// Заменяет доступы роли к страницам для указанной роли
		/// </summary>
		/// <param name="roleId">Идентификатор роли</param>
		/// <param name="pagesToRoles">Доступы роли к страницам</param>
		/// <returns></returns>
		public async Task<int?> ReplacePageToRolesForRoleIdAsync(int roleId, IEnumerable<PageToRole> pagesToRoles)
		{
			var removingPagesToRole = await db.PagesToRoles.Where(pr => pr.RoleId == roleId).ToListAsync();
			db.PagesToRoles.RemoveRange(removingPagesToRole);
			db.PagesToRoles.AddRange(pagesToRoles);

			return await db.SaveChangesAsync();
		}

		#endregion

		#region Server

		public Task<Server> GetServerByIpAddress(string ipAddress)
		{
			if (string.IsNullOrEmpty(ipAddress))
				throw new ArgumentException(nameof(ipAddress));

			return db.Servers
				.Include(s => s.Nodes)
				.ThenInclude(x => x.Versions)
				.Include(x => x.VendorModel)
				.ThenInclude(x => x.Vendor)
				.FirstOrDefaultAsync(p => ipAddress.Equals(p.IpAddress) && !p.IsDeleted.Value);
		}

		/// <summary>
		/// Получить информацию по всем серверам
		/// </summary>
		/// <param name="offset">Смещение</param>
		/// <param name="limit">Количество на страницу</param>
		/// <returns></returns> 
		public Task<List<Server>> GetServersAsync(int offset, int limit)
		{
			return db.Servers
				.Include(x => x.Versions)
				.Include(s => s.Nodes)
				.ThenInclude(n => n.Versions)
				.Include(x => x.VendorModel)
				.ThenInclude(x => x.Vendor)
				.Where(s => !s.IsDeleted.Value)
				.Skip(offset * limit)
				.Take(limit)
				.ToListAsync();
		}

		public async Task<List<ServerNodeRow>> GetServerRowsFromProcedure(FilterRequest request)
		{
			var serverNodeRows = new List<ServerNodeRow>();

			using (var connection = new SqlConnection(db.Database.GetDbConnection().ConnectionString))
			{
				var command = connection.CreateCommand();
				command.CommandType = CommandType.StoredProcedure;
				command.CommandText = "dbo.api_get_servers_selection";
				command.Parameters.AddWithValue("@__filter_0", request.Search ?? string.Empty);
				command.Parameters.AddWithValue("@__filter_1", request.TableColumn ?? string.Empty);
				command.Parameters.AddWithValue("@__p_1", request.Offset);
				command.Parameters.AddWithValue("@__p_2", request.Limit);
				command.Parameters.AddWithValue("@__sort_direction_p_3", request.OrderDesc ?? true ? "desc" : "asc");
				await connection.OpenAsync();
				using var reader = await command.ExecuteReaderAsync();
				while (await reader.ReadAsync())
					serverNodeRows.Add(MapToServerNodeRow(reader));
			}
			return serverNodeRows;
		}

		public async Task<int> GetServersCountFromProcedureAsync(FilterCountRequest request)
		{
			using var connection = new SqlConnection(db.Database.GetDbConnection().ConnectionString);
			var command = connection.CreateCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "dbo.api_get_servers_totalcount";
			command.Parameters.AddWithValue("@__filter_0", request.Search ?? string.Empty).DbType = DbType.String;
			command.Parameters.AddWithValue("@__filter_1", request.TableColumn ?? string.Empty).DbType = DbType.String;
			await connection.OpenAsync();
			return (int)await command.ExecuteScalarAsync();
		}

		private ServerNodeRow MapToServerNodeRow(IDataReader reader)
		{
			var row = new ServerNodeRow();

			row.ServerId = reader.GetInt32(0);
			row.ServerFQDN = reader.IsDBNull(1) ? null: reader.GetString(1);
			row.ServerIpAddress = reader.IsDBNull(2) ? null: reader.GetString(2);
			row.ServerPort = reader.IsDBNull(3) ? null:  (int?)reader.GetInt32(3);
			row.ServerLogin =  reader.IsDBNull(4) ? null: reader.GetString(4);
			row.ServerDescription = reader.IsDBNull(5) ? null : reader.GetString(5);
			row.ServerVendorModelId = reader.IsDBNull(6) ? null:  (int?)reader.GetInt32(6);
			row.ServerVendorName =  reader.IsDBNull(7) ? null: reader.GetString(7);
			row.ServerIsEnabled = reader.IsDBNull(8) ? null : (bool?)reader.GetBoolean( 8);
			row.ServerIsTest = reader.IsDBNull(9) ? null : (bool?)reader.GetBoolean(9);
			row.NodeId = reader.IsDBNull(10) ? null: reader.GetString(10);
			return row;
		}

		/// <summary>
		/// Получить данные сервера
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Task<Server> GetServerAsync(int id)
		{
			return db.Servers
				.Include(s => s.Nodes)
				.ThenInclude(x => x.Versions)
				.Include(x => x.VendorModel)
				.ThenInclude(x => x.Vendor)
				.SingleOrDefaultAsync(p => p.Idr == id && !p.IsDeleted.Value);
		}

		/// <summary>
		/// Получить данные сервера
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public Task<Server> GetServerAsync(string name)
		{
			return db.Servers.SingleOrDefaultAsync(p => p.FQDN == name);
		}



		/// <summary>
		/// Добавить сервер
		/// </summary>
		/// <param name="server"></param>
		/// <returns></returns>
		public async Task<int?> AddServerAsync(Server server)
		{
			await db.Servers.AddAsync(server);
			await db.SaveChangesAsync();
			return server.Idr;
		}

		/// <summary>
		/// Удалить сервер
		/// </summary>
		/// <param name="server"></param>
		/// <returns></returns>
		public async Task<int?> DeleteServerAsync(Server server)
		{
			var versions = new List<Version>(server.Versions);
			versions.AddRange(server.Nodes.SelectMany(x => x.Versions));

			if (versions.Count > 0)
				db.RemoveRange(versions);

			if (server.Nodes.Count > 0)
			{
				var ipAddress = server.Nodes.OrderBy(x => x.Priority).First().IpAddress;
				if (!string.IsNullOrEmpty(ipAddress))
					server.IpAddress = ipAddress;
				db.RemoveRange(server.Nodes);
			}
			server.IsDeleted = true;
			db.Entry(server).State = EntityState.Modified;
			return await db.SaveChangesAsync();
		}

		/// <summary>
		/// Изменить информацию о сервере
		/// </summary>
		/// <param name="server"></param>
		/// <returns></returns>
		public async Task<int?> UpdateServerAsynс(Server server)
		{
			db.Servers.Update(server);
			return await db.SaveChangesAsync();
		}

		public Task<Node> GetSingleNodeAsync(Expression<Func<Node, bool>> predicate)
		{
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));
			return db.Nodes.Include(x => x.Versions).AsNoTracking().FirstOrDefaultAsync(predicate);
		}

		public Task<List<Node>> GetNodesAsync(Expression<Func<Node, bool>> predicate)
		{
			if (predicate == null)
				return db.Nodes.Include(x => x.Versions).AsNoTracking().ToListAsync();

			return db.Nodes.Where(predicate).Include(x => x.Versions).AsNoTracking().ToListAsync();
		}

		#endregion

		#region Version

		public Task<Version> GetActualNodeVersionAsync(int nodeId)
		{
			return db.Versions.AsNoTracking().SingleOrDefaultAsync(x => nodeId.Equals(x.NodeId.Value) && x.IsLastRecord.Value);
		}

		public async Task<int> AddNodeVersionAsync(Version version)
		{
			if (version == null)
				throw new ArgumentNullException(nameof(version));

			db.Versions.Add(version);
			await db.SaveChangesAsync();
			return version.Idr;
		}

		#endregion

		#region Task

		/// <summary>
		/// Получить информацию по всем задачам
		/// </summary>
		/// <param name="offset">Смещение</param>
		/// <param name="limit">Количество на страницу</param>
		/// <param name="filter">Фильтр</param>
		/// <returns></returns>
		public async Task<List<PoolTaskResponse>> GetTasksAsync(int offset, int limit, string filter)
		{
			var list = new List<PoolTaskResponse>();
			using var connection = new SqlConnection(db.Database.GetDbConnection().ConnectionString);
			var command = connection.CreateCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "dbo.api_get_tasks";
			command.Parameters.AddWithValue("@__p_1", offset);
			command.Parameters.AddWithValue("@__p_2", limit);
			await connection.OpenAsync();
			var reader = await command.ExecuteReaderAsync();
			while (await reader.ReadAsync())
				list.Add(MapToPoolTaskResponse(reader));
			return list;
		}

		//todo: refactor via common mapping method
		private PoolTaskResponse MapToPoolTaskResponse(IDataReader reader)
		{
			var fc = reader.FieldCount;

			var row = new PoolTaskResponse();
			row.TaskId = reader.GetInt32(0).ToString();
			row.DevicePhoneNumber = reader.GetString(1);
			row.TaskType = reader.GetString(2);
			row.UserName = reader.IsDBNull(3) ? null: reader.GetString(3);
			row.UserLogin = reader.IsDBNull(4) ? null: reader.GetString(4);
			row.TaskDescription = reader.IsDBNull(5) ? null: reader.GetString(5);
			row.TaskDateRun = reader.IsDBNull(6) ? null : reader.GetDateTime(6).ToString("yyyy-MM-dd HH:mm:ss.fff");
			row.ServerTestId = reader.IsDBNull(7) ? null : reader.GetInt32(7).ToString();
			row.ServerFQDN = reader.IsDBNull(8) ? null : reader.GetString(8);
			return row;
		}


		//todo: refactor via common mapping method
		private PoolTaskResponse MapToFilterePoolTaskResponse(IDataReader reader)
		{
			var row = new PoolTaskResponse();
			row.TaskId = reader.GetInt32(0).ToString();
			row.TaskDescription = reader.IsDBNull(1) ? null : reader.GetString(1);
			row.DevicePhoneNumber = reader.GetString(2);
			row.UserLogin = reader.IsDBNull(3) ? null : reader.GetString(3);
			row.UserName = reader.IsDBNull(4) ? null : reader.GetString(4);
			row.TaskType = reader.GetString(5);
			row.ServerTestId = reader.IsDBNull(6) ? null : reader.GetInt32(6).ToString();
			row.ServerFQDN = reader.IsDBNull(7) ? null : reader.GetString(7);
			row.TaskDateRun = reader.IsDBNull(8) ? null : reader.GetDateTime(8).ToString("yyyy-MM-dd HH:mm:ss.fff");
			return row;
		}

		

		/// <summary>
		/// Получить детальную информацию по указанной задаче
		/// </summary>
		/// <param name="taskNumber"></param>
		/// <param name="taskStatus"></param>
		/// <returns></returns>
		public Task<TaskDetailsRow> GetTaskDetailsAsync(int taskNumber, string taskStatus)
		{
			if (string.IsNullOrEmpty(taskStatus))
				throw new ArgumentNullException(nameof(taskStatus));

			var query = GetTaskDetailsQuery(taskNumber, taskStatus);
			return query.FirstOrDefaultAsync();
		}


		/// <summary>
		/// Получить информацию по количеству задач
		/// </summary>
		/// <returns>Количество задач</returns>
		public async Task<int> GetTasksCountAsync(FilterCountRequest request)
		{
			using var connection = new SqlConnection(db.Database.GetDbConnection().ConnectionString);
			var command = connection.CreateCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "dbo.api_get_tasks_totalcount";
			command.Parameters.AddWithValue("@__filter_0", request.Search ?? string.Empty);
			command.Parameters.AddWithValue("@__filter_1", request.TableColumn ?? string.Empty);
			await connection.OpenAsync();
			var result  = (int)await command.ExecuteScalarAsync();
			return result;
		}

		/// <summary>
		/// Получить данные по задаче
		/// </summary>
		/// <param name="id">Фильтр</param>
		/// <returns></returns>
		public async Task<ServerTask> GetTaskAsync(int id)
		{
			var serverTask = await db.Tasks
				.Include(task => task.Device)
				.Include(task => task.User)
				.Include(task => task.TaskList)
				.SingleOrDefaultAsync(p => p.Idr == id);

			return serverTask;
		}

		/// <summary>
		/// Добавить задачу
		/// </summary>
		/// <param name="task"></param>
		/// <returns></returns>
		public async Task<int?> AddTaskAsync(ServerTask task)
		{
			await db.Tasks.AddAsync(task);
			await db.SaveChangesAsync();
			return task.Idr;
		}


		/// <summary>
		/// Добавить новую задачу в новый пул задач
		/// </summary>
		/// <param name="task"></param>
		/// <param name="pool"></param>
		/// <returns></returns>
		public async Task AddTaskInTaskPoolAsync(ServerTask task, TasksPool pool)
		{
			pool.Task = task;
			await db.TasksPools.AddAsync(pool);

			await db.SaveChangesAsync();
		}

		/// <inheritdoc/>
		public async Task<bool> AnyTaskFromTaskPoolByDeviceIdAndTaskListId(int deviceId, int taskListId)
		{
			return await db.TasksPools.AnyAsync(pool => pool.Task.TaskListId == taskListId && pool.Task.DeviceId == deviceId);
		}

		/// <inheritdoc/>
		public async Task<IEnumerable<ServerTask>> GetTasksFromTaskPoolByDeviceIdAndTaskListId(int deviceId, int taskListId)
		{
			return await db.TasksPools.Where(pool => pool.Task.TaskListId == taskListId && pool.Task.DeviceId == deviceId)
									  .Include(pool => pool.Task)
									  .ThenInclude(task => task.User)
									  .Include(pool => pool.Task)
										  .ThenInclude(task => task.Device)
										  .ThenInclude(device => device.Lines)
									  .Select(pool => pool.Task)
									  .ToListAsync();
		}

		/// <summary>
		/// Получить информацию по всем историям заданий
		/// </summary>
		/// <param name="offset">Смещение</param>
		/// <param name="limit">Количество на страницу</param>
		/// <param name="filter">Фильтр</param>
		/// <returns></returns>
		public async Task<List<CompletedTaskRow>> GetCompletedTasksAsync(int offset, int limit)
		{
			var list = new List<CompletedTaskRow>();

			using var connection = new SqlConnection(db.Database.GetDbConnection().ConnectionString);
			var command = connection.CreateCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "dbo.api_get_tasks_completed";
			command.Parameters.AddWithValue("@__p_1", offset);
			command.Parameters.AddWithValue("@__p_2", limit);
			await connection.OpenAsync();
			var reader = await command.ExecuteReaderAsync();

			while (await reader.ReadAsync())
				list.Add(MapToCompletedTaskRow(reader));
			return list;
		}

		public Task<int> GetCompletedTasksCountAsync()
		{
			return db.TasksHistories.AsNoTracking().CountAsync();
		}

		public async Task<List<CompletedTaskRow>> GetCompletedTasksByDateRangeAsync(DateRangeRequest request)
		{
			ValidateDateRangeRequest(request);
			var completedTasks = new List<CompletedTaskRow>();

			using var connection = new SqlConnection(db.Database.GetDbConnection().ConnectionString);

			var command = connection.CreateCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "dbo.api_get_tasks_completed_selectiondate";
			command.Parameters.AddWithValue("@__filter_0", request.DateFrom);
			command.Parameters.AddWithValue("@__filter_1", request.DateTo);
			command.Parameters.AddWithValue("@__filter_2", request.TableColumn);
			command.Parameters.AddWithValue("@__p_1", request.Offset);
			command.Parameters.AddWithValue("@__p_2", request.Limit);
			command.Parameters.AddWithValue("@__sort_direction_p_3", request.OrderDesc ?? true ? "desc" : "asc");
			await connection.OpenAsync();
			using var reader = await command.ExecuteReaderAsync();
			while (await reader.ReadAsync())
				completedTasks.Add(MapToCompletedTaskRow(reader));

			return completedTasks;
		}

		public async Task<int> GetTotalCountByDateRangeAsync(DateRangeCountRequest request)
		{
			ValidateDateRangeCountRequest(request);

			using var connection = new SqlConnection(db.Database.GetDbConnection().ConnectionString);
			var command = connection.CreateCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "dbo.api_get_tasks_completed_totalcountdate";
			command.Parameters.AddWithValue("@__filter_0", request.DateFrom);
			command.Parameters.AddWithValue("@__filter_1", request.DateTo);
			command.Parameters.AddWithValue("@__filter_2", request.TableColumn);
			await connection.OpenAsync();
			return (int)await command.ExecuteScalarAsync();
		}

		private void ValidateDateRangeRequest(DateRangeRequest request)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			if (request.Offset == null || request.Limit == null)
				throw new ArgumentException($"{nameof(DateRangeRequest.Offset)} or {nameof(DateRangeRequest.Limit)} is not defined");

			if (request.DateFrom == null || request.DateTo == null)
				throw new ArgumentException($"{nameof(DateRangeRequest.DateFrom)} or {nameof(DateRangeRequest.DateTo)} is not defined");

			var correctArray = new[]
			{
				nameof(CompletedTaskRow.TaskDateCreate),
				nameof(CompletedTaskRow.TaskDateEnd),
				nameof(CompletedTaskRow.TaskDateRun),
			};

			if (string.IsNullOrEmpty(request.TableColumn) || !correctArray.Contains(request.TableColumn))
				throw new ArgumentException(nameof(DateRangeRequest.TableColumn));
		}

		private void ValidateDateRangeCountRequest(DateRangeCountRequest request)
		{
			if (request == null) throw new ArgumentNullException(nameof(request));

			var correctArray = new[]
			{
				nameof(CompletedTaskRow.TaskDateCreate),
				nameof(CompletedTaskRow.TaskDateEnd),
				nameof(CompletedTaskRow.TaskDateRun),
			};

			if (string.IsNullOrEmpty(request.TableColumn) || !correctArray.Contains(request.TableColumn))
				throw new ArgumentException(nameof(DateRangeRequest.TableColumn));
		}


		public async Task<List<CompletedTaskRow>> GetCompletedTasksFromProcedureAsync(FilterRequest request)
		{
			var list = new List<CompletedTaskRow>();
			using var connection = new SqlConnection(db.Database.GetDbConnection().ConnectionString);
			using var command = connection.CreateCommand();
			command.CommandText = "dbo.api_get_tasks_completed_selection";
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.AddWithValue("@__filter_0", request.Search ?? string.Empty);
			command.Parameters.AddWithValue("@__filter_1", request.TableColumn ?? string.Empty);
			command.Parameters.AddWithValue("@__p_1", request.Offset);
			command.Parameters.AddWithValue("@__p_2", request.Limit);
			command.Parameters.AddWithValue("@__sort_direction_p_3", request.OrderDesc ?? true ? "desc" : "asc");
			await connection.OpenAsync();
			using var reader = await command.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				list.Add(MapToCompletedTaskRowFromSelection(reader));
			}

			return list;
		}

		public async Task<int> GetCompletedTasksCountFromProcedureAsync(FilterCountRequest request)
		{
			using var connection = new SqlConnection(db.Database.GetDbConnection().ConnectionString);
			using var command = connection.CreateCommand();
			command.CommandText = "dbo.api_get_tasks_completed_totalcount";
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.AddWithValue("@__filter_0", request.Search ?? string.Empty).DbType = DbType.String;
			command.Parameters.AddWithValue("@__filter_1", request.TableColumn ?? string.Empty).DbType = DbType.String;
			await connection.OpenAsync();
			var count = (int)await command.ExecuteScalarAsync();
			return count;
		}

		private CompletedTaskRow MapToCompletedTaskRowFromSelection(IDataReader reader)
		{
			var row = new CompletedTaskRow();

			row.TaskCompletedId = reader.GetInt32(0);
			row.TaskId = reader.GetInt32(1);
			row.DevicePhoneNumber = reader.IsDBNull(2) ? null : reader.GetString(2);
			row.TaskType = reader.IsDBNull(3) ? null : reader.GetString(3);
			row.UserName = reader.IsDBNull(4) ? null : reader.GetString(4);
			row.UserLogin = reader.IsDBNull(5) ? null : reader.GetString(5);
			row.TaskDescription = reader.IsDBNull(6) ? null : reader.GetString(6);
			row.TaskDateCreate = reader.IsDBNull(7) ? null : (DateTime?)reader.GetDateTime(7);
			row.TaskDateRun = reader.IsDBNull(8) ? null : (DateTime?)reader.GetDateTime(8);
			row.TaskDateEnd = reader.IsDBNull(9) ? null : (DateTime?)reader.GetDateTime( 9);
			row.TaskStatus = reader.IsDBNull(10) ? null : reader.GetString(10);
			row.TaskStatusDesc = reader.IsDBNull(11) ? null : reader.GetString(11);
			row.TaskCancelId = reader.IsDBNull(12) ? null : (int?)reader.GetInt32(12);
			row.ServerTestId = reader.IsDBNull(13) ? null : (int?)reader.GetInt32(13);
			row.ServerFQDN = reader.IsDBNull(14) ? null : reader.GetString(14);
			return row;
		}

		private CompletedTaskRow MapToCompletedTaskRow(IDataReader reader)
		{
			var row = new CompletedTaskRow();
			row.TaskCompletedId = reader.GetInt32(0);
			row.TaskId = reader.GetInt32(1);
			row.DevicePhoneNumber = reader.IsDBNull(2) ? null : reader.GetString(2);
			row.TaskType = reader.GetString(3);
			row.UserName = reader.IsDBNull(4) ? null : reader.GetString(4);
			row.UserLogin = reader.IsDBNull(5) ? null : reader.GetString(5);
			row.TaskDescription = reader.IsDBNull(6) ? null : reader.GetString(6);
			row.TaskDateCreate = reader.GetDateTime(7);
			row.TaskDateRun = reader.IsDBNull(8) ? null : (DateTime?)reader.GetDateTime(8);
			row.TaskDateEnd = reader.IsDBNull(9) ? null : (DateTime?)reader.GetDateTime(9);
			row.TaskStatus = reader.IsDBNull(10) ? null : reader.GetString(10);
			row.TaskStatusDesc = reader.IsDBNull(11) ? null : reader.GetString(11);
			row.TaskCancelId = reader.IsDBNull(12) ? null : (int?)reader.GetInt32(12);
			row.ServerTestId = reader.IsDBNull(13) ? null : (int?)reader.GetInt32(13);
			row.ServerFQDN = reader.IsDBNull(14) ? null : reader.GetString(14);
			return row;
		}

		#endregion

		#region TasksList

		/// <summary>
		/// Получить данные списка заданий
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<TasksList> GetTasksListAsync(int id)
		{
			var tasksList = await db.TasksLists.SingleOrDefaultAsync(p => p.Idr == id);

			return tasksList ?? null;
		}

		public Task<List<TasksList>> GetAllTasksListsAsync()
		{
			return db.TasksLists.ToListAsync();
		}


		/// <summary>
		/// Добавить пул задач
		/// </summary>
		/// <param name="taskPool"></param>
		/// <returns></returns>
		public async Task<int> AddTasksPoolAsync(TasksPool taskPool)
		{
			await db.TasksPools.AddAsync(taskPool);
			await db.SaveChangesAsync();

			return taskPool.Idr;
		}
		#endregion

		#region TasksPool

		/// <summary>
		/// Получить данные пула заданий
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Task<TasksPool> GetTasksPoolByTaskIdAsync(int id)
		{
			return db.TasksPools.SingleOrDefaultAsync(p => p.TaskId == id);
		}

		#endregion

		#region User

		/// <summary>
		/// Получить информацию по пользователям
		/// </summary>
		/// <param name="offset">Смещение</param>
		/// <param name="limit">Количество на страницу</param>
		/// <returns></returns>
		public async Task<List<User>> GetUsersAsync(int offset, int limit)
		{
			return await db.Users
				.Include(r => r.UserToRoles)
				.ThenInclude(r => r.Role)
				.Skip(offset * limit).Take(limit).ToListAsync();
		}

		public async Task<List<UserRow>> GetUsersFromProcedureAsync(FilterRequest request)
		{
			if (request == null) throw new ArgumentNullException(nameof(request));


			using var connection = new SqlConnection(db.Database.GetDbConnection().ConnectionString);
			using var command = connection.CreateCommand();
			command.CommandText = "dbo.api_get_security_users_selection";
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.AddWithValue("@__filter_0", request.Search ?? string.Empty);
			command.Parameters.AddWithValue("@__filter_1", request.TableColumn ?? string.Empty);
			command.Parameters.AddWithValue("@__p_1", request.Offset ?? 0);
			command.Parameters.AddWithValue("@__p_2", request.Limit ?? 10);
			command.Parameters.AddWithValue("@__sort_direction_p_3", request.OrderDesc ?? true ? "desc": "asc");

			await connection.OpenAsync();

			var list = new List<UserRow>();

			using var reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);

			while(await reader.ReadAsync())
			{
				list.Add(MapToUserRowForCollection(reader));
			}

			return list;
		}


		public async Task<int> GetUsersCountFromProcedureAsync(FilterCountRequest request)
		{
			if (request == null) throw new ArgumentNullException(nameof(request));

			using var connection = new SqlConnection(db.Database.GetDbConnection().ConnectionString);
			using var command = connection.CreateCommand();
			command.CommandText = "dbo.api_get_security_users_totalcount";
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.AddWithValue("@__filter_0", request.Search ?? string.Empty);
			command.Parameters.AddWithValue("@__filter_1", request.TableColumn ?? string.Empty);

			await connection.OpenAsync();
			return (int)await command.ExecuteScalarAsync();
		}

		/// <summary>
		/// Получить данные пользоателя
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Task<User> GetUserAsync(int id)
		{
			return db.Users.Include(x => x.UserToRoles).AsNoTracking().SingleOrDefaultAsync(p => p.Idr == id);
		}

		public async Task<UserRow> GetUserAsyncFromProcedureAsync(int userId)
		{
			UserRow user = null;

			using var connection = new SqlConnection(db.Database.GetDbConnection().ConnectionString);
			using var command = connection.CreateCommand();
			command.CommandText = "dbo.api_get_security_user_detail";
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.AddWithValue("@__p_1", userId);

			await connection.OpenAsync();
			using var reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow);

			if (await reader.ReadAsync())
			{
				user = MapToUserRow(reader);
			}

			return user;
		}

		private UserRow MapToUserRow(IDataReader reader)
		{
			var user = new UserRow();
			user.UserId = reader.GetInt32(0);
			user.UserLogin = reader.GetString(1);
			user.UserDisplayName = reader.IsDBNull(2) ? null : reader.GetString(2);
			user.UserEmail = reader.IsDBNull(3) ? null : reader.GetString(3);
			user.UserSid = reader.IsDBNull(4) ? null : reader.GetString(4);
			user.UserProvider = reader.IsDBNull(5) ? null : reader.GetString(5);
			user.UserPosition = reader.IsDBNull(6) ? null : reader.GetString(6);
			user.UserIsDeleted = reader.IsDBNull(7) ? null : (bool?) reader.GetBoolean(7);
			user.UserPassword = reader.GetString(8);
			user.RoleId = reader.GetString(9);
			return user;
		}

		private UserRow MapToUserRowForCollection(IDataReader reader)
		{
			var user = new UserRow();
			user.UserId = reader.GetInt32(0);
			user.UserLogin = reader.GetString(1);
			user.UserDisplayName = reader.IsDBNull(2) ? null : reader.GetString(2);
			user.UserEmail = reader.IsDBNull(3) ? null : reader.GetString(3);
			user.UserSid = reader.IsDBNull(4) ? null : reader.GetString(4);
			user.UserProvider = reader.IsDBNull(5) ? null : reader.GetString(5);
			user.UserPosition = reader.IsDBNull(6) ? null : reader.GetString(6);
			user.UserIsDeleted = reader.IsDBNull(7) ? null : (bool?)reader.GetBoolean(7);
			user.RoleId = reader.GetString(8);
			return user;
		}


		/// <summary>
		/// Получить данные пользоателя
		/// </summary>
		/// <param name="login"></param>
		/// <returns></returns>
		public Task<User> GetUserAsync(string login)
		{
			return db.Users.AsNoTracking().FirstOrDefaultAsync(p => p.Login == login);
		}

		/// <summary>
		/// Добавить пользователя
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public async Task<int?> AddUserAsync(User user)
		{
			await db.Users.AddAsync(user);
			await db.SaveChangesAsync();

			return user.Idr;
		}

		/// <summary>
		/// Изменить информацию о пользователе
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public async Task<int?> UpdateUserAsynс(User user)
		{
			var entry = db.ChangeTracker.Entries<User>().FirstOrDefault(x => x.Entity.Idr == user.Idr);
			if (entry != null)
				entry.State = EntityState.Detached;

			db.Users.Update(user);
			return await db.SaveChangesAsync();
		}

		#endregion

		#region UserToRoles

		/// <summary>
		/// Получить данные о роли пользоателя
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<UserToRole> GetUserRoleAsync(int id)
		{
			var userToRole = await db.UsersToRoles.SingleOrDefaultAsync(p => p.UserId == id);

			return userToRole ?? null;
		}

		/// <summary>
		/// Добавляет пользоавтелю роль
		/// </summary>
		/// <param name="userRole"></param>
		/// <returns></returns>
		public async Task<int?> AddUserRoleAsync(UserToRole userRole)
		{
			await db.UsersToRoles.AddAsync(userRole);

			return await db.SaveChangesAsync();
		}

		/// <summary>
		/// Изменить информацию о роли пользователя
		/// </summary>
		/// <param name="userRole"></param>
		/// <param name="newRoleId"></param>
		/// <returns></returns>
		public async Task<int?> UpdateUserRoleAsynс(UserToRole userRole, int newRoleId)
		{
			var newRecord = new UserToRole
			{
				UserId = userRole.UserId,
				RoleId = newRoleId
			};
			db.UsersToRoles.Remove(userRole);
			await db.UsersToRoles.AddAsync(newRecord);

			return await db.SaveChangesAsync();
		}

		#endregion

		#region Vendor

		/// <summary>
		/// Получить данные о вендоре
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<Vendor> GetVendorAsync(int id)
		{
			var vendor = await db.Vendors.SingleOrDefaultAsync(p => p.Idr == id);

			return vendor ?? null;
		}

		#endregion

		#region VendorModel

		/// <summary>
		/// Получить данные о моделях вендора
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<List<VendorModel>> GetVendorModelsAsync(int id)
		{
			return await db.VendorModels.Where(item => item.VendorId == id).ToListAsync();
		}

		#endregion

		#region ProductInfo

		/// <summary>
		/// Получить данные о продукте
		/// </summary>
		/// <param name="productName"></param>
		/// <returns></returns>
		public async Task<ProductInfoRow> GetProductInfo(string productName)
		{
			if (string.IsNullOrEmpty(productName))
				throw new ArgumentException(nameof(productName));

			var product = await db.ProductInfos.FirstOrDefaultAsync(x => x.Product == productName);

			return product == null
				? null
				: new ProductInfoRow
				{
					ProductContact = product.Contacts,
					ProductAddInfo = product.AddInfo,
					ProductDNumber = product.DecimalNumber,
					ProductName = product.Name,
					ProductVersion = product.Version
				};
		}

		#endregion
		#region Line
		/// <summary>
		/// Проверяет существует ли телефоная линия по указанному номеру телефона
		/// </summary>
		/// <param name="phoneNumber">Номер телефона</param>
		/// <returns></returns>
		public async Task<bool> AnyLineByPhoneNumberAsync(string phoneNumber)
		{
			return await db.Lines.AnyAsync(line => line.PhoneNumber == phoneNumber);
		}

		/// <summary>
		/// Получить телефоную линию по номеру телефона
		/// </summary>
		/// <param name="phoneNumber">Номер телефона</param>
		/// <returns></returns>
		public async Task<Line> GetLineByPhoneNumberAsync(string phoneNumber)
		{
			return await db.Lines.FirstOrDefaultAsync(line => line.PhoneNumber == phoneNumber);
		}
		#endregion

		#region Impelementation details

		private IQueryable<TaskDetailsRow> GetTaskDetailsQuery(int taskNumber, string status)
		{
			IQueryable<TaskDetailsRow> current = null;
			IQueryable<TaskDetailsRow> done = null;

			current =
				from tp in db.Set<TasksPool>()
				join t in db.Set<ServerTask>() on tp.TaskId equals t.Idr
				join tl in db.Set<TasksList>() on t.TaskListId equals tl.Idr
				join u in db.Set<User>() on t.UsersId equals u.Idr into usersJoin
				from uj in usersJoin.DefaultIfEmpty()
				join ts in db.Set<Server>() on t.TestServerId equals ts.Idr into tsJoin
				from tsj in tsJoin.DefaultIfEmpty()
				join l in db.Set<Line>() on t.IdLine equals l.Idr into linesJoin
				from lj in linesJoin.DefaultIfEmpty()
				where tp.TaskId.Equals(taskNumber)
				select new TaskDetailsRow
				{
					TaskId = t.Idr,
					DevicePhoneNumber = lj.PhoneNumber,
					TaskType = tl.Name,
					UserLogin = uj.Login,
					UserName = uj.DisplayName,
					TaskDescription = t.Description,
					TaskDateRun = tp.DateRun,
					ServerId = t.ServerId,
					ServerTestId = t.TestServerId,
					ServerTestName = tsj.FQDN,
					DeviceJson = t.JsonNew
				};


			done =
				from th in db.Set<TasksHistory>()
				join t in db.Set<ServerTask>() on th.TaskId equals t.Idr
				join tl in db.Set<TasksList>() on t.TaskListId equals tl.Idr
				join u in db.Set<User>() on t.UsersId equals u.Idr into usersJoin
				from uj in usersJoin.DefaultIfEmpty()
				join ts in db.Set<Server>() on t.TestServerId equals ts.Idr into tsJoin
				from tsj in tsJoin.DefaultIfEmpty()
				join l in db.Set<Line>() on t.IdLine equals l.Idr into linesJoin
				from lj in linesJoin.DefaultIfEmpty()
				where th.TaskId.Equals(taskNumber)
				select new TaskDetailsRow
				{
					TaskId = t.Idr,
					DevicePhoneNumber = lj.PhoneNumber,
					TaskType = tl.Name,
					UserLogin = uj.Login,
					UserName = uj.DisplayName,
					TaskDescription = t.Description,
					TaskDateRun = th.DateRun,
					ServerId = t.ServerId,
					ServerTestId = t.TestServerId,
					ServerTestName = tsj.FQDN,
					ProductionServerName = tsj.FQDN,
					ServerTestBench = t.TestServerId,
					DeviceJson = t.JsonNew
				};

			var resultQuery = status switch
			{
				Constants.TaskStatus.Current => current,
				Constants.TaskStatus.Done => done,
				_ => throw new ArgumentException(nameof(status))
			};

			resultQuery =
				from q in resultQuery
				group q by q.TaskId
				into g
				select new TaskDetailsRow
				{
					TaskId = g.Key,
					DevicePhoneNumber = g.Max(d => d.DevicePhoneNumber),
					ServerId = g.Max(d => d.ServerId),
					ServerTestId = g.Max(d => d.ServerTestId),
					TaskDateRun = g.Max(d => d.TaskDateRun),
					DeviceJson = g.Max(d => d.DeviceJson),
					TaskDescription = g.Max(d => d.TaskDescription),
					TaskType = g.Max(d => d.TaskType),
					UserLogin = g.Max(d => d.UserLogin),
					UserName = g.Max(d => d.UserName),
					ServerTestName = g.Max(d => d.ServerTestName),
				};

			return resultQuery;
		}

		public async Task<List<PoolTaskResponse>> GetFilteredTasksWithProcedureAsync(FilterRequest request)
		{
			if (request == null) 
				throw new ArgumentNullException(nameof(request));

			var list = new List<PoolTaskResponse>();

			using var connection = new SqlConnection(db.Database.GetDbConnection().ConnectionString);
			var command = connection.CreateCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "dbo.api_get_tasks_selection";
			command.Parameters.AddWithValue("@__p_1", request.Offset);
			command.Parameters.AddWithValue("@__p_2", request.Limit);
			command.Parameters.AddWithValue("@__filter_0", request.Search ?? string.Empty);
			command.Parameters.AddWithValue("@__filter_1", request.TableColumn ?? string.Empty);
			command.Parameters.AddWithValue("@__sort_direction_p_3", request.OrderDesc ?? true ? "desc": "asc");
			await connection.OpenAsync();
			var reader = await command.ExecuteReaderAsync();
			while (await reader.ReadAsync())
				list.Add(MapToFilterePoolTaskResponse(reader));
			return list;
		}

		#endregion
	}
}