using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyApiClient.Exceptions
{
    public class MissingAttributeException : EasyApiClientException
    {
        public MissingAttributeException(string message) : base(message) { }
    }
}
