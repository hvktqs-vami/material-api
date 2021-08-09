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
    /// Model thông tin nhóm các loại vật liệu
    /// </summary>
    [Table("Material_Groups")]
    public class MaterialGroup : Int32BaseEntity
    {
        [Required]
        [StringLength(255), Column]
        public string Name { get; set; }
    }
}
