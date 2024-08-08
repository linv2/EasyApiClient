using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyApiClient.Attributes.HttpVerbs
{
    public class HttpGet : RequestAttribute
    {
        public HttpGet(string url) : base(url)
        {
            base.HttpMethod = HttpHeaders.HttpMethod.Get;
        }
    }
}
