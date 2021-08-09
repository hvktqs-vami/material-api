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
    /// Model thông tin các cơ tính của vật liệu ở nhiệt độ cao
    /// </summary>
    [Table("Material_High_Temp_Mec_Props")]
    public class MaterialHighTempMecProp : Int64BaseEntity
    {
        [Column]
        public long MaterialId { get; set; }

        /// <summary>
        /// Nhiệt độ
        /// </summary>
        [Column]
        public decimal Temperature { get; set; }
        
        /// <summary>
        /// Giới hạn chảy dưới
        /// </summary>
        [Column]
        public decimal YieldRp02 { get; set; }

        /// <summary>
        /// Giới hạn chảy trên
        /// </summary>
        [Column]
        public decimal YieldRp1 { get; set; }
        
        /// <summary>
        /// Giới hạn bền kéo
        /// </summary>
        [Column]
        public decimal TensileRm { get; set; }

        /// <summary>
        /// Độ cứng
        /// </summary>
        [Column]
        public decimal Hardness { get; set; }

        [Column]
        public decimal H1k { get; set; }

        [Column]
        public decimal H10k { get; set; }

        [Column]
        public decimal H100k { get; set; }
    }
}
