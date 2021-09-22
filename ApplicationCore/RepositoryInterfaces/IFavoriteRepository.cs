using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IFavoriteRepository: IAsyncRepository<Favorite>
    {
        Task<Favorite> GetFavoriteAsync(int movieId, int userId);
    }
}
