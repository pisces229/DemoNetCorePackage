using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace DemoExpression
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> MutilStartWith<T>(Expression<Func<T, string>> exp, IEnumerable<string> words)
        {
            var _w = words.ToList();
            if (_w.Count >= 2)
            {
                Expression<Func<T, bool>> tmp = StartsWithFunction(exp, _w[0]);
                for (int i = 1; i < _w.Count; i++)
                    tmp = tmp.Or(StartsWithFunction(exp, _w[i]));

                return tmp;
            }
            else if (_w.Count == 1)
            {
                return StartsWithFunction(exp, _w[0]);
            }
            else
            {
                return x => false;
            }
        }

        private static Expression<Func<T, bool>> StartsWithFunction<T>(Expression<Func<T, string>> exp, string word)
        {
            //var parameterExp = Expression.Parameter(typeof(T), "str");
            MethodInfo method = typeof(string).GetMethod(nameof(string.StartsWith), new[] { typeof(string) });

            var someValue = Expression.Constant(word, typeof(string));
            var containsMethodExp = Expression.Call(exp.Body, method, someValue);

            return Expression.Lambda<Func<T, bool>>(containsMethodExp, exp.Parameters);
        }

        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> leftExpression,
            Expression<Func<T, bool>> rightExpression) =>
            Combine(leftExpression, rightExpression, Expression.AndAlso);

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> leftExpression,
            Expression<Func<T, bool>> rightExpression) =>
            Combine(leftExpression, rightExpression, Expression.Or);

        public static Expression<Func<T, bool>> Combine<T>(Expression<Func<T, bool>> leftExpression, Expression<Func<T, bool>> rightExpression, Func<Expression, Expression, BinaryExpression> combineOperator)
        {
            var leftParameter = leftExpression.Parameters[0];
            var rightParameter = rightExpression.Parameters[0];

            var visitor = new ReplaceParameterVisitor(rightParameter, leftParameter);

            var leftBody = leftExpression.Body;
            var rightBody = visitor.Visit(rightExpression.Body);

            return Expression.Lambda<Func<T, bool>>(combineOperator(leftBody, rightBody), leftParameter);
        }

        private class ReplaceParameterVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _oldParameter;
            private readonly ParameterExpression _newParameter;

            public ReplaceParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
            {
                _oldParameter = oldParameter;
                _newParameter = newParameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return ReferenceEquals(node, _oldParameter) ? _newParameter : base.VisitParameter(node);
            }
        }
    }
}
