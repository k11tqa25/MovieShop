using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMemoryCache _memoryCache;

        public GenreService(IGenreRepository genreRepository, IMemoryCache memoryCache)
        {
            _genreRepository = genreRepository;
            _memoryCache = memoryCache;
        }

        private GenreModel ConvertGenreToGenreModel(Genre genre)
        {
            return new GenreModel
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }

        public async Task<List<GenreModel>> GetAllGenreModels()
        {
            var genre_repo =  _genreRepository.GetAllGenres();
            List<GenreModel> genreModels = new List<GenreModel>();
            foreach (var genre in genre_repo)
            {
                genreModels.Add(ConvertGenreToGenreModel(genre));
            }
            return genreModels;
        }

        public async Task<GenreModel> GetGenreModelByIdAsync(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            return ConvertGenreToGenreModel(genre);
        }

        public  async Task<List<GenreModel>> GetAllGenreModelsAsync()
        {
            // Check if the cache has all the genres
            // .Net In-memory Caching => smaller amount of data
            // Distributed Caching => Redis Cache => for large amount of data
            var genre_repo = await _memoryCache.GetOrCreateAsync("genresData", async entry =>
            {
                entry.SlidingExpiration = System.TimeSpan.FromDays(30);
                // Query from the database to get all the genres
                return await _genreRepository.ListAllAsync();
            });
            List<GenreModel> genreModels = new List<GenreModel>();
            foreach (var genre in genre_repo)
            {
                genreModels.Add(ConvertGenreToGenreModel(genre));
            }
            return genreModels;
        }
    }
}
