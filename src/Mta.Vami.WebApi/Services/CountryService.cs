using Mta.Vami.WebApi.Core;
using Mta.Vami.WebApi.Entities;
using Mta.Vami.WebApi.Models;
using Mta.Vami.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB.Linq;
using LinqToDB;

namespace Mta.Vami.WebApi.Services
{
    public class CountryService : BaseCRUDService<CountrySearchRequest, Country, string, CountryRepository>
    {
        public CountryService(WorkingContext<CountryService> context) : base(context)
        {
        }

        public override Result<IQueryable<Country>> ApplySearchParameter(CountrySearchRequest parameter, IQueryable<Country> query)
        {
            if (!string.IsNullOrWhiteSpace(parameter.SearchKey))
            {
                query = query.Where(x => Sql.Like(x.Id, "%" + parameter.SearchKey + "%") || Sql.Like(x.Name, "%" + parameter.SearchKey + "%"));
            }

            if (parameter.OrderBys != null && parameter.OrderBys.Count > 0)
            {
                query = query.OrderBy(parameter.OrderBys);
            }

            return Result.Ok(query);
        }


        public override Result Validate(Country entity, ActionType action, Country originEntity = null)
        {
            if(action == ActionType.Create)
            {
                var objExist = _repo.Get(entity.Id);
                if(objExist != null)
                {
                    return Result.Error(string.Format("Mã nước <{0}> đã tồn tại", entity.Id));
                }

                objExist = _repo.GetTable().FirstOrDefault(x => x.Name == entity.Name);

                if (objExist != null)
                {
                    return Result.Error(string.Format("Tên nước <{0}> đã tồn tại", entity.Name));
                }
            }

            return Result.Ok();
        }

    }
}
