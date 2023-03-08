using MorningStar.Models;
using MorningStar.Repositories.Interfaces;
using MorningStar.Services.Interfaces;

namespace MorningStar.Services
{
    public class EntradaService : IEntradaService
    {
        private readonly IEntradaRepository _repository;

        public EntradaService(IEntradaRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(Entrada entrada)
            => await _repository.CreateAsync(entrada);

        public async Task DeleteAsync(int id)
            => await _repository.DeleteAsync(id);

        public bool Exists(int id)
            => _repository.Exists(id);

        public async Task<IEnumerable<Entrada>> GetAllAsync()
            => await _repository.GetAllAsync();

        public IEnumerable<Entrada> GetAll()
            => _repository.GetAll();

        public async Task<Entrada> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task UpdateAsync(Entrada entrada)
            => await _repository.UpdateAsync(entrada);
    }
}
