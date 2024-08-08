using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyApiClient.Attributes.HttpVerbs
{
    public class HttpPut : RequestAttribute
    {
        public HttpPut(string url) : base(url)
        {
            base.HttpMethod = HttpHeaders.HttpMethod.POST;
        }
    }
}