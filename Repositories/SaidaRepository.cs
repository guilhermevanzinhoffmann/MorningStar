using Microsoft.EntityFrameworkCore;
using MorningStar.Data;
using MorningStar.Models;
using MorningStar.Repositories.Interfaces;

namespace MorningStar.Repositories
{
    public class SaidaRepository : ISaidaRepository
    {
        private readonly MorningStarContext _context;

        public SaidaRepository(MorningStarContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Saida saida)
        {
            _context.Add(saida);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Saida>> GetAllAsync()
        {
            if (_context.Saidas == null)
                return null;

            return await _context.Saidas
                .Include(x => x.Mercadoria)
                .ToListAsync();
        }

        public IEnumerable<Saida> GetAll()
        {
            if (_context.Saidas == null)
                return null;

            return _context.Saidas
                .Include(x => x.Mercadoria)
                .ToList();
        }

        public async Task<Saida> GetByIdAsync(int id)
        {
            if (_context.Saidas == null)
                return null;

            return await _context.Saidas
                .Include(x => x.Mercadoria)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public bool Exists(int id)
        {
            if (_context.Saidas == null)
                return false;

            return _context.Saidas.Any(x => x.Id == id);
        }

        public async Task DeleteAsync(int id)
        {
            var saida = await _context.Saidas
                .Include(x => x.Mercadoria)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (saida != null)
                _context.Saidas.Remove(saida);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Saida saida)
        {
            _context.Saidas.Update(saida);
            await _context.SaveChangesAsync();
        }
    }
}
