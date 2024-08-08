using EasyApiClient;
using EasyApiClient.Attributes;
using EasyApiClient.Attributes.HttpVerbs;
using EasyApiClient.Attributes.MethodParameter;

namespace EasyApiClient.Console
{
    public interface IHttpBin : IHttp
    {

        [HttpGet("https://httpbin.org/get")]
        string TestGet([Query] int id, [Query] string name);

        [HttpGet("https://httpbin.org/get?id={id}&name={name}")]
        string TestGet2([Route] int id, [Route] string name);

        [HttpPost("https://httpbin.org/post")]
        string TestPost([Query] int id, [Query] string name, [FormBody] string postParam1, [FormBody] string postParam2);

    }
}
