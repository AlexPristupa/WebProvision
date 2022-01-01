using System;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MentolProvisionModel;
using MentolProvisionInterface;
using MentolProvision.Auth;
using MentolProvision.Extensions;
using MentolProvision.Models;
using MentolProvision.Models.Request.User;
using MentolProvision.Models.Response;
using MentolProvision.Models.Response.Common;
using MentolProvision.Models.Response.Summaries;
using MentolProvision.Models.Response.User;
using MentolProvisionModel.CustomQueries;
using MentolProvisionModel.Infrastructure;
using MentolProvisionRepository;
using MentolProvisionRepository.Filter;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.WebEncoders.Testing;
using Serilog;

namespace MentolProvision.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : FilteredController<User, UsersController, UserResponse>
    {
        #region General
        private ILogger _logger;
        private IDataInterface _data;
        private readonly IStringLocalizer<UsersController> _localizer;
        private readonly ApplicationContext _context;
        private readonly IMemoryCache _memoryCache;
        private const string _cacheKeyFragment = nameof(User);

        internal override Dictionary<string, List<string>> _propNames { get; set; } =
            new Dictionary<string, List<string>>()
            {
                    { nameof(MentolProvisionModel.User.Login), new List<string>() {  nameof(MentolProvisionModel.User.Login) } },
                    { nameof(MentolProvisionModel.User.DisplayName), new List<string>() { nameof(MentolProvisionModel.User.DisplayName) } },
                    { nameof(MentolProvisionModel.User.Email), new List<string>() { nameof(MentolProvisionModel.User.Email) } },
                    { nameof(MentolProvisionModel.User.Sid), new List<string>() { nameof(MentolProvisionModel.User.Sid) } },
                    { nameof(MentolProvisionModel.User.Provider), new List<string>() { nameof(MentolProvisionModel.User.Provider) } }
            };

        public UsersController(ILogger logger, IDataInterface data, IStringLocalizer<UsersController> localizer, IMemoryCache memoryCache, ApplicationContext context)
            : base(logger, localizer, context)
        {
            _logger = logger;
            _data = data;
            _localizer = localizer;
            _context = context;
            _memoryCache = memoryCache;
        }
		#endregion

		#region End-points

		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
	        try
	        {
		        var user = await ((Repository)_data).GetUserAsyncFromProcedureAsync(userId);
		        if (user == null)
			        return NotFound();

		        var userResponse = await InitUserWithRoles(user);
		        var summary = new UserWithRoleSummaryResponse(userResponse);
		        return Ok(summary);
	        }
	        catch (Exception e)
	        {
		        _logger.Error(e, _localizer["ERROR_METHOD_GET"].Value + e.Message);
		        return StatusCode(StatusCodes.Status500InternalServerError);
	        }
		}


        public override async Task<IActionResult> GetFilteredAsync(FilterRequest request)
        {
	        if (request?.Offset == null || request.Limit == null)
		        return BadRequest(_localizer["ERROR_NEED_LIMIT_OFFSET"]);
	        
            var meta = new FiltrationMetadata()
	        {
		        Offset = request.Offset,
		        Limit = request.Limit,
		        OrderDesc = request.OrderDesc ?? true,
		        TableColumn = request.TableColumn,
		        Search = request.Search
	        };

	        try
            {
	            var users = await  ((Repository) _data).GetUsersFromProcedureAsync(request);

	            var list = new List<UserWithRolesResponse>();
	            foreach (var userRow in users)
	            {
		            list.Add(await InitUserWithRoles(userRow));
	            }
	            var usersSummary = new UsersWithRolesSummaryResponse(meta, list);
	            return Ok(usersSummary);
            }
            catch (Exception e)
            {
	            _logger.Error(e, _localizer["ERROR_METHOD_GET"].Value + e.Message);
	            return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        public async override Task<IActionResult> GetFilteredCountAsync(FilterCountRequest request)
        {
	        try
	        {
		        var count = await ((Repository) _data).GetUsersCountFromProcedureAsync(request);
		        return Ok(GetCountResponse(count, request));
            }
	        catch (Exception e)
	        {
		        _logger.Error(_localizer["ERROR_METHOD_GET"].Value + e.Message);
		        return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        private async Task<UserWithRolesResponse> InitUserWithRoles(UserRow user)
        {
	        List<RoleResponse> rolesResponse = new List<RoleResponse>();

            if (!string.IsNullOrEmpty(user.RoleId))
	        {
		        var roles = await _data.GetUserRolesAsync(user.UserId);
		        rolesResponse.AddRange(roles.Select(x => new RoleResponse
		        {
			        RoleId = x.Idr,
			        RoleName = x.Name,
			        RoleDescription = x.Description
		        }));
	        }

	        var userResponse = new UserWithRolesResponse()
	        {
		        UserId = user.UserId,
		        UserDisplayName = user.UserDisplayName,
		        UserEmail = user.UserEmail,
		        UserSid = user.UserSid,
		        UserProvider = user.UserProvider,
		        UserPosition = user.UserPosition,
		        UserIsDeleted = user.UserIsDeleted ?? false,
		        UserPassword = user.UserPassword
	        };

	        userResponse.Roles.AddRange(rolesResponse);
	        return userResponse;
        }

        /// <summary>
        /// Добавить пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddUserRequest userRequest)
        {
	        if (userRequest?.User == null)
		        return BadRequest("Request body has no User property specified");

	        if (!AddUserRequestActions.IsTaskAction(userRequest.Meta.Action))
		        return BadRequest(_localizer["ERROR_OPERATION_NAME_MUST_BE_ONE_OF_FOLLOWING", string.Join(", ", AddUserRequestActions.TaskActions)]);

	        var userData = userRequest.User;

			if (string.IsNullOrEmpty(userData.UserName) || string.IsNullOrEmpty(userData.UserPassword))
			{
				return BadRequest(_localizer["ERROR_NEED_LOGIN_PASSWORD"].Value);
			}

			var action = userRequest.Meta.Action; 
			var cacheKey = GetCacheKey(userData.UserName);

			try
			{
				User user;
				if (action.Is(AddUserRequestActions.Preview))
		        {
			        var existingUser = await _data.GetUserAsync(userData.UserName);

			        if (existingUser == null)
			        {
				        user = new User
				        {
					        Login = userData.UserName,
					        Password = CryptoManager.AesEncrypt(userData.UserPassword),
					        DisplayName = userData.UserDisplayName,
					        Email = userData.UserEmail,
					        Sid = userData.UserSid,
					        Provider = userData.UserProvider,
					        IsDeleted = false,
					        Position = userData.UserPosition
				        };

				        var pk = (int)await _data.AddUserAsync(user);
				        return CreatedAtAction(nameof(Post), new { id = pk });
					}

			        if (!existingUser.IsDeleted ?? false)
				        return StatusCode(StatusCodes.Status409Conflict, 
					        _localizer["ERROR_USER_WITH_LOGIN_ALREADY_EXISTS", userData.UserName].Value);


			        if (!_memoryCache.TryGetValue(cacheKey, out user))
			        {
				        existingUser.Password = CryptoManager.AesEncrypt(userData.UserPassword);
				        existingUser.IsDeleted = false;
				        _memoryCache.Set(cacheKey, existingUser, TimeSpan.FromMinutes(1));

				        var resp = new RecoverOrCancelUserCreationResponse
				        {
					        Meta = { Result = "success", Message =  "Пользователь с таким логином определен как удаленный, восстановить?"},
							User =
							{
								UserName = existingUser.Login,
								UserId = existingUser.Idr,
								UserPosition = existingUser.Position,
								UserIsDeleted = true,
								UserDisplayName = existingUser.DisplayName,
								UserSid = existingUser.Sid,
								UserProvider = existingUser.Provider,
								UserEmail = existingUser.Email
							}
				        };
				        return Accepted(resp);
			        }
		        }

				if (!_memoryCache.TryGetValue(cacheKey, out user))
					return BadRequest(_localizer["ERROR_ADD_USER_OPERATION_TIME_EXPIRED"].Value);

				if (action.Is(AddUserRequestActions.Recovery))
				{
					_context.Update(user);
					await _context.SaveChangesAsync();
					_memoryCache.Remove(cacheKey);
					return CreatedAtAction(nameof(Post), user);
				}

				//cancel action
				_memoryCache.Remove(cacheKey);
				return StatusCode(StatusCodes.Status410Gone);
			}
	        catch (Exception e)
	        {
		        _logger.Error(e, _localizer["ERROR_METHOD_POST"].Value + e.Message);
		        return StatusCode(StatusCodes.Status500InternalServerError);
	        }
        }

        /// <summary>
		/// Изменить информацию о пользователе
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateUserRequest request)
        {
            try
            {
	            if (request?.User == null)
		            return BadRequest("Request body has no User property specified");

	            var userData = request.User;
	            if (string.IsNullOrEmpty(userData.UserName) || string.IsNullOrEmpty(userData.UserPassword))
	            {
		            return BadRequest(_localizer["ERROR_NEED_LOGIN_PASSWORD"].Value);
	            }

	            if (userData.UserId == null)
		            return BadRequest(_localizer["ERROR_NEED_USER_ID"].Value);

	            var existedUser = await _data.GetUserAsync(userData.UserId.Value);
                if (existedUser is null)
                {
                    return NotFound(_localizer["ERROR_USER_NOT_EXIST", userData.UserId].Value);
                }

                var userByLogin = await _data.GetUserAsync(userData.UserName);
                if (userByLogin != null && userByLogin.Idr != userData.UserId.Value)
                {
                    return BadRequest(_localizer["ERROR_IS_NOT_UNIQUE_LOGIN"].Value);
                }

                existedUser.Login = userData.UserName;
                existedUser.Password = CryptoManager.AesEncrypt(userData.UserPassword);
                existedUser.DisplayName = userData.UserDisplayName;
                existedUser.Email = userData.UserEmail;
                existedUser.Sid = userData.UserSid;
                existedUser.Provider = userData.UserProvider;

                await _data.UpdateUserAsynс(existedUser);
                return Accepted(new UserRequest
                {
                    Id = existedUser.Idr,
                    Login = existedUser.Login,
                    Password = existedUser.Password,
                    DisplayName = existedUser.DisplayName,
                    Email = existedUser.Email,
                    Sid = existedUser.Sid,
                    Provider = existedUser.Provider
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex, _localizer["ERROR_METHOD_PUT"].Value + ex.Message);
                return StatusCode(500);
            }
        }

        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int? userId)
        {
	        try
	        {
		        if (userId == null)
			        return BadRequest(_localizer["ERROR_NEED_USER_ID"].Value);

		        var user = await _data.GetUserAsync(userId.Value);

		        if (user == null)
			        return NotFound($"User with Idr={userId} was not found");

		        var roles = _context.UsersToRoles.Where(x => x.UserId == userId).ToArray();

		        if (roles.Any())
		        {
                    _context.RemoveRange(roles);
                    await _context.SaveChangesAsync();
		        }

		        user.IsDeleted = true;
		        await _data.UpdateUserAsynс(user);

                return Accepted();
	        }
	        catch (Exception e)
	        {
		        _logger.Error(e, _localizer["ERROR_METHOD_DELETE"].Value + e.Message);
		        return StatusCode(StatusCodes.Status500InternalServerError);
	        }
        }

        /// <summary>
        ///  Добавление роли для указанного пользователя
        /// </summary>
        /// <param name="userRequest">Данные запроса</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("role")]
        public async Task<IActionResult> AddRole(AddRoleToUserRequest userRequest)
        {
	        if (userRequest?.User.RoleId == null || userRequest.User.UserId == null)
		        return BadRequest(_localizer["ERROR_NEED_USERID_AND_ROLEID"].Value);

	        var roleId = userRequest.User.RoleId.Value;
	        var userId = userRequest.User.UserId.Value;

	        try
	        {
		        var user = await _data.GetUserAsync(userId);
		        if (user == null)
			        return NotFound($"User with Idr {userId} was not found");

		        var role = await _context.Roles.FirstOrDefaultAsync(x => x.Idr == roleId);
		        if (role == null)
			        return NotFound($"Role with Idr {roleId} was not found");

		        if (user.UserToRoles.FirstOrDefault(ur => ur.RoleId == roleId) == null)
		        {
			        _context.Add(new UserToRole
                    {
	                    RoleId = roleId,
	                    UserId = userId
                    });
			        await _context.SaveChangesAsync();
                }
		        return Accepted();
            }
	        catch (Exception e)
	        {
		        _logger.Error(e, $"Ошибка добавления роли: {e.Message}");
		        return StatusCode(StatusCodes.Status500InternalServerError);
	        }
        }

        [ProducesResponseType(typeof(User), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("role")]
        public async Task<IActionResult> DeleteRole(DeleteUserRoleRequest request)
        {
	        try
	        {
		        if (request.RoleId == null || request.UserId == null)
			        return BadRequest(_localizer["ERROR_NEED_USERID_AND_ROLEID"].Value);

		        var roleId = request.RoleId.Value;
		        var userId = request.UserId.Value;

		        var user = await _data.GetUserAsync(userId);
		        if (user == null)
			        return NotFound($"User with Idr {userId} was not found");

		        var role = await _context.Roles.FirstOrDefaultAsync(x => x.Idr == roleId);
		        if (role == null)
			        return NotFound($"Role with Idr {roleId} was not found");

		        var roleToDelete = user.UserToRoles.FirstOrDefault(x => x.RoleId == roleId);

		        if (roleToDelete != null)
		        {
			        _context.Set<UserToRole>().Remove(roleToDelete);
			        await _context.SaveChangesAsync();
		        }
		        return Accepted();
	        }
	        catch (Exception e)
	        {
		        _logger.Error(e, $"Ошибка при удалении роли: {e.Message}");
		        return StatusCode(StatusCodes.Status500InternalServerError);
	        }
        }


        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRoles()
        {
	        try
	        {
		        var roles = await _data.GetAllRolesAsync();
		        var roleResponses = roles.Select(x => x.ToRoleResponse()).ToArray();
		        return Ok(new {Roles = roleResponses});
	        }
	        catch (Exception e)
	        {
		        _logger.Error(e, $"Ошибка при получении ролей: {e.Message}");
		        return StatusCode(StatusCodes.Status500InternalServerError);
	        }
        }

        #endregion

        #region Private filtering methods
        public override Expression<Func<User, UserResponse>> GetSelectExpression()
        {
            Expression<Func<User, UserResponse>> expression = t => new UserResponse
            {
                Login = t.Login,
                DisplayName = t.DisplayName,
                Email = t.Email,
                Sid = t.Sid,
                Provider = t.Provider
            };

            return expression;
        }

        public override IOrderedQueryable<UserResponse> GetOrderExpression(string columnName, bool desc, IQueryable<UserResponse> _query) =>
            columnName switch
            {
                nameof(MentolProvisionModel.User.Login)       => !desc ? _query.OrderBy(r => r.Login) : _query.OrderByDescending(r => r.Login),
                nameof(MentolProvisionModel.User.DisplayName) => !desc ? _query.OrderBy(r => r.DisplayName) : _query.OrderByDescending(r => r.DisplayName),
                nameof(MentolProvisionModel.User.Email)       => !desc ? _query.OrderBy(r => r.Email) : _query.OrderByDescending(r => r.Email),
                nameof(MentolProvisionModel.User.Sid)         => !desc ? _query.OrderBy(r => r.Sid) : _query.OrderByDescending(r => r.Sid),
                nameof(MentolProvisionModel.User.Provider)    => !desc ? _query.OrderBy(r => r.Provider) : _query.OrderByDescending(r => r.Provider),
                _ => throw new ArgumentException(message: "invalid order column name", paramName: nameof(columnName)),
            };

        public override List<string> GeIncludes()
        {
            return new List<string>()
            {
                
            };
        }

        private static string GetCacheKey(string loginName) => $"{_cacheKeyFragment}_{loginName}";

        public static class AddUserRequestActions
        {
	        public const string Preview = "preview";
	        public const string Cancel = "cancel";
	        public const string Recovery = "recovery";

	        public static IReadOnlyCollection<string> TaskActions { get; } = new[] { Preview, Recovery, Cancel };

	        public static bool IsTaskAction(string action)
	        {
		        return TaskActions.Any(taskAction => taskAction.Is(action));
	        }
        }
		#endregion
	}
}
