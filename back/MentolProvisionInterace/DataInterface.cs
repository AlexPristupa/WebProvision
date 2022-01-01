using System;
using MentolProvisionModel;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MentolProvision.Models.Request;
using MentolProvisionModel.CustomQueries;
using MentolProvisionModel.Infrastructure;
using MentolProvisionModel.Infrastructure.FilterModels;
using Version = MentolProvisionModel.Version;

namespace MentolProvisionInterface
{
    public interface IDataInterface
    {
        #region ActionList
        /// <summary>
        /// Получить данные действий по идентификаторам
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<List<ActionList>> GetActionListAsync(IEnumerable<int> ids);
        #endregion

        #region Device
        /// <summary>
        /// Получить данные устройства
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Device> GetDeviceAsync(int id);

        /// <summary>
		/// Получить данные устройства с линиями звязи
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<Device> GetDeviceWithLinesAsync(int id);
        
        /// <summary>
        /// Получить информацию по всем устройствам
        /// </summary>
        /// <param name="offset">Смещение</param>
        /// <param name="limit">Количество на страницу</param>
        /// <returns></returns>
        Task<List<DeviceRow>> GetDevicesAsync(int offset, int limit);

        Task<int> GetDevicesFilteredCountAsync(string search, string tableColumn);

        Task<List<LineRow>> GetLinesByDeviceIdsAsync(int[] devicesIds);
        #endregion

        #region ObjectList
        /// <summary>
        /// Получить данные объектов по идентификаторам
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<List<ObjectList>> GetObjectListAsync(IEnumerable<int> ids);
        #endregion

        #region Page
        /// <summary>
        /// Получить страницу по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Page> GetPageByIdAsync(int id);

        /// <summary>
        /// Получить все страницы
        /// </summary>
        /// <returns></returns>
        Task<List<Page>> GetAllPageAsync();

        /// <summary>
        /// Получить страницы для указанного пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        Task<List<Page>> GetPagesByUserIdAsync(int userId);
        #endregion

        #region PageToRole
        /// <summary>
        /// Получить данные о страницах в разрезе роли
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<List<PageToRole>> GetPagesToRolesAsync(int roleId);
        #endregion

        #region Role
        /// <summary>
        /// Получить информацию по ролям
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">Название</param>
        /// <returns></returns>
        Task<List<Role>> GetRolesAsync(int? id, string name);

        /// <summary>
        /// Получить роли пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<Role>> GetUserRolesAsync(int userId);

        /// <summary>
        /// Получить информацию по всем ролям
        /// </summary>
        /// <returns>Список ролей</returns>
        Task<List<Role>> GetAllRolesAsync();

        /// <summary>
        /// Изменить информацию для роли
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<int?> UpdateRoleAsynс(Role role);
        #endregion

        #region RoleAction
        /// <summary>
        /// Заменяет доступы роли к объектам для указанной роли
        /// </summary>
        /// <param name="roleId">Идентификатор роли</param>
        /// <param name="roleActions">Доступы роли к объектам</param>
        /// <returns></returns>
        Task<int?> ReplaceRoleActionForRoleIdAsync(int roleId, IEnumerable<RoleAction> roleActions);
        #endregion

        #region PageToRole
        /// <summary>
        /// Заменяет доступы роли к страницам для указанной роли
        /// </summary>
        /// <param name="roleId">Идентификатор роли</param>
        /// <param name="pagesToRoles">Доступы роли к страницам</param>
        /// <returns></returns>
        Task<int?> ReplacePageToRolesForRoleIdAsync(int roleId, IEnumerable<PageToRole> pagesToRoles);
        #endregion

        #region Server
        /// <summary>
        /// Получить данные сервера
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Server> GetServerAsync(int id);

        /// <summary>
        /// Получить данные сервера
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<Server> GetServerAsync(string name);


        Task<Server> GetServerByIpAddress(string ipAddress);

        /// <summary>
        /// Получить информацию по всем серверам 
        /// </summary>
        /// <param name="offset">Смещение</param>
        /// <param name="limit">Количество на страницу</param>
        /// <returns></returns>
        Task<List<Server>> GetServersAsync(int offset, int limit);

        /// <summary>
        /// Добавить сервер
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        Task<int?> AddServerAsync(Server server);

        /// <summary>
        /// Удалить сервер
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        Task<int?> DeleteServerAsync(Server server);

        /// <summary>
        /// Изменить информацию о сервере
        /// </summary>
        /// <param name="server"></param>
        /// <param name="nodesToUpdate"></param>
        /// <returns></returns>
        Task<int?> UpdateServerAsynс(Server server);

        Task<Node> GetSingleNodeAsync(Expression<Func<Node, bool>> predicate);

        Task<List<Node>> GetNodesAsync(Expression<Func<Node, bool>> predicate);

        #endregion

        #region Version

        Task<Version> GetActualNodeVersionAsync(int nodeId);
        Task<int> AddNodeVersionAsync(Version version);

        #endregion

        #region Task
        /// <summary>
        /// Получить информацию по всем задачам
        /// </summary>
        /// <param name="offset">Смещение</param>
        /// <param name="limit">Количество на страницу</param>
        /// <param name="filter">Фильтр</param>
        /// <returns></returns>
        Task<List<PoolTaskResponse>> GetTasksAsync(int offset, int limit, string filter);

        Task<TaskDetailsRow> GetTaskDetailsAsync(int taskNumber, string taskStatus);

        /// <summary>
        /// Получить информацию о количестве задач
        /// </summary>
        /// <returns>Количество задач</returns>
        Task<int> GetTasksCountAsync(FilterCountRequest request);

        /// <summary>
        /// Получить данные по задаче
        /// </summary>
        /// <param name="id">Фильтр</param>
        /// <returns></returns>
        Task<ServerTask> GetTaskAsync(int id);

        /// <summary>
        /// Добавить задачу
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        Task<int?> AddTaskAsync(ServerTask task);

        /// <summary>
        /// Добавить новую задачу в новый пул задач
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pool"></param>
        /// <returns></returns>
        Task AddTaskInTaskPoolAsync(ServerTask task, TasksPool pool);

        /// <summary>
        /// Проверяет содержится ли задания для указанного номера и идентификатора списка заданий содержащийся в пуле задач
        /// </summary>
        /// <param name="phoneNumber">Номера телефона</param>
        /// <param name="taskListId">Идентификатор списка заданий</param>
        /// <returns></returns>
        Task<bool> AnyTaskFromTaskPoolByDeviceIdAndTaskListId(int deviceId, int taskListId);

        /// <summary>
        /// Получает задания для указанного номера и идентификатора списка заданий содержащийся в пуле задач
        /// </summary>
        /// <param name="phoneNumber">Номера телефона</param>
        /// <param name="taskListId">Идентификатор списка заданий</param>
        /// <returns></returns>
        Task<IEnumerable<ServerTask>> GetTasksFromTaskPoolByDeviceIdAndTaskListId(int deviceId, int taskListId);
        #endregion

        #region Completed tasks
        /// <summary>
        /// Получить информацию по всем историям заданий
        /// </summary>
        /// <param name="offset">Смещение</param>
        /// <param name="limit">Количество на страницу</param>
        /// <returns></returns>
        Task<List<CompletedTaskRow>> GetCompletedTasksAsync(int offset, int limit);

        /// <summary>
        /// Получить общее количество всех задач в истории
        /// </summary>
        /// <returns></returns>
        Task<int> GetCompletedTasksCountAsync();

        Task<List<CompletedTaskRow>> GetCompletedTasksByDateRangeAsync(DateRangeRequest request);

        Task<int> GetTotalCountByDateRangeAsync(DateRangeCountRequest request);

        #endregion

        #region TasksList
        /// <summary>
        /// Получить данные списка заданий
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TasksList> GetTasksListAsync(int id);

        Task<List<TasksList>> GetAllTasksListsAsync();
        #endregion

        #region TasksPool
        /// <summary>
        /// Получить данные пула заданий
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TasksPool> GetTasksPoolByTaskIdAsync(int id);

        /// <summary>
        /// Добавить пул задач
        /// </summary>
        /// <param name="taskPool"></param>
        /// <returns></returns>
        Task<int> AddTasksPoolAsync(TasksPool taskPool);
        #endregion

        #region User
        /// <summary>
        /// Получить информацию по пользователям
        /// </summary>
        /// <param name="offset">Смещение</param>
        /// <param name="limit">Количество на страницу</param>
        /// <returns></returns>
        Task<List<User>> GetUsersAsync(int offset, int limit);

        /// <summary>
        /// Получить данные пользоателя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User> GetUserAsync(int id);

        /// <summary>
        /// Получить данные пользоателя
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        Task<User> GetUserAsync(string login);

        /// <summary>
        /// Добавить пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<int?> AddUserAsync(User user);

        /// <summary>
        /// Изменить информацию о пользователе
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<int?> UpdateUserAsynс(User user);
        #endregion

        #region UserToRole
        /// <summary>
        /// Получить данные о роли пользоателя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserToRole> GetUserRoleAsync(int id);

        /// <summary>
        /// Добавляет пользоавтелю роль
        /// </summary>
        /// <param name="userRole"></param>
        /// <returns></returns>
        Task<int?> AddUserRoleAsync(UserToRole userRole);

        /// <summary>
        /// Изменить информацию о роли пользователя
        /// </summary>
        /// <param name="userRole"></param>
        /// <param name="newRoleId"></param>
        /// <returns></returns>
        Task<int?> UpdateUserRoleAsynс(UserToRole userRole, int newRoleId);
        #endregion

        #region Vendor
        /// <summary>
        /// Получить данные о вендоре
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Vendor> GetVendorAsync(int id);
        #endregion

        #region VendorModel
        /// <summary>
        /// Получить данные о моделях вендора 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<VendorModel>> GetVendorModelsAsync(int id);
        #endregion

        #region ProductInfo
        Task<ProductInfoRow> GetProductInfo(string productName);
        #endregion

        #region Line
        /// <summary>
        /// Проверяет существует ли телефоная линия по указанному номеру телефона
        /// </summary>
        /// <param name="phoneNumber">Номер телефона</param>
        /// <returns></returns>
        Task<bool> AnyLineByPhoneNumberAsync(string phoneNumber);

        /// <summary>
        /// Получить телефоную линию по номеру телефона
        /// </summary>
        /// <param name="phoneNumber">Номер телефона</param>
        /// <returns></returns>
        Task<Line> GetLineByPhoneNumberAsync(string phoneNumber);
        #endregion
    }
}
