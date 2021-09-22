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
    public class PurchaseRepository : EFRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }


        public override async Task<IEnumerable<Purchase>> ListAllAsync()
        {
            var all = await _dbContext.Purchases.Include(p => p.Movie).Include(p => p.User).ToListAsync();
            return all;
        }
    }
}
