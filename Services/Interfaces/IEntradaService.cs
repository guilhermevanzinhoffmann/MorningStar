using MorningStar.Models;

namespace MorningStar.Services.Interfaces
{
    public interface IEntradaService
    {
        Task<Entrada> GetByIdAsync(int id);
        Task<IEnumerable<Entrada>> GetAllAsync();
        IEnumerable<Entrada> GetAll();
        Task CreateAsync(Entrada entrada);
        Task DeleteAsync(int id);
        Task UpdateAsync(Entrada entrada);
        bool Exists(int id);
    }
}
