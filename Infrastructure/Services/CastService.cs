using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;

        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }

        public async Task<CastResponseModel> GetCastById(int id)
        {
            var cast = await _castRepository.GetByIdAsync(id);
            return new CastResponseModel
            {
                Id = cast.Id,
                Name = cast.Name,
                Gender = cast.Gender,
                ProfilePath = cast.ProfilePath,
                TmdbUrl = cast.TmdbUrl
            };
        }

        public async Task<CastResponseModel> GetCastDetailsAsync(int castId, int movieId)
        {
            var cast = await _castRepository.GetCastByIdWithAllInfoAsync(castId);
            string character = cast.MovieCasts.Where(mc => mc.MovieId == movieId).FirstOrDefault().Character;
            return new CastResponseModel
            {
                Id = cast.Id,
                Name = cast.Name,
                Gender = cast.Gender,
                ProfilePath = cast.ProfilePath,
                TmdbUrl = cast.TmdbUrl,
                Character = character
            };
        }
    }
}
