using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dpic.Connect.SharedDefinitions.MovieCodes
{
    /// <summary>
    /// Service for movie codes
    /// </summary>
    public interface IMovieCodesService
    {
        /// <summary>
        /// Retrieves all movie codes
        /// </summary>
        Task<List<MovieCode>> GetAllMovieCodes();

        /// <summary>
        /// Retrieves active movie codes
        /// </summary>
        Task<List<MovieCode>> GetActiveMovieCodes();

        /// <summary>
        /// Retrieves the specified movie code
        /// </summary>
        /// <param name="id">Movie code ID</param>
        Task<MovieCode> GetMovieCodeById(int id);

        /// <summary>
        /// Raises an infrastructure exception for demonstration
        /// purposes
        /// </summary>
        Task<MovieCode> MakeInfraException();
    }
}