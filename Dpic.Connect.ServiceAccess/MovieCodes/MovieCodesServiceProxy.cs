using System.Collections.Generic;
using System.Threading.Tasks;
using Dpic.Connect.ServiceAccess.Core;
using Dpic.Connect.SharedDefinitions.MovieCodes;

namespace Dpic.Connect.ServiceAccess.MovieCodes
{
    public class MovieCodesServiceProxy: ServiceProxyBase, IMovieCodesService
    {
        /// <summary>
        /// Creates the proxy with the specified base URI
        /// </summary>
        /// <param name="baseUri">The base URI of the API</param>
        public MovieCodesServiceProxy(string baseUri = null) : base(baseUri)
        {
        }

        /// <summary>
        /// Retrieves all movie codes
        /// </summary>
        public async Task<List<MovieCode>> GetAllMovieCodes()
        {
            var result = await Get<List<MovieCode>>("moviecodes");
            return result.Data;
        }

        /// <summary>
        /// Retrieves active movie codes
        /// </summary>
        public async Task<List<MovieCode>> GetActiveMovieCodes()
        {
            var result = await Get<List<MovieCode>>("moviecodes/active");
            return result.Data;
        }

        /// <summary>
        /// Retrieves the specified movie code
        /// </summary>
        /// <param name="id">Movie code ID</param>
        public async Task<MovieCode> GetMovieCodeById(int id)
        {
            var result = await Get<MovieCode>("moviecodes/{id}",
                new Dictionary<string, object>
                {
                    {"id", id}
                });
            return result.Data;
        }

        /// <summary>
        /// Raises an infrastructure exception for demonstration
        /// purposes
        /// </summary>
        public async Task<MovieCode> MakeInfraException()
        {
            var result = await Get<MovieCode>("moviecodes/makeex");
            return result.Data;
        }
    }
}