using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using MentolProvisionModel;
using MentolProvisionInterface;
using MentolProvision.Models.Request;
using MentolProvision.Models.Response;
using Serilog;

namespace MentolProvision.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : FilteredController<Role, RolesController, RoleResponse>
    {
        #region General
        private ILogger _logger;
        private IDataInterface _data;
        private readonly IStringLocalizer<RolesController> _localizer;

        internal override Dictionary<string, List<string>> _propNames { get; set; } =
            new Dictionary<string, List<string>>()
            {
                    { nameof(Role.Name), new List<string>() { nameof(Role.Name) } },
                    { nameof(Role.Description), new List<string>() { nameof(Role.Description) } }
            };

        public RolesController(ILogger logger, IDataInterface data, IStringLocalizer<RolesController> localizer, ApplicationContext context)
            : base(logger, localizer, context)
        {
            _logger = logger;
            _data = data;
            _localizer = localizer;
        }
        #endregion

        #region End-points
        /// <summary>
        /// Изменить информацию о роле
        /// </summary>
        /// <param name="idr"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(RolesRequest), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] RolesRequest role)
        {
            try
            {
                if (role.Idr is null)
                {
                    return BadRequest(_localizer["ERROR_NEED_IDR"].Value);
                }

                if (role.Name is null)
                {
                    return BadRequest(_localizer["ERROR_NEED_NAME"].Value);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingRole = (await _data.GetRolesAsync(role.Idr.Value, null)).FirstOrDefault();
                if (existingRole == null)
                {
                    return NotFound();
                }

                existingRole.Name = role.Name;
                existingRole.Description = role.Description;

                await _data.UpdateRoleAsynс(existingRole);

                return Ok(new RoleResponse
                {
                    RoleName = existingRole.Name,
                    RoleDescription = existingRole.Description,
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex, _localizer["ERROR_METHOD_PUT"].Value + ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Измененить доступ роли к страницам
        /// </summary>
        /// <param name="idr"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(PutRolesByPagesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{idr}/pages")]
        public async Task<IActionResult> Put(int? idr, [FromBody] PutRolesByPagesRequest request)
        {
            try
            {
                if (idr is null)
                {
                    return BadRequest(_localizer["ERROR_NEED_IDR"].Value);
                }

                if (request.PageIdList is null)
                {
                    return BadRequest(_localizer["ERROR_NEED_PAGEDIDLIST"].Value);
                }

                var existingRole = (await _data.GetRolesAsync(idr, null)).FirstOrDefault();
                if (existingRole == null)
                {
                    return NotFound(_localizer["ERROR_ROLE_NOT_EXIST"].Value);
                }

                var allPageIds = (await _data.GetAllPageAsync()).Select(page => page.Idr);
                var notExistRoleId = request.PageIdList.Except(allPageIds);

                if (notExistRoleId.Any())
                {
                    return NotFound(_localizer["ERROR_PAGES_NOT_EXIST", string.Join(",", notExistRoleId)].Value);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var uniqPagesIds = request.PageIdList.Distinct().ToList();

                var newRolesToPages = uniqPagesIds.Select(pageId => new PageToRole
                {
                    PageId = pageId,
                    RoleId = idr.Value
                })
                .ToList();

                await _data.ReplacePageToRolesForRoleIdAsync(idr.Value, newRolesToPages);

                return Ok(new PutRolesByPagesResponse
                {
                    PageIdList = uniqPagesIds
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex, _localizer["ERROR_METHOD_PUT_ROLESPAGES"].Value + ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Измененить доступ роли к объектам
        /// </summary>
        /// <param name="idr"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(PutRolesObjectsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{idr}/objects")]
        public async Task<IActionResult> Put(int? idr, [FromBody] PutRolesObjectsRequest request)
        {
            try
            {
                if (idr is null)
                {
                    return BadRequest(_localizer["ERROR_NEED_IDR"].Value);
                }

                if (request.ObjectsIdList is null)
                {
                    return BadRequest(_localizer["ERROR_NEED_OBJECTSIDLIST"].Value);
                }

                var existingRole = (await _data.GetRolesAsync(idr, null)).FirstOrDefault();
                if (existingRole == null)
                {
                    return NotFound(_localizer["ERROR_ROLE_NOT_EXIST"].Value);
                }

                var requestObjectIds = request.ObjectsIdList.Select(ra => ra.ObjectId).ToList();
                var uniqRequestObjectIds = requestObjectIds.Distinct().ToList();
                if (requestObjectIds.Count > uniqRequestObjectIds.Count)
                {
                    var set = new HashSet<int>();
                    var duplicateObjectIds = requestObjectIds.Where(ol => !set.Add(ol));
                    return BadRequest(_localizer["ERROR_OBJECTIDS_NOT_UNIQUE", string.Join(",", duplicateObjectIds)].Value);
                }

                var existingObjectListIds = (await _data.GetObjectListAsync(uniqRequestObjectIds)).Select(ol => ol.Idr).ToList();
                if (uniqRequestObjectIds.Count > existingObjectListIds.Count)
                {
                    var notExistObjectIds = requestObjectIds.Except(existingObjectListIds);
                    return NotFound(_localizer["ERROR_OBJECTLIST_NOT_EXIST", string.Join(",", notExistObjectIds)].Value);
                }

                var requestActionIds = request.ObjectsIdList.Select(ra => ra.ActionId).Distinct().ToList();
                var existingActionListIds = (await _data.GetActionListAsync(requestActionIds)).Select(al => al.Idr).ToList();

                if (requestActionIds.Count != existingActionListIds.Count)
                {
                    var notExistActionIds = requestActionIds.Except(existingActionListIds);
                    return NotFound(_localizer["ERROR_ACTIONLIST_NOT_EXIST", string.Join(",", notExistActionIds)].Value);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newActionRoles = request.ObjectsIdList.Select(objectId => new RoleAction
                {
                    ActionId = objectId.ActionId,
                    ObjectId = objectId.ObjectId,
                    RoleId = idr.Value
                })
                .ToList();

                await _data.ReplaceRoleActionForRoleIdAsync(idr.Value, newActionRoles);

                return Ok(new PutRolesObjectsResponse
                {
                    ObjectsIdList = request.ObjectsIdList
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex, _localizer["ERROR_METHOD_PUT_ROLESOBJECTS"].Value + ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Получить страницы для роли
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pagesId"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(GetPagesByUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("pages")]
        public async Task<IActionResult> Get(int? userId, int? pagesId)
        {
            try
            {
                if (userId is null)
                {
                    return BadRequest(_localizer["ERROR_NEED_USERID"].Value);
                }

                var user = await _data.GetUserAsync(userId.Value);
                if ( user is null)
                {
                    return NotFound(_localizer["ERROR_USER_NOT_EXIST", userId.Value].Value);
                }

                if (pagesId != null)
                {
                    var page = await _data.GetPageByIdAsync(pagesId.Value);
                    if(page is null)
                        return NotFound(_localizer["ERROR_PAGE_NOT_EXIST", pagesId.Value].Value);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var pagesByUser = await _data.GetPagesByUserIdAsync(userId.Value);

                var responsePages = new List<Page>();
                if (pagesId is null)
                {
                    responsePages.AddRange(pagesByUser.Where(page => page.ParentId == null));
                }
                else
                {
                    responsePages.AddRange(pagesByUser.Where(page => page.ParentId == pagesId));
                }

                return Ok(new GetPagesByUserResponse
                {
                    menuList = responsePages.Select(page => new PageByUser
                    {
                        Id = page.Idr,
                        ViewName = page.ViewName,
                        ParentId = page.ParentId,
                        Action = page.Action
                    }).ToList()
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex, _localizer["ERROR_METHOD_GET_PAGESBYROLE"].Value + ex.Message);
                return StatusCode(500);
            }
        }
        #endregion

        #region Private filtering methods
        public override Expression<Func<Role, RoleResponse>> GetSelectExpression()
        {
            Expression<Func<Role, RoleResponse>> expression = t => new RoleResponse
            {
                RoleName = t.Name,
                RoleDescription = t.Description,
            };

            return expression;
        }

        public override IOrderedQueryable<RoleResponse> GetOrderExpression(string columnName, bool desc, IQueryable<RoleResponse> _query) =>
            columnName switch
            {
                nameof(Role.Name)        => !desc ? _query.OrderBy(r => r.RoleName) : _query.OrderByDescending(r => r.RoleName),
                nameof(Role.Description) => !desc ? _query.OrderBy(r => r.RoleDescription) : _query.OrderByDescending(r => r.RoleDescription),
                _ => throw new ArgumentException(message: "invalid order column name", paramName: nameof(columnName)),
            };
        #endregion
    }
}