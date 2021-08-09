using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi
{
    public class PredicateBuilder<T>
    {
        public ParameterExpression Parameter { get; private set; }

        private Expression<Func<T, bool>> CurrentExpression { get; set; }

        public PredicateBuilder()
        {
            Parameter = ExpressionExtensions.GetParameterExpression<T>();
        }

        public PredicateBuilder(Expression<Func<T, bool>> exp) : this()
        {
            if (exp != null)
            {
                And(exp);
            }
        }

        public static PredicateBuilder<T> Create(string propName, PredicateOperator op, object value)
        {
            PredicateBuilder<T> builder = new PredicateBuilder<T>();
            if (builder.CurrentExpression != null)
            {
                throw new Exception("[DEBUG] Hàm create chỉ được gọi khi khởi tạo");
            }

            builder.CurrentExpression = ExpressionExtensions.Operation<T>(builder.Parameter, propName, op, value);
            return builder;
        }

        public static PredicateBuilder<T> Create(Expression<Func<T, bool>> exp)
        {
            return new PredicateBuilder<T>(exp);
        }

        public Expression<Func<T, bool>> CreateExpression(string propName, PredicateOperator op, object value)
        {
            return ExpressionExtensions.Operation<T>(Parameter, propName, op, value);
        }

        public PredicateBuilder<T> And(string propName, PredicateOperator op, object value)
        {
            var exp = ExpressionExtensions.Operation<T>(Parameter, propName, op, value);
            return And(exp);
        }

        public PredicateBuilder<T> AndIf(bool condition, string propName, PredicateOperator op, object value)
        {
            if (condition)
            {
                return And(propName, op, value);
            }

            return this;
        }

        public PredicateBuilder<T> Or(string propName, PredicateOperator op, object value)
        {
            var exp = ExpressionExtensions.Operation<T>(Parameter, propName, op, value);
            return Or(exp);
        }

        public PredicateBuilder<T> OrIf(bool condition, string propName, PredicateOperator op, object value)
        {
            if (condition)
            {
                return Or(propName, op, value);
            }

            return this;
        }

        public PredicateBuilder<T> And(Expression<Func<T, bool>> expr2)
        {
            if (CurrentExpression == null)
            {
                CurrentExpression = ExpressionExtensions.ChangeParameter<T>(expr2, Parameter);
                return this;
            }

            CurrentExpression = ExpressionExtensions.And<T>(CurrentExpression, expr2);
            return this;
        }

        public PredicateBuilder<T> AndIf(bool condition, Expression<Func<T, bool>> expr2, Expression<Func<T, bool>> elseExp = null)
        {
            if (condition)
            {
                return And(expr2);
            }
            else
            {
                if (elseExp != null)
                {
                    return And(elseExp);
                }
            }

            return this;
        }

        public PredicateBuilder<T> Or(Expression<Func<T, bool>> expr2)
        {
            CurrentExpression = ExpressionExtensions.Or<T>(CurrentExpression, expr2);
            return this;
        }

        public PredicateBuilder<T> OrIf(bool condition, Expression<Func<T, bool>> expr2, Expression<Func<T, bool>> elseExp = null)
        {
            if (condition)
            {
                return Or(expr2);
            }
            else
            {
                if (elseExp != null)
                {
                    return Or(elseExp);
                }
            }

            return this;
        }

        public Expression<Func<T, bool>> Build()
        {
            return CurrentExpression;
        }
    }
}
