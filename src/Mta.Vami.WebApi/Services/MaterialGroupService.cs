using Mta.Vami.WebApi.Entities;
using Mta.Vami.WebApi.Repositories;
using Mta.Vami.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;

namespace Mta.Vami.WebApi.Services
{
    public class MaterialGroupService : BaseCRUDService<MaterialGroupSearchRequest,  MaterialGroup, int, MaterialGroupRepository>
    {
        public MaterialGroupService(WorkingContext<MaterialGroupService> context) : base(context)
        {
        }

        public override Result<IQueryable<MaterialGroup>> ApplySearchParameter(MaterialGroupSearchRequest parameter, IQueryable<MaterialGroup> query)
        {
            var builder = new PredicateBuilder<MaterialGroup>();
            if (!string.IsNullOrWhiteSpace(parameter.SearchKey))
            {
                builder.And(x => Sql.Like(x.Name, "%" + parameter.SearchKey + "%"));
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
