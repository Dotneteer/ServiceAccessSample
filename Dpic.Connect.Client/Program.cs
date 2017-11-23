using System;
using System.Threading.Tasks;
using Dpic.Connect.ServiceAccess.MovieCodes;
using Newtonsoft.Json;

namespace Dpic.Connect.Client
{
    class Program
    {
        private const string BASE_URI = "http://localhost:58888/";

        static void Main(string[] args)
        {
            ExecuteCalls().Wait();
        }

        private static async Task ExecuteCalls()
        {
            var proxy = new MovieCodesServiceProxy(BASE_URI);

            // Call #1
            Console.WriteLine("#1: GetAllMovieCodes");
            Console.WriteLine();
            var movies = await proxy.GetAllMovieCodes();
            Console.WriteLine(JsonConvert.SerializeObject(movies, Formatting.Indented));
            Console.WriteLine();

            // Call #2
            Console.WriteLine("#2: GetActiveMovieCodes");
            Console.WriteLine();
            var activeMovies = await proxy.GetActiveMovieCodes();
            Console.WriteLine(JsonConvert.SerializeObject(activeMovies, Formatting.Indented));
            Console.WriteLine();

            // Call #3
            Console.WriteLine("#3: GetMovieCodeById");
            Console.WriteLine();
            var movie2 = await proxy.GetMovieCodeById(2);
            Console.WriteLine(JsonConvert.SerializeObject(movie2, Formatting.Indented));
            Console.WriteLine();

            // Call #4
            Console.WriteLine("#4: GetMovieCodeById (Exception)");
            Console.WriteLine();
            try
            {
                var movie8 = await proxy.GetMovieCodeById(8);
                Console.WriteLine(JsonConvert.SerializeObject(movie8, Formatting.Indented));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine();

            // Call #5
            Console.WriteLine("#5: MakeInfraException");
            Console.WriteLine();
            try
            {
                var movieOther = await proxy.MakeInfraException();
                Console.WriteLine(JsonConvert.SerializeObject(movieOther, Formatting.Indented));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine();
        }
    }
}
