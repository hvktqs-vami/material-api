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
    /// Model thông tin nội dung vật liệu
    /// </summary>
    [Table("Material_Contents")]
    public class MaterialContent : Int64BaseEntity
    {
        public MaterialContent()
        {
        }

        /// <summary>
        /// Bài viết mô tả chi tiết dạng html
        /// </summary>
        [Column]
        public string HtmlContent { get; set; }

        /// <summary>
        /// Bài viết mô tả chi tiết dạng text
        /// </summary>
        [Column]
        public string PlainTextContent { get; set; }

        [Column]
        public long MaterialId { get; set; }
    }
}
