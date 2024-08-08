using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyApiClient.Attributes.HttpVerbs
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RequestAttribute : Attribute
    {
        public HttpHeaders.HttpMethod HttpMethod { get; set; } = HttpHeaders.HttpMethod.Get;
        public string ContentType { get; set; }
        public string Url { get; }
        /// <summary>
        /// 失败重试
        /// </summary>
        public int FailRetry { get; set; } = 0;
        public RequestAttribute(string url)
        {
            Url = url;
        }
    }
}
