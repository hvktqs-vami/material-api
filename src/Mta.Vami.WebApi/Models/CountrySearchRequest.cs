using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi.Models
{
    /// <summary>
    /// Reques tìm kiếm Country
    /// </summary>
    public class CountrySearchRequest: BaseSearchRequest
    {
        /// <summary>
        /// Từ khóa tìm kiếm
        /// </summary>
        public string SearchKey { get; set; }

    }
}
