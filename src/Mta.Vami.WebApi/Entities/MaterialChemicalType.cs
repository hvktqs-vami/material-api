using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using Mta.Vami.WebApi.Core;
namespace Mta.Vami.WebApi.Entities
{
    /// <summary>
    /// Model chứa danh mục các loại thành phần hóa học
    /// </summary>
    [Table("Material_Chemical_Types")]
    public class MaterialChemicalType : Int32BaseEntity
    {
        [Required]
        [StringLength(255), Column]
        public string Code { get; set; }

        [Required]
        [StringLength(255), Column]
        public string Name { get; set; }
    }
}
