using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi
{
    public static class CoreService
    {
        public static IConfiguration Configuration { get; set; }
    }
}
