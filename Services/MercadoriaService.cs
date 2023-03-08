using MorningStar.Models;
using MorningStar.Repositories.Interfaces;
using MorningStar.Services.Interfaces;

namespace MorningStar.Services
{
    public class MercadoriaService :IMercadoriaService
    {
        private readonly IMercadoriaRepository _repository;

        public MercadoriaService(IMercadoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(Mercadoria mercadoria)
        {
            if (_repository.Exists(mercadoria.Nome))
                throw new ArgumentException("Mercadoria já existe com esse nome.");
            
            await _repository.CreateAsync(mercadoria);
        }
            

        public async Task DeleteAsync(int id)
            => await _repository.DeleteAsync(id);

        public bool Exists(int id)
            => _repository.Exists(id);

        public bool Exists(string nome)
            => _repository.Exists(nome);

        public Task<IEnumerable<Mercadoria>> GetAllAsync()
            => _repository.GetAllAsync();

        public IEnumerable<Mercadoria> GetAll()
            => _repository.GetAll();

        public Task<Mercadoria> GetByIdAsync(int id)
            => _repository.GetByIdAsync(id);

        public async Task UpdateAsync(Mercadoria mercadoria)
            => await _repository.UpdateAsync(mercadoria);
    }
}
