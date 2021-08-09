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
    /// Api quản lý các tiêu chuẩn
    /// </summary>
    public class MaterialStandardController : BaseApiController<MaterialStandardRequest, MaterialStandard, int, MaterialStandardService>
    {
        public MaterialStandardController(WorkingContext<MaterialStandardController> context, MaterialStandardService service) : base(context, service)
        {
        }
    }
}
