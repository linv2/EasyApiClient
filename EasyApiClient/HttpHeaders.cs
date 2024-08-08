using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyApiClient
{
    public class HttpHeaders
    {
        public class ContentType
        {
            public const string Json = "application/json";
            public const string Xml = "application/xml";
            public const string Form = "application/x-www-form-urlencoded";
            public const string File = "multipart/form-data";
        }
        public enum HttpMethod
        {
            Get,
            POST,
            PUT,
            DELETE
        }
    }
}
