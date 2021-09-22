using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;

        public MovieController(IMovieService movieService, IGenreService genreService)
        {
            _movieService = movieService;
            _genreService = genreService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieDetailsAsync(id);
            return View(movie);
        }

        public async Task<IActionResult> FilterByGenre(int id)
        {
            var genre = await _genreService.GetGenreModelByIdAsync(id);
            ViewData["CurrentGenre"] = genre;
            var movies = await _movieService.GetMoviesByGenreAsync(id);
            return View(movies);
        }
    }
}
