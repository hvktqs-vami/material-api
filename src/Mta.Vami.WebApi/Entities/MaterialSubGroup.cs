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
    /// Model thông tin nhóm các loại vật liệu
    /// </summary>
    [Table("Material_Sub_Groups")]
    public class MaterialSubGroup : Int32BaseEntity
    {
        [Column]
        public string Name { get; set; }

        [Column]
        public int GroupId { get; set; }
    }
}
