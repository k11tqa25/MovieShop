using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CastRepository : EFRepository<Cast>, ICastRepository
    {
        public CastRepository(MovieShopDbContext movieShopDbContext): base(movieShopDbContext)
        {

        }

        public async Task<Cast> GetCastByIdWithAllInfoAsync(int id)
        {
            var cast = await _dbContext.Casts.Include(c => c.MovieCasts).Where(c => c.Id == id).FirstOrDefaultAsync();
            return cast;
        }
    }
}
