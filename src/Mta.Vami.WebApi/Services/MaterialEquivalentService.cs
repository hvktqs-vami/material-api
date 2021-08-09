using Mta.Vami.WebApi.Entities;
using Mta.Vami.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi.Services
{
    public class MaterialEquivalentService : BaseCRUDService<MaterialEquivalent, long, MaterialEquivalentRepository>
    {
        public MaterialEquivalentService(WorkingContext<MaterialEquivalentService> context) : base(context)
        {
        }
    }
}
