using MorningStar.Models;

namespace MorningStar.Repositories.Interfaces
{
    public interface IEntradaRepository
    {
        Task<Entrada> GetByIdAsync(int id);
        Task<IEnumerable<Entrada>> GetAllAsync();
        IEnumerable<Entrada> GetAll();
        Task CreateAsync(Entrada entrada);
        Task UpdateAsync(Entrada entrada);
        Task DeleteAsync(int id);
        bool Exists(int id);
    }
}
