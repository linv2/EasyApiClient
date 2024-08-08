using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyApiClient.Serialization
{
    public class DefaultSerializationer : ISerializationer
    {
        public DefaultSerializationer() { }
        public object DeserializeObject(byte[] bytes, Type type)
        {
            var strValue = Encoding.UTF8.GetString(bytes);
            return JsonConvert.DeserializeObject(strValue, type);
        }


        public string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}
