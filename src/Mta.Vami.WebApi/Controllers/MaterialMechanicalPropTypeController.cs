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
    /// Api quản lý loại thuộc tính cơ học
    /// </summary>
    public class MaterialMechanicalPropTypeController : BaseApiController<MaterialMechanicalPropTypeSearchRequest, MaterialMechanicalPropType, int, MaterialMechanicalPropTypeService>
    {
        public MaterialMechanicalPropTypeController(WorkingContext<MaterialMechanicalPropTypeController> context, MaterialMechanicalPropTypeService service) : base(context, service)
        {
        }
    }
}
