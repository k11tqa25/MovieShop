using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FavoriteRepository: EFRepository<Favorite>, IFavoriteRepository 
    {
        public FavoriteRepository(MovieShopDbContext dbContext): base(dbContext)
        {
                
        }

        public async Task<Favorite> GetFavoriteAsync(int movieId, int userId)
        {
            var favorite = await _dbContext.Favorates.Where(f => f.MovieId == movieId && f.UserId == userId).Include(f=>f.Movie).Include(f=>f.User).FirstOrDefaultAsync();
            return favorite;
        }
    }
}
