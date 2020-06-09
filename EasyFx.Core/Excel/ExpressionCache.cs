using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace EasyFx.Core.Excel
{
    public class ExpressionCache<T> where T : class
    {
        static ExpressionCache()
        {
            Func = GenerateFunc();
        }
        private static Func<T,Hashtable> Func;

        private static readonly object lockObj=new object();

        public static Hashtable GetColumnValues(T t)
        {
            if (Func == null)
            {
                lock (lockObj)
                {
                    if (Func == null)
                    {
                        Func = GenerateFunc();
                    }
                }
            }
            return Func.Invoke(t);
        }

        private static Func<T, Hashtable> GenerateFunc()
        {
            Type tType = typeof(T);
            Type hashType = typeof(Hashtable);
            LabelTarget returnTarget = Expression.Label(hashType);
            LabelExpression labelExpression = Expression.Label(returnTarget, Expression.Default(hashType));
            ParameterExpression parameter = Expression.Parameter(tType);
            NewExpression newExp = Expression.New(hashType);
            ParameterExpression variate = Expression.Parameter(hashType, "instance");
            List<Expression> expressions = new List<Expression>();
            BinaryExpression assVar = Expression.Assign(variate, newExp);
            expressions.Add(assVar);
            MethodInfo method = hashType.GetMethod("Add", new Type[] { typeof(string), typeof(object) });

            foreach (var prop in tType.GetProperties())
            {
                expressions.Add(Expression.Call(variate, method, Expression.Constant(prop.Name),
                    Expression.Convert(Expression.Property(parameter, prop.Name), typeof(object))));
            }
            expressions.Add(Expression.Return(returnTarget, variate));
            expressions.Add(labelExpression);
            BlockExpression block = Expression.Block(new[] { variate }, expressions);
            var lambda = Expression.Lambda<Func<T, Hashtable>>(block, parameter);
            return lambda.Compile();
        }
    }
}