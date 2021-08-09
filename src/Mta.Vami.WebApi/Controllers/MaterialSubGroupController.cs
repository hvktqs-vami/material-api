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
    /// Api quản lý nhóm con
    /// </summary>
    public class MaterialSubGroupController : BaseApiController<MaterialSubGroupRequest, MaterialSubGroup, int, MaterialSubGroupService>
    {
        public MaterialSubGroupController(WorkingContext<MaterialSubGroupController> context, MaterialSubGroupService service) : base(context, service)
        {
        }
    }
}
