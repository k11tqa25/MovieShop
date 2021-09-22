using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IUserRepository: IAsyncRepository<User>
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<User> GetUserPurchasesAsync(int id);

        Task<User> GetUserReviewsAsync(int id);

        Task<User> GetUserFavoritesAsync(int id);

        
                
    }
}
