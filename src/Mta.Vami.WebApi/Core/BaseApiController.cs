using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mta.Vami.WebApi.Core;
using Mta.Vami.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [AllowAnonymous]//Tạm thời để anymous để test
    public abstract class BaseApiController : ControllerBase
    {
        public WorkingContext Context { get; set; }

        public ILogger Logger => Context.Logger;

        protected BaseApiController(WorkingContext context)
        {
            Context = context;            
            if (User != null && User.Identity != null)
                Context.UserName = User.Identity.Name;
        }
    }

    public abstract class BaseApiController<TEntity, TPrimaryKey, TService> : BaseApiController where TEntity : BaseEntity<TPrimaryKey>, new()
                                                                                                    where TService : ICRUDService<TEntity, TPrimaryKey>
    {
        protected TService _service;

        protected BaseApiController(WorkingContext context, TService service) : base(context)
        {
            _service = service;
            _service.Context.UserName = Context.UserName;
        }

        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public Result<TEntity> Create([FromBody] TEntity model)
        {
            return _service.Create(model);
        }


        /// <summary>
        /// Sửa dữ liệu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public Result<TEntity> Update([FromBody] TEntity model)
        {
            return _service.Update(model);
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete("[action]")]
        public Result Delete([FromBody] KeyModel<TPrimaryKey> model)
        {
            return _service.Delete(model.Id);
        }
    }

    public abstract class BaseApiController<TSearchParamter, TEntity, TPrimaryKey, TService> : BaseApiController<TEntity, TPrimaryKey, TService> where TEntity : BaseEntity<TPrimaryKey>, new()
                                                                                                   where TService : ICRUDService<TSearchParamter, TEntity, TPrimaryKey>
                                                                                                   where TSearchParamter : BaseSearchRequest
    {

        protected BaseApiController(WorkingContext context, TService service) : base(context, service)
        {
        }

        /// <summary>
        /// Tìm kiếm: Paging
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public Result<PaginationSet<TEntity>> SearchPaging([FromBody] TSearchParamter model)
        {
            var rs = _service.SearchPaging(model);
            return rs;
        }

        /// <summary>
        /// Tìm kiếm
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public Result<List<TEntity>> Search([FromBody] TSearchParamter model)
        {
            var rs = _service.Search(model);
            return rs;
        }
    }
}
