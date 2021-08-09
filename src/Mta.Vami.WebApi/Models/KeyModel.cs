using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi.Models
{
    public class KeyModel<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
    }
}
