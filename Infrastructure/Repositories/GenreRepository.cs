using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class GenreRepository: EFRepository<Genre>, IGenreRepository 
    {
        public GenreRepository(MovieShopDbContext dbContext): base(dbContext)
        {
                
        }

        public List<Genre> GetAllGenres()
        {
            return _dbContext.Genres.ToList();
        }
    }
}
