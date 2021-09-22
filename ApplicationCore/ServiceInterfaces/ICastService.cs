using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface ICastService
    {
        Task<CastResponseModel> GetCastById(int id);

        Task<CastResponseModel> GetCastDetailsAsync(int cast_id, int movie_id);
    }
}
