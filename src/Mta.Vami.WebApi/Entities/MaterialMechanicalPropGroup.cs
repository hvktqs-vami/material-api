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
    /// Nhóm thuộc tính cơ học
    /// </summary>
    [Table("Material_Mechanical_Prop_Groups")]
    public class MaterialMechanicalPropGroup : Int32BaseEntity
    {
        [Column]
        public string Name { get; set; }


        [Column]
        public string Description { get; set; }
    }
}
