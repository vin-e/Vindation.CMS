using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Vin.Core.Data.Expressions
{
    public static class Expressions
    {
        public static IQueryable<T> Where<T, K>(IQueryable<T> entity, string field, K query, WhereType whereType)
        {
            // Get the method information for the EnumType method
            MethodInfo mi = typeof(K).GetMethod(Enum.GetName(typeof(WhereType), whereType), new Type[] { typeof(K) });

            // Build the parameter for the expression
            ParameterExpression X = Expression.Parameter(typeof(T), "x");

            // Build the member that was specified for the expression
            MemberExpression meField = Expression.PropertyOrField(X, field);

            // Call the Type Method method on the member
            MethodCallExpression filter = Expression.Call(meField, mi, Expression.Constant(query));

            // Build the expression
            Expression<Func<T, bool>> expression = Expression.Lambda<Func<T, bool>>(filter, X);

            // Perform the search logic and return the results
            return entity.Where(expression);
        }
    }

    public enum WhereType
    {
        Equals = 1,
        Contains = 2,
        StartsWith = 3,
        EndsWith = 4,
        GreaterThan = 5,
        LessThan = 6
    }
}
