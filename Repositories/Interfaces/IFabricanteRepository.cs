using MorningStar.Models;

namespace MorningStar.Repositories.Interfaces
{
    public interface IFabricanteRepository
    {
        Task<Fabricante> GetByIdAsync(int id);
        Task<IEnumerable<Fabricante>> GetAllAsync();
        IEnumerable<Fabricante> GetAll();
        Task CreateAsync(Fabricante fabricante);
    }
}