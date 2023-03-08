using MorningStar.Models;
using MorningStar.Repositories.Interfaces;
using MorningStar.Services.Interfaces;

namespace MorningStar.Services
{
    public class FabricanteService : IFabricanteService
    {
        private readonly IFabricanteRepository _repository;
        
        public FabricanteService(IFabricanteRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(Fabricante fabricante)
            => await _repository.CreateAsync(fabricante);

        public Task<IEnumerable<Fabricante>> GetAllAsync()
            => _repository.GetAllAsync();

        public IEnumerable<Fabricante> GetAll()
            => _repository.GetAll();

        public Task<Fabricante> GetByIdAsync(int id)
            => _repository.GetByIdAsync(id);
    }
}
