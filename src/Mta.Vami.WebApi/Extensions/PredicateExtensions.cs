using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi
{
    public static class PredicateExtensions
    {
        public static Result<List<string>> Validate(this List<Predicate> predicates, List<string> allow)
        {
            var lstNotAllow = new List<string>();
            foreach (var predicate in predicates)
            {
                if (!allow.Contains(predicate.FieldName))
                {
                    lstNotAllow.Add(predicate.FieldName);
                }
            }

            if (lstNotAllow.Count > 0)
            {
                return Result.Error("Danh sách các điều kiện không được lọc", lstNotAllow.Distinct().ToList());
            }

            return Result.Ok(new List<string>());
        }

        public static Expression<Func<T, bool>> ToExpression<T>(this List<Predicate> predicates)
        {
            var parameter = ExpressionExtensions.GetParameterExpression<T>();
            Expression<Func<T, bool>> exp = null;
            var logic = PredicateOperator.AND;
            foreach (var predicate in predicates)
            {
                var prop = typeof(T).GetProperty(predicate.FieldName);
                var expChild = ExpressionExtensions.Operation<T>(parameter, predicate.FieldName, predicate.OperatorKey, GetPredicateValue(predicate.Value, predicate.OperatorKey, prop.PropertyType));
                if (exp != null)
                {
                    if (logic == PredicateOperator.AND)
                    {
                        exp = ExpressionExtensions.And(exp, expChild);
                    }
                    else
                    {
                        exp = ExpressionExtensions.Or(exp, expChild);
                    }
                }
                else
                {
                    exp = expChild;
                }    

                logic = predicate.OperatorSearch.HasValue ? predicate.OperatorSearch.Value : PredicateOperator.AND;
            }

            return exp;
        }

        private static object GetPredicateValue(JToken value, PredicateOperator opr, Type valueType)
        {
            return value.ParseFromToken(valueType);
        }
    }
}
