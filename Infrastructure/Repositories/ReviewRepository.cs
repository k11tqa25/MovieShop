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
    public class ReviewRepository : EFRepository<Review>, IReviewRepository
    {
        public ReviewRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Review> GetReviewByIdAsync(int userId, int movieId)
        {
            return await _dbContext.Reviews.Where(r => r.UserId == userId && r.MovieId == movieId).FirstOrDefaultAsync();
        }
    }
}
