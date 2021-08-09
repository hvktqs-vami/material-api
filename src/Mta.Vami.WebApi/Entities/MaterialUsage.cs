using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;
using Mta.Vami.WebApi.Core;
namespace Mta.Vami.WebApi.Entities
{
    /// <summary>
    /// Model thông tin ứng dụng của vật liệu
    /// </summary>
    [Table("Material_Usages")]
    public class MaterialUsage : Int32BaseEntity
    {
        /// <summary>
        /// ứng dụng của vật liệu
        /// </summary>
        [Column]
        public string Name { get; set; }

        [Column]
        public long MaterialId { get; set; }
    }
}
