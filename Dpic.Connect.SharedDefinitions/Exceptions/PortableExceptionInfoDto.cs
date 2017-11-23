using System.Collections.Generic;

namespace Dpic.Connect.SharedDefinitions.Exceptions
{
    /// <summary>
    /// This class holds business exception information
    /// </summary>
    public class PortableExceptionInfoDto
    {
        /// <summary>
        /// Full type name of the exception
        /// </summary>
        public string ExceptionType { get; set; }

        /// <summary>
        /// Is it a business exception?
        /// </summary>
        public bool IsBusiness { get; set; }

        /// <summary>
        /// Exception message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Serialized exception properties
        /// </summary>
        public Dictionary<string, object> ExceptionProps { get; set; }
    }
}