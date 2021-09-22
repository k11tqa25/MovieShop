using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IReviewRepository: IAsyncRepository<Review>
    {
        Task<Review> GetReviewByIdAsync(int userId, int movieId);

    }       
}
