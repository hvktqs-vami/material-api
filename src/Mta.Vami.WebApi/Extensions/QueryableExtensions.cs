using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LinqToDB;
namespace Mta.Vami.WebApi
{
    public static class QueryableExtensions
    {
        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty, bool desc, bool useThenBy = false)
        {
            string command = "";
            if (useThenBy)
            {
                command = desc ? nameof(Enumerable.ThenByDescending) : nameof(Enumerable.ThenBy);
            }
            else
            {
                command = desc ? nameof(Enumerable.OrderByDescending) : nameof(Enumerable.OrderBy);
            }

            var type = typeof(TEntity);
            var property = type.GetProperty(orderByProperty);
            var parameter = Expression.Parameter(type, "x");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
                source.Expression, Expression.Quote(orderByExpression));
            return (IOrderedQueryable<TEntity>)source.Provider.CreateQuery<TEntity>(resultExpression);
        }

        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, SortInfo sort, bool useThenBy = false)
        {
            return source.OrderBy(sort.Field, sort.Direction == SortDirection.Desc, useThenBy);
        }

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, List<SortInfo> lstSort)
        {
            bool useThenBy = false;
            foreach (var sort in lstSort)
            {
                source = source.OrderBy(sort, useThenBy);
                useThenBy = true;
            }

            return (IOrderedQueryable<T>)source;
        }
    }
}
