using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dpic.Connect.SharedDefinitions.MovieCodes;

namespace Dpic.Connect.Services.MovieCodes
{
    /// <summary>
    /// Implements the movie code service
    /// </summary>
    public class MovieCodeService: IMovieCodesService
    {
        private static readonly List<MovieCode> s_MovieCodes = new List<MovieCode>
        {
            new MovieCode { Id = 1, DisplayName = "First", Active = true },
            new MovieCode { Id = 2, DisplayName = "Second", Active = true },
            new MovieCode { Id = 3, DisplayName = "Third", Active = false },
            new MovieCode { Id = 4, DisplayName = "Fourth", Active = true },
            new MovieCode { Id = 5, DisplayName = "Fifth", Active = false },
            new MovieCode { Id = 6, DisplayName = "Sixth", Active = true },
        };

        /// <summary>
        /// Retrieves all movie codes
        /// </summary>
        public Task<List<MovieCode>> GetAllMovieCodes()
        {
            return Task.FromResult(s_MovieCodes);
        }

        /// <summary>
        /// Retrieves active movie codes
        /// </summary>
        public Task<List<MovieCode>> GetActiveMovieCodes()
        {
            return Task.FromResult(s_MovieCodes.Where(mc => mc.Active).ToList());
        }

        /// <summary>
        /// Retrieves the specified movie code
        /// </summary>
        /// <param name="id">Movie code ID</param>
        public Task<MovieCode> GetMovieCodeById(int id)
        {
            var movieCode = s_MovieCodes.FirstOrDefault(mc => mc.Id == id);
            if (movieCode != null)
            {
                return Task.FromResult(movieCode);
            }
            throw new MovieNotFoundException(id);
        }

        /// <summary>
        /// Raises an infrastructure exception for demonstration
        /// purposes
        /// </summary>
        public Task<MovieCode> MakeInfraException()
        {
            throw new NotImplementedException("This exception in intentionally raised.");
        }
    }
}