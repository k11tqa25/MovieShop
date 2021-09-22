using ApplicationCore.Entities;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface ICastRepository: IAsyncRepository<Cast>
    {
        Task<Cast> GetCastByIdWithAllInfoAsync(int id);
    }
}
