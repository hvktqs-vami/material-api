using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi
{
    public class BaseSearchRequest
    {
        /// <summary>
        /// Các thông tin sắp xếp
        /// </summary>
        public List<SortInfo> OrderBys { get; set; }

        /// <summary>
        /// Index trang: Tính từ page 1
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Số lượng item trên mỗi trang
        /// </summary>
        public int PageSize { get; set; }
    }

    /// <summary>
    /// Model sắp xếp
    /// </summary>
    public class SortInfo
    {
        /// <summary>
        /// Trường cần sắp xếp
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Hướng sắp xếp Asc Tăng dần Desc Giảm dần
        /// </summary>
        public SortDirection Direction { get; set; }
    }

    /// <summary>
    /// Hướng sắp xếp Asc Tăng dần Desc Giảm dần
    /// </summary>
    public enum SortDirection
    {
        /// <summary>
        /// Tăng dần
        /// </summary>
        Asc = 0,

        /// <summary>
        /// Giảm dần
        /// </summary>
        Desc = 1
    }
}
