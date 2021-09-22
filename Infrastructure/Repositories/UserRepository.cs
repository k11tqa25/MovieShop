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
    public class UserRepository: EFRepository<User>, IUserRepository
    {
        public UserRepository(MovieShopDbContext dbContext): base(dbContext)
        {

        }

        public async Task<User> GetUserPurchasesAsync(int id)
        {
            var user = await _dbContext.Users.Where(u=> u.Id == id).Include(u => u.Purchases).ThenInclude(p => p.Movie).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<User> GetUserReviewsAsync(int id)
        {
            var user = await _dbContext.Users.Where(u => u.Id == id).Include(u => u.Reviews).ThenInclude(r => r.Movie).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> GetUserFavoritesAsync(int id)
        {
            var user = await _dbContext.Users.Where(u => u.Id == id).Include(u => u.Favorates).ThenInclude(f => f.Movie).FirstOrDefaultAsync();
            return user;
        }
    }
}
