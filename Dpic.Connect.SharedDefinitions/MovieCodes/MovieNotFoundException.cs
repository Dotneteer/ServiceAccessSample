using Dpic.Connect.SharedDefinitions.Exceptions;

namespace Dpic.Connect.SharedDefinitions.MovieCodes
{
    /// <summary>
    /// Represents the business exception when a movie code cannot be found
    /// </summary>
    public class MovieNotFoundException: PortableBusinessOperationExceptionBase
    {
        /// <summary>
        /// Invalid movie code ID
        /// </summary>
        public int WrongId { get; set; }

        /// <summary>
        /// Instantiates an empty exception
        /// </summary>
        /// <remarks>We need this constructor for client-side deserialization</remarks>
        public MovieNotFoundException()
        {
        }

        /// <summary>
        /// Instantiates an empty exception
        /// </summary>
        public MovieNotFoundException(int wrongId):
            base("WRONG_ID", $"Movie code with id {wrongId} not found.")
        {
            WrongId = wrongId;
        }
    }
}