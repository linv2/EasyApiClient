using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyApiClient.Attributes.MethodParameter
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class FormBodyAttribute : ParameterAttribute
    {
        public FormBodyAttribute(string alias = null) : base(alias)
        {
        }
    }
}
