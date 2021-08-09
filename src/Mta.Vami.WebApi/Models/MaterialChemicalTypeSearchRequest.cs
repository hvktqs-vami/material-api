using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi.Models
{
    /// <summary>
    /// Request tìm kiếm loại thành phần hóa học
    /// </summary>
    public class MaterialChemicalTypeSearchRequest : BaseSearchRequest
    {
        /// <summary>
        /// Từ khóa tìm kiếm
        /// </summary>
        public string SearchKey { get; set; }

    }
}
