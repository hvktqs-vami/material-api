using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi.Models
{
    /// <summary>
    /// Tham số tìm kiểu tiêu chuẩn
    /// </summary>
    public class MaterialStandardRequest : BaseSearchRequest
    {
        /// <summary>
        /// Từ khóa tìm kiếm
        /// </summary>
        public string SearchKey { get; set; }

        /// <summary>
        /// Các nước cần tìm kiếm
        /// </summary>
        public List<string> CountryIds { get; set; }
    }
}
