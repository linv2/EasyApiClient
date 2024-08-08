using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyApiClient.Exceptions
{
    public class EasyApiClientException : Exception
    {
        public EasyApiClientException(string message) : base(message) { }
    }
}
