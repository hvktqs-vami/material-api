using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi.Models
{
    /// <summary>
    /// Tham số tìm kiếm nhóm con
    /// </summary>
    public class MaterialSubGroupRequest : BaseSearchRequest
    {
        /// <summary>
        /// Từ khóa tìm kiếm
        /// </summary>
        public string SearchKey { get; set; }

        /// <summary>
        /// Tìm kiếm theo nhóm
        /// </summary>
        public List<int> GroupIds { get; set; }
    }
}
