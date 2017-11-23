using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using Dpic.Connect.SharedDefinitions.Exceptions;
using Newtonsoft.Json;

namespace Dpic.Connect.ServiceAccess.Core
{
    /// <summary>
    /// This class implements the common functionality to be used by service proxies
    /// to access backend REST services
    /// </summary>
    public abstract class ServiceProxyBase
    {
        /// <summary>
        /// Optional base URI
        /// </summary>
        public string BaseUri { get; }

        /// <summary>
        /// Creates the proxy with the specified base URI
        /// </summary>
        /// <param name="baseUri">The base URI of the API</param>
        protected ServiceProxyBase(string baseUri = null)
        {
            BaseUri = baseUri;
        }

        /// <summary>
        /// Executes a GET request
        /// </summary>
        /// <typeparam name="TResult">Type of the result</typeparam>
        /// <param name="resource">The request URI</param>
        /// <param name="args">Request arguments</param>
        /// <returns>Response data</returns>
        protected async Task<IServiceProxyResponse<TResult>> Get<TResult>(string resource,
            Dictionary<string, object> args = null)
        {
            using (var client = CreateHttpClient())
            {
                var uri = CreateUri(resource, args);
                var request = new HttpRequestMessage();
                request.Headers.Add("Accept-Encoding", "gzip,deflate");
                var response = await InvokeBackend(client.GetAsync(uri));
                using (response)
                {
                    var proxyResponse = new ServiceProxyResponse<TResult>(response);
                    await proxyResponse.ParseContent();
                    CheckFault(proxyResponse);
                    return proxyResponse;
                }
            }
        }

        /// <summary>
        /// Executes a POST request
        /// </summary>
        /// <typeparam name="TArg">Type of request body</typeparam>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="resource">Request URI</param>
        /// <param name="bodyArg">Request body</param>
        /// <param name="args">Request arguments</param>
        /// <returns>Response data</returns>
        protected async Task<IServiceProxyResponse<TResult>> Post<TArg, TResult>(string resource, TArg bodyArg, Dictionary<string, object> args = null)
        {
            using (var client = CreateHttpClient())
            {
                var uri = CreateUri(resource, args);
                var request = new HttpRequestMessage();
                request.Headers.Add("Accept-Encoding", "gzip,deflate");
                using (var response = await InvokeBackend(client.PostAsync(uri, CreateJsonContent(bodyArg))))
                {
                    var proxyResponse = new ServiceProxyResponse<TResult>(response);
                    await proxyResponse.ParseContent();
                    CheckFault(proxyResponse);
                    return proxyResponse;
                }
            }
        }

        /// <summary>
        /// Executes a POST request with now response data
        /// </summary>
        /// <typeparam name="TArg">Type of request body</typeparam>
        /// <param name="resource">Request URI</param>
        /// <param name="bodyArg">Request body</param>
        /// <param name="args">Request arguments</param>
        /// <returns>Response data</returns>
        protected async Task<IServiceProxyResponse> Post<TArg>(string resource, TArg bodyArg, Dictionary<string, object> args = null)
        {
            using (var client = CreateHttpClient())
            {
                var uri = CreateUri(resource, args);
                var request = new HttpRequestMessage();
                request.Headers.Add("Accept-Encoding", "gzip,deflate");
                using (var response = await InvokeBackend(client.PostAsync(uri, CreateJsonContent(bodyArg))))
                {
                    var proxyResponse = new ServiceProxyResponse(response);
                    await proxyResponse.GetContent();
                    CheckFault(proxyResponse);
                    return proxyResponse;
                }
            }
        }

        /// <summary>
        /// Executes a POST request with no request and response data
        /// </summary>
        /// <param name="resource">Request URI</param>
        /// <param name="args">Request arguments</param>
        /// <returns>Response data</returns>
        protected async Task<IServiceProxyResponse> Post(string resource, Dictionary<string, object> args = null)
        {
            using (var client = CreateHttpClient())
            {
                var uri = CreateUri(resource, args);
                var request = new HttpRequestMessage();
                request.Headers.Add("Accept-Encoding", "gzip,deflate");
                using (var response = await InvokeBackend(client.PostAsync(uri, new StringContent(string.Empty))))
                {
                    var proxyResponse = new ServiceProxyResponse(response);
                    await proxyResponse.GetContent();
                    CheckFault(proxyResponse);
                    return proxyResponse;
                }
            }
        }

        /// <summary>
        /// Executes a POST request with no request data
        /// </summary>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="resource">Request URI</param>
        /// <param name="args">Request arguments</param>
        /// <returns>Response data</returns>
        protected async Task<IServiceProxyResponse<TResult>> Post<TResult>(string resource, Dictionary<string, object> args = null)
        {
            using (var client = CreateHttpClient())
            {
                var uri = CreateUri(resource, args);
                var request = new HttpRequestMessage();
                request.Headers.Add("Accept-Encoding", "gzip,deflate");
                using (var response = await InvokeBackend(client.PostAsync(uri, new StringContent(string.Empty))))
                {
                    var proxyResponse = new ServiceProxyResponse<TResult>(response);
                    await proxyResponse.ParseContent();
                    CheckFault(proxyResponse);
                    return proxyResponse;
                }
            }
        }

        /// <summary>
        /// Executes a PUT request
        /// </summary>
        /// <typeparam name="TArg">Type of request body</typeparam>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="resource">Request URI</param>
        /// <param name="bodyArg">Request body</param>
        /// <param name="args">Request arguments</param>
        /// <returns>Response data</returns>
        protected async Task<IServiceProxyResponse<TResult>> Put<TArg, TResult>(string resource, TArg bodyArg, Dictionary<string, object> args = null)
        {
            using (var client = CreateHttpClient())
            {
                var uri = CreateUri(resource, args);
                var request = new HttpRequestMessage();
                request.Headers.Add("Accept-Encoding", "gzip,deflate");
                using (var response = await InvokeBackend(client.PutAsync(uri, CreateJsonContent(bodyArg))))
                {
                    var proxyResponse = new ServiceProxyResponse<TResult>(response);
                    await proxyResponse.ParseContent();
                    CheckFault(proxyResponse);
                    return proxyResponse;
                }
            }
        }

        /// <summary>
        /// Executes a PUT request with no response data
        /// </summary>
        /// <typeparam name="TArg">Type of request body</typeparam>
        /// <param name="resource">Request URI</param>
        /// <param name="bodyArg">Request body</param>
        /// <param name="args">Request arguments</param>
        /// <returns>Response data</returns>
        protected async Task<IServiceProxyResponse> Put<TArg>(string resource, TArg bodyArg, Dictionary<string, object> args = null)
        {
            using (var client = CreateHttpClient())
            {
                var uri = CreateUri(resource, args);
                var request = new HttpRequestMessage();
                request.Headers.Add("Accept-Encoding", "gzip,deflate");
                using (var response = await InvokeBackend(client.PutAsync(uri, CreateJsonContent(bodyArg))))
                {
                    var proxyResponse = new ServiceProxyResponse(response);
                    await proxyResponse.GetContent();
                    CheckFault(proxyResponse);
                    return proxyResponse;
                }
            }
        }

        /// <summary>
        /// Executes a PUT request with no request and response data
        /// </summary>
        /// <param name="resource">Request URI</param>
        /// <param name="args">Request arguments</param>
        /// <returns>Response data</returns>
        protected async Task<IServiceProxyResponse> Put(string resource, Dictionary<string, object> args = null)
        {
            using (var client = CreateHttpClient())
            {
                var uri = CreateUri(resource, args);
                var request = new HttpRequestMessage();
                request.Headers.Add("Accept-Encoding", "gzip,deflate");
                using (var response = await InvokeBackend(client.PutAsync(uri, new StringContent(string.Empty))))
                {
                    var proxyResponse = new ServiceProxyResponse(response);
                    await proxyResponse.GetContent();
                    CheckFault(proxyResponse);
                    return proxyResponse;
                }
            }
        }

        /// <summary>
        /// Executes a PUT request with no response data
        /// </summary>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="resource">Request URI</param>
        /// <param name="args">Request arguments</param>
        /// <returns>Response data</returns>
        protected async Task<IServiceProxyResponse<TResult>> Put<TResult>(string resource, Dictionary<string, object> args = null)
        {
            using (var client = CreateHttpClient())
            {
                var uri = CreateUri(resource, args);
                var request = new HttpRequestMessage();
                request.Headers.Add("Accept-Encoding", "gzip,deflate");
                using (var response = await InvokeBackend(client.PutAsync(uri, new StringContent(string.Empty))))
                {
                    var proxyResponse = new ServiceProxyResponse<TResult>(response);
                    await proxyResponse.ParseContent();
                    CheckFault(proxyResponse);
                    return proxyResponse;
                }
            }
        }

        /// <summary>
        /// Executes a DELETE request with no request and response data
        /// </summary>
        /// <param name="resource">Request URI</param>
        /// <param name="args">Request arguments</param>
        /// <returns>Response data</returns>
        protected async Task<IServiceProxyResponse> Delete(string resource, Dictionary<string, object> args = null)
        {
            using (var client = CreateHttpClient())
            {
                var uri = CreateUri(resource, args);
                var request = new HttpRequestMessage();
                request.Headers.Add("Accept-Encoding", "gzip,deflate");
                using (var response = await InvokeBackend(client.DeleteAsync(uri)))
                {
                    var proxyResponse = new ServiceProxyResponse(response);
                    await proxyResponse.GetContent();
                    CheckFault(proxyResponse);
                    return proxyResponse;
                }
            }
        }

        /// <summary>
        /// Executes a DELETE request with no request data
        /// </summary>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="resource">Request URI</param>
        /// <param name="args">Request arguments</param>
        /// <returns>Response data</returns>
        protected async Task<IServiceProxyResponse<TResult>> Delete<TResult>(string resource, Dictionary<string, object> args = null)
        {
            using (var client = CreateHttpClient())
            {
                var uri = CreateUri(resource, args);
                var request = new HttpRequestMessage();
                request.Headers.Add("Accept-Encoding", "gzip,deflate");
                using (var response = await InvokeBackend(client.DeleteAsync(uri)))
                {
                    var proxyResponse = new ServiceProxyResponse<TResult>(response);
                    await proxyResponse.ParseContent();
                    CheckFault(proxyResponse);
                    return proxyResponse;
                }
            }
        }

        /// <summary>
        /// Létrehozza a kérés elküldését végző HttpClient példányt
        /// </summary>
        /// <returns>A kérést kezelő HttpClient példány</returns>
        protected virtual HttpClient CreateHttpClient()
        {
            return new HttpClient();
        }


        /// <summary>
        /// Létrehoz egy URI-t az átadott REST <paramref name="resource"/>-ból az 
        /// <paramref name="args"/> helyettesítésekkel
        /// </summary>
        /// <param name="resource">URI resource minta</param>
        /// <param name="args">A paraméterek helyettesítési értéke</param>
        /// <returns></returns>
        private Uri CreateUri(string resource, Dictionary<string, object> args = null)
        {

            // --- Prepare the eintire URI
            var uri = resource;
            if (args != null)
            {
                foreach (var key in args.Keys)
                {
                    uri = uri.Replace("{" + key + "}", args[key].ToString());
                }
            }
            return new Uri(new Uri(BaseUri ?? ""), uri);
        }

        /// <summary>
        /// Checks whether the response is an error. Provided it is, this method classifies these issues
        /// </summary>
        /// <param name="response">Response object</param>
        protected void CheckFault(IServiceProxyResponse response)
        {
            if (response.IsSuccess) return;
            PortableExceptionBase backendException;
            try
            {
                var portable = JsonConvert.DeserializeObject<PortableExceptionInfoDto>(response.Content);
                if (portable.IsBusiness)
                {
                    var type = Type.GetType(portable.ExceptionType);
                    var typeInfo = type.GetTypeInfo();
                    backendException = (PortableExceptionBase)PortableExceptionConverter
                        .Deserialize(portable.ExceptionProps, typeInfo);
                }
                else
                {
                    backendException = new BackendInfrastructureException(portable.Message);
                }
            }
            catch (Exception)
            {
                throw new BackendInfrastructureException("Invalid exception explanation received from the backend.");
            }
            throw backendException;
        }

        /// <summary>
        /// Executes the backend call
        /// </summary>
        /// <param name="invokeTask">The task to execute</param>
        protected virtual async Task<HttpResponseMessage> InvokeBackend(Task<HttpResponseMessage> invokeTask)
        {
            // --- Later we can add retries here
            await invokeTask;
            return invokeTask.Result;
        }

        /// <summary>
        /// Creates JSON content from the body
        /// </summary>
        /// <typeparam name="TArg">Type of request</typeparam>
        /// <param name="bodyArg">Request body</param>
        /// <returns>JSON content</returns>
        private static StringContent CreateJsonContent<TArg>(TArg bodyArg)
        {
            var body = bodyArg == null ? string.Empty : JsonConvert.SerializeObject(bodyArg);
            var content = new StringContent(body);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }
    }
}