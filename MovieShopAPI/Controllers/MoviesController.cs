using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // Attribute based routing
        [HttpGet]
        [Route("toprevenue")]
        public async Task<IActionResult> GetTopRevenueMoives()
        {
            var movies = await _movieService.GetTopRevenueMoviesAsync();

            if (!movies.Any())
            {
                return NotFound("NO moives found.");
            }
            // Two popular JSON library:
            // NewtonSoft Json, 
            // .Net Core 3.1> System.Text.Json
            return Ok(movies);

        }


        [HttpGet]
        [Route("toprated")]
        public async Task<IActionResult> GetTopRatedMoives()
        {
            var movies = await _movieService.GetTopRatedMoviesAsync();

            if (!movies.Any())
            {
                return NotFound("NO moives found.");
            }
            return Ok(movies);

        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieDetailsAsync(id);
            if(movie == null)
            {
                return NotFound($"No movie found for id = {id}");
            }
            return Ok(movie);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            if(movies == null)
            {
                return NotFound("No movie is found.");
            }
            return Ok(movies);
        }

        [HttpGet]
        [Route("genre/{id:int}")]
        public async Task<IActionResult> GetMoviesByGenre(int id)
        {
            var movies = await _movieService.GetMoviesByGenreAsync(id);
            if (movies == null)
            {
                return NotFound("No movie is found.");
            }
            return Ok(movies);
        }

        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<IActionResult> GetMovieReviews(int id)
        {
            var reviews = await _movieService.GetMovieReviewsAsync(id);
            if (reviews == null)
            {
                return NotFound("No review is found.");
            }
            return Ok(reviews);
        }
    }
}
