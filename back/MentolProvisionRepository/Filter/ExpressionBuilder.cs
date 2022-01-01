using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace MentolProvisionRepository.ExpressionBuilder
{
    public class ExpressionBuilder<T> where T : class
    {
        public Expression<Func<T, bool>> BuildWhereExpression(object value, OperatorComparer comparer, params string[] properties)
        {
            var parameterExpression = Expression.Parameter(typeof(T), typeof(T).Name);
            return (Expression<Func<T, bool>>)BuildNavigationExpression(parameterExpression, comparer, value, properties);
        }

        private static Expression BuildNavigationExpression(Expression parameter, OperatorComparer comparer, object value, params string[] properties)
        {
            Expression childParameter, predicate;
            Type childType = null;

            Expression resultExpression;
            if (properties.Count() > 1)
            {
                parameter = Expression.Property(parameter, properties[0]);

                var isCollection = typeof(IEnumerable).IsAssignableFrom(parameter.Type);
                if (isCollection)
                {
                    childType = parameter.Type.GetGenericArguments()[0];
                    childParameter = Expression.Parameter(childType, childType.Name);
                }
                else
                    childParameter = parameter;

                var innerProperties = properties.Skip(1).ToArray();
                predicate = BuildNavigationExpression(childParameter, comparer, value, innerProperties);

                if (isCollection)
                    resultExpression = BuildSubQuery(parameter, childType, predicate);
                else
                    resultExpression = predicate;
            }
            else
                resultExpression = BuildCondition(parameter, properties[0], comparer, value);

            return resultExpression;
        }

        private static Expression BuildSubQuery(Expression parameter, Type childType, Expression predicate)
        {
            var anyMethod = typeof(Enumerable).GetMethods().Single(m => m.Name == "Any" && m.GetParameters().Length == 2);
            anyMethod = anyMethod.MakeGenericMethod(childType);
            predicate = Expression.Call(anyMethod, parameter, predicate);
            return MakeLambda(parameter, predicate);
        }

        private static Expression BuildCondition(Expression parameter, string property, OperatorComparer comparer, object value)
        {
            var childProperty = parameter.Type.GetProperty(property);
            var left = Expression.Property(parameter, childProperty);
            var right = Expression.Constant(value);
            var predicate = BuildComparsion(left, comparer, right);
            return MakeLambda(parameter, predicate);
        }

        private static Expression BuildComparsion(Expression left, OperatorComparer comparer, Expression right)
        {
            var toString = typeof(object).GetMethod("ToString");

            if (left.Type != typeof(string))
                left = Expression.Call(left, toString);

            return BuildStringCondition(left, comparer, right);
        }

        private static Expression BuildStringCondition(Expression left, OperatorComparer comparer, Expression right)
        {
            var compareMethod = typeof(string).GetMethods().FirstOrDefault(m => m.Name.Equals(Enum.GetName(typeof(OperatorComparer), comparer)) && m.GetParameters().Count() == 1);
            var toLowerMethod = typeof(string).GetMethods().Single(m => m.Name.Equals("ToLower") && m.GetParameters().Count() == 0);

            left = Expression.Call(left, toLowerMethod);
            right = Expression.Call(right, toLowerMethod);

            return Expression.Call(left, compareMethod, right);
        }

        private static Expression MakeLambda(Expression parameter, Expression predicate)
        {
            var resultParameterVisitor = new ParameterVisitor();
            resultParameterVisitor.Visit(parameter);
            var resultParameter = resultParameterVisitor.Parameter;
            return Expression.Lambda(predicate, (ParameterExpression)resultParameter);
        }

        private class ParameterVisitor : ExpressionVisitor
        {
            public Expression Parameter
            {
                get;
                private set;
            }
            protected override Expression VisitParameter(ParameterExpression node)
            {
                Parameter = node;
                return node;
            }
        }
    }
}