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
    /// Model thông tin các mã tương đương của vật liệu
    /// </summary>
    [Table("Material_Equivalents")]
    public class MaterialEquivalent: Int64BaseEntity
  {

        [Column]
        public long MaterialId { get; set; }

        /// <summary>
        /// Id của vật liệu tương đương
        /// </summary>
        [Column]
        public long EquivMaterialId { get; set; }
    }
}
