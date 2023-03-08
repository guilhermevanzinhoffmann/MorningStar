using MorningStar.Models;

namespace MorningStar.Services.Interfaces
{
    public interface IFabricanteService
    {
        Task<Fabricante> GetByIdAsync(int id);

        Task<IEnumerable<Fabricante>> GetAllAsync();
        IEnumerable<Fabricante> GetAll();

        Task CreateAsync(Fabricante fabricante);
    }
}
