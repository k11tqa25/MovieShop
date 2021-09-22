using ApplicationCore.Models;
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
    public class AdminController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IMovieService _movieService;
        private readonly ICurrentUser _currentUser;

        public AdminController(IUserService userService, IMovieService movieService, ICurrentUser currentUser)
        {
            _movieService = movieService;
            _userService = userService;
            _currentUser = currentUser;
        }

        [HttpPost]
        [Route("movie")]
        public async Task<IActionResult> CreateMovie([FromBody] MovieCreateRequestModel model)
        {
            var newMovie = await _movieService.AddMovieAsync(model);
            if (newMovie == null) return BadRequest();
            return Ok(newMovie);
        }

        [HttpPut]
        [Route("movie")]
        public async Task<IActionResult> UpdateMovie([FromBody] MovieCreateRequestModel model)
        {
            var newMovie = await _movieService.UpdateMovieAsync(model);
            if (newMovie == null) return BadRequest();
            return Ok(newMovie);
        }

        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> GetUserPurchases()
        {
            var userPurchases = await _userService.GetUserPurchasesAsync(_currentUser.UserId);
            if (userPurchases == null) return NotFound("No Purchase is found.");
            return Ok(userPurchases);
        }

    }
}
