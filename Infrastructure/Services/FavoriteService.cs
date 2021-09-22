using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoriteService(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        public async Task<FavoriteResponseModel> GetFavoriteByIdAsync(int id, int movieId)
        {
            var favorite = await _favoriteRepository.GetFavoriteAsync(movieId, id);

            return new FavoriteResponseModel
            {
                MovieId = favorite.MovieId,
                UserId = favorite.UserId,
                MovieTitle = favorite.Movie.Title,
                UserName = favorite.User.FirstName + " " + favorite.User.LastName
            };
        }
    }
}
