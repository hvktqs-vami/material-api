using Mta.Vami.WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi.Models
{
    /// <summary>
    /// Model thông tin vật liệu
    /// </summary>
    public class MaterialModel
    {
        public long Id { get; set; }

        /// <summary>
        /// Tên vật liệu
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ID nước của vật liệu
        /// </summary>
        public string CountryId { get; set; }

        /// <summary>
        /// ID nhóm 
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Tên nhóm
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Id nhóm con
        /// </summary>
        public int SubGroupId { get; set; }

        /// <summary>
        /// Tên nhóm con
        /// </summary>
        public string SubGroupName { get; set; }

        /// <summary>
        /// Xử lý nhiệt
        /// </summary>
        public string HeatTreatment { get; set; }


        /// <summary>
        /// ID tiêu chuẩn
        /// </summary>
        public int StandardId { get; set; }

        /// <summary>
        /// Tên tiêu chuẩn
        /// </summary>
        public string StandardName { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedUser { get; set; }

        /// <summary>
        /// Thời gian sửa
        /// </summary>
        public DateTime UpdatedTime { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        public string UpdatedUser { get; set; }
    }

    /// <summary>
    /// Model thông tin chi tiết của vật liệu
    /// </summary>
    public class MaterialDetailModel: MaterialModel
    {
        /// <summary>
        /// Nội dung mô tả chi tiết
        /// </summary>
        public MaterialContent Content { get; set; }

        /// <summary>
        /// Danh sách thành phần hóa học
        /// </summary>
        public List<MaterialChemical> ChemicalElements { get; set; }

        /// <summary>
        /// Danh sách độ bền mỏi cao
        /// </summary>
        public List<MaterialHighFatigue> HighFatigues { get; set; }

        /// <summary>
        /// Danh sách độ bền mỏi thấp
        /// </summary>
        public List<MaterialLowFatigue> LowFatigues { get; set; }

        /// <summary>
        /// Cơ tính ở nhiệt độ cao
        /// </summary>
        public List<MaterialHighTempMecProp> HighTempMecProps { get; set; }

        /// <summary>
        /// Cơ tính của vật liệu
        /// </summary>
        public List<MaterialMechanicalProp> MechanicalProps { get; set; }

        /// <summary>
        /// Các vật liệu tương đương
        /// </summary>
        public List<MaterialEquivalent> Equivalents { get; set; }

        /// <summary>
        /// Các ứng dụng của vật liệu
        /// </summary>
        public List<MaterialUsage> Usages { get; set; }
    }

    /// <summary>
    /// Request thêm mới, sửa thông tin vật liệu
    /// </summary>
    public class MaterialSaveRequest: MaterialModel
    {
        /// <summary>
        /// Nội dung mô tả chi tiết
        /// </summary>
        public MaterialContent Content { get; set; }

        /// <summary>
        /// Danh sách thành phần hóa học
        /// </summary>
        public List<MaterialChemical> ChemicalElements { get; set; }

        /// <summary>
        /// Danh sách độ bền mỏi cao
        /// </summary>
        public List<MaterialHighFatigue> HighFatigues { get; set; }

        /// <summary>
        /// Danh sách độ bền mỏi thấp
        /// </summary>
        public List<MaterialLowFatigue> LowFatigues { get; set; }

        /// <summary>
        /// Cơ tính ở nhiệt độ cao
        /// </summary>
        public List<MaterialHighTempMecProp> HighTempMecProps { get; set; }

        /// <summary>
        /// Cơ tính của vật liệu
        /// </summary>
        public List<MaterialMechanicalProp> MechanicalProps { get; set; }

        /// <summary>
        /// Các vật liệu tương đương
        /// </summary>
        public List<MaterialEquivalent> Equivalents { get; set; }

        /// <summary>
        /// Các ứng dụng của vật liệu
        /// </summary>
        public List<MaterialUsage> Usages { get; set; }
    }
}
