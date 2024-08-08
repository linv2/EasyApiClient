using EasyApiClient.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EasyApiClient
{
    public class Http
    {
        public static IProxy Create<IProxy>() where IProxy : IHttp
        {
            return DispatchProxy.Create<IProxy, HttpProxyFactory<IProxy, DefaultSerializationer>>();
        }
    }
}
