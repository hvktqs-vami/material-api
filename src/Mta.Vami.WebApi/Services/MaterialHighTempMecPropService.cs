using Mta.Vami.WebApi.Entities;
using Mta.Vami.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi.Services
{
    public class MaterialHighTempMecPropService : BaseCRUDService<MaterialHighTempMecProp, long, MaterialHighTempMecPropRepository>
    {
        public MaterialHighTempMecPropService(WorkingContext<MaterialHighTempMecPropService> context) : base(context)
        {
        }
    }
}
