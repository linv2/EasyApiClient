using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyApiClient.Attributes.MethodParameter
{

    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public abstract class ParameterAttribute : Attribute
    {
        /// <summary>
        /// alias name
        /// </summary>
        public string Alias { get; private set; }
        public ParameterAttribute(string alias)
        {
            Alias = alias;
        }
    }
}
