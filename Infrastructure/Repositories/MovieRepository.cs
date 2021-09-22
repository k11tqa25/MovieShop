using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MovieRepository : EFRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Movie>> GetHighest30GrossingMoviesAsync()
        {
            var topMovies = await _dbContext.Movies.OrderByDescending(m=>m.Revenue).Take(30).ToListAsync();
            return topMovies;            
        }

        public override async Task<Movie> GetByIdAsync(int id)
        {
            var movie = await _dbContext.Movies.Include(m => m.MovieCasts)
                                                                                   .ThenInclude(m => m.Cast)
                                                                                   .Include(m => m.Genres)
                                                                                   .FirstOrDefaultAsync(m => m.Id == id);
            if(movie == null)
            {
                return null;
            }

            var movieRating = await _dbContext.Reviews.Where(r => r.MovieId == id)
                                                                                                 .DefaultIfEmpty()
                                                                                                 .AverageAsync(r => r == null ? 0 : r.Rating);

            if (movieRating > 0) movie.Rating = movieRating;

            return movie;
        }

        public async Task<List<Movie>> GetMoviesByGenreIdAsync(int genre_id)
        {
            var genre = await _dbContext.Genres.Include(g => g.Movies).Where(g => g.Id == genre_id).FirstOrDefaultAsync();

            return genre.Movies.ToList();
        }

        public async Task<List<Movie>> GetHightset30TopRatingMoviesAsync()
        {
            //var topMovieIds = _dbContext.Movies.Include(m => m.Reviews).GroupBy(m => m.Id)
            //                                                                                                                        .Select(m => new { key = m.Key, average = m.Select( x => (int?)x.Rating).Average() })
            //                                                                                                                        .OrderByDescending(m => m.average)
            //                                                                                                                        .Take(30)
            //                                                                                                                        .Select(m => m.key)
            //                                                                                                                        .ToHashSet();


            //var movies = await _dbContext.Movies.Where(m => topMovieIds.Contains(m.Id)).ToListAsync();
            //return movies;


            var topRatedMovies = await _dbContext.Reviews.Include(m => m.Movie)
                                                 .GroupBy(r => new
                                                 {
                                                     Id = r.MovieId,
                                                     r.Movie.PosterUrl,
                                                     r.Movie.Title,
                                                     r.Movie.ReleaseDate
                                                 })
                                                 .OrderByDescending(g => g.Average(m => m.Rating))
                                                 .Select(m => new Movie
                                                 {
                                                     Id = m.Key.Id,
                                                     PosterUrl = m.Key.PosterUrl,
                                                     Title = m.Key.Title,
                                                     ReleaseDate = m.Key.ReleaseDate,
                                                     Rating = m.Average(x => x.Rating)
                                                 })
                                                 .Take(30)
                                                 .ToListAsync();

            return topRatedMovies;


        }

        public async Task<Movie> GetMovieWithReviewsAsync(int id)
        {
            var movie = await _dbContext.Movies.Include(m => m.Reviews).ThenInclude(r => r.User).Where(m => m.Id == id).FirstOrDefaultAsync();
            return movie;
        }
    }
}
