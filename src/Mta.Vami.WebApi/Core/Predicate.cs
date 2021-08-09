using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi
{
    public class Predicate
    {
        public string FieldName { get; set; }

        public PredicateOperator OperatorKey { get; set; }

        public JToken Value { get; set; }

        public PredicateOperator? OperatorSearch { get; set; }
    }

    public enum PredicateOperator
    {
        AND,
        OR,
        GREATE,
        GREATETHAN,
        LESS,
        LESSTHAN,
        EQUA,
        LIKE
    }
}
