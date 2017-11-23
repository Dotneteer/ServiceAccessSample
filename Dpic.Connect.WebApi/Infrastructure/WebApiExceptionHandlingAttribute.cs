using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using Dpic.Connect.SharedDefinitions.Exceptions;
using Newtonsoft.Json;

namespace Dpic.Connect.WebApi.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class WebApiExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Raises the exception event.
        /// </summary>
        /// <param name="context">The context for the action.</param>
        public override void OnException(HttpActionExecutedContext context)
        {
            var exception = context.Exception;

            // --- Special conversion from Seemplest ArgumentValidationException
            var portableEx = exception as PortableExceptionBase;
            if (portableEx != null)
            {
                // --- This is a business issue
                var info = new PortableExceptionInfoDto
                {
                    ExceptionType = portableEx.GetType().AssemblyQualifiedName,
                    IsBusiness = true,
                    Message = portableEx.Message,
                    ExceptionProps = PortableExceptionConverter.Serialize(portableEx)
                };
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(info))
                });
            }

            // --- This is an infrastructure issue
            var infraInfo = new PortableExceptionInfoDto
            {
                ExceptionType = context.Exception.GetType().AssemblyQualifiedName,
                IsBusiness = false,
                Message = context.Exception.Message
            };
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(JsonConvert.SerializeObject(infraInfo))
            });
        }
    }
}