using MorningStar.Models;

namespace MorningStar.Repositories.Interfaces
{
    public interface IMercadoriaRepository
    {
        Task<Mercadoria> GetByIdAsync(int id);
        Task<IEnumerable<Mercadoria>> GetAllAsync();
        IEnumerable<Mercadoria> GetAll();
        Task CreateAsync(Mercadoria mercadoria);
        Task UpdateAsync(Mercadoria mercadoria);
        Task DeleteAsync(int id);
        bool Exists(int id);
        bool Exists(string nome);
    }
}
