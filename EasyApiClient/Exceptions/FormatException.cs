using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyApiClient.Exceptions
{
    public class FormatException : EasyApiClientException
    {
        public FormatException(string message) : base(message)
        {
        }
    }
}
