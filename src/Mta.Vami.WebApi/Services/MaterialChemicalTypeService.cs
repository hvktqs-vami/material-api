using Mta.Vami.WebApi.Entities;
using Mta.Vami.WebApi.Repositories;
using Mta.Vami.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mta.Vami.WebApi.Core;
using LinqToDB;

namespace Mta.Vami.WebApi.Services
{
    public class MaterialChemicalTypeService : BaseCRUDService<MaterialChemicalTypeSearchRequest, MaterialChemicalType, int, MaterialChemicalTypeRepository>
    {
        public MaterialChemicalTypeService(WorkingContext<MaterialChemicalTypeService> context) : base(context)
        {
        }

        public override Result<IQueryable<MaterialChemicalType>> ApplySearchParameter(MaterialChemicalTypeSearchRequest parameter, IQueryable<MaterialChemicalType> query)
        {
            var builder = new PredicateBuilder<MaterialChemicalType>();
            if (!string.IsNullOrWhiteSpace(parameter.SearchKey))
            {
                builder.And(x => Sql.Like(x.Name, "%" + parameter.SearchKey + "%") || Sql.Like(x.Code, "%" + parameter.SearchKey + "%"));
            }

            var exp = builder.Build();
            if (exp != null)
            {
                query = query.Where(exp);
            }

            if (parameter.OrderBys != null && parameter.OrderBys.Count > 0)
            {
                query = query.OrderBy(parameter.OrderBys);
            }

            return Result.Ok(query);
        }

    }
}
