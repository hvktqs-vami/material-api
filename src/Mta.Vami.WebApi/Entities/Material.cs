using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Mta.Vami.WebApi.Core;
namespace Mta.Vami.WebApi.Entities
{
    /// <summary>
    /// Model thông tin các loại vật liệu
    /// </summary>
    [Table("Materials")]
    public class Material : Int64BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public Material()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(255), Column]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(255), Column]
        public string CountryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(255), Column]
        public int GroupId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(255), Column]
        public int SubGroupId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(1024), Column]
        public string HeatTreatment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public int StandardId { get; set; }
    }
}
