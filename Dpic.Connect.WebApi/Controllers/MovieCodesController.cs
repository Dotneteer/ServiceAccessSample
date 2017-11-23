using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Dpic.Connect.Services.MovieCodes;
using Dpic.Connect.SharedDefinitions.MovieCodes;

namespace Dpic.Connect.WebApi.Controllers
{
    [RoutePrefix("moviecodes")]
    public class MovieCodesController : ApiController
    {
        [Route("")]
        [HttpGet]
        public async Task<List<MovieCode>> GetAllMovieCodes()
        {
            var service = new MovieCodeService();
            return await service.GetAllMovieCodes();
        }

        [Route("active")]
        [HttpGet]
        public async Task<List<MovieCode>> GetActiveMovieCodes()
        {
            var service = new MovieCodeService();
            return await service.GetActiveMovieCodes();
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<MovieCode> GetMovieCodeById(int id)
        {
            var service = new MovieCodeService();
            return await service.GetMovieCodeById(id);
        }

        [Route("makeex")]
        [HttpGet]
        public async Task<MovieCode> MakeInfraException()
        {
            var service = new MovieCodeService();
            return await service.MakeInfraException();
        }
    }
}
