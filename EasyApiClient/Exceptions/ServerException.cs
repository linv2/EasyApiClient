using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyApiClient.Exceptions
{
    public class ServerException : EasyApiClientException
    {
        public ServerException(string message) : base(message)
        {
        }
    }
}
