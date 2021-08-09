using Mta.Vami.WebApi.Entities;
using Mta.Vami.WebApi.Services;
using Mta.Vami.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi.Controllers
{
    /// <summary>
    /// Api quản lý nhóm vật liệu
    /// </summary>
    public class MaterialGroupController : BaseApiController<MaterialGroupSearchRequest, MaterialGroup, int, MaterialGroupService>
    {
        public MaterialGroupController(WorkingContext<MaterialGroupController> context, MaterialGroupService service) : base(context, service)
        {
        }
    }
}
