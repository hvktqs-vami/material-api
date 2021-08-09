using Microsoft.AspNetCore.Mvc;
using Mta.Vami.WebApi.Entities;
using Mta.Vami.WebApi.Services;
using Mta.Vami.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mta.Vami.WebApi.Core;

namespace Mta.Vami.WebApi.Controllers
{
    /// <summary>
    /// Api quản lý vật liệu
    /// </summary>
    public class MaterialController : BaseApiController
    {
        private MaterialService _service;
        public MaterialController(WorkingContext<MaterialController> context, MaterialService service) : base(context)
        {
            _service = service;
            _service.Context.UserName = Context.UserName;
        }

        /// <summary>
        /// Thêm mới vật liệu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public Result<MaterialDetailModel> Create([FromBody] MaterialSaveRequest model)
        {
            return _service.Create(model);
        }

        /// <summary>
        /// Cập nhật thông tin vật liệu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public Result<MaterialDetailModel> Update([FromBody] MaterialSaveRequest model)
        {
            return _service.Update(model);
        }

        /// <summary>
        /// Lấy thông tin chi tiết vật liệu theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public Result<MaterialDetailModel> Get([FromRoute] long id)
        {
            return _service.Get(id);
        }

        /// <summary>
        /// Xóa vật liệu theo Id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete("[action]")]
        public Result Delete([FromBody] KeyModel<long> model)
        {
            return _service.Delete(model.Id);
        }

        /// <summary>
        /// Tìm kiếm danh sách vật liệu: Paging
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public Result<PaginationSet<MaterialModel>> SearchPaging([FromBody] MaterialSearchRequest model)
        {
            return _service.SearchPaging(model);
        }

        /// <summary>
        /// Tìm kiếm danh sách vật liệu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public Result<List<MaterialModel>> Search([FromBody] MaterialSearchRequest model)
        {
            return _service.Search(model);
        }
    }
}
