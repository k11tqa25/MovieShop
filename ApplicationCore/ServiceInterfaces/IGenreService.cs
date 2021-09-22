using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IGenreService
    {
        public Task<GenreModel> GetGenreModelByIdAsync(int id);

        public Task<List<GenreModel>> GetAllGenreModels();

        public Task<List<GenreModel>> GetAllGenreModelsAsync();

    }
}
