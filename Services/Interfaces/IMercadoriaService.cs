using MorningStar.Models;

namespace MorningStar.Services.Interfaces
{
    public interface IMercadoriaService
    {
        Task<Mercadoria> GetByIdAsync(int id);
        Task<IEnumerable<Mercadoria>> GetAllAsync();
        IEnumerable<Mercadoria> GetAll();
        Task CreateAsync(Mercadoria mercadoria);
        Task DeleteAsync(int id);
        Task UpdateAsync(Mercadoria mercadoria);
        bool Exists(int id);
        bool Exists(string nome);
    }
}
