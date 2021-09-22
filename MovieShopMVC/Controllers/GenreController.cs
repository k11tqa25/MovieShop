using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class GenreController: Controller
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

    }
}
