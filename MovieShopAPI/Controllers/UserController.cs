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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IFavoriteService _favoriteService;

        public UserController(IUserService userService, IFavoriteService favoriteService)
        {
            _userService = userService;
            _favoriteService = favoriteService;
        }

        [HttpPost]
        [Route("purchase")]
        public async Task<IActionResult> AddPurchase([FromBody] PurchaseRequestModel model)
        {
            var response = await _userService.AddPurchaseAsync(model);
            if (response == null) return BadRequest();
            return Ok(response);
        }

        [HttpPost]
        [Route("review")]
        public async Task<IActionResult> AddReview([FromBody] ReviewRequestModel model)
        {
            var response = await _userService.AddReviewAsync(model);
            if (response == null) return BadRequest();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}/reviews")]
        public async Task<IActionResult> GetUserReviews(int id)
        {
            var response = await _userService.GetUserReviewsAsync(id);
            if (response == null) return NotFound("No review is found.");
            return Ok(response);
        }

        [HttpPut]
        [Route("review")]
        public async Task<IActionResult> UpdateUserReview([FromBody] ReviewRequestModel model)
        {
            var response = await _userService.UpdateReviewAsync(model);
            if (response == null) return BadRequest();
            return Ok(response);
        }

        [HttpDelete]
        [Route("{userId}/movie/{movieId}")]
        public async Task<IActionResult> DeleteUserReview(int userId, int movieId)
        {
            var response = await _userService.RemoveReviewAsync(userId, movieId);
            if (!response) return BadRequest();
            return Ok(response);
        }

        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> AddFavorite([FromBody] FavoriteRequestModel model)
        {
            var response = await _userService.AddFavoriteAsync(model);
            if (response == null) return BadRequest();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:int}/favorite")]
        public async Task<IActionResult> GetFavorite(int id)
        {
            var response = await _userService.GetUserFavoritesAsync(id);
            if (response == null) return NotFound("No favorite is found.");
            return Ok(response);
        }

        [HttpPost]
        [Route("unfavorite")]
        public async Task<IActionResult> RemoveFavorite([FromBody] FavoriteRequestModel model)
        {
            var response = await _userService.RemoveFavoriteAsync(model);
            if (!response) return BadRequest();
            return Ok(response);
        }


        [HttpGet]
        [Route("{id:int}/movie/{movieId:int}/favorite")]
        public async Task<IActionResult> GetFavorite(int id, int movieId)
        {
            var response = await _favoriteService.GetFavoriteByIdAsync(id, movieId);
            if (response == null) return NotFound("No favorite is found.");
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:int}/purchases")]
        public async Task<IActionResult> GetUserPurchases(int id)
        {
            var userPurchases = await _userService.GetUserPurchasesAsync(id);
            if (userPurchases == null ) return NotFound("No Purchase is found.");
            return Ok(userPurchases);
        }
    }
}
