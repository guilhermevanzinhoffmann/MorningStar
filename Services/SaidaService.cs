using MorningStar.Models;
using MorningStar.Repositories.Interfaces;
using MorningStar.Services.Interfaces;

namespace MorningStar.Services
{
    public class SaidaService : ISaidaService
    {
        private readonly ISaidaRepository _repository;
        private readonly IMercadoriaService _mercadoriaService;

        public SaidaService(ISaidaRepository repository, IMercadoriaService mercadoriaService)
        {
            _repository = repository;
            _mercadoriaService = mercadoriaService;
        }

        public async Task CreateAsync(Saida saida)
        {
            var mercadoria = await _mercadoriaService.GetByIdAsync(saida.MercadoriaID);
            
            var quantidadeMercadoria = mercadoria.Quantidade;
            
            if (quantidadeMercadoria - saida.Quantidade < 0)
                throw new ArgumentException("Quantidade de itens de saida deve ser menor ou igual à quantidade de itens disponíveis.");

            mercadoria.Quantidade-= saida.Quantidade;

            await _mercadoriaService.UpdateAsync(mercadoria);

            await _repository.CreateAsync(saida);
        }
            

        public async Task DeleteAsync(int id)
            => await _repository.DeleteAsync(id);

        public bool Exists(int id)
            => _repository.Exists(id);

        public async Task<IEnumerable<Saida>> GetAllAsync()
            => await _repository.GetAllAsync();

        public IEnumerable<Saida> GetAll()
            => _repository.GetAll();

        public async Task<Saida> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task UpdateAsync(Saida saida)
            => await _repository.UpdateAsync(saida);
    }
}
