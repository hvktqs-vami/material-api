using LinqToDB;
using Mta.Vami.WebApi.Core;
using Mta.Vami.WebApi.Entities;
using Mta.Vami.WebApi.Models;
using Mta.Vami.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi.Services
{
    public class MaterialStandardService : BaseCRUDService<MaterialStandardRequest, MaterialStandard, int, MaterialStandardRepository>
    {
        public MaterialStandardService(WorkingContext<MaterialStandardService> context) : base(context)
        {
        }


        public override Result<IQueryable<MaterialStandard>> ApplySearchParameter(MaterialStandardRequest parameter, IQueryable<MaterialStandard> query)
        {
            var builder = new PredicateBuilder<MaterialStandard>();
            if (!string.IsNullOrWhiteSpace(parameter.SearchKey))
            {
                builder.And(x => Sql.Like(x.Name, "%" + parameter.SearchKey + "%"));
            }

            if (parameter.CountryIds != null && parameter.CountryIds.Count > 0)
            {
                builder.And(x => parameter.CountryIds.Contains(x.CountryId));
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
