using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi.Models
{
    /// <summary>
    /// Request tìm kiếm loại cơ tính vật liệu
    /// </summary>
    public class MaterialMechanicalPropTypeSearchRequest : BaseSearchRequest
    {
        /// <summary>
        /// Từ khóa tìm kiếm
        /// </summary>
        public string SearchKey { get; set; }


        /// <summary>
        /// Nhóm tìm kiếm
        /// </summary>
        public List<int> GroupIds { get; set; }

    }
}
