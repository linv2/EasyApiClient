using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyApiClient.Attributes.HttpVerbs
{
    public class HttpDelete : RequestAttribute
    {
        public HttpDelete(string url) : base(url)
        {
            base.HttpMethod = HttpHeaders.HttpMethod.POST;
        }
    }
}
