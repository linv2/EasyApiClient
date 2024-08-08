using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyApiClient.Attributes.MethodParameter
{
    /// <summary>
    /// for format url 
    /// </summary>
    /// 
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class RouteAttribute : ParameterAttribute
    {
        public RouteAttribute(string alias=null) : base(alias)
        {
        }
    }
}
