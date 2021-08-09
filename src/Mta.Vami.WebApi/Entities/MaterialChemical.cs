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
    /// Model thông tin các thành phần hóa học của vật liệu
    /// </summary>
    [Table("Material_Chemicals")]
    public class MaterialChemical : Int64BaseEntity
    {
        [Column]
        public long MaterialId { get; set; }

        /// <summary>
        /// ID của thành phần
        /// </summary>
        [Column]
        public int ElementId { get; set; }

        /// <summary>
        /// Tỷ lệ nhỏ nhất
        /// </summary>
        [Column]
        public decimal Min { get; set; }

        /// <summary>
        /// Tỷ lệ lớn nhất
        /// </summary>
        [Column]
        public decimal Max { get; set; }

        /// <summary>
        /// Xấp xỉ
        /// </summary>
        [Column]
        public decimal Approx { get; set; }
    }
}
