using Mta.Vami.WebApi.Entities;
using Mta.Vami.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi.Services
{
    public class MaterialLowFatigueService : BaseCRUDService<MaterialLowFatigue, long, MaterialLowFatigueRepository>
    {
        public MaterialLowFatigueService(WorkingContext<MaterialLowFatigueService> context) : base(context)
        {
        }
    }
}
