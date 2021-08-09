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
    /// Api quản lý nhóm thuộc tính cơ học
    /// </summary>
    public class MaterialMechanicalPropGroupController : BaseApiController<MaterialMechanicalPropGroupSearchRequest, MaterialMechanicalPropGroup, int, MaterialMechanicalPropGroupService>
    {
        public MaterialMechanicalPropGroupController(WorkingContext<MaterialMechanicalPropGroupController> context, MaterialMechanicalPropGroupService service) : base(context, service)
        {
        }
    }
}
