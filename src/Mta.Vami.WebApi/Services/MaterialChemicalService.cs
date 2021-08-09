using Mta.Vami.WebApi.Entities;
using Mta.Vami.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi.Services
{
    public class MaterialChemicalService : BaseCRUDService<MaterialChemical, long, MaterialChemicalRepository>
    {
        public MaterialChemicalService(WorkingContext<MaterialChemicalService> context) : base(context)
        {
        }
    }
}
