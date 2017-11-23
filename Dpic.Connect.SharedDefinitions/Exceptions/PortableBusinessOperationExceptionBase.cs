namespace Dpic.Connect.SharedDefinitions.Exceptions
{
    /// <summary>
    /// This exception is intended to be the base class of all business exceptions.
    /// </summary>
    public abstract class PortableBusinessOperationExceptionBase : PortableExceptionBase
    {
        /// <summary>
        /// Instantiates an empty exception
        /// </summary>
        protected PortableBusinessOperationExceptionBase()
        {
        }

        /// <summary>
        /// Instantiates an exception with the specified message
        /// </summary>
        /// <param name="code">Exception code</param>
        /// <param name="message">Exception message</param>
        protected PortableBusinessOperationExceptionBase(string code, string message) : base(code, message)
        {
        }
    }
}