using System;
using System.Collections.Generic;
using LinqToDB.Mapping;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using Mta.Vami.WebApi.Core;
using System.ComponentModel.DataAnnotations;

namespace Mta.Vami.WebApi.Entities
{
    /// <summary>
    /// Model thông tin các cơ tính của vật liệu
    /// </summary>
    [Table("Material_Mechanical_Props")]
    public class MaterialMechanicalProp : Int64BaseEntity
    {
        [Column]
        public long MaterialId { get; set; }

        [Column]
        public int TypeId { get; set; }


        [StringLength(512), Column]
        public string DimensionHeat { get; set; }

        [Column]
        public decimal Min { get; set; }

        [Column]
        public decimal Max { get; set; }


        [Column]
        public decimal Approx { get; set; }

        /// <summary>
        /// Đơn vị tính
        /// </summary>
        [StringLength(64), Column]
        public string Unit { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        [StringLength(512), Column]
        public string Comment { get; set; }
    }
}
