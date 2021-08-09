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
    public class MaterialSubGroupService : BaseCRUDService<MaterialSubGroupRequest, MaterialSubGroup, int, MaterialSubGroupRepository>
    {
        public MaterialSubGroupService(WorkingContext<MaterialSubGroupService> context) : base(context)
        {
        }

        public override Result<IQueryable<MaterialSubGroup>> ApplySearchParameter(MaterialSubGroupRequest parameter, IQueryable<MaterialSubGroup> query)
        {
            var builder = new PredicateBuilder<MaterialSubGroup>();
            if (!string.IsNullOrWhiteSpace(parameter.SearchKey))
            {
                builder.And(x => Sql.Like(x.Name, "%" + parameter.SearchKey + "%"));
            }

            if (parameter.GroupIds != null && parameter.GroupIds.Count > 0)
            {
                builder.And(x => parameter.GroupIds.Contains(x.GroupId));
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
