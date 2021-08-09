using LinqToDB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi
{
    public class ParameterExpressionReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(_parameter);
        }

        internal ParameterExpressionReplacer(ParameterExpression parameter)
        {
            _parameter = parameter;
        }
    }

    public static class ExpressionExtensions
    {
        public static ParameterExpression GetParameterExpression(Type t, string name = "x")
        {
            return Expression.Parameter(t, name);
        }

        public static Expression IgnoreConvertType(this Expression exp)
        {
            if (exp.NodeType == ExpressionType.Convert)
            {
                var ue = exp as UnaryExpression;
                return ((ue != null) ? ue.Operand : null);
            }

            return exp;
        }

        public static MemberExpression CreateMemberExpression(ParameterExpression param, string propName)
        {
            var propInfo = param.Type.GetProperty(propName);
            if (propInfo == null)
            {
                throw new Exception(string.Format("[DEBUG] Property {0} not exist in Type {1}", propName, param.Type.FullName));
            }

            return Expression.Property(param, propInfo);
        }

        public static ParameterExpression GetParameterExpression<T>()
        {
            return GetParameterExpression(typeof(T));
        }

        public static Expression ChangeParameter(this Expression expr1, ParameterExpression param)
        {
            var replacer = new ParameterExpressionReplacer(param);
            var temp = replacer.Visit(expr1);
            return temp;
        }

        public static LambdaExpression ChangeParameter(this LambdaExpression expr1, ParameterExpression param)
        {
            var temp = expr1.Body.ChangeParameter(param);
            return Expression.Lambda(temp, param);
        }

        public static Expression<Func<T, bool>> ChangeParameter<T>(this Expression<Func<T, bool>> expr1, ParameterExpression param)
        {
            var temp = expr1.Body.ChangeParameter(param);
            return Expression.Lambda<Func<T, bool>>
                  (temp, param);
        }


        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var tempExp = Expression.OrElse(expr1.Body, expr2.Body);
            tempExp = (BinaryExpression)tempExp.ChangeParameter(expr1.Parameters.FirstOrDefault());

            return Expression.Lambda<Func<T, bool>>
                  (tempExp, expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var tempExp = Expression.AndAlso(expr1.Body, expr2.Body);
            tempExp = (BinaryExpression)tempExp.ChangeParameter(expr1.Parameters.FirstOrDefault());

            return Expression.Lambda<Func<T, bool>>
                  (tempExp, expr1.Parameters);
        }

        public static Expression<Func<T, bool>> Operation<T>(this ParameterExpression param, string propName, PredicateOperator operation, object value)
        {
            var member = CreateMemberExpression(param, propName);
            Expression constant = GetConstantExp(operation, member.Type, value);
            var exp = ToExpression(member, operation, constant);
            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        public static Expression ToExpression(MemberExpression member, PredicateOperator opr, Expression value)
        {

            if (opr == PredicateOperator.EQUA)
            {
                return Expression.Equal(member, value);
            }
            else if (opr == PredicateOperator.LESS)
            {
                return Expression.LessThan(member, value);
            }
            else if (opr == PredicateOperator.LESSTHAN)
            {
                return Expression.LessThanOrEqual(member, value);
            }
            else if (opr == PredicateOperator.GREATE)
            {
                return Expression.GreaterThan(member, value);
            }
            else if (opr == PredicateOperator.GREATETHAN)
            {
                return Expression.GreaterThanOrEqual(member, value);
            }
            else if(opr == PredicateOperator.LIKE)
            {
                var method = typeof(Sql).GetMethod(nameof(Sql.Like), new Type[] { typeof(string), typeof(string) });
                return Expression.Call(null, method, member, value);
            }    
        
            throw new NotSupportedException(Enum.GetName(opr));
        }

        public static ConstantExpression GetConstantExp(PredicateOperator opr, Type memberType, object value)
        {
            //if (opr == PredicateOperator.In || opr == PredicateOperator.NotIn)
            //{
            //    if (value is Array)
            //    {
            //        var type = typeof(List<>).MakeGenericType(memberType);
            //        var lst = Activator.CreateInstance(type) as IList;
            //        foreach (var a in ((Array)value))
            //        {
            //            lst.Add(a);
            //        }

            //        value = lst;
            //    }
            //}

            return Expression.Constant(value);
        }

    }
}
