using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi.Models
{
    /// <summary>
    /// Tham số tìm kiểu vật liệu
    /// </summary>
    public class MaterialSearchRequest : BaseSearchRequest
    {
        /// <summary>
        /// Điều kiện tìm kiếm
        /// </summary>
        public List<Predicate> ListExpression { get; set; }
    }
}
