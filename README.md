# EasyApiClient
a easy to invoke web api c# client
## definition
```
    public interface IHttpBin : IHttp
    {

        [HttpGet("https://httpbin.org/get")]
        string TestGet([Query] int id, [Query] string name);

        [HttpGet("https://httpbin.org/get?id={id}&name={name}")]
        string TestGet2([Route] int id, [Route] string name);

        [HttpPost("https://httpbin.org/post")]
        string TestPost([Query] int id, [Query] string name, [FormBody] string postParam1, [FormBody] string postParam2);

    }
```

## invoke
```
      // See https://aka.ms/new-console-template for more information
      
      using EasyApiClient;
      using EasyApiClient.Console;
      
      var httpBin = Http.Create<IHttpBin>();
      
      Console.WriteLine(httpBin.TestGet(1, "A"));
      Console.WriteLine("--------------------------");
      
      Console.WriteLine(httpBin.TestGet(2, "b"));
      Console.WriteLine("--------------------------");
      
      
      Console.WriteLine(httpBin.TestPost(3, "c","param1","param2"));
      Console.WriteLine("--------------------------");
      Console.ReadKey();

```

## output
```
{
  "args": {
    "id": "1",
    "name": "A"
  },
  "headers": {
    "Accept": "application/json, text/json, text/x-json, text/javascript, application/xml, text/xml",
    "Accept-Encoding": "gzip, deflate, br",
    "Host": "httpbin.org",
    "User-Agent": "RestSharp/111.4.1.0",
    "X-Amzn-Trace-Id": "Root=1-66b45558-37b1a5a00172babd55572718"
  },
  "origin": "104.28.225.125",
  "url": "https://httpbin.org/get?id=1&name=A"
}

--------------------------
{
  "args": {
    "id": "2",
    "name": "b"
  },
  "headers": {
    "Accept": "application/json, text/json, text/x-json, text/javascript, application/xml, text/xml",
    "Accept-Encoding": "gzip, deflate, br",
    "Host": "httpbin.org",
    "User-Agent": "RestSharp/111.4.1.0",
    "X-Amzn-Trace-Id": "Root=1-66b45558-3573a9aa236301823ebffd49"
  },
  "origin": "104.28.225.125",
  "url": "https://httpbin.org/get?id=2&name=b"
}

--------------------------
{
  "args": {
    "id": "3",
    "name": "c"
  },
  "data": "{\"postParam1\":\"param1\",\"postParam2\":\"param2\"}",
  "files": {},
  "form": {},
  "headers": {
    "Accept": "application/json, text/json, text/x-json, text/javascript, application/xml, text/xml",
    "Accept-Encoding": "gzip, deflate, br",
    "Content-Length": "45",
    "Content-Type": "application/json; charset=utf-8",
    "Host": "httpbin.org",
    "User-Agent": "RestSharp/111.4.1.0",
    "X-Amzn-Trace-Id": "Root=1-66b45559-112f23b8073656db0e7bac23"
  },
  "json": {
    "postParam1": "param1",
    "postParam2": "param2"
  },
  "origin": "104.28.225.125",
  "url": "https://httpbin.org/post?id=3&name=c"
}

--------------------------

```
