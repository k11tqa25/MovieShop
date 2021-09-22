using ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IGenreRepository: IAsyncRepository<Genre>
    {
        public List<Genre> GetAllGenres();

    }
}
