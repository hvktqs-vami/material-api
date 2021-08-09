using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi.Models
{
    /// <summary>
    /// Request tìm kiếm nhóm cơ tính vật liệu
    /// </summary>
    public class MaterialMechanicalPropGroupSearchRequest : BaseSearchRequest
    {
        /// <summary>
        /// Từ khóa tim kiếm
        /// </summary>
        public string SearchKey { get; set; }

    }
}
