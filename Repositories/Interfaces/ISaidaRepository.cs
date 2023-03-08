using MorningStar.Models;

namespace MorningStar.Repositories.Interfaces
{
    public interface ISaidaRepository
    {
        Task<Saida> GetByIdAsync(int id);
        Task<IEnumerable<Saida>> GetAllAsync();
        IEnumerable<Saida> GetAll();
        Task CreateAsync(Saida saida);
        Task UpdateAsync(Saida saida);
        Task DeleteAsync(int id);
        bool Exists(int id);
    }
}
