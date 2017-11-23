using Dpic.Connect.SharedDefinitions.Exceptions.Bwp.Core.PortableDefinitions.Exceptions;

namespace Dpic.Connect.SharedDefinitions.Exceptions
{
    /// <summary>
    /// This class represenst az infrasturcture exception
    /// </summary>
    public class BackendInfrastructureException : PortableExceptionBase
    {
        /// <summary>
        /// Instantiates an empty exception
        /// </summary>
        public BackendInfrastructureException() :
            base(PortableExceptionCodes.INFRASTRUCTURE, "A backend exception occurred.")
        {
        }

        /// <summary>
        /// Instantiates an exception with the specified message
        /// </summary>
        /// <param name="message">Exception message</param>
        public BackendInfrastructureException(string message) :
            base(PortableExceptionCodes.INFRASTRUCTURE, message)
        {
        }
    }
}