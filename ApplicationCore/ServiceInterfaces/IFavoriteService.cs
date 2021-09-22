using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IFavoriteService
    {
        Task<FavoriteResponseModel> GetFavoriteByIdAsync(int id, int movieId);
    }
}
