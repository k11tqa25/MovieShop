using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EFRepository<T> : IAsyncRepository<T> where T: class
    {
        protected readonly MovieShopDbContext _dbContext;

        public EFRepository(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
             // This will be saved in the memory, not in database.
            await _dbContext.Set<T>().AddAsync(entity);
            // Save to database
            await _dbContext.SaveChangesAsync();
            // This will contain the newly created primary key!
            return entity;
        }

        public virtual async Task<T> DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public override bool Equals(object obj)
        {
            return obj is EFRepository<T> repository &&
                   EqualityComparer<MovieShopDbContext>.Default.Equals(_dbContext, repository._dbContext);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null)
        {
            if(filter!= null)
            {
                return await _dbContext.Set<T>().Where(filter).CountAsync();
            }

            return await _dbContext.Set<T>().CountAsync();
        }

        public async Task<bool> GetExistsAsnyc(Expression<Func<T, bool>> filter)
        {
            if(filter == null)
            {
                return false;
            }
            return await _dbContext.Set<T>().Where(filter).AnyAsync();
        }

        public virtual async Task<IEnumerable<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbContext.Set<T>().Where(filter).ToListAsync();
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
