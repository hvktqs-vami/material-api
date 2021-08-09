using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi.Core
{
    public class PaginationSet<T>
    {
        public List<T> Items { get; set; }

        public int TotalCount { get; set; }

        public int Page { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int Count => Items != null ? Items.Count : 0;


        public PaginationSet(List<T> items, int totalCount, int page, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            Page = page;
            PageSize = pageSize;
            TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(TotalCount)/ Convert.ToDecimal(PageSize)));
        }

        /// <summary>
        /// Nếu Mapping Function = null --> Sẽ sử dụng hàm MapToList mặc định của hệ thống
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="mapping"></param>
        /// <returns></returns>
        public PaginationSet<TDestination> ToResult<TDestination>(Func<T, TDestination> mapping)
        {
            List<TDestination> lst = new List<TDestination>();
            foreach (var obj in Items)
            {
                lst.Add(mapping(obj));
            }

            var rs = new PaginationSet<TDestination>(lst, TotalCount, Page, PageSize);
            return rs;
        }
    }

    public class PagingInfo
    {
        public int Size { get; set; }

        public int Index { get; set; }

        public PagingInfo()
        {

        }

        public PagingInfo(int size, int index)
        {
            Size = size;
            Index = index;
        }

        public int GetSkip()
        {
            return (Index < 1 ? 0 : (Index - 1)) * Size;
        }

        public int GetTake()
        {
            return Index < 1 ? 0 : Size;
        }
    }
}
