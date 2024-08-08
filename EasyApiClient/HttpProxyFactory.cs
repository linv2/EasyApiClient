using EasyApiClient.Attributes;
using EasyApiClient.Attributes.HttpVerbs;
using EasyApiClient.Attributes.MethodParameter;
using EasyApiClient.Exceptions;
using EasyApiClient.Serialization;
using RestSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using FormatException = EasyApiClient.Exceptions.FormatException;

namespace EasyApiClient
{
    internal class HttpProxyFactory<TProxy, TSerialization> : DispatchProxy
     where TSerialization : ISerializationer, new()
        where TProxy : IHttp
    {
        private readonly Type _infaceType;
        private readonly RestClient _restClient;
        private readonly ConcurrentDictionary<string, HttpBehavior> _httpDict = new ConcurrentDictionary<string, HttpBehavior>();
        private readonly ISerializationer _serializationer;
        public HttpProxyFactory()
        {
            _infaceType = typeof(TProxy);
            _restClient = new RestClient();
            var headers = _infaceType.GetCustomAttributes<HeaderAttribute>();
            foreach (var header in headers)
            {
                _restClient.AddDefaultHeader(header.Name, header.Value);
            }
            var converterAttribute = _infaceType.GetCustomAttribute<ConverterAttribute>();
            _serializationer = Lambda.CreateInstance<ISerializationer>(converterAttribute?.ConverterType ?? typeof(TSerialization));
        }
        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {

            if (targetMethod == null)
            {
                return null;
            }
            var httpBehavior = _httpDict.GetOrAdd(targetMethod.Name, CreateHttpBehavior, targetMethod);
            if (httpBehavior == null)
            {
                return null;
            }
            var resource = BuildRequestUrl(httpBehavior, args);
            var restRequest = new RestRequest(resource, (Method)Enum.Parse(typeof(Method), httpBehavior.HttpMethod.ToString(), true));
            foreach (var headerItem in httpBehavior.Header)
            {
                restRequest.AddHeader(headerItem.Key, headerItem.Value);
            }
            BuildPayload(httpBehavior, args, restRequest);

            var retryNumber = httpBehavior.FailRetry;
            do
            {
                var restResponse = _restClient.Execute(restRequest);
                if (restResponse.IsSuccessful)
                {
                    try
                    {
                        if (httpBehavior.ReturnType.IsValueType)
                        {
                            return Convert.ChangeType(restResponse.Content, httpBehavior.ReturnType);
                        }
                        else if (httpBehavior.ReturnType.FullName.Equals("System.String"))
                        {
                            return restResponse.Content;
                        }
                        else
                        {
                            return _serializationer.DeserializeObject(restResponse.RawBytes, httpBehavior.ReturnType);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new FormatException(ex.Message);
                    }
                }
                retryNumber--;
            }
            while (retryNumber > 0);
            throw new ServerException("服务端异常，请求失败");
        }



        private string BuildRequestUrl(HttpBehavior httpBehavior, object[] args)
        {
            var routeParams = httpBehavior.HttpParameter.Where(x => x.IsRoute);
            var reg = new Regex(@"\{(.*?)\}");
            //var mat = reg.Matches(webcofnigstring2);

            var routeValue = new ConcurrentDictionary<string, string>();
            foreach (var routeItem in routeParams)
            {
                var value = args.ElementAtOrDefault(routeItem.ParamIndex);
                routeValue[routeItem.Name] = value?.ToString();
            }

            var url = reg.Replace(httpBehavior.SourceUrl,
                new MatchEvaluator(m =>
                    routeValue.TryGetValue(m.Groups[1].Value, out var value) ? m.Value : value
                ));
            var urlBuilder = new UriBuilder(url);
            var queryString = HttpUtility.ParseQueryString(urlBuilder.Query);
            var queryParams = httpBehavior.HttpParameter.Where(x => x.IsQueryString);
            foreach (var queryStringItem in queryParams)
            {
                var value = args.ElementAtOrDefault(queryStringItem.ParamIndex);
                queryString[queryStringItem?.Name] = value?.ToString();
            }
            urlBuilder.Query = string.Join("&", queryString.AllKeys.Select(x => $"{x}={HttpUtility.UrlEncode(queryString[x])}"));
            return urlBuilder.ToString();
        }

        private void BuildPayload(HttpBehavior httpBehavior, object[] args, RestRequest restRequest)
        {
            if (httpBehavior.HttpMethod != HttpHeaders.HttpMethod.Get)
            {
                object payload = null;
                var bodyParams = httpBehavior.HttpParameter.Where(x => x.IsFormBody);
                if (bodyParams.Count() == 1)
                {
                    payload = args[bodyParams.ElementAt(0).ParamIndex];
                }
                else
                {
                    payload = bodyParams.ToDictionary(x => x.Name, x => args[x.ParamIndex]);
                }
                if (payload != null)
                {

                    restRequest.AddBody(_serializationer.SerializeObject(payload));
                }
            }
        }


        private HttpBehavior CreateHttpBehavior(string methodName, MethodInfo targetMethod)
        {

            #region RequestAttribute
            var requestAttribute = targetMethod.GetCustomAttribute<RequestAttribute>(true);
            if (requestAttribute == null)
            {
                throw new MissingAttributeException($"{_infaceType.Name}.{targetMethod.Name}缺少特性RequestAttribute");
            }
            var httpBehavior = new HttpBehavior(requestAttribute.Url);
            httpBehavior.HttpMethod = requestAttribute.HttpMethod;
            httpBehavior.FailRetry = requestAttribute.FailRetry;
            httpBehavior.ContentType = requestAttribute.ContentType;
            httpBehavior.ReturnType = targetMethod.ReturnType;
            #endregion

            #region HeaderAttribute
            var headers = _infaceType.GetCustomAttributes<HeaderAttribute>();
            foreach (var header in headers)
            {
                httpBehavior.Header[header.Name] = header.Value;
            }
            #endregion

            var parameters = targetMethod.GetParameters();
            var index = 0;
            foreach (var param in parameters)
            {
                var httpParam = new HttpParameter(param.Name ?? string.Empty);
                httpParam.ParamIndex = index++;
                var parameterAttribute = param.GetCustomAttribute<ParameterAttribute>();
                if (parameterAttribute != null)
                {
                    httpParam.AliasName = parameterAttribute.Alias;
                }
                if (parameterAttribute is FormBodyAttribute)
                {
                    httpParam.IsFormBody = true;
                }
                else if (parameterAttribute is RouteAttribute)
                {
                    httpParam.IsRoute = true;
                }
                else if (parameterAttribute is QueryAttribute)
                {
                    httpParam.IsQueryString = true;
                }
                httpBehavior.HttpParameter.Add(httpParam);
            }
            return httpBehavior;
        }

    }
}
