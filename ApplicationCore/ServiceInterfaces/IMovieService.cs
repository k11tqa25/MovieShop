using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
       Task<List<MovieCardResponseModel>> GetTopRevenueMoviesAsync();
        Task<List<MovieCardResponseModel>> GetTopRatedMoviesAsync();
        Task<MovieDetailsResponseModel>  GetMovieDetailsAsync(int id);
        Task<List<MovieCardResponseModel>> GetMoviesByGenreAsync(int genre_id);
        Task<List<MovieCardResponseModel>> GetAllMoviesAsync();
        Task<List<ReviewResponseModel>> GetMovieReviewsAsync(int id);
        Task<MovieDetailsResponseModel> AddMovieAsync(MovieCreateRequestModel model);
        Task<MovieDetailsResponseModel> UpdateMovieAsync(MovieCreateRequestModel model);
    }
}
