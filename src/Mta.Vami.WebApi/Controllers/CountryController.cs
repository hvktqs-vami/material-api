using Mta.Vami.WebApi.Entities;
using Mta.Vami.WebApi.Services;
using Mta.Vami.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi.Controllers
{
    /// <summary>
    /// Api quản lý danh mục Country
    /// </summary>
    public class CountryController : BaseApiController<CountrySearchRequest, Country, string, CountryService>
    {
        public CountryController(WorkingContext<CountryController> context, CountryService service) : base(context, service)
        {
        }
    }
}
