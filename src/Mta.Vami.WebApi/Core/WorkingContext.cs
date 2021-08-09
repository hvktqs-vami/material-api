using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi
{
    public class WorkingContext
    {
        public ILogger Logger { get; set; }

        public string UserName { get; set; }

        public WorkingContext(ILogger logger)
        {
            Logger = logger;
        }
    }

    public class WorkingContext<TCatalog>: WorkingContext
    {
        public WorkingContext(ILogger<TCatalog> logger):base(logger)
        {
           
        }
    }    
}
