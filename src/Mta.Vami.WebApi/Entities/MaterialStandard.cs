using System;
using System.Collections.Generic;
using LinqToDB.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mta.Vami.WebApi.Core;
namespace Mta.Vami.WebApi.Entities
{
    /// <summary>
    /// Model thông tin tiêu chuẩn của vật liệu
    /// </summary>
    [Table("Material_Standards")]
    public class MaterialStandard : Int32BaseEntity
    {
        [Column]
        public string Name { get; set; }

        [Column]
        public string CountryId { get; set; }
    }
}
