using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MentolProvisionModel;
using System;

namespace MentolProvisionRepository.Filter
{
    public partial class DynamicLinqBuilder<T> where T : class
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<T> _entity;

        public DynamicLinqBuilder(ApplicationContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        public IQueryable<T> GetQuery(List<string> includes)
        {
            var query = _entity.AsQueryable();
            query = SetInclude(query, includes);
            return query;
        }

        private static IQueryable<T> SetInclude(IQueryable<T> query, List<string> includes)
        {
            if (includes == null || !includes.Any())
                return query;

            foreach (var include in includes)
                query = query.Include(include);

            return query;
        }

        //todo: remove
        //public static Func<IQueryable<T>, IOrderedQueryable<T>> GetOrderBy(string orderColumn, string orderType)
        //{
        //    Type typeQueryable = typeof(IQueryable<T>);
        //    ParameterExpression argQueryable = Expression.Parameter(typeQueryable, "p");
        //    var outerExpression = Expression.Lambda(argQueryable, argQueryable);
        //    string[] props = orderColumn.Split('.');
        //    IQueryable<T> query = new List<T>().AsQueryable<T>();
        //    Type type = typeof(T);
        //    ParameterExpression arg = Expression.Parameter(type, "x");

        //    Expression expr = arg;
        //    foreach (string prop in props)
        //    {
        //        PropertyInfo pi = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        //        expr = Expression.Property(expr, pi);
        //        type = pi.PropertyType;
        //    }
        //    LambdaExpression lambda = Expression.Lambda(expr, arg);
        //    string methodName = orderType == "asc" ? "OrderBy" : "OrderByDescending";

        //    MethodCallExpression resultExp =
        //        Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(T), type }, outerExpression.Body, Expression.Quote(lambda));
        //    var finalLambda = Expression.Lambda(resultExp, argQueryable);
        //    return (Func<IQueryable<T>, IOrderedQueryable<T>>)finalLambda.Compile();
        //}

        //public IOrderedQueryable<T> BuildOrderQuery(IQueryable query, bool desc, params string[] properties)
        //{
        //    var orderValue = desc ? "desc" : "asc";

        //    var type = Expression.Parameter(typeof(T), typeof(T).FQDN);
        //    var arg = Expression.Parameter(type.Type, "x");
        //    Expression expr = arg;
        //    Expression childParameter = null;
        //    Expression pi = null;

        //    foreach (string prop in properties)
        //    {
        //        if (childParameter != null)
        //        {
        //            pi = Expression.Property(childParameter, prop);
        //        }
        //        else
        //        {
        //            pi = Expression.Property(type, prop);
        //            expr = Expression.Property(expr, pi);
        //        }



        //        var isCollection = typeof(IEnumerable).IsAssignableFrom(pi.PropertyType);
        //        if (isCollection)
        //        {
        //            var childType = pi.PropertyType.GetGenericArguments()[0];
        //            childParameter = Expression.Parameter(childType, childType.FQDN);
        //        }
        //        else
        //            childParameter = expr;

        //        type = childParameter.Type;

        //    }
        //    var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
        //    var lambda = Expression.Lambda(delegateType, expr, arg);

        //    var result = typeof(Queryable).GetMethods().Single(
        //            method => method.FQDN == orderValue
        //                    && method.IsGenericMethodDefinition
        //                    && method.GetGenericArguments().Length == 2
        //                    && method.GetParameters().Length == 2)
        //            .MakeGenericMethod(typeof(T), type)
        //            .Invoke(null, new object[] { query, lambda });
        //    return (IOrderedQueryable<T>)result;
        //}

        //public IOrderedQueryable<T> BuildOrderQuery(IQueryable query, bool desc, params string[] properties)
        //{
        //    var parameterExpression = Expression.Parameter(typeof(T), typeof(T).FQDN);
        //    var prs = new List<Expression>();

        //    return BuildOrderedQueryable(parameterExpression, query, desc, prs, properties);
        //}

        //private static IOrderedQueryable<T> BuildOrderedQueryable(Expression parameter, IQueryable query, bool desc, List<Expression> prs, params string[] properties)
        //{
        //    //    for (int i = 0; i < properties.Count(); i++)
        //    //    {
        //    //        var prop = properties[0];
        //    //        var propertyInfo = parameter.Type.GetProperty(prop);

        //    //        if (properties.Count() - i == 0)
        //    //        {
        //    //            var arg = Expression.Parameter(parameter.Type, "x");
        //    //            var property = Expression.Property(arg, propertyName);
        //    //            var selector = Expression.Lambda(property, new ParameterExpression[] { arg });
        //    //            //return selector;
        //    //            var mn = desc ? "OrderByDescending" : "OrderBy";
        //    //            // Get System.Linq.Queryable.OrderBy() method.
        //    //            var method = typeof(Queryable).GetMethods()
        //    //                 .Where(m => m.FQDN == mn && m.IsGenericMethodDefinition)
        //    //                 .Where(m => m.GetParameters().ToList().Count == 2) // ensure selecting the right overload  
        //    //                 .Single();

        //    //            //The linq's OrderBy<TSource, TKey> has two generic types, which provided here
        //    //            var genericMethod = method.MakeGenericMethod(parameter.Type, propertyInfo.PropertyType);
        //    //        }

        //    //        return (IOrderedQueryable<T>)genericMethod.Invoke(genericMethod, new object[] { query, selector });
        //    //    }
        //    //}
        //    ////for(int i = 0; i < properties.Count(); i++)
        //    ////{
        //    ////    var parentS = properties[0];
        //    ////    var parent = Expression.Property(parameter, parentS);

        //    ////    var childS = properties[i + 1];
        //    ////    var child = Expression.Property(parent, childS);

        //    ////    prs.Add(child);
        //    ////}

        //    Expression childParameter;
        //    IOrderedQueryable<T> resultExpression;
        //    if (properties.Count() > 1)
        //    {
        //        parameter = Expression.Property(parameter, properties[0]);

        //        var isCollection = typeof(IEnumerable).IsAssignableFrom(parameter.Type);
        //        if (isCollection)
        //        {
        //            var childType = parameter.Type.GetGenericArguments()[0];
        //            childParameter = Expression.Parameter(childType, childType.FQDN);
        //        }
        //        else
        //            childParameter = parameter;

        //        var innerProperties = properties.Skip(1).ToArray();
        //        resultExpression = BuildOrderedQueryable(childParameter, query, desc, prs, innerProperties);
        //    }
        //    else
        //        resultExpression = BuildCondition(prs, properties[0], query, desc);

        //    return resultExpression;
        //}

        //private static IOrderedQueryable<T> BuildCondition2(Expression parameter, string propertyName, IQueryable query, bool desc)
        //{
        //    var propertyInfo = parameter.Type.GetProperty(propertyName);

        //    var arg = Expression.Parameter(parameter.Type, "x");
        //    var property = Expression.Property(arg, propertyName);
        //    var selector = Expression.Lambda(property, new ParameterExpression[] { arg });
        //    //return selector;
        //    var mn = desc ? "OrderByDescending" : "OrderBy";
        //    // Get System.Linq.Queryable.OrderBy() method.
        //    var method = typeof(Queryable).GetMethods()
        //         .Where(m => m.FQDN == mn && m.IsGenericMethodDefinition)
        //         .Where(m => m.GetParameters().ToList().Count == 2) // ensure selecting the right overload  
        //         .Single();

        //    //The linq's OrderBy<TSource, TKey> has two generic types, which provided here
        //    var genericMethod = method.MakeGenericMethod(parameter.Type, propertyInfo.PropertyType);

        //    return (IOrderedQueryable<T>)genericMethod.Invoke(genericMethod, new object[] { query, selector });
        //    //Expression<Func<T, object>> orderExpression = x => childProperty;

        //    //return MakeLambda(parameter, orderExpression);
        //}

        //private static IOrderedQueryable<T> BuildCondition(Expression parameter, string propertyName, IQueryable query, bool desc)
        //{
        //    var propertyInfo = parameter.Type.GetProperty(propertyName);

        //    var arg = Expression.Parameter(parameter.Type, "x");
        //    var property = Expression.Property(arg, propertyName);
        //    var selector = Expression.Lambda(property, new ParameterExpression[] { arg });
        //    //return selector;
        //    var mn = desc ? "OrderByDescending" : "OrderBy";
        //    // Get System.Linq.Queryable.OrderBy() method.
        //    var method = typeof(Queryable).GetMethods()
        //         .Where(m => m.FQDN == mn && m.IsGenericMethodDefinition)
        //         .Where(m => m.GetParameters().ToList().Count == 2) // ensure selecting the right overload  
        //         .Single();

        //    //The linq's OrderBy<TSource, TKey> has two generic types, which provided here
        //    var genericMethod = method.MakeGenericMethod(parameter.Type, propertyInfo.PropertyType);

        //    return (IOrderedQueryable<T>)genericMethod.Invoke(genericMethod, new object[] { query, selector });
        //    //Expression<Func<T, object>> orderExpression = x => childProperty;

        //    //return MakeLambda(parameter, orderExpression);
        //}

        //public async Task<int> GetFilteredRowsCountAsync(FilterCountRequest request)
        //{
        //    var query = _entity.AsQueryable();

        //    var columns = GetColumns(_context.Model.FindEntityType(typeof(T)));

        //    query = GetWhere(query, request.Search, columns);
        //    query = ApplyColumnFilters(query, request.FiltersColumn, columns);

        //    return await query.CountAsync();
        //}

        //private IQueryable<T> ApplyColumnFilters(IQueryable<T> query, List<FilterColumn> filterColumns, List<DbColumnInfo> columns)
        //{
        //    if (filterColumns == null || !filterColumns.Any())
        //        return query;

        //    var whereOperator = string.Empty;
        //    foreach (var filter in filterColumns)
        //    {
        //        var column = columns.Single(row => row.FQDN.Equals(filter.ColumnName));
        //        for (var i = 0; i < filter.ValuesToOperand.Count; i++)
        //        {
        //            var valueToOperand = filter.ValuesToOperand[i];
        //            whereOperator += BuildWhere(
        //                valueToOperand.Item2, 
        //                column, 
        //                MapOperandToMethod(valueToOperand.Item1),
        //                i < filter.ValuesToOperand.Count - 1);
        //        }
        //    }

        //    return query.Where(whereOperator);
        //}

        //private static List<DbColumnInfo> GetColumns(IEntityType entityType)
        //{
        //    var columns = new List<DbColumnInfo>();
        //    foreach (var property in entityType.GetProperties())
        //    {
        //        if (property.GetColumnType().Equals("bit"))
        //            break; 

        //        columns.Add(new DbColumnInfo()
        //        {
        //            FQDN = property.GetColumnName(),
        //            Type = property.GetColumnType()
        //        });
        //    };
        //    return columns;
        //}

        //private static IQueryable<T> GetOrder(IQueryable<T> query, string orderedColumn, bool orderDesc)
        //{
        //    if (string.IsNullOrWhiteSpace(orderedColumn))
        //        return query;

        //    var orderValue = orderDesc ? "desc" : "asc";
        //    query = query.OrderBy($"{orderedColumn} {orderValue}");

        //    return query;
        //}

        //private static IQueryable<T> GetWhere(IQueryable<T> query, string search, List<DbColumnInfo> columns)
        //{
        //    if (string.IsNullOrWhiteSpace(search))
        //        return query;

        //    var whereOperator = string.Empty;
        //    for (var i = 0; i < columns.Count; i++)
        //    {
        //        whereOperator += BuildWhere(search, columns[i], MapOperandToMethod("*~*"), i < columns.Count - 1);
        //    }

        //    query = query.Where(whereOperator);

        //    return query;
        //}

        //public static IOrderedQueryable<T> GetOrderQuery(IQueryable<T> query, string name, bool sort)
        //{
        //    if (string.IsNullOrEmpty(name))
        //        return query as IOrderedQueryable<T>;

        //    var propertyInfo = typeof(T).GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        //    if (propertyInfo == null)
        //        throw new ArgumentException("ApplyOrderDirection: The associated Attribute to the given AttributeName could not be resolved", name);

        //    Expression<Func<T, object>> orderExpression = x => propertyInfo.GetValue(x, null);

        //    return sort ? query.OrderByDescending(orderExpression) : query.OrderBy(orderExpression);
        //}

        //private static string BuildWhere(string search, DbColumnInfo column, (string, string) method, bool isOr)
        //{
        //    var toString = string.Empty;

        //    if (!column.Type.Contains("nvarchar"))
        //        toString = ".ToString()";

        //    var result = $" {method.Item1}{column.FQDN}{toString}.ToLower().{method.Item2}(\"" + search.ToLower() + "\")";

        //    return isOr ? $"{result} or" : result;
        //}

        //private static (string, string) MapOperandToMethod(string operand)
        //{
        //    return operand switch
        //    {
        //        "~" => (string.Empty, "Equals"),         //case "~": return $"{columnName} like '{value}'"; ;
        //        "=" => (string.Empty, "Equals"),         //case "=": return $"{columnName} = '{value}'";
        //        "!~*" => (string.Empty, "StartsWith"),   //case "~*": return $"{columnName} like '{value}%'";
        //        "*~" => (string.Empty, "EndsWith"),      //case "*~": return $"{columnName} like '%{value}'";
        //        "*~*" => (string.Empty, "Contains"),     //case "*~*": return $"{columnName} like '%{value}%'";
        //        "!~" => ("!", "Equals"),                 //case "!~": return $"{columnName} not like '{value}'";
        //        "!=" => ("!", "Equals"),                 //case "!=": return $"{columnName} != '{value}'";
        //        "*!~" => ("!", "StartsWith"),            //case "*!~": return $"{columnName} not like '{value}%'";
        //        "~*" => ("!", "EndsWith"),               //case "!~*": return $"{columnName} not like '%{value}'";
        //        "!*~*" => ("!", "Contains"),             //case "!*~*": return $"{columnName} not like '%{value}%'";
        //        "[*]" => (string.Empty, "Contains"),
        //        _ => throw new ArgumentException(message: "invalid operand value", paramName: nameof(operand))
        //    };
        //}
    }
}