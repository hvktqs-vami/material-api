﻿using Mta.Vami.WebApi.Core;
using Mta.Vami.WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi.Repositories
{
    public class MaterialMechanicalPropRepository : SqlRepository<MaterialMechanicalProp, long>
    {
        public MaterialMechanicalPropRepository() : base(DatasourceConst.Default)
        {
        }
    }
}
