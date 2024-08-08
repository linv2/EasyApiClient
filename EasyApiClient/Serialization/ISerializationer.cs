using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyApiClient.Serialization
{
    public interface ISerializationer
    {
        object DeserializeObject(byte[] bytes, Type type);
        string SerializeObject(object value);
    }
}
