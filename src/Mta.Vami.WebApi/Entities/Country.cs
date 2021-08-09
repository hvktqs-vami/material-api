using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mta.Vami.WebApi.Core;
namespace Mta.Vami.WebApi.Entities
{
    /// <summary>
    /// Model danh sách country
    /// </summary>
    [Table("Countries")]
    public class Country : BaseEntity<string>
    {
        /// <summary>
        /// Tên Quốc gia
        /// </summary>
        [Column]        
        public string Name { get; set; }
    }
}
