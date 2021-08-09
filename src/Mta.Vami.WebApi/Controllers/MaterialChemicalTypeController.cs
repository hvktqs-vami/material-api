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
    /// Api quản lý loại thành phần hóa học
    /// </summary>
    public class MaterialChemicalTypeController : BaseApiController<MaterialChemicalTypeSearchRequest, MaterialChemicalType, int, MaterialChemicalTypeService>
    {
        public MaterialChemicalTypeController(WorkingContext<MaterialChemicalTypeController> context, MaterialChemicalTypeService service) : base(context, service)
        {
        }
    }
}
