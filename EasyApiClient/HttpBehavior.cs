using EasyApiClient.Attributes.HttpVerbs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyApiClient
{
    public class HttpBehavior
    {
        public HttpBehavior(string sourceUrl)
        {
            SourceUrl = sourceUrl;
        }
        public HttpHeaders.HttpMethod HttpMethod { get; set; }
        public string SourceUrl { get; set; }

        public string ContentType { get; set; }
        /// <summary>
        /// 失败重试
        /// </summary>
        public int FailRetry { get; set; }
        public Dictionary<string, string> Header { get; set; } = new Dictionary<string, string>();
        public IList<HttpParameter> HttpParameter { get; set; } = new List<HttpParameter>();

        public Type ReturnType { get; set; }
    }
}
