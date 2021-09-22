using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ICurrentUser _currentUser;

        public MovieService(IMovieRepository movieRepository, ICurrentUser currentUser)
        {
            _movieRepository = movieRepository;
            _currentUser = currentUser;
        }

        private MovieDetailsResponseModel MapMovieDetail(Movie movie)
        {
            var movieDetails = new MovieDetailsResponseModel()
            {
                Id = movie.Id,
                Title = movie.Title,
                Budget = movie.Budget.GetValueOrDefault(),
                Rating = movie.Rating,
                ReleaseDate = movie.ReleaseDate,
                BackdropUrl = movie.BackdropUrl,
                ImdbUrl = movie.ImdbUrl,
                PosterUrl = movie.PosterUrl,
                Overview = movie.Overview,
                Price = movie.Price,
                RunTime = movie.RunTime,
                Revenue = movie.Revenue,
                Tagline = movie.Tagline,
            };
            movieDetails.Casts = new List<CastResponseModel>();
            foreach (var cast in movie.MovieCasts)
            {
                movieDetails.Casts.Add(new CastResponseModel
                {
                    Id = cast.CastId,
                    Name = cast.Cast.Name,
                    Character = cast.Character,
                    ProfilePath = cast.Cast.ProfilePath
                });
            }

            movieDetails.Genres = new List<GenreModel>();
            foreach (var genre in movie.Genres)
            {
                movieDetails.Genres.Add(new GenreModel()
                {
                    Id = genre.Id,
                    Name = genre.Name
                });
            }
            return movieDetails;
        }

        private MovieCardResponseModel MapMovieCard(Movie movie)
        {
            return new MovieCardResponseModel
            {
                Id = movie.Id,
                Budget = movie.Budget.GetValueOrDefault(),
                PosterUrl = movie.PosterUrl,
                Title = movie.Title
            };
        }

        public async Task<List<MovieCardResponseModel>> GetTopRevenueMoviesAsync()
        {
            var movies = await _movieRepository.GetHighest30GrossingMoviesAsync();
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    Budget = movie.Budget.GetValueOrDefault(),
                    PosterUrl = movie.PosterUrl,
                    Title = movie.Title
                });
            }

            return movieCards;
        }

        public async Task<MovieDetailsResponseModel> GetMovieDetailsAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);

            return MapMovieDetail(movie);
        }

        public async Task<List<MovieCardResponseModel>> GetMoviesByGenreAsync(int genre_id)
        {
            var movies = await _movieRepository.GetMoviesByGenreIdAsync(genre_id);
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel()
                {
                    Id = movie.Id,
                    Budget = movie.Budget.GetValueOrDefault(),
                    PosterUrl = movie.PosterUrl,
                    Title = movie.Title
                });
            }
            return movieCards;
        }

        public async Task<List<MovieCardResponseModel>> GetTopRatedMoviesAsync()
        {
            var movies = await _movieRepository.GetHightset30TopRatingMoviesAsync();
            var responseModel = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                responseModel.Add(MapMovieCard(movie));
            }
            return responseModel;
        }

        public async Task<List<MovieCardResponseModel>> GetAllMoviesAsync()
        {
            var movies = await _movieRepository.ListAllAsync();
            var responseModel = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                responseModel.Add(MapMovieCard(movie));
            }
            return responseModel;
        }

        public async Task<List<ReviewResponseModel>> GetMovieReviewsAsync(int id)
        {
            var movie= await _movieRepository.GetMovieWithReviewsAsync(id);
            var responseModel = new List<ReviewResponseModel>();
            foreach (var m in movie.Reviews)
            {
                responseModel.Add(new ReviewResponseModel()
                {
                    MovieId = m.MovieId,
                    UserId = m.UserId,
                    MovieTitle = m.Movie.Title,
                    UserName = m.User.FirstName + " " + m.User.LastName,
                    Rating = m.Rating,
                    Review = m.ReviewText
                });
            }
            return responseModel;
        }

        public async Task<MovieDetailsResponseModel> AddMovieAsync(MovieCreateRequestModel model)
        {
            var genres = new List<Genre>();
            foreach (var g in model.Genres)
            {
                genres.Add(new Genre()
                {
                    Id = g.Id,
                    Name = g.Name
                });
            }

            var movie = await _movieRepository.AddAsync(new Movie
            {
                Title = model.Title,
                Budget = model.Budget,
                BackdropUrl = model.BackdropUrl,
                ImdbUrl = model.ImdbUrl,
                PosterUrl = model.PosterUrl,
                TmdbUrl = model.TmdbUrl,
                CreatedDate  = DateTime.Now,
                CreatedBy = _currentUser.FullName,
                Price = model.Price,
                Genres = genres,
                ReleaseDate = model.ReleaseTime,
                Revenue = model.Revenue,                
            });

            var movieDetails = await _movieRepository.GetByIdAsync(movie.Id);

            if (movieDetails == null) return null;
            return MapMovieDetail(movieDetails);
        }

        public async Task<MovieDetailsResponseModel> UpdateMovieAsync(MovieCreateRequestModel model)
        {
            var genres = new List<Genre>();
            foreach (var g in model.Genres)
            {
                genres.Add(new Genre()
                {
                    Id = g.Id,
                    Name = g.Name
                });
            }
            var movie = await _movieRepository.UpdateAsync(new Movie
            {
                Title = model.Title,
                Budget = model.Budget,
                BackdropUrl = model.BackdropUrl,
                ImdbUrl = model.ImdbUrl,
                PosterUrl = model.PosterUrl,
                TmdbUrl = model.TmdbUrl,
                CreatedDate = DateTime.Now,
                CreatedBy = _currentUser.FullName,
                Price = model.Price,
                Genres = genres,
                ReleaseDate = model.ReleaseTime,
                Revenue = model.Revenue,
            });

            var movieDetails = await _movieRepository.GetByIdAsync(movie.Id);

            if (movieDetails == null) return null;

            return MapMovieDetail(movieDetails);
        }
    }

}

