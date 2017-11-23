using System;

namespace Dpic.Connect.SharedDefinitions.Exceptions
{
    /// <summary>
    /// This class in intended to be the base class to all portable exceptions that can be
    /// serialized/deserialized from the backend to the client side
    /// </summary>
    public abstract class PortableExceptionBase : Exception
    {
        /// <summary>
        /// This exception code allows the client to use localized
        /// exception messages
        /// </summary>
        public string ExceptionCode { get; set; }

        /// <summary>
        /// Exception message
        /// </summary>
        public new string Message { get; set; }

        /// <summary>
        /// This property retrieves the arguments of the exception that can 
        /// be put into cleint error messages
        /// </summary>
        public virtual object[] MessageArguments => new object[0];

        /// <summary>
        /// Instantiates an empty exception
        /// </summary>
        protected PortableExceptionBase()
        {
        }

        /// <summary>
        /// Instantiates an exception with the specified message
        /// </summary>
        /// <param name="code">Exception code</param>
        /// <param name="message">Exception message</param>
        protected PortableExceptionBase(string code, string message)
        {
            ExceptionCode = code;
            Message = message;
        }
    }
}