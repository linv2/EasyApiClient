using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyApiClient.Attributes.MethodParameter
{
    [AttributeUsage(AttributeTargets.Parameter,
        Inherited = false,AllowMultiple =false)]
    public class QueryAttribute : ParameterAttribute
    {
        public QueryAttribute(string alias = null) : base(alias)
        {
        }
    }
}
