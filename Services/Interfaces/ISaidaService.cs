using MorningStar.Models;

namespace MorningStar.Services.Interfaces
{
    public interface ISaidaService
    {
        Task<Saida> GetByIdAsync(int id);
        Task<IEnumerable<Saida>> GetAllAsync();
        IEnumerable<Saida> GetAll();
        Task CreateAsync(Saida saida);
        Task DeleteAsync(int id);
        Task UpdateAsync(Saida saida);
        bool Exists(int id);
    }
}
