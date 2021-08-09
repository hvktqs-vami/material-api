using System;
using System.Collections.Generic;
using LinqToDB.Mapping;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using Mta.Vami.WebApi.Core;
namespace Mta.Vami.WebApi.Entities
{
    /// <summary>
    /// Model chứa danh mục các loại thuộc tính cơ học
    /// </summary>
    [Table("Material_Mechanical_Prop_Types")]
    public class MaterialMechanicalPropType : Int32BaseEntity
    {
        [Column]
        public string Name { get; set; }

        [Column]
        public int GroupId { get; set; }

        [Column]
        public int SortOrder { get; set; }
    }
}
