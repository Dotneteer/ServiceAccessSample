using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Dpic.Connect.ServiceAccess.Core
{
    /// <summary>
    /// This interface represents a response coming back from the backend
    /// through a service proxy
    /// </summary>
    public interface IServiceProxyResponse
    {
        /// <summary>
        /// The request sent
        /// </summary>
        HttpRequestMessage Request { get; }

        /// <summary>
        /// Indicates if the status shows success
        /// </summary>
        bool IsSuccess { get; }

        /// <summary>
        /// Response status code
        /// </summary>
        HttpStatusCode StatusCode { get; }

        /// <summary>
        /// The response content in string form
        /// </summary>
        string Content { get; }
    }

    /// <summary>
    /// This interface represents a response coming back from the backend
    /// through the service proxy -- with a typed result
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IServiceProxyResponse<out TResult> : IServiceProxyResponse
    {
        /// <summary>
        /// Indicates if the content parsing failed
        /// </summary>
        bool ParsingFailed { get; }

        /// <summary>
        /// The parsed data
        /// </summary>
        TResult Data { get; }
    }

    /// <summary>
    /// Represents the response coming from the response
    /// </summary>
    public class ServiceProxyResponse : IServiceProxyResponse
    {
        protected readonly HttpResponseMessage Response;

        /// <summary>
        /// The request sent
        /// </summary>
        public HttpRequestMessage Request { get; }

        /// <summary>
        /// Indicates if the status shows success
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Response status code
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// The response content in string form
        /// </summary>
        public string Content { get; protected set; }

        public ServiceProxyResponse(HttpResponseMessage message)
        {
            Response = message;
            Request = message.RequestMessage;
            IsSuccess = message.IsSuccessStatusCode;
            StatusCode = message.StatusCode;
            Content = null;
        }

        /// <summary>
        /// Parses the response and converts it to result
        /// </summary>
        /// <returns></returns>
        public async Task GetContent()
        {
            Content = await Response.Content.ReadAsStringAsync();
        }

    }

    /// <summary>
    /// Represents the response coming from the response
    /// </summary>
    /// <typeparam name="TResult">Type of the content</typeparam>
    public class ServiceProxyResponse<TResult> : ServiceProxyResponse,
        IServiceProxyResponse<TResult>
    {
        /// <summary>
        /// The parsed data
        /// </summary>
        public TResult Data { get; private set; }

        /// <summary>
        /// Indicates if the content parsing failed
        /// </summary>
        public bool ParsingFailed { get; private set; }


        public ServiceProxyResponse(HttpResponseMessage message) : base(message)
        {
            Data = default(TResult);
        }

        /// <summary>
        /// Parses the response and converts it to result
        /// </summary>
        /// <returns></returns>
        public async Task ParseContent()
        {
            await GetContent();
            if (!IsSuccess) return;
            try
            {
                Data = JsonConvert.DeserializeObject<TResult>(Content);
                ParsingFailed = false;
            }
            catch
            {
                ParsingFailed = true;
            }
        }
    }
}