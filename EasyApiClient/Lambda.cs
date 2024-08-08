using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyApiClient
{
    public class Lambda
    {

        private static readonly ConcurrentDictionary<string, Func<object>> _constructorDict = new ConcurrentDictionary<string, Func<object>>();
        public static TInstance CreateInstance<TInstance>(Type type)
        {
            if (!typeof(TInstance).IsAssignableFrom(type))
            {
                throw new InvalidCastException("类型不匹配");
            }
            var newLambda = _constructorDict.GetOrAdd(type.FullName, (key) =>
               {
                   var lambda = Expression.Lambda<Func<object>>(Expression.New(type)).Compile();
                   return lambda;
               });
            var instance = newLambda();
            return (TInstance)newLambda();
        }
    }
}
