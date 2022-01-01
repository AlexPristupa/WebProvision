using System;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Extensions;
using MentolProvision.Extensions;
using MentolProvision.Models.Response.Common;
using MentolProvisionModel;
using MentolProvisionModel.Infrastructure;
using MentolProvisionRepository.Filter;
using MentolProvisionRepository.ExpressionBuilder;
using Microsoft.VisualBasic;
using Serilog;

namespace MentolProvision.Controllers
{
    [ApiController]
    public abstract class FilteredController<TEntity, TController, TResponse> : ControllerBase
       where TEntity : class
       where TController : class
       where TResponse : class
    {
        private readonly ILogger _logger;
        private readonly IStringLocalizer<TController> _localizer;
        private readonly ApplicationContext _context;
        private IQueryable<TEntity> _query;
        protected readonly DynamicLinqBuilder<TEntity> _dynamicQueryBuilder;
        private readonly ExpressionBuilder<TEntity> _expressionBuilder;
        internal abstract Dictionary<string, List<string>> _propNames { get; set; }

        public FilteredController(ILogger logger, IStringLocalizer<TController> localizer, ApplicationContext context)
        {
            _logger = logger;
            _localizer = localizer;
            _context = context;
            _dynamicQueryBuilder = new DynamicLinqBuilder<TEntity>(_context);
            _expressionBuilder = new ExpressionBuilder<TEntity>();
            _query = _dynamicQueryBuilder.GetQuery(GeIncludes());

        }

        #region End-points
        [HttpGet("selection")]
        public virtual async Task<IActionResult> GetFilteredAsync([FromQuery] FilterRequest request)
        {
            try
            {
	            if (request?.Limit == null || request.Offset == null)
		            return BadRequest("ERROR NEED LIMIT AND OFFSET");

	            if(!string.IsNullOrWhiteSpace(request.Search))
		            _query = _query.Where(GetWhereExpression(request.Search));

                var items = _query.Select(GetSelectExpression());

	            if (!string.IsNullOrWhiteSpace(request.TableColumn))
	            {
		            items = GetOrderExpression(request.TableColumn.Capitalize(), request.OrderDesc ?? true, items);
	            }

	            var result = await items
		            .Skip(request.Offset.Value * request.Limit.Value)
		            .Take(request.Limit.Value)
		            .ToListAsync();

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

        [HttpGet("totalCount")]
        public virtual async Task<IActionResult> GetFilteredCountAsync([FromQuery] FilterCountRequest request)
        {
            try
            {
                var query = _dynamicQueryBuilder.GetQuery(GeIncludes());

                if (!string.IsNullOrWhiteSpace(request.Search))
                    query = query.Where(GetWhereExpression(request.Search));

                var count = await GetCountExpressionAsync(query, request);
                return Ok(GetCountResponse(count, request));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, _localizer["ERROR_METHOD_GET"].Value + ex.Message);
                return StatusCode(500);
            }
        }

        [NonAction]
        internal Expression<Func<TEntity, bool>> CreateWhereExpression(
            string searchPattern,
            OperatorComparer comparer,
            List<string> properties)
        {
            return _expressionBuilder.BuildWhereExpression(searchPattern, comparer, properties.ToArray());
        }

        [NonAction]
        public Expression<Func<TEntity, bool>> GetWhereExpression(string searchPattern)
        {
            var expressions = new List<Expression<Func<TEntity, bool>>>();

            foreach (var prop in _propNames)
                expressions.Add(CreateWhereExpression(searchPattern, OperatorComparer.Contains, prop.Value));

            return expressions.Aggregate((x, y) => x.OrElse(y));
        }

        #endregion

        #region abstract
        [NonAction]
        public abstract Expression<Func<TEntity, TResponse>> GetSelectExpression();

        [NonAction]
        public abstract IOrderedQueryable<TResponse> GetOrderExpression(string columnName, bool desc, IQueryable<TResponse> _query);


        protected virtual Task<int> GetCountExpressionAsync(IQueryable<TEntity> query, FilterCountRequest request)
        {
	        return query.Select(GetSelectExpression()).CountAsync();
        }

        [NonAction]
        public virtual List<string> GeIncludes()    
        {
            return null;
        }

        protected virtual object GetCountResponse(int count, FilterCountRequest request)
        {
	        var response = new CountResponse {Status = {Result = "success"}, Meta = {Count = count, Search = request.Search, TableColumn = request.TableColumn}};
	        return response;
        }

        protected virtual object GetSelectionResponse(List<TResponse> data, FilterRequest request)
        {
	        return data;
        }

        #endregion
    }
}
