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
    /// Model độ bền mỏi thấp
    /// </summary>
    [Table("Material_Low_Fatigues")]
    public class MaterialLowFatigue : Int64BaseEntity
    {
        [Column]
        public long MaterialId { get; set; }

        /// <summary>
        /// Điều kiện
        /// </summary>
        [StringLength(512), Column]
        public string Condition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(128), Column]
        public string Direction { get; set; }

        [Column]
        public decimal CYieldStrength { get; set; }

        [Column]
        public decimal CStrengthExp { get; set; }

        [Column]
        public decimal CStrengthCoef { get; set; }

        [Column]
        public decimal FStrengthExp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public decimal FStrengthCoef { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public decimal FDuctilityExp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public decimal FDuctilityCoef { get; set; }
    }
}
