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
    /// Model độ bền mỏi cao
    /// </summary>
    [Table("Material_High_Fatigues")]
    public class MaterialHighFatigue : Int64BaseEntity
    {
        [Column]
        public long MaterialId { get; set; }

        /// <summary>
        /// Điều kiện
        /// </summary>
        [StringLength(512), Column]
        public string Condition { get; set; }

        /// <summary>
        /// Giá trị
        /// </summary>
        [Column]
        public decimal Value { get; set; }
    }
}
