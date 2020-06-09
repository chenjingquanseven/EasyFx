using System;
using System.Linq;
using System.Linq.Expressions;

namespace EasyFx.Core.Extensions
{
    public static partial class QueryableExtensions
    {
        public static IQueryable<T> AndIf<T>(this IQueryable<T> queryable, Expression<Func<T, bool>> precidate,
            bool condition)
        {
            return condition ? queryable.Where(precidate) : queryable;
        }
    }
}