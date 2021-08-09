using LinqToDB;
using LinqToDB.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Mta.Vami.WebApi.Core
{
    public static class SqlRepositoryExtensions
    {
        public static PaginationSet<T> ToPaging<T>(this IQueryable<T> query, int pagingIndex, int pagingSize)
        {
            return query.ToPaging(new PagingInfo(pagingSize, pagingIndex));
        }

        public static PaginationSet<T> ToPaging<T>(this IQueryable<T> query, PagingInfo page)
        {
            var count = query.Count();
            var lst = query.Skip(page.GetSkip()).Take(page.GetTake()).ToList();
            return new PaginationSet<T>(lst, count, page.Index, page.Size);
        }

        public static IDbConnection EnsureOpen(this IDbConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            return connection;
        }
    }
}
